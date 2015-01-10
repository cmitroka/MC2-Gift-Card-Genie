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
using GCGCommon;
namespace DVB
{
    public enum FocusTypes { RemoveFocus, Focus };
    public partial class Main : Form
    {
        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        // Activate or minimize a window
        [DllImportAttribute("User32.DLL")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;

        Balance bal;
        AllDetails ad;
        
        static int SecondsPassed,millisecs;
        string CLIrqFile = "", AppName = "", AutoRun = "", pAppRunLogLoc="";
        
        int WBPageDoneCnt;
        long WBSecondsIdleSet;
        int WBSecondsIdleCnt;

        int Instruction,DelayAmnt,DelayCnt,RetryCnt;
        int CopyPasteCnt,GetCAPTCHACnt;
        string CopyAndPasteResult, GetAndSaveCAPTCHAResult;
        string tmrResponseHandlerSt;
        string PathToWriteCAPTCHA;
        string CAPTCHAName;
        bool ProcessInstructionsAfterDelay;
        mshtml.IHTMLControlRange imgRange;

        public Main()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            webBrowser1.ScriptErrorsSuppressed = true;
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
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;            
            LoadSettings();
            tmrResponseHandlerSt = "";
            SupportMethods.WriteToWindowsEventLog("WebsiteDownloader-" + AppName, "WebsiteDownloader-" + AppName, "App Started");
            string result = LoadCLIrqFile();
            if (result != "1")
            {
                SupportMethods.WriteToWindowsEventLog(AppName, "No CLIrqFileIn was sent.", "W");
            }
            SecondsPassed = 0;
            if (AutoRun == "1")
            {
                tmrRunning.Enabled = true;
                tmrTimeout.Enabled = true;
            }
        }
        private string LoadCLIrqFile()
        {
            if (CLIrqFile == "")
            {
                
                CLIrqFile = txtTestRqRsPath.Text + "\\test-0rq-" + AppName + ".txt";
                CLIrqFile = CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T LOADED; WHATEVERS AT " + CLIrqFile + " WILL BE USED");
                //txtLog.Text = "A CLIrqFile WASN'T LOADED; WHATEVERS AT " + CLIrqFile + " WILL BE USED";
            }
            ad = new GCGCommon.AllDetails(CLIrqFile,txtCAPTCHAPath.Text);
            string retVal = "1";
            StreamReader s = null;
            try
            {
                s = new StreamReader(CLIrqFile);
                string CardType = s.ReadLine();
                txtCardNumber.Text = s.ReadLine();
                txtCardPIN.Text = s.ReadLine();
                txtLogin.Text = s.ReadLine();
                txtPassword.Text = s.ReadLine();
                s.Close();
            }
            catch (Exception e)
            {
                retVal = "LoadCLIrqFile() Error - " + e.Message;
            }
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

        private void ProcessInstructions()
        {
            tmrRunning.Enabled = false;
            tmrTimeout.Enabled = true;
            Instruction++;
            GCGMethods.WriteTextBoxLog(txtLog, "Inst " + Instruction.ToString());
            if (Instruction == 1)
            {
                webBrowser1.Navigate(txtBaseURL.Text);
            }
            else if (Instruction == 2)
            {
                DoHandleRqRs(GCTypes.GCCAPTCHA, "guid");
            }
            else if (Instruction == 3)
            {
                ChangeFocus(HTMLEnumAttributes.name, "TextBoxCardNumber", FocusTypes.Focus);
                DoDelay(1, true);
            }
            else if (Instruction == 4)
            {
                SendKeys.Send("{tab}");
                DoDelay(1, true);
            }
            else if (Instruction == 5)
            {
                SendKeys.Send("+{tab}");
                DoDelay(1, true);
            }
            else if (Instruction == 6)
            {
                SendKeys.Send(txtCardNumber.Text);
                DoDelay(1, true);
            }
            else if (Instruction == 7)
            {
                SendKeys.Send("{tab}");
                DoDelay(1, true);
            }
            else if (Instruction == 8)
            {
                SendKeys.Send("{tab}");
                DoDelay(1, true);
            }
            else if (Instruction == 9)
            {
                SendKeys.Send(txtCAPTCHAAnswer.Text);
                DoDelay(1, true);
            }
            else if (Instruction == 10)
            {
                SendKeys.Send("{tab}");
                DoDelay(1, true);
            }
            else if (Instruction == 11)
            {
                SendKeys.Send("{enter}");
                DoDelay(1, true);
            }

            else if (Instruction == 12)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "Big Delay");
                DoDelay(50, true);
            }
            else
            {
                //Defocus(CJMUtilities.HTMLEnumAttributes.name, "Txtgcnum");
                GCGMethods.WriteTextBoxLog(txtLog, "Get Balance");
                string test = DoManualCopyAndPaste();
                //GCGMethods.WriteFile("C:\\GetBalance.txt", test, true);
                string lfour = "";
                try
                {
                    lfour = txtCardNumber.Text.Substring(12, 4);
                }
                catch (Exception) { }
                string balanceResult = GetBalance("Balance:", "\r\n", test);
                if (balanceResult == "")
                {
                    RetryCnt++;
                    if (RetryCnt < 3)
                    {
                        GCGMethods.WriteTextBoxLog(txtLog, "Retrying the page for a balance...");
                        Instruction--;
                        DoDelay(1, true);
                        return;
                    }
                }
                if (balanceResult == "") balanceResult = "N/A";
                GCGMethods.WriteTextBoxLog(txtLog, "Balance Result: " + balanceResult);
                txtCardBalance.Text = balanceResult;
                if (balanceResult == "N/A")
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCEERR.ToString(), balanceResult, ad.RsPathAndFileToWrite);
                }
                else
                {
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCE.ToString(), balanceResult, ad.RsPathAndFileToWrite);
                }
                if (CLIrqFile == "") File.Delete(ad.RsPathAndFileToWrite);
                tmrTimeout.Enabled = false;
                MaybeExitApp();
            }
            string LogMsg = "Instruction " + Instruction.ToString() + " Done";

        
        }
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            txtIsBusy.Text = webBrowser1.IsBusy.ToString();
            //txtIsDone=webBrowser1.Document.
            string myValue = e.CurrentProgress.ToString();
            txtMaxProg.Text = e.MaximumProgress.ToString();
            txtStatus.Text = myValue;
            if (e.CurrentProgress <= 0)
            {
                tmrPageDone.Enabled = true;
            }
        }
        /*
        private void CheckForPageComplete(long SecondsIdle)
        {
            WBPageDoneCnt = 0;
            WBSecondsIdleSet = SecondsIdle;
            long cnt1 = ((WBSecondsIdleSet * 1000) / tmrPageDone.Interval);
            long cnt2 = cnt1 * WBSecondsIdleSet;
            WBSecondsIdleSet = cnt2;
            //tmrPageDone.Enabled = true;
        }
         */ 
        private void tmrPageDone_Tick(object sender, EventArgs e)
        {
            bool skipIsBusyCheck=true;
            long test;
            try
            {
                test = Convert.ToInt64(txtStatus.Text);
            }
            catch (Exception)
            {
                test = 99999999;
            }

            if (test <= 0)
            {
                WBPageDoneCnt++;
            }
            else if (webBrowser1.IsBusy == false)
            {
                WBPageDoneCnt++;
            }
            else
            {
                WBPageDoneCnt = 0;
            }
            txtCntr.Text = WBPageDoneCnt.ToString();
            if (WBPageDoneCnt >= 6)
            {
                WBPageDoneCnt = 0;
                tmrPageDone.Enabled = false;
                ProcessInstructions();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        private void SaveSettings()
        {
            
            StreamWriter sw = null;
            try
            {
                 sw = new StreamWriter(Application.StartupPath + "\\" + AppName + "_cfg.txt");
                sw.WriteLine(chkNeverAutoExit.Checked.ToString());
                DVB.GCGRegistry MR = new DVB.GCGRegistry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                MR.Write("CAPTCHAPath", txtCAPTCHAPath.Text);
                MR.Write("TestRqRsPath", txtTestRqRsPath.Text);
                MR.Write("AppStaticDBPath", txtAppStaticDBPath.Text);
            }
            catch (Exception ex)
            {
            }
            try
            {
                sw.Close();
            }
            catch (Exception ex1) { }
        }
        private void LoadSettings()
        {
            StreamReader sr = null;
            try
            {
                DVB.GCGRegistry MR = new DVB.GCGRegistry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                txtTestRqRsPath.Text = MR.Read("TestRqRsPath");
                txtAppStaticDBPath.Text=MR.Read("AppStaticDBPath");
                GCGCommon.DB db = new GCGCommon.DB(txtAppStaticDBPath.Text);
                string[][] setting = db.GetMultiValuesOfSQL("SELECT URL,Timeout FROM tblMerchants WHERE EXE='" + AppName + "'");
                txtBaseURL.Text = setting[0][0];
                txtTimeout.Text = setting[0][1];
                sr = new StreamReader(Application.StartupPath + "\\" + AppName + "_cfg.txt");
                chkNeverAutoExit.Checked = Convert.ToBoolean(sr.ReadLine());
                sr.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                sr.Close();
            }
            catch (Exception ex1){}
        }
        private void cmdExecute_Click(object sender, EventArgs e)
        {
            //tmrRunning.Enabled = true;
        }
        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            Instruction = 0;
            txtCardBalance.Text = "";
            tmrRunning.Enabled = true;
        }
        private void tmrRunning_Tick(object sender, EventArgs e)
        {
            tmrRunning.Enabled = false;
            if (CLIrqFile == "")
            {
                GCGMethods.WriteTextBoxLog(txtLog, "Can't run without a CLI file");
            }
            ProcessInstructions();
        }
        private void tmrTimeout_Tick(object sender, EventArgs e)
        {
            int MaxSeconds = 0;
            SecondsPassed = SecondsPassed + 1;
            this.Text = "Balance Extractor - " + SecondsPassed;
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
            if (chkNeverAutoExit.Checked == true) { return; }
            bool exit = true;
            if (AutoRun == "") { exit = false; }
            if (exit == true) { Application.Exit(); }
        }
        private void DefinetlyExitApp(string reason)
        {
            System.Diagnostics.Debug.WriteLine("DefinetlyExitApp - " + reason, pAppRunLogLoc);
            Application.Exit();
        }
        private void DefinetlyExitAppWrs(string reason, string message)
        {
            GCGCommon.SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), message, ad.RsPathAndFileToWrite);
            System.Diagnostics.Debug.WriteLine("DefinetlyExitAppWrs - " + message, pAppRunLogLoc);
            Application.Exit();
        }

        private void DoDelay(int pDelayAmnt, bool pProcessInstructionsAfterDelay)
        {
            DelayAmnt = pDelayAmnt;
            ProcessInstructionsAfterDelay = pProcessInstructionsAfterDelay;
            DelayCnt = 0;
            tmrDelay.Enabled = true;
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            GCGMethods.WriteTextBoxLog(txtLog,"Waiting " +DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                GCGMethods.WriteTextBoxLog(txtLog, "Done Waiting, ProcessInstructions()");
                if (ProcessInstructionsAfterDelay==true) ProcessInstructions();
            }
        }
        private void DoHandleRqRs(GCGCommon.GCTypes pGCType, string GCTypeSpecifics)
        {
            
            if (pGCType ==GCTypes.GCCAPTCHA)
            {
                string OK=GetAndSaveCAPTCHA(GCTypeSpecifics, ad.CAPTCHAPathAndFileToWrite);
                if (OK != "")
                {
                    DefinetlyExitAppWrs("GCERR", AppName + "Couldn't write CAPTCHA file, probably permissions error.");
                    return;
                }
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
            }
            else if (pGCType ==GCTypes.GCNEEDSMOREINFO)
            {
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCNEEDSMOREINFO, ad.NextRxFileWOExt + ad.POSDEL + GCTypeSpecifics, ad.RsPathAndFileToWrite);
            }
            tmrResponseHandler.Enabled = true;
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
                } while (File.Exists(ad.NextRqPathAndFileToRead)==true);
                tmrResponseHandler.Enabled = false;
                ad.UpdateAllDetails(ad.NextRqPathAndFileToRead, txtCAPTCHAPath.Text);
                ProcessInstructions();
            }
        }
        private void tmrCopyPaste_Tick(object sender, EventArgs e)
        {
            CopyPasteCnt++;
            try
            {
                if (CopyPasteCnt == 1)
                {
                    webBrowser1.Document.ExecCommand("SelectAll", false, null);
                    System.Diagnostics.Debug.WriteLine("tmrCopyPaste Select");
                    tmrCopyPaste.Interval = 100;
                }
                else if (CopyPasteCnt == 2)
                {
                    webBrowser1.Document.ExecCommand("Copy", false, null);
                    System.Diagnostics.Debug.WriteLine("tmrCopyPaste Copy");
                    //The copy takes a while, maybe a second, otherwise it may exception
                    tmrCopyPaste.Interval = 1000;
                }
                else if (CopyPasteCnt == 3)
                {
                    CopyAndPasteResult = Clipboard.GetText();
                    GCGMethods.WriteTextBoxLog(txtLog,"tmrCopyPaste SUCCESSFUL.");
                }
            }
            catch (Exception)
            {
                CopyAndPasteResult = "Failed";
                GCGMethods.WriteTextBoxLog(txtLog, "tmrCopyPaste FAILED.");
                CopyPasteCnt = 4;
            }
            if (CopyPasteCnt > 3)
            {
                System.Diagnostics.Debug.WriteLine("tmrCopyPaste Resetting/Disabling");
                tmrCopyPaste.Enabled = false;
                CopyPasteCnt = 0;
            }
        }
        private string DoManualCopyAndPaste()
        {
            CopyAndPasteResult = "";
            tmrCopyPaste.Enabled=true;
            do
            {
                Application.DoEvents();
            } while (tmrCopyPaste.Enabled == true);
            System.Diagnostics.Debug.WriteLine("Done.");
            return CopyAndPasteResult;
        }
        private string GetBalance(string startTag,string endTag,string allText)
        {
            string retval="";
            string RoughBalance = "";
            RoughBalance = GCGMethods.RoughExtract(startTag, endTag, allText);
            retval = GCGMethods.ReturnStandardBalance(RoughBalance);
            return retval; 
        }
        private string GetAndSaveCAPTCHA(string tagName,string SavePath)
        {
            CAPTCHAName = tagName;
            PathToWriteCAPTCHA = SavePath;
            GetAndSaveCAPTCHAResult = "";
            GetCAPTCHACnt = 0;
            tmrGetCAPTCHA.Enabled = true;
            bool a=webBrowser1.IsBusy;
            do
            {
                Application.DoEvents();
            } while (tmrGetCAPTCHA.Enabled == true);
            System.Diagnostics.Debug.WriteLine("Done.");
            return GetAndSaveCAPTCHAResult;
        }

        private void tmrGetCAPTCHA_Tick(object sender, EventArgs e)
        {
            GetCAPTCHACnt++;
            string retVal = "";
            if (GetCAPTCHACnt == 1)
            {
                try
                {
                    string AllHTML = webBrowser1.DocumentText;
                    HtmlElementCollection imgs = webBrowser1.Document.GetElementsByTagName("img");
                    string temp = "";
                    for (int i = 0; i < imgs.Count; i++)
                    {
                        Console.WriteLine(temp);
                    }
                    mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
                    //mshtml.IHTMLControlRange imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
                    imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
                    foreach (mshtml.IHTMLImgElement img in doc.images)
                    {
                        if (img.nameProp.Contains(CAPTCHAName))
                        {
                            imgRange.add((mshtml.IHTMLControlElement)img);
                            imgRange.execCommand("Copy", false, null);
                            tmrGetCAPTCHA.Interval = 500;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 1 FAILED.");
                    GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                }

            }
            else if (GetCAPTCHACnt == 2)
            {
                try
                {
                    Bitmap bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    bmp.Save(PathToWriteCAPTCHA); //img.nameProp
                    GetAndSaveCAPTCHAResult = "";
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA Completed.");
                }
                catch (Exception ex)
                {
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 2 FAILED.");
                    GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                }
                tmrGetCAPTCHA.Enabled = false;
            }

        }
        private void Defocus(GCGCommon.HTMLEnumAttributes idorname, string whatitscalled)
        {
            whatitscalled = whatitscalled.ToUpper();
            HtmlElementCollection col = webBrowser1.Document.GetElementsByTagName("input");  //can be hr, or input, or whatever
            foreach (HtmlElement element in col)
            {
                string tempValue = element.GetAttribute(idorname.ToString()).ToUpper(); //can be id, or src, or whatever...
                System.Diagnostics.Debug.WriteLine("Defocus: " + tempValue);
                if (tempValue == whatitscalled)
                {
                    element.RemoveFocus();
                }
            }
        }
        private void ChangeFocus(GCGCommon.HTMLEnumAttributes idorname, string whatitscalled, FocusTypes fc)
        {
            whatitscalled = whatitscalled.ToUpper();
            HtmlElementCollection col = webBrowser1.Document.GetElementsByTagName("input");  //can be hr, or input, or whatever
            foreach (HtmlElement element in col)
            {
                string tempValue = element.GetAttribute(idorname.ToString()).ToUpper(); //can be id, or src, or whatever...
                System.Diagnostics.Debug.WriteLine("Defocus: " + tempValue);
                if (tempValue == whatitscalled)
                {
                    if (fc == FocusTypes.Focus)
                    {
                        element.Focus();
                    }
                    else if (fc == FocusTypes.RemoveFocus)
                    {
                        element.RemoveFocus();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txtBaseURL.Text);

        }
        private void ActivateApplication(string briefAppName)
        {
            Process[] procList = Process.GetProcessesByName(briefAppName);

            if (procList.Length > 0)
            {
                ShowWindow(procList[0].MainWindowHandle, SW_RESTORE);
                SetForegroundWindow(procList[0].MainWindowHandle);
            }
        }
    }
}
