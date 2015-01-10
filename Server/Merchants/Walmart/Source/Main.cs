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
        SHDocVw.InternetExplorer IE = new SHDocVw.InternetExplorer();
        public static IHTMLDocument2 FrameDoc;
        System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
        int IEQuit;
        private void ProcessInstructions()
        {
            //WebProxy proxy = WebProxy.GetDefaultProxy();
            txtRetryCntr.Text = RetryCnt.ToString();
            string OK = "1";
            Instruction++;
            //IHTMLDocument2 FrameDoc = null;
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
                IE.Visible = true;
                IE.Navigate2(txtBaseURL.Text);
            }
            else if (Instruction == 2)
            {
                int SubFrame = GCGMethods.FindWhatFrameItsIn(IE, "login-username");
                FrameDoc = GCGMethods.ConvertIEToIHTMLDocument(IE, SubFrame);
                //OK = DoHandleCAPTCHARqRs("SecurityImage.do", 999);
                if (FrameDoc==null)
                {
                    OK = "-1";
                }
                HandleInstruction(OK);
            }

            else if (Instruction == 3)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "login-username", txtLogin.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "login-password", txtPassword.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.button, HTMLEnumAttributes.classs, "btn login-sign-in-btn js-login-sign-in ", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 4)
            {
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream _imageStream = myAssembly.GetManifestResourceStream("DVB.Bitmap1.bmp");
                Bitmap image = new Bitmap(_imageStream);
                //PathToWriteCAPTCHA = ad.CAPTCHAPathAndFileToWrite;
                //image.Save(PathToWriteCAPTCHA); //img.nameProp
                //GetAndSaveCAPTCHAResult = "1";
                Clipboard.SetImage(image);
                image.Save(ad.NextRqPathAndFileToRead);  //writes fake resp
                OK = DoHandleCAPTCHARqRs();
                OK = "1";
                HandleInstruction(OK);
            }
            else if (Instruction == 5)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.a, HTMLEnumAttributes.href, "*%yourGiftCardsAndOffers.do", "");
                HandleInstruction(OK);

            }
            else if (Instruction == 6)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.a, HTMLEnumAttributes.href, "*%?registerAnotherGiftCardButton", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 7)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "cardNumber", txtCardNumber.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "pin", txtCardPIN.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "saveGiftCardButton", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 8)
            {
                
                string balanceResult = "";
                string test = GCGMethods.GetHTML(FrameDoc);
                test = GCGMethods.GetPlainTextFromHTML(test);
                //GCGMethods.WriteFile("C:\\test.txt", test, true);
                balanceResult = GetBalance("-xxxx", "history", test);
                if (balanceResult == "")
                {
                    OK = "-1";
                    if (test.Contains("That Gift Card already exists in your account"))
                    {
                        OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.a, HTMLEnumAttributes.href, "*%yourGiftCardsAndOffers.do", "");
                        OK = "-1";
                        //txtCardBalance.Text = "InvCN";
                        //SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sorry, we couldn't get the balance.", ad.RsPathAndFileToWrite);
                    }
                    else if (test.Contains("do not match our records"))
                    {
                        OK = "-1";
                        txtCardBalance.Text = "InvCN";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sorry, Invalid Card or PIN", ad.RsPathAndFileToWrite);
                        Instruction = 11;
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
                }
            }
            else if (Instruction == 9)
            {
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.a, HTMLEnumAttributes.OuterText, "Delete", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 10)
            {
                int SubFrame = GCGMethods.FindWhatFrameItsIn(IE, "BTN_yes_blue.gif");
                System.Diagnostics.Debug.WriteLine("SubFrame: " + SubFrame.ToString());
                FrameDoc = GCGMethods.ConvertIEToIHTMLDocument(IE, SubFrame);
                //OK = DoHandleCAPTCHARqRs("SecurityImage.do", 999);
                if (FrameDoc == null)
                {
                    OK = "-1";
                }
                else
                {
                    OK = "1";
                }
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.img, HTMLEnumAttributes.src, "*%BTN_yes_blue.gif", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 11)
            {
                DoGCGDelay(50,true);
                IE.Quit();
                tmrRunning.Enabled = false;
                tmrTimeout.Enabled = false;
                MaybeExitApp();
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
            if (exit == true) { ApplicationExit(); }
        }
        private void ApplicationExit()
        {
            IE.Quit();
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
            else if (FrameItsIn == 999)
            {
                copyImageToClipBoard(ImgSrcOfCaptcha);
                OK = "1";
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
                    webBrowser1 = (System.Windows.Forms.WebBrowser)IE;
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
            ApplicationExit();
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
            WS.SaveWDDataForEXE01(AppName,txtCleanName.Text, txtBaseURL.Text, txtCardNumber.Text, txtCardPIN.Text, txtLogin.Text, txtPassword.Text, cmbSupportCode.Text, txtTimeout.Text,  "CJMGCG");
            //WS.SaveURLForEXE(AppName,txtBaseURL.Text,"");
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

        private void cmdSave2_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runRequestFromWebserverToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        private void LoadSettingsFromDB()
        {
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                txtRqRsPath.Text = MR.Read("RqRsPath");
                txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
                GCGCommon.DB db = new GCGCommon.DB(txtAppStaticDBPath.Text);
                string[][] setting = db.GetMultiValuesOfSQL("SELECT URL,Timeout FROM tblMerchants WHERE EXE='" + AppName + "'");
                txtBaseURL.Text = setting[0][0];
                txtTimeout.Text = setting[0][1];
            }
            catch (Exception ex2)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "LoadSettingsFromDB error " + ex2.Message);
            }
        }
        private void SaveSettingsToDB()
        {
            try
            {
                GCGCommon.DB db = new GCGCommon.DB(txtAppStaticDBPath.Text);
                int retV = db.ExecuteSQLParamed("UPDATE tblMerchants SET URL=P0, Timeout=P1", txtBaseURL.Text, txtTimeout.Text);
                GCGMethods.WriteTextBoxLog(txtLog, retV.ToString());
            }
            catch (Exception ex2)
            {
                GCGMethods.WriteTextBoxLog(txtLog, "SaveSettingsToDB error " + ex2.Message);
            }
        }





        private void MSaveDataToTestFile_Click(object sender, EventArgs e)
        {
            SaveLoad.SaveDataToTestFile(this);
        }
        private void MLoadDataFromTestFile_Click(object sender, EventArgs e)
        {
            SaveLoad.LoadDataFromTestFile(this);
        }
        private void MSaveDataToWebserver_Click(object sender, EventArgs e)
        {
            SaveLoad.SaveDataToWebserver(this);
        }
        private void MLoadDataFromWebserver_Click(object sender, EventArgs e)
        {
            SaveLoad.LoadDataFromWebserver(this);
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

    }
}
