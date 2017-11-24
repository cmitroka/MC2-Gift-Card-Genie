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
namespace DVB
{
    public partial class Main : Form
    {
        public string CLIrqFile = "", AppName = "", AutoRun = "";

        public AllDetails ad;
        static int SecondsPassed;

        public int SpecificRetryCnt, SpecificRetryCntMax;
        public int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt, DelayCnt;

        public string tmrResponseHandlerSt;
        SHDocVw.InternetExplorer IE;
        public static IHTMLDocument2 FrameDoc;
        private void ProcessInstructions()
        {
            SupportMethods.WriteResponseFile(txtType.Text, txtMessageToReturn.Text, ad.RsPathAndFileToWrite);
            if (CLIrqFile == "") File.Delete(ad.RsPathAndFileToWrite);
            MaybeExitApp();
        }

        public Main()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
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
            this.Location = new Point(0, 0);
            string[] AllPieces = SupportMethods.GetFilePathPieces(System.Reflection.Assembly.GetEntryAssembly().Location);
            AppName = System.AppDomain.CurrentDomain.FriendlyName;
            string[] AppPieces = SupportMethods.GetFilePathPieces(AppName);
            AppName = AppPieces[3];
            string configFile = AllPieces[1] + "\\" + AppName +"_NONE.txt";
            try
            {
                StreamReader sr = new StreamReader(configFile);
                txtMessageToReturn.Text = sr.ReadLine();
                txtType.Text= sr.ReadLine();
                sr.Close();
            }
            catch (Exception)
            {

            }

            this.Text = "Returner - " + AppName;
            SpecificRetryCntMax = 999;
            RetryCntMax = 30;
            SaveLoad.LoadSettingsFromRegistry(this);
            string result = LoadCLIrqFile();
            if (result != "1")
            {
                SupportMethods.WriteToWindowsEventLog(AppName, "No CLIrqFileIn was sent.", "W");
            }
            SecondsPassed = 0;
            if (AutoRun == "1")
            {
                ProcessInstructions();
            }
            GCGMethods.WriteTextBoxLog(txtLog, AppName + " Launched");
        }
        private string LoadCLIrqFile()
        {
            string retVal = "1";
            bool faulted = false;
            try
            {
                StreamReader s = new StreamReader(CLIrqFile);
                string CardType = s.ReadLine();
                //txtLogin.Text = s.ReadLine();
                //txtPassword.Text = s.ReadLine();
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
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T loaded; using " + AppName + "_LastRun.txt");
                
            }
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            ProcessInstructions();
            return retVal;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            ad = null;
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            Instruction = 0;
            RetryCnt = 0;
            SpecificRetryCnt = 0;
            ProcessInstructions();
        }

        private void Save()
        {
            string[] AllPieces = SupportMethods.GetFilePathPieces(System.Reflection.Assembly.GetEntryAssembly().Location);
            AppName = System.AppDomain.CurrentDomain.FriendlyName;
            string[] AppPieces = SupportMethods.GetFilePathPieces(AppName);
            AppName = AppPieces[3];
            string configFile = AllPieces[1] + "\\" + AppName + "_NONE.txt";
            try
            {
                StreamWriter sw = new StreamWriter(configFile);
                sw.WriteLine(txtMessageToReturn.Text);
                sw.WriteLine(txtType.Text);
                sw.Close();
            }
            catch (Exception)
            {

            }

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
        public string DoGCGDelay(int pDelayAmnt, bool ResumetmrRunning)
        {
            DelayAmnt = pDelayAmnt;
            DelayCnt = 0;
            tmrDelay.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (tmrDelay.Enabled == true);
            if (ResumetmrRunning == true)
            {
                ProcessInstructions();
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

        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(),
            "Forced to exit", ad.RsPathAndFileToWrite);
            ApplicationExit();
            return;
        }

        private void cmdMakeWebFiles_Click(object sender, EventArgs e)
        {
            CLIrqFile = txtRqRsPath.Text + "\\test-0rq-" + AppName + ".txt";
            try
            {
                StreamWriter s = new StreamWriter(txtRqRsPath.Text + "\\testbat.txt");
                s.WriteLine("\"" + txtMerchEXEPath.Text + "\\" + AppName + ".exe\" \"" + txtRqRsPath.Text + "\\test-0rq.txt\" 1");
                s.Close();
                StreamWriter s2 = new StreamWriter(CLIrqFile);
                s2.WriteLine("From Autoreturner");
                s2.Close();

            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(txtLog, ex.Message);

                throw;
            }
        }

        private void cmdSimulateGCG_Click(object sender, EventArgs e)
        {
            string pathAndEXE= System.Reflection.Assembly.GetEntryAssembly().Location;
            StreamReader sr = new StreamReader(txtRqRsPath.Text + "\\testbat.txt");
            string temp = sr.ReadLine();
            string[] parsed = temp.SplitByString("\" \"");
            sr.Close();
            SupportMethods.LaunchIt(parsed[0]+ "\"", "\"" + parsed[1]);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdReloadRequest_Click(object sender, EventArgs e)
        {
            CLIrqFile = "";
            //AuxFunctions.LoadEverything(this);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MSaveLastRun_Click(object sender, EventArgs e)
        {
        }

        private void MLoadLastRun_Click(object sender, EventArgs e)
        {
        }

        private string SetFocusOnElement(string pElement, GCGMethods.ByNameOrID pByNameOrID, int FrameIndex)
        {
            string retVal = "-1";
            try
            {
                IHTMLDocument2 htmlDoc2 = GCGMethods.ConvertIEToIHTMLDocument2(IE, FrameIndex);
                IHTMLWindow2 parentWindow = htmlDoc2.parentWindow;
                if (parentWindow != null)
                {
                    try
                    {

                        parentWindow.execScript("document.getElementBy" + pByNameOrID.ToString() + "('" + pElement + "').focus();", "javascript");
                        System.Diagnostics.Debug.WriteLine("SetFocusOnElement OK");
                        retVal = "1";
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("SetFocusOnElement Err:" + ex.Message);
                        retVal = "-1";
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SetFocusOnElement parentWindow != null");
                    retVal = "-1";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SetFocusOnElement screen not ready: " + ex.Message);
                retVal = "-1";
            }
            //mshtml.IHTMLDocument2 htmlDoc2 = IE.Document as mshtml.IHTMLDocument2;
            return retVal;
        }
    }
}
