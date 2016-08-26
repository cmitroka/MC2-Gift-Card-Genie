﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Automation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using mshtml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GCGCommon;
namespace DVB
{
    public partial class Main : Form
    {

        public string CLIrqFile = "", AppName = "", AutoRun = "";

        public AllDetails ad;
        static int SecondsPassed;
        private static string whattotype;
        private static string whattotypeall;
        private static int whattotypeloc;
        private static bool whattotypecompleted;
        IntPtr MainWindowHWND;
        bool tmrDelayLog;
        bool CAPTCHAFound;
        public int SpecificRetryCnt, SpecificRetryCntMax;
        public int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt, DelayCnt;
        int GetCAPTCHACnt;
        string GetAndSaveCAPTCHAResult;

        public string tmrResponseHandlerSt;
        string PathToWriteCAPTCHA;
        string CAPTCHAName;
        int CAPTCHAFrame;
        mshtml.IHTMLControlRange imgRange;
        public static IHTMLDocument2 FrameDoc;
        int IEQuit;
        private void ProcessInstructions()
        {
            txtRetryCntr.Text = RetryCnt.ToString();
            string OK = "1";
            Instruction++;
            GCGMethods.WriteTextBoxLog(txtLog, "Inst " + Instruction.ToString());
            if (RetryCnt >= RetryCntMax)
            {
                txtCardBalance.Text = "N/A";
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), "The retry count went above " + RetryCntMax, ad.RsPathAndFileToWrite);
                tmrRunning.Enabled = false;
                tmrTimeout.Enabled = false;
                MaybeExitApp();
                return;
            }
            if (Instruction == 1)
            {
                CloseAllChromeBrowsers();
                string sPntr =SupportMethods.LaunchIt("chrome", txtBaseURL.Text);
                DoGCGDelay(15, true);
                long iPntr = Convert.ToInt64(SupportMethods.FindChromeHNWD());
                MainWindowHWND = (IntPtr)iPntr;
                GCGCommon.SupportMethods.AdjustWindow(MainWindowHWND, 0, 0, 800, 800);
            }
            else if (Instruction == 2)
            {
                DoHandleValidateBytesSize2(0, 0, 80, 40, txtVBS1.Text);
                DoUI.DoMouseClick(342, 20);
                DoGCGDelay(2, true);
                DoHandleTyper("{PGDN}");
                DoGCGDelay(5, true);
                DoUI.DoMouseClick(342, 307);
                DoGCGDelay(5, true);
            }
            else if (Instruction == 3)
            {
                DoHandleValidateBytesSize2(0, 0, 80, 40, txtVBS2.Text);
                DoUI.DoMouseClick(375, 100);
                bool isThere = IsTextPresent("Security Check");
                if (isThere == false)
                {
                    GCGCommon.SupportMethods.SetForegroundWindowByHWND(MainWindowHWND);
                    DoGCGDelay(5, true);
                    DoUI.DoMouseClick(179, 376);
                    DoHandleTyper(txtCardNumber.Text + "{TAB}" + txtCardPIN.Text + "{TAB}{ENTER}");
                }
                else
                {
                    DoHandleCAPTCHARqRs(14, 490, 300, 70);
                    GCGCommon.SupportMethods.SetForegroundWindowByHWND(MainWindowHWND);
                    DoGCGDelay(5, true);
                    DoUI.DoMouseClick(179, 376);
                    DoGCGDelay(5, true);
                    DoHandleTyper(txtCardNumber.Text + "{TAB}" + txtCardPIN.Text + "{TAB}{TAB}{TAB}" + txtCAPTCHAAnswer.Text +"{TAB}{ENTER}");
                }
                HandleInstruction("1");
            }
            else
            {
                OK = "-1";
                DoHandleTyper("{SELECTALL}");
                DoGCGDelay(5, true);
                DoHandleTyper("{COPY}");
                string test= Clipboard.GetText();
                string lfour = "";
                try
                {
                    lfour = txtCardNumber.Text.Substring(txtCardNumber.TextLength - 4, 4);
                }
                catch (Exception) { }
                //GCGMethods.WriteFile("C:\\test" + SpecificRetryCnt + ".txt", test, false);
                string balanceResult = GetBalance(lfour, "check", test);
                if (balanceResult == "")
                {
                    SpecificRetryCnt++;
                    GCGMethods.WriteTextBoxLog(txtLog, "SpecificRetryCnt: " + SpecificRetryCnt.ToString());
                    if (SpecificRetryCnt >= 10)
                    {
                        txtCardBalance.Text = "Error";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sorry, we couldn't get the balance for some reason.", ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                }
                else
                {
                    txtCardBalance.Text = balanceResult;
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCE.ToString(), balanceResult, ad.RsPathAndFileToWrite);
                    OK = "1";
                }
                if (OK == "1")
                {
                    if (CLIrqFile == "") File.Delete(ad.RsPathAndFileToWrite);
                    DoGCGDelay(10, true);
                    tmrRunning.Enabled = false;
                    tmrTimeout.Enabled = false;
                    MaybeExitApp();
                }
                else
                {
                    HandleInstruction(OK);
                }
            }
        }
        private void HandleInstruction(string OK)
        {
            if (OK == "1")
            {
                RetryCnt = 0;
            }
            else
            {
                RetryCnt++;
                Instruction--;
            }
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
                //CLIrqFile = "";
                CLIrqFile = commands[0].ToString();
            }
            catch (Exception)
            {
            }
            try
            {
                AutoRun = "1";
                AutoRun = commands[1].ToString();
            }
            catch (Exception)
            {
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = new Size(980, 250);
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            this.Text = "Balance Extractor - " + AppName;
            SpecificRetryCntMax = 999;
            RetryCntMax = 100;
            LoadConfig();
            SaveLoad.LoadSettingsFromRegistry(this);
            bool OK = SaveLoad.LoadSettingsFromDB(this);
            if (OK == false) { LoadLastRun(); }
            string result = LoadCLIrqFile();
            if (result != "1")
            {
                SupportMethods.WriteToWindowsEventLog(AppName, "No CLIrqFileIn was sent.", "W");
            }
            SecondsPassed = 0;
            if (AutoRun == "1")
            {
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
                txtCardNumber.Text = s.ReadLine();
                txtCardPIN.Text = s.ReadLine();
                //txtLogin.Text = s.ReadLine();
                //txtPassword.Text = s.ReadLine();
                txtLogin.Text = "temp1@mc2techservices.com";
                txtPassword.Text = "Temppass1";
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
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T loaded; using " + CLIrqFile);
            }
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            return retVal;
        }
        private void CloseAllChromeBrowsers()
        {

            for (int i = 0; i < 2; i++)
            {
                foreach (Process process in Process.GetProcessesByName("chrome"))
                {
                    if (process.MainWindowHandle == IntPtr.Zero) // some have no UI
                        continue;

                    AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                    if (element != null)
                    {
                        ((WindowPattern)element.GetCurrentPattern(WindowPattern.Pattern)).Close();
                    }
                }
                DoGCGDelay(5, false);
            }
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
        private void SaveLastRun()
        {
            try
            {
                StreamWriter s = new StreamWriter(Application.StartupPath + "\\" + AppName + "_LastRun.txt");
                s.WriteLine(txtCardNumber.Text);
                s.WriteLine(txtCardPIN.Text);
                s.WriteLine(txtTimeout.Text);
                s.WriteLine(txtCleanName.Text);
                s.WriteLine(txtBaseURL.Text);
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveLastRun failed");
                return;
            }
            GCGMethods.WriteTextBoxLog(txtLog, "SaveLastRun completed");
            return;
        }
        private void SaveConfig()
        {
            try
            {
                StreamWriter s = new StreamWriter(Application.StartupPath + "\\" + AppName + "_Config.txt");
                s.WriteLine(txtDelay1.Text);
                s.WriteLine(txtDelay2.Text);
                s.WriteLine(txtDelay3.Text);
                s.WriteLine(txtDelay4.Text);
                s.WriteLine(txtDelay5.Text);
                s.WriteLine(txtVBS1.Text);
                s.WriteLine(txtVBS2.Text);
                s.WriteLine(txtVBS3.Text);
                s.WriteLine(txtVBS4.Text);
                s.WriteLine(txtVBS5.Text);
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveConfig failed");
                return;
            }
            GCGMethods.WriteTextBoxLog(txtLog, "SaveConfig completed");
            return;
        }
        private void LoadConfig()
        {
            try
            {
                StreamReader s = new StreamReader(Application.StartupPath + "\\" + AppName + "_Config.txt");
                txtDelay1.Text = s.ReadLine();
                txtDelay2.Text = s.ReadLine();
                txtDelay3.Text = s.ReadLine();
                txtDelay4.Text = s.ReadLine();
                txtDelay5.Text = s.ReadLine();
                txtVBS1.Text = s.ReadLine();
                txtVBS2.Text = s.ReadLine();
                txtVBS3.Text = s.ReadLine();
                txtVBS4.Text = s.ReadLine();
                txtVBS5.Text = s.ReadLine();
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "LoadConfig " + Application.StartupPath + "\\" + AppName + "_LastRun.txt failed");
                return;
            }
            GCGMethods.WriteTextBoxLog(txtLog, "LoadConfig completed");
            return;
        }
        private void LoadLastRun()
        {
            try
            {
                StreamReader s = new StreamReader(Application.StartupPath + "\\" + AppName + "_LastRun.txt");
                txtCardNumber.Text = s.ReadLine();
                txtCardPIN.Text = s.ReadLine();
                txtTimeout.Text = s.ReadLine();
                txtCleanName.Text= s.ReadLine();
                txtBaseURL.Text = s.ReadLine();
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "LoadLastRun " + Application.StartupPath + "\\" + AppName + "_LastRun.txt failed");
                return;
            }
            GCGMethods.WriteTextBoxLog(txtLog, "LoadLastRun completed");
            return;
        }

        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            ad = null;
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
            Instruction = 0;
            RetryCnt = 0;
            SpecificRetryCnt = 0;
            txtCardBalance.Text = "";
            tmrTimeout.Enabled = true;
            tmrRunning.Enabled = true;
        }
        private void tmrRunning_Tick(object sender, EventArgs e)
        {
            ProcessInstructions();
        }
        private void tmrTimeout_Tick(object sender, EventArgs e)
        {
            int MaxSeconds = 0;
            SecondsPassed = SecondsPassed + 1;
            if (SecondsPassed==1){SaveLastRun();}
            txtTimeoutMonitor.Text = SecondsPassed.ToString();
            try
            {
                MaxSeconds = Convert.ToInt16(txtTimeout.Text);
            }
            catch (Exception)
            {

            }
            if (SecondsPassed >= MaxSeconds)
            {
                txtCardBalance.Text = "Timeout";
                GCGCommon.SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCTIMEOUT.ToString(), "Timeout", ad.RsPathAndFileToWrite);
                tmrTimeout.Enabled = false;
                MaybeExitApp();
            }
        }

        private void MaybeExitApp()
        {
            //if (chkNeverAutoExit.Checked == true) { return; }
            //DoGCGDelay(200, true);
            bool exit = true;
            if (AutoRun == "") { exit = false; }
            if (exit == true) { ApplicationExit(); }
        }
        private void ApplicationExit()
        {
            CloseAllChromeBrowsers();
            //DoGCGDelay(100, true);
            Application.Exit();
            Environment.Exit(0);
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

        public string DoGCGDelay(int pDelayAmnt, bool WriteToLog)
        {
            tmrDelayLog = WriteToLog;
            DelayAmnt = pDelayAmnt;
            DelayCnt = 0;
            tmrRunning.Enabled = false;
            tmrDelay.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (tmrDelay.Enabled == true);
            tmrRunning.Enabled = true;
            txtDelay.Text = "";
            return "1";
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            txtDelay.Text = DelayCnt.ToString();
            if (tmrDelayLog == true) GCGMethods.WriteTextBoxLog(txtLog, "Waiting " + DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                if (tmrDelayLog == true) GCGMethods.WriteTextBoxLog(txtLog, "Done Waiting");
            }
        }


        private void DoHandleValidateBytesSize2(int pX, int pY, int pW, int pH, string pSizeShouldBe)
        {
            bool DoApplicationExit = false;
            string pSize = "-999";
            string OK = "1";
            RetryCnt = 0;
            tmrRunning.Enabled = false;
            do
            {
                pSize = DoUI.ValidateBytesSize2(pX, pY, pW, pH);
                GCGMethods.WriteTextBoxLog(txtLog, pSize.ToString());
                if (pSizeShouldBe.Length != 0)
                {
                    if (pSize == pSizeShouldBe)
                    {
                        break;
                    }
                }
                System.Threading.Thread.Sleep(1000);
                RetryCnt++;
                if (RetryCnt > 10)
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "There was a problem getting the balance.", ad.RsPathAndFileToWrite);
                    DoApplicationExit = true;
                    break;
                }
                Application.DoEvents();
            } while (pSize != pSizeShouldBe);
            tmrRunning.Enabled = true;
            if (DoApplicationExit == true) { ApplicationExit(); }
        }
        private bool IsTextPresent(string TextToCheck)
        {
            tmrRunning.Enabled = false;
            bool OK = false;
            GCGCommon.SupportMethods.SetForegroundWindowByHWND(MainWindowHWND);
            DoGCGDelay(3, true);
            DoHandleTyper("{SELECTALL}");
            DoGCGDelay(3, true);
            DoHandleTyper("{COPY}");
            DoGCGDelay(10, true);
            TextToCheck = TextToCheck.ToUpper();
            string test = Clipboard.GetText().ToUpper();
            if (test.Contains(TextToCheck))
            {
                OK = true;
            }
            tmrRunning.Enabled = true;
            return OK;

        }
        private bool IsUIGraphicPresent(int pX, int pY, int pW, int pH, string pSizeShouldBe)
        {
            tmrRunning.Enabled = false;
            string pSize = "-999";
            bool OK = false;
            RetryCnt = 0;
            pSize = DoUI.ValidateBytesSize2(pX, pY, pW, pH);
            GCGMethods.WriteTextBoxLog(txtLog, pSize.ToString());
            if (pSize == pSizeShouldBe)
            {
                OK = true;
            }
            tmrRunning.Enabled = false;
            return OK;
        }
        private void DoHandleValidateBytesSize(int pX, int pY, int pW, int pH, string pSizeShouldBe, int pWiggleRoom)
        {
            DoHandleValidateBytesSize(pX, pY, pW, pH, Convert.ToInt16(pSizeShouldBe), pWiggleRoom);
        }
        private void DoHandleValidateBytesSize(int pX, int pY, int pW, int pH, int pSizeShouldBe, int pWiggleRoom)
        {
            bool DoApplicationExit=false;
            string OK = "1";
            RetryCnt = 0;
            tmrRunning.Enabled = false;
            do
            {
                OK = DoUI.ValidateBytesSize(pX, pY, pW, pH, pSizeShouldBe, pWiggleRoom);
                System.Threading.Thread.Sleep(1000);
                RetryCnt++;
                if (RetryCnt>100)
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "There was a problem getting the balance.", ad.RsPathAndFileToWrite);
                    DoApplicationExit = true;
                    break;
                }
                Application.DoEvents();
            } while (OK=="-1");
            tmrRunning.Enabled = true;
            if (DoApplicationExit == true) { ApplicationExit(); }
        }
        private string DoHandleTyper(string whatToType)
        {
            string OK = "1";
            DoHandleTyper(whatToType, 1);
            return OK;
        }
        private string DoHandleTyper(string whatToType, int speedToType)
        {
            string OK = "1";
            DoTyper t = new DoTyper(whatToType, speedToType);
            tmrRunning.Enabled = false;
            int stop = 0;
            do
            {
                Application.DoEvents();
                if (t.IsDisposed==true)
                {
                    stop++;
                }
                if (t.Enabled== false)
                {
                    stop++;
                }
            } while (stop==0);
            tmrRunning.Enabled = true;
            return OK;

        }
        private void DoHandleCAPTCHARqRs(int X, int Y, int width, int height)
        {
            string OK = "1";
            tmrRunning.Enabled = false;
            Bitmap bmp = null;
            PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;
            try
            {
                //GCGMethods.ForceForegroundWindow(this.Handle);                            //or SnagIt to get the positions
                DoGCGDelay(5, true);
                if ((X == 0) && (Y == 0) && (width == 0) && (height == 0))
                {
                    IDataObject d = Clipboard.GetDataObject();
                    bmp = (Bitmap)d.GetData(DataFormats.Bitmap);
                }
                else
                {
                    bmp = GCGMethods.CaptureRegionAsBMP(X, Y, width, height);
                }
                bmp.Save(PathToWriteCAPTCHA); //img.nameProp
                GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA Completed.");
                GCGMethods.WriteTextBoxLog(txtLog, "Waiting for...");
                GCGMethods.WriteTextBoxLog(txtLog, ad.NextRqPathAndFileToRead);
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
                bool Complete = false;
                do
                {
                    DoGCGDelay(10, false);
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
                        Complete = true;
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
                    }
                } while (Complete == false);
            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 2 FAILED.");
                GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                OK = "-1";
            }
        }
        private string GetAndSaveCAPTCHAFromClipboard()
        {
            string GetAndSaveCAPTCHAResult = "1";
            PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;
            try
            {
                Bitmap bmp = null;
                bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                bmp.Save(PathToWriteCAPTCHA); //img.nameProp
                GetAndSaveCAPTCHAResult = "1";
                GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA Completed.");

            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 2 FAILED.");
                GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
            }
            return GetAndSaveCAPTCHAResult;
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
            string retVal = "-1";
        }
        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(),
            txtForceExit.Text, ad.RsPathAndFileToWrite);
            ApplicationExit();
            return;
        }
        private void cmdReloadRequest_Click(object sender, EventArgs e)
        {
            CLIrqFile = "";
            //AuxFunctions.LoadEverything(this);
        }


        private string copyImageToClipBoard(string containsimgname)
        {
            string retVal = "1";
            mshtml.IHTMLDocument2 doc = FrameDoc;
            mshtml.IHTMLControlRange imgRange = null;
            try
            {
                imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
            }
            catch (Exception)
            {
                retVal = "-1";
                return retVal;
            }
            foreach (mshtml.IHTMLImgElement img in doc.images)
            {
                imgRange.@add((mshtml.IHTMLControlElement)img);
                if (img.src.Contains(containsimgname))
                {
                    try
                    {
                        imgRange.execCommand("Copy", false, null);
                    }
                    catch (Exception)
                    {
                        retVal = "-1";
                    }
                }

                try
                {
                    Bitmap bmp = null;
                    bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;
                    bmp.Save(PathToWriteCAPTCHA); //img.nameProp
                    GetAndSaveCAPTCHAResult = "1";
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA Completed.");
                }
                catch (Exception)
                {
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA failed");
                    retVal = "-1";
                }
            }
            return retVal;
        }


        private void MSaveDataToTestFile_Click(object sender, EventArgs e)
        {
            SaveLoad.SaveDataToTestFile(this);
        }
        private void MLoadDataFromTestFile_Click(object sender, EventArgs e)
        {
            SaveLoad.LoadDataFromTestFile(this);
        }

        private void MLoadSettingsFromDB_Click(object sender, EventArgs e)
        {
            SaveLoad.LoadSettingsFromDB(this);
        }
        private void MSaveSettingsToDB_Click(object sender, EventArgs e)
        {
            SaveLoad.SaveSettingsToDB(this);
        }
        private void MSaveSettingsToRegistry_Click(object sender, EventArgs e)
        {
            SaveLoad.SaveSettingsToRegistry(this);
        }

        private void MLoadSettingsFromRegistry_Click(object sender, EventArgs e)
        {
            SaveLoad.LoadSettingsFromRegistry(this);
        }
        private void InspectIt(string test, string contains)
        {
            if (test.Contains(contains))
            {
                GCGMethods.WriteFile("C:\\test.txt", test, true);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void MSaveLastRun_Click(object sender, EventArgs e)
        {
            SaveLastRun();
        }

        private void cmdBigSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void tmrCustom_Tick(object sender, EventArgs e)
        {
            txtCurrX.Text = Cursor.Position.X.ToString();
            txtCurrY.Text = Cursor.Position.Y.ToString();
        }

        private void cmdCapture_Click(object sender, EventArgs e)
        {
            Bitmap bmp = null;
            int iCapX = Convert.ToInt16(txtCapX.Text);
            int iCapY = Convert.ToInt16(txtCapY.Text);
            int iCapW = Convert.ToInt16(txtCapW.Text);
            int iCapH = Convert.ToInt16(txtCapH.Text);
            bmp = GCGMethods.CaptureRegionAsBMP(iCapX, iCapY, iCapW, iCapH);
            using (var ms = new MemoryStream()) // estimatedLength can be original fileLength
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // save image to stream in Jpeg format    
                txtCapOut.Text = ms.Length.ToString();
            }
        }

    private void MLoadLastRun_Click(object sender, EventArgs e)
        {
            LoadLastRun();
        }

        private void MSaveDataForConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void MLoadDataForConfig_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }


    }
}