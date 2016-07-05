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
        AllDetails ad;
        static int SecondsPassed;
        string CLIrqFile = "", AppName = "", AutoRun = "";

        int msgcurrpos;
        string msg;

        bool CAPTCHAFound;
        int SpecificRetryCnt;
        int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt,DelayCnt;
        int CopyPasteCnt,GetCAPTCHACnt;
        string CopyAndPasteResult, GetAndSaveCAPTCHAResult;

        string tmrResponseHandlerSt;
        string PathToWriteCAPTCHA;
        string CAPTCHAName;
        mshtml.IHTMLControlRange imgRange;
        private void ProcessInstructions()
        {
            txtRetryCntr.Text = RetryCnt.ToString();
            string OK = "1";
            Instruction++;
            GCGMethods.WriteTextBoxLog(txtLog, "Inst " + Instruction.ToString());
            if (RetryCnt == RetryCntMax)
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
                webBrowser1.Navigate(txtBaseURL.Text);
            }
            else if (Instruction == 2)
            {

                OK = GCGMethods.SimInput(webBrowser1, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "cardnum",txtCardNumber.Text);
                if (OK != "-1")
                {
                    OK = GCGMethods.SimInput(webBrowser1, HTMLEnumTagNames.input, HTMLEnumAttributes.OuterHtml, "*%Submit");
                }
                HandleInstruction(OK);
            }
            else if (Instruction == 3)
            {
                OK = GCGMethods.SimInput(webBrowser1, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "reg_code",txtCardPIN.Text);
                if (OK != "-1") OK = GCGMethods.SimInput(webBrowser1, HTMLEnumTagNames.input, HTMLEnumAttributes.OuterHtml, "*%Submit");
                HandleInstruction(OK);                
            }
            /*
            else if (Instruction == 4)
            {
                IHTMLDocument2 temp = GCGMethods.ConvertWebBrowserToIHTMLDocument2(webBrowser1,0);
                OK = GCGMethods.SimInput2(temp, HTMLEnumTagNames.a, HTMLEnumAttributes.id, "cardNoH", txtCardNumber.Text);
                if (OK!="-1") OK = GCGMethods.SimInput2(temp, HTMLEnumTagNames.a, HTMLEnumAttributes.id, "inCaptchaChars", txtCAPTCHAAnswer.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(temp, HTMLEnumTagNames.a, HTMLEnumAttributes.id, "submit", "");
                HandleInstruction(OK);
            }
            */
            else
            {
                string test = GCGMethods.GetHTMLFromWebObject(webBrowser1);
                test = GCGMethods.GetPlainTextFromHTML(test);
                //string test = DoManualCopyAndPaste();
                GCGMethods.WriteFile("C:\\GetBalance.txt", test, true);
                string balanceResult = GetBalance("Trade $ - Advertising", "XXXXXXXXXXXXXXXXXX", test);
                if (balanceResult == "")
                {
                    OK = "-1";
                    HandleInstruction(OK);
                }
                else
                {
                    txtCardBalance.Text = balanceResult;
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCE.ToString(), balanceResult, ad.RsPathAndFileToWrite);
                    if (CLIrqFile == "") File.Delete(ad.RsPathAndFileToWrite);
                    tmrRunning.Enabled = false;
                    tmrTimeout.Enabled = false;
                    MaybeExitApp();
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
            if (AutoRun == "1") { tmrRunning.Enabled = true; }
        }
        private string LoadCLIrqFile()
        {
            if (CLIrqFile == "")
            {
                
                CLIrqFile = txtTestRqRsPath.Text + "\\test-0rq-" + AppName + ".txt";
                CLIrqFile = CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T LOADED; USING " + CLIrqFile);
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
                GCGCommon.Registry MR = new GCGCommon.Registry();
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
            RetryCntMax = 15;
            StreamReader sr = null;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
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
        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            Instruction = 0;
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
            SupportMethods.WriteToWindowsEventLog(AppName, "DefinetlyExitApp - " + reason, "W");
            Application.Exit();
        }
        private void DefinetlyExitAppWrs(string reason, string message)
        {
            SupportMethods.WriteToWindowsEventLog(AppName, "DefinetlyExitAppWrs - " + reason, "W");
            GCGCommon.SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), message, ad.RsPathAndFileToWrite);
            Application.Exit();
        }

        private void DoDelay(int pDelayAmnt)
        {
            DelayAmnt = pDelayAmnt;
            DelayCnt = 0;
            tmrRunning.Enabled = false;
            tmrDelay.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (tmrDelay.Enabled==true);
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            GCGMethods.WriteTextBoxLog(txtLog,"Waiting " +DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                GCGMethods.WriteTextBoxLog(txtLog, "Done Waiting, enabling tmrRunning");
                tmrRunning.Enabled = true;
            }
        }
        private string DoHandleRqRs(GCGCommon.GCTypes pGCType, string GCTypeSpecifics)
        {
            string OK = "";
            tmrRunning.Enabled = false;
            if (pGCType ==GCTypes.GCCAPTCHA)
            {
                OK=GetAndSaveCAPTCHA(GCTypeSpecifics, ad.CAPTCHAPathAndFileToWrite);
                if (OK != "1")
                {
                    tmrRunning.Enabled = true;
                }
                else
                {
                    GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
                    tmrResponseHandler.Enabled = true;
                }
            }
            else if (pGCType ==GCTypes.GCNEEDSMOREINFO)
            {
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCNEEDSMOREINFO, ad.NextRxFileWOExt + ad.POSDEL + GCTypeSpecifics, ad.RsPathAndFileToWrite);
            }
            System.Diagnostics.Debug.WriteLine(Instruction.ToString());
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
                } while (File.Exists(ad.NextRqPathAndFileToRead)==true);
                tmrResponseHandler.Enabled = false;
                ad.UpdateAllDetails(ad.NextRqPathAndFileToRead, txtCAPTCHAPath.Text);
                System.Diagnostics.Debug.WriteLine(Instruction.ToString());
                tmrRunning.Enabled = true;
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
                    GCGMethods.WriteTextBoxLog(txtLog, "tmrCopyPaste Select");
                    //System.Diagnostics.Debug.WriteLine("tmrCopyPaste Select");
                    tmrCopyPaste.Interval = 500;
                }
                else if (CopyPasteCnt == 2)
                {
                    webBrowser1.Document.ExecCommand("Copy", false, null);
                    GCGMethods.WriteTextBoxLog(txtLog, "tmrCopyPaste Copy");
                    //System.Diagnostics.Debug.WriteLine("tmrCopyPaste Copy");
                    //The copy takes a while, maybe a second, otherwise it may exception
                    tmrCopyPaste.Interval = 500;
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
            tmrRunning.Enabled = false;
            CopyPasteCnt = 0;
            CopyAndPasteResult = "";
            tmrCopyPaste.Enabled=true;
            do
            {
                Application.DoEvents();
            } while (tmrCopyPaste.Enabled == true);
            System.Diagnostics.Debug.WriteLine("Done.");
            tmrRunning.Enabled = true;
            return CopyAndPasteResult;
        }
        private string GetAndSaveCAPTCHA(string tagName,string SavePath)
        {
            CAPTCHAFound = false;
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
            int failpoint=0;
            string retVal = "";
            if (GetCAPTCHACnt == 1)
            {
            }
            else if (GetCAPTCHACnt == 2)
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
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA grabbing doc");
                    IHTMLDocument2 doc = null;
                    /*
                    mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
                    IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(0);
                    IHTMLDocument2 doc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                    */                    
                    doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA making imgRange");
                    imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
                    foreach (mshtml.IHTMLImgElement img in doc.images)
                    {
                        System.Diagnostics.Debug.WriteLine(img.nameProp);
                        if (img.nameProp.Contains(CAPTCHAName))
                        {
                            imgRange.add((mshtml.IHTMLControlElement)img);
                            imgRange.execCommand("Copy", false, null);
                            tmrGetCAPTCHA.Interval = 500;
                            CAPTCHAFound = true;
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 1 FAILED at " + failpoint.ToString());
                    GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                }

            }
            else if (GetCAPTCHACnt == 3)
            {
                if (CAPTCHAFound == true)
                {
                    try
                    {
                        Bitmap bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                        bmp.Save(PathToWriteCAPTCHA); //img.nameProp
                        GetAndSaveCAPTCHAResult = "1";
                        GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA Completed.");
                    }
                    catch (Exception ex)
                    {
                        GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 2 FAILED.");
                        GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                    }
                }
                tmrGetCAPTCHA.Enabled = false;
            }

        }
        private string GetBalance(string startTag, string endTag, string allText)
        {
            string retval = "";
            string RoughBalance = "";
            RoughBalance = GCGMethods.RoughExtract(startTag, endTag, allText);
            retval = GCGMethods.ReturnStandardBalance(RoughBalance);
            return retval;
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
        private void cmdGo_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txtBaseURL.Text);
        }
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            txtIsBusy.Text = webBrowser1.IsBusy.ToString();
            string myValue = e.CurrentProgress.ToString();
            txtMaxProg.Text = e.MaximumProgress.ToString();
            txtStatus.Text = myValue;
        }
        private string DoSendKeys(string message)
        {
            msgcurrpos = 0;
            tmrSendKeys.Enabled = true;
            msg = message;
            do
	        {
                Application.DoEvents();
	        } while (msgcurrpos<msg.Length);
            tmrSendKeys.Enabled = false;
            System.Diagnostics.Debug.Write("DONE.");
            return "1";
        }
        private void tmrSendKeys_Tick(object sender, EventArgs e)
        {
            SendKeys.Send(msg.Substring(msgcurrpos,1));
            msgcurrpos++;
        }

    }
}
