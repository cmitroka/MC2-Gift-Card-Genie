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
        AllDetails ad;
        static int SecondsPassed;
        string CLIrqFile = "", AppName = "", AutoRun = "";

        int msgcurrpos;
        string msg;

        bool CAPTCHAFound;
        int SpecificRetryCnt, SpecificRetryCntMax;
        int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt, DelayCnt;
        int CopyPasteCnt, GetCAPTCHACnt;
        string CopyAndPasteResult, GetAndSaveCAPTCHAResult;

        string tmrResponseHandlerSt;
        string PathToWriteCAPTCHA;
        string CAPTCHAName;
        int CAPTCHAFrame;
        mshtml.IHTMLControlRange imgRange;
        SHDocVw.WebBrowser_V1 axBrowser;
        SHDocVw.WebBrowser_V1 axWebBrowser1;
        private SHDocVw.WebBrowser_V1 Web_V1; //Interface to expose ActiveX methods
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        private void ProcessInstructions()
        {
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
            }
            else if (Instruction == 2)
            {
                OK = GCGMethods.ChangeFocus(webBrowser1, HTMLEnumAttributes.name, "TextBoxCardNumber", GCGMethods.FocusTypes.Focus);
                HandleInstruction(OK);
            }
            else if (Instruction == 3)
            {
                OK = DoHandleCAPTCHARqRs("type=rca");
                HandleInstruction(OK);
            }
            else if (Instruction == 4)
            {
                DoMacro1();
            }
            else
            {
                string balanceResult = "";
                FrameDoc = GCGMethods.ConvertWebBrowserToIHTMLDocument2(webBrowser1);
                string test = GCGMethods.GetHTML(FrameDoc);
                test = GCGMethods.GetPlainTextFromHTML(test);
                //string test = DoManualCopyAndPaste();
                //GCGMethods.WriteFile("C:\\GetBalance.txt", test, true);
                balanceResult = GetBalance("Balance: ", "Transaction", test);
                if (balanceResult == "") balanceResult = GetBalance("Balance: ", "Date", test);
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

        private void Web_V1_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            /*
            Processed = true; //Stop event from being processed

            //Code to open in same window
            //this.webBrowser1.Navigate(URL);

            //Code to open in new window instead of same window

            Main Popup = new Main();
            Popup.webBrowser1.Navigate(URL);
            Popup.Show();

             */
        }
        private void HandleInstruction(string OK)
        {
            if (Instruction == 2)
            {
                if (OK != "1")
                {
                    SpecificRetryCnt++;
                    if (SpecificRetryCnt >= 5)
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
            axBrowser = (SHDocVw.WebBrowser_V1)webBrowser1.ActiveXInstance;
            // listen for new windows
            axBrowser.NewWindow += axBrowser_NewWindow;

            //tmrInitialize.Enabled = true;
        }
        void axBrowser_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            // cancel the PopUp event
            // Processed = true;

            // send the popup URL to the WebBrowser control
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
            Web_V1 = (SHDocVw.WebBrowser_V1)this.webBrowser1.ActiveXInstance;
            Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(Web_V1_NewWindow);
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            this.Text = "Balance Extractor - " + AppName;
            LoadSettings();
            LoadCommonConfig();
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
                tmrTimeout.Enabled = true;
                tmrRunning.Enabled = true;
            }
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
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
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
                //MR.Write("CAPTCHAPath", txtCAPTCHAPath.Text);
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
            SpecificRetryCntMax = 999;
            RetryCntMax = 16;
            StreamReader sr = null;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                //txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                txtTestRqRsPath.Text = MR.Read("TestRqRsPath");
                txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
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
            catch (Exception ex1) { }
        }
        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
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
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            txtIsBusy.Text = webBrowser1.IsBusy.ToString();
            string myValue = e.CurrentProgress.ToString();
            txtMaxProg.Text = e.MaximumProgress.ToString();
            txtStatus.Text = myValue;
        }
        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "There's a problem with your data, the merchants reporting 'invalid card info'", ad.RsPathAndFileToWrite);
            Application.Exit();
            return;
        }
        private void cmdReloadRequest_Click(object sender, EventArgs e)
        {
            LoadCLIrqFile();
        }

        private void DoMacro0()
        {
            tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(txtRqRsPath.Text, "" +
                "Pause,1000~!~" +
                "WinMove,0,0,800,700,Balance Extractor - XXX~!~" +
                "Pause,1000~!~" +
                "Move,479,429~!~" +
                "RightClick~!~" +
                "Pause,1000~!~" +
                "Move,550,665~!~" +
                "LeftClick");
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            tmrRunning.Enabled = true;
        }
        private void DoMacro1()
        {
            tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "WinActivate,Balance Extractor - Qdoba~!~" +
                "Pause,100~!~" +
                "Move,153,178~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "SendText," + txtCardNumber.Text + "~!~" +
                "Pause,100~!~" +
                "SendKey,TAB,2~!~" +
                "Pause,100~!~" +
                "SendText," + txtCAPTCHAAnswer.Text + "~!~" +
                "Pause,100~!~" +
                "SendKey,TAB,1~!~" +
                "Pause,100~!~" +
                "SendKey,ENTER,1"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro1 Done");
            tmrRunning.Enabled = true;
        }

        
        
        private void LoadCommonConfig()
        {            
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\Janitor";
            //txtCurrentCount.Text = MR.Read("txtCurrentCount");
            txtRqRsPath.Text = MR.Read("txtLocBatchRqRs");
            txtCAPTCHAPath.Text= MR.Read("txtLocCAPTCHA");
        }
    }
}
