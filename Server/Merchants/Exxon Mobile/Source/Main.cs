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

        private void ProcessInstructions()
        {
            //WebProxy proxy = WebProxy.GetDefaultProxy();
            txtRetryCntr.Text = RetryCnt.ToString();
            string OK = "1";
            Instruction++;
            IHTMLDocument2 FrameDoc = null;
            FrameDoc = GCGMethods.ConvertWebBrowserToIHTMLDocument2(webBrowser1);
            GCGMethods.WriteTextBoxLog(txtLog, "Inst " + Instruction.ToString());
            if ((RetryCnt >= RetryCntMax) || (SpecificRetryCnt >= SpecificRetryCntMax))
            {
                txtCardBalance.Text = "N/A";
                SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCERR.ToString(), "The retry count went above " + RetryCntMax, ad.RsPathAndFileToWrite);
                tmrRunning.Enabled = false;
                tmrTimeout.Enabled = false;
                MaybeExitApp();
                return;
            }
            DetectPageErrors();
            if (Instruction == 1)
            {
                webBrowser1.Navigate(txtBaseURL.Text);
                //OK = AnonWebBrowser.Use_http_anonymouse(this, txtBaseURL.Text);
                //HandleInstruction(OK);
            }
            else if (Instruction == 2)
            {
                OK = GCGMethods.ModifyHTML(FrameDoc, "area", "alt", "View Card Balance", "<area alt=\"View Card Balance\" id=\"cjm\" coords=\"0,354,256,405\" href=\"https://wbiprod.storedvalue.com/WBI/lookupservlet?language=en&amp;brand_id=exxonmobil\">");
                if (OK != "-1") OK = DoGCGDelay(5,true);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.area, HTMLEnumAttributes.id, "cjm", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 3)
            {
                OK=DoHandleCAPTCHARqRs("jsessionid");
                HandleInstruction(OK);
            }
            
            else if (Instruction == 4)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "cardNoH", txtCardNumber.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "inCaptchaChars", txtCAPTCHAAnswer.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "action", "");
                HandleInstruction(OK);
            }
            else
            {
                string balanceResult = "";
                IHTMLDocument2 doc = null;
                doc = GCGMethods.ConvertWebBrowserToIHTMLDocument2(webBrowser1);
                //webBrowser1.DocumentText();
                RetryCntMax = 15;
                string testhtml = GCGMethods.GetHTML(FrameDoc);
                string test = testhtml;
                test = GCGMethods.GetPlainTextFromHTML(test);
                //GCGMethods.WriteFile("C:\\GetBalance.txt", test, true);
                //GCGMethods.WriteFile("C:\\GetBalance.txt", testhtml, true);
                string l4 = "9999";
                try
                {
                    int l4l = txtCardNumber.Text.Length - 4;
                    l4 = txtCardNumber.Text.Substring(l4l, 4);
                }
                catch (Exception ex)
                {
                }
                balanceResult = GetBalance("balance", "Close", test);
                if (balanceResult == "")
                {
                    OK = "-1";
                    string VendErr1 = "Invalid information entered, please try again";
                    string VendErr2 = "Card balance unavailable. The card number you entered may be incorrect, has no available history, or is a promotional card for which balance information is not available or has expired.";
                    string VendErr3 = "The PIN number entered is invalid. Please check your PIN number and try again.";
                    if (test.Contains(VendErr1))
                    {
                        txtCardBalance.Text = "VE1";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr1, ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                    else if (test.Contains(VendErr2))
                    {
                        txtCardBalance.Text = "VE2";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr2, ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                    else if (test.Contains(VendErr3))
                    {
                        txtCardBalance.Text = "VE3";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr3, ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                    if (OK == "-1") HandleInstruction(OK);
                }
                else
                {
                    txtCardBalance.Text = balanceResult;
                    SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCBALANCE.ToString(), balanceResult, ad.RsPathAndFileToWrite);
                }
                if (OK == "1")
                {
                    if (CLIrqFile == "") File.Delete(ad.RsPathAndFileToWrite);
                    tmrRunning.Enabled = false;
                    tmrTimeout.Enabled = false;
                    MaybeExitApp();
                }
            }
        }
        private void DetectPageErrors()
        {
            try
            {
                bool Error = false;
                if (webBrowser1.Document.Url.ToString().StartsWith("res://ieframe.dll")) Error = true;
                if (Error == true)
                {
                    Instruction--;
                    if (Instruction < 0) Instruction = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void HandleInstruction(string OK)
        {
            if (Instruction == 2)
            {
                if (OK != "1")
                {
                    SpecificRetryCnt++;
                    if (SpecificRetryCnt >= 25)
                    {
                        SpecificRetryCnt = 0;
                        Instruction = 0;
                        GCGMethods.WriteTextBoxLog(txtLog, "Re-Requesting URL");
                        return;
                    }
                }
            }

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
            this.Location = new Point(0, 0);
            this.Size = new Size(973, 800);
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            this.Text = "Balance Extractor - " + AppName;
            string result = AuxFunctions.LoadEverything(this);
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
            AuxFunctions.SaveSettings(this);
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
        private string DoHandleCAPTCHARqRs()
        {
            string retVal = DoHandleCAPTCHARqRs(null, -1);
            return retVal;
        }
        private string DoHandleCAPTCHARqRs(string ImgSrcOfCaptcha)
        {
            string retVal = DoHandleCAPTCHARqRs(ImgSrcOfCaptcha, -1);
            return retVal;
        }
        private string DoHandleCAPTCHARqRs(string ImgSrcOfCaptcha, int FrameItsIn)
        {
            string OK = "1";
            tmrRunning.Enabled = false;
            if (ImgSrcOfCaptcha == null)
            {
                OK = GetAndSaveCAPTCHAFromClipboard();
            }
            else
            {
                OK = GetAndSaveCAPTCHAFromBrowser(ImgSrcOfCaptcha, FrameItsIn);
            }
            if (OK != "1")
            {
                tmrRunning.Enabled = true;
            }
            else
            {
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
                tmrResponseHandlerSt = "";
                tmrResponseHandler.Enabled = true;
            }
            return OK;
        }
        private string DoHandleCAPTCHARqRs(int X, int Y, int width, int height)
        {
            string OK = "1";
            tmrRunning.Enabled = false;
            Bitmap bmp = null;
            PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;
            try
            {
                //GCGMethods.ForceForegroundWindow(this.Handle);                            //or SnagIt to get the positions
                DoGCGDelay(5, false);
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
                GCGCommon.SupportMethods.WriteResponseFile(ad.GCCAPTCHA, ad.NextRxFileWOExt, ad.RsPathAndFileToWrite);
                tmrResponseHandlerSt = "";
                tmrResponseHandler.Enabled = true;
            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA 2 FAILED.");
                GetAndSaveCAPTCHAResult = "SaveCAPTCHA Error bmp.Save: " + ex.Message + Environment.NewLine + "(Probably cant write to " + PathToWriteCAPTCHA + ")";
                OK = "-1";
            }
            return OK;

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
        private string GetAndSaveCAPTCHAFromBrowser(string ImgSrcOfCaptcha, int FrameItsIn)
        {
            CAPTCHAFound = false;
            CAPTCHAName = ImgSrcOfCaptcha;
            CAPTCHAFrame = FrameItsIn;
            PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;

            GetAndSaveCAPTCHAResult = "";
            GetCAPTCHACnt = 0;
            tmrGetCAPTCHA.Enabled = true;
            bool a = webBrowser1.IsBusy;
            do
            {
                Application.DoEvents();
            } while (tmrGetCAPTCHA.Enabled == true);
            System.Diagnostics.Debug.WriteLine("Done.");
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
        private void tmrGetCAPTCHA_Tick(object sender, EventArgs e)
        {
            GetCAPTCHACnt++;
            int failpoint = 0;
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
                    mshtml.HTMLDocument htmlDoc = null;
                    IHTMLWindow2 htmlWindow = null;
                    if (CAPTCHAFrame == -1)
                    {
                        doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
                    }
                    else
                    {
                        htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
                        htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(CAPTCHAFrame);
                        doc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                    }

                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA making imgRange");
                    imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc.body).createControlRange();
                    foreach (mshtml.IHTMLImgElement img in doc.images)
                    {
                        //System.Diagnostics.Debug.WriteLine(img.nameProp);
                        string ImageDetails = img.src.ToUpper();
                        System.Diagnostics.Debug.WriteLine(img.src.ToUpper());
                        CAPTCHAName = CAPTCHAName.ToUpper();
                        if (ImageDetails.Contains(CAPTCHAName))
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
                }
                tmrGetCAPTCHA.Enabled = false;
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
            webBrowser1.Navigate(txtBaseURL.Text);
        }
        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(),
            txtForceExit.Text, ad.RsPathAndFileToWrite);
            Application.Exit();
            return;
        }
        private void cmdReloadRequest_Click(object sender, EventArgs e)
        {
            CLIrqFile = "";
            AuxFunctions.LoadEverything(this);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            com.mc2techservices.gcg.WebService WS = new com.mc2techservices.gcg.WebService();
            WebProxy proxy = WebProxy.GetDefaultProxy();
            WS.Proxy = proxy;
            WS.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            WS.SaveWDDataForEXE01(AppName, txtCleanName.Text, txtBaseURL.Text, txtCardNumber.Text, txtCardPIN.Text, txtLogin.Text, txtPassword.Text, cmbSupportCode.Text, txtTimeout.Text,  "CJMGCG");
            //WS.SaveURLForEXE(AppName,txtBaseURL.Text,"");
        }
    }
}
