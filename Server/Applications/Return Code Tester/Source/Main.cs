using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using mshtml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SHDocVw;
using GCGCommon;
using DVB.Properties;
namespace DVB
{
    public partial class Main : Form
    {
        public AllDetails ad;
        public string CLIrqFile = "", AppName = "", AutoRun = "";
        public int SpecificRetryCnt, SpecificRetryCntMax;
        public int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt, DelayCnt;
        string tmrResponseHandlerSt, ScriptFile;
        string CurrCommand, PrevCommand;
        StreamReader Script = null;
        private void ProcessInstructions()
        {
            string OK = "1";
            Instruction++;
            GCGMethods.WriteTextBoxLog(txtLog, "Inst " + Instruction.ToString());
            if (PrevCommand == "GCCAPTCHA")
            {
                if (txtCAPTCHAAnswer.Text=="0")
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sent back 0 for GCCAPTCHA", ad.RsPathAndFileToWrite);
                    MaybeExitApp();
                    return;
                }
            }
            else if (PrevCommand == "GCNEEDSMOREINFO")
            {
                if (txtAdditionalParam.Text == "0")
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sent back 0 for GCNEEDSMOREINFO", ad.RsPathAndFileToWrite);
                    MaybeExitApp();
                    return;
                }
            }

            System.Diagnostics.Debug.WriteLine(GCGCommon.GCTypes.GCBALANCE.ToString());
            if (CurrCommand == "GCCAPTCHA")
            {
                DoHandleCAPTCHARqRs();
            }
            else if (CurrCommand == "GCNEEDSMOREINFO")
            {
                DoHandleNeedsMoreInfoRqRs("Send response for Instruction " + Instruction);
            }
            else if (CurrCommand == "GCBALANCE")
            {
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCE.ToString(), "$25.00", ad.RsPathAndFileToWrite);
                MaybeExitApp();
                return;
            }
            else if (CurrCommand == "GCBALANCEERR")
            {
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCEERR.ToString(), "GCBALANCEERR resp", ad.RsPathAndFileToWrite);
                MaybeExitApp();
                return;
            }
            else if (CurrCommand == "GCERR")
            {
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), "RETERR resp", ad.RsPathAndFileToWrite);
                MaybeExitApp();
                return;
            }
            else if (CurrCommand == "GCCUSTOM")
            {
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "GCCUSTOM resp", ad.RsPathAndFileToWrite);
                MaybeExitApp();
                return;
            }
            else if (CurrCommand == "GCTIMEOUT")
            {
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCTIMEOUT.ToString(), "GCTIMEOUT resp", ad.RsPathAndFileToWrite);
                MaybeExitApp();
                return;
            }

        }
        public Main()
        {
            InitializeComponent();
        }

        public Main(string[] commands)
            : this()
        {
            try
            {
                CLIrqFile = commands[0].ToString();
            }
            catch (Exception)
            {
            }
            try
            {
                AutoRun = commands[1].ToString();
            }
            catch (Exception)
            {
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(0, 0);
            //this.Size = new Size(980,250);
            string[] AppNameArr = GCGCommon.SupportMethods.GetFilePathPieces(Application.ExecutablePath);
            AppName = AppNameArr[3];
            ScriptFile = Application.StartupPath + "\\fake" + AppName + ".txt";
            this.Text = "Return Code Generator";
            RetryCntMax = 999;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                txtRqRsPath.Text = MR.Read("RqRsPath");

            }
            catch (Exception ex)
            {            
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            string result = LoadCLIrqFile();
            if (result != "1")
            {
                SupportMethods.WriteToWindowsEventLog(AppName, "No CLIrqFileIn was sent.", "W");
            }
            if (AutoRun == "1")
            {
                Script = new StreamReader(ScriptFile);
                LoadScript();
                tmrTimeout.Enabled = true;
                tmrRunning.Enabled = true;
            }
        }
        private string LoadCLIrqFile()
        {
            string retVal = "1";
            bool faulted = false;
            try
            {
                StreamReader s = new StreamReader(CLIrqFile);
                string CardType = s.ReadLine();
                s.Close();
            }
            catch (Exception e)
            {
                faulted = true;
                retVal = "LoadCLIrqFile() Error - " + e.Message;
            }

            if (faulted == true)
            {
                CLIrqFile = txtRqRsPath.Text + "\\test-0rq-" + AppName + ".txt";
                CLIrqFile = CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T loaded; simulating with webservice data.");
            }
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            return retVal;
        }
        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            ad = null;
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            Instruction = 0;
            RetryCnt = 0;
            SpecificRetryCnt = 0;
            Script = new StreamReader(ScriptFile);
            tmrTimeout.Enabled = true;
            tmrRunning.Enabled = true;
        }
        private void MaybeExitApp()
        {
            //if (chkNeverAutoExit.Checked == true) { return; }
            bool exit = true;
            if (AutoRun == "") { exit = false; }
            if (exit == true) { ApplicationExit(); }
        }
        private void ApplicationExit()
        {
            Application.Exit();
        }
        private void DefinetlyExitApp(string reason)
        {
            SupportMethods.WriteToWindowsEventLog(AppName, "DefinetlyExitApp - " + reason, "W");
            ApplicationExit();
        }
        private void DefinetlyExitAppWrs(string reason, string message)
        {
            SupportMethods.WriteToWindowsEventLog(AppName, "DefinetlyExitAppWrs - " + reason, "W");
            GCGCommon.SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), message, ad.RsPathAndFileToWrite);
            ApplicationExit();
        }
        private void tmrRunning_Tick(object sender, EventArgs e)
        {
            //GCGMethods.WriteTextBoxLog(txtLog, "Running...");
            PrevCommand = CurrCommand;
            CurrCommand = Script.ReadLine();
            
            ProcessInstructions();
        }
        private string DoHandleNeedsMoreInfoRqRs(string WhatToAsk)
        {
            string OK = "1";
            tmrRunning.Enabled = false;
            GCGCommon.SupportMethods.WriteResponseFile(ad.GCNEEDSMOREINFO, ad.NextRxFileWOExt + ad.POSDEL + WhatToAsk, ad.RsPathAndFileToWrite);
            tmrResponseHandlerSt = "";
            tmrResponseHandler.Enabled = true;
            return OK;
        }

        private string DoGCGDelay(int pDelayAmnt, bool ResumetmrRunning)
        {
            DelayAmnt = pDelayAmnt;
            DelayCnt = 0;
            tmrRunning.Enabled = false;
            tmrDelay.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (tmrDelay.Enabled == true);
            if (ResumetmrRunning == true)
            {
                tmrRunning.Enabled = true;
            }
            return "1";
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            GCGMethods.WriteTextBoxLog(txtLog, "Waiting " + DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                GCGMethods.WriteTextBoxLog(txtLog, "Done Waiting");
            }
        }
        //needed
        private string DoHandleCAPTCHARqRs()
        {
            string OK = "1";
            tmrRunning.Enabled = false;
            try
            {
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream _imageStream = myAssembly.GetManifestResourceStream("DVB.fake.jpg");  //Make the image an embedded resource.
                Bitmap image = new Bitmap(_imageStream);
                image.Save(ad.CAPTCHAPathAndFileToWrite);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
            tmrResponseHandlerSt = "";
            tmrResponseHandler.Enabled = true;
            return OK;
        }
        private void tmrResponseHandler_Tick(object sender, EventArgs e)
        {
            if (File.Exists(ad.NextRqPathAndFileToRead) == false)
            {
                if (tmrResponseHandlerSt == "")
                {
                    GCGMethods.WriteTextBoxLog(txtLog, "Waiting for " + ad.NextRqPathAndFileToRead);
                    tmrResponseHandlerSt = "Waiting";
                }

            }
            else
            {
                tmrResponseHandlerSt = "";
                GCGMethods.WriteTextBoxLog(txtLog, "Recieved " + ad.NextRqPathAndFileToRead);
                txtCAPTCHAAnswer.Text = GCGCommon.SupportMethods.GetAnswerFromFile(ad.NextRqPathAndFileToRead);
                do
                {
                    File.Delete(ad.NextRqPathAndFileToRead);
                } while (File.Exists(ad.NextRqPathAndFileToRead) == true);
                tmrResponseHandler.Enabled = false;
                ad.UpdateAllDetails(ad.NextRqPathAndFileToRead, txtCAPTCHAPath.Text);
                System.Diagnostics.Debug.WriteLine(Instruction.ToString());
                tmrRunning.Enabled = true;
            }
        }
        private string GetBalance(string startTag, string allText)
        {
            string retval = "";
            retval = GetBalance(startTag, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", allText);
            return retval;
        }
        private string GetBalance(string startTag, string endTag, string allText)
        {
            string retval = "";
            string RoughBalance = "";
            RoughBalance = GCGMethods.RoughExtract(startTag, endTag, allText);
            retval = GCGMethods.ReturnStandardBalance(RoughBalance);
            return retval;
        }

        private void cmdGo_Click(object sender, EventArgs e)
        {
            try {
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream _imageStream = myAssembly.GetManifestResourceStream("DVB.fake.jpg");  //Make the image an embedded resource.
                //_imageStream = myAssembly.GetManifestResourceStream("DVB.fake.jpg");
                Bitmap image = new Bitmap(_imageStream);
                image.Save(ad.CAPTCHAPathAndFileToWrite);
            }
            catch( Exception ex )
             {
                MessageBox .Show (ex.Message );
            }

        }
        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Forced Exit.", ad.RsPathAndFileToWrite);
            ApplicationExit();
            return;
        }

        private void cmsSaveScript_Click(object sender, EventArgs e)
        {
            SaveScript();
        }
        public void SaveScript()
        {
            try
            {
                StreamWriter s = new StreamWriter(ScriptFile);
                s.Write(txtScript.Text);
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveScript Error " + e.Message);
            }
        }
        public void LoadScript()
        {
            txtScript.Text = "";
            try
            {
                StreamReader s = new StreamReader(ScriptFile);
                do
                {
                    txtScript.Text= txtScript.Text+s.ReadLine() + Environment.NewLine;
                } while (s.EndOfStream!=true);
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "LoadScript Error " + e.Message);
            }
        }

        private void cmdLoadScript_Click(object sender, EventArgs e)
        {
            LoadScript();
        }

        private void cmdGCBALANCE_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCBALANCE");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCNEEDSMOREINFO_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCNEEDSMOREINFO");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCCAPTCHA_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCCAPTCHA");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCBALANCEERR_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCBALANCEERR");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCERR_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCERR");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCCUSTOM_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCCUSTOM");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

        private void cmdGCTIMEOUT_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("GCTIMEOUT");
            txtScript.Text = txtScript.Text + Clipboard.GetText() + Environment.NewLine;
        }

    }
}
