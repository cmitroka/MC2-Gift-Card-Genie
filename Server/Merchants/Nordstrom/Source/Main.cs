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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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
        SHDocVw.InternetExplorer IE;
        public static IHTMLDocument2 FrameDoc;
        int IEQuit;
        private void ProcessInstructions()
        {
            //WebProxy proxy = WebProxy.GetDefaultProxy();
            txtRetryCntr.Text = RetryCnt.ToString();
            string OK = "1";
            Instruction++;
            //IHTMLDocument2 FrameDoc = null;
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
                IE = new SHDocVw.InternetExplorer();
                IE.Visible = true;
                //ShowWindow((IntPtr)IE.HWND, 3);
                IE.Navigate2(txtBaseURL.Text);
            }
            else if (Instruction == 2)
            {
                DoGCGDelay(10, true);
            }
            else if (Instruction == 3)
            {
                //OK = WebpageLib00.CAPTCHAGetImage(IE, "WBServlet?jsessionid", ad.CAPTCHAPathAndFileToWrite);
                //if (OK == "1") OK = DoHandleCAPTCHARqRs();
                //HandleInstruction(OK);
            }
            else if (Instruction == 4)
            {
                OK = WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zinput, WebpageLib00.UsingIdentifier.Zname, WebpageLib00.ComparisonType.Zexact, "ctl00$mainContentPlaceHolder$txtGiftCardNumber", txtCardNumber.Text, 1);
                Typer t = new Typer();
                t.switchWindow(IE.HWND);
                OK = WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zinput, WebpageLib00.UsingIdentifier.Zname, WebpageLib00.ComparisonType.Zexact, "ctl00$mainContentPlaceHolder$txtGiftCardNumber", "focus", 1);
                t.TypeIt("{TAB}"+txtCardPIN.Text);
                //if (OK != "-1") WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zinput, WebpageLib00.UsingIdentifier.Zid, WebpageLib00.ComparisonType.Zexact, "ctl00_mainContentPlaceHolder_txtAccessNumber_giftCardTextBox1", txtCardPIN.Text, 1);
                if (OK != "-1") WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zimg, WebpageLib00.UsingIdentifier.Zsrc, WebpageLib00.ComparisonType.Zcontains, "button_check-balance.gif", "", 1);
                OK = "1";
                HandleInstruction(OK);

                //OK = WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zselect, WebpageLib00.UsingIdentifier.Zname, WebpageLib00.ComparisonType.Zexact, "ctl00$ContentPlaceHolderBodyContent$ddlCardNumberLength", "focus", 1);
                //if (OK != "-1") WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zselect, WebpageLib00.UsingIdentifier.Zname, WebpageLib00.ComparisonType.Zexact, "ctl00$ContentPlaceHolderBodyContent$ddlCardNumberLength", "Digits16", 1);
                //t.switchWindow(IE.HWND);
                //t.TypeIt(txtCardNumber.Text + "{TAB} ");
                //if (OK != "-1") WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zinput, WebpageLib00.UsingIdentifier.Zid, WebpageLib00.ComparisonType.Zexact, "submitBalanceButton", "", 1);
                //if (OK != "-1") WebpageLib00.ElemFindAndAct(IE, WebpageLib00.WhatIsIt.Zinput, WebpageLib00.UsingIdentifier.Zvalue, WebpageLib00.ComparisonType.Zexact, "Check Balance", "", 1);
                //OK = "-1";
            }
            else
            {
                OK = "-1";
                string testi = "";
                string testo = "";
                string balanceResult = "";
                mshtml.HTMLDocument FrameDoc = WebpageLib00.ConvertIEtoHTMLDocument(IE);
                string test = GCGMethods.GetHTMLFromHTMLDocument(FrameDoc);
                //string test = GCGMethods.GetHTML(FrameDoc);
                test = GCGMethods.GetPlainTextFromHTML(test);
                //GCGMethods.WriteFile("C:\\test.txt", test, true);
                try
                {
                    testi = FrameDoc.body.innerHTML;
                    testo = FrameDoc.body.outerHTML;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return;
                }
                System.Diagnostics.Debug.WriteLine("OK");
                //GCGMethods.WriteFile("C:\\testi.txt", testi, true);
                //GCGMethods.WriteFile("C:\\testo.txt", testo, true);
                //InspectIt(test, "has a balance ");
                balanceResult = GetBalance("_balanceInfoAmount\" style=\"font-size: small; font-weight: bold;\">", ".</span>", testo);
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
                    else if (test.Contains("Invalid information entered"))
                    {
                        txtCardBalance.Text = "Inv";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Invalid information entered.", ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                    else if (test.Contains("We did not recognize "))
                    {
                        txtCardBalance.Text = "InvCN";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Invalid Card # or PIN", ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                    /*
                    else if (test.Contains("The code you entered is invalid"))
                    {
                        txtCardBalance.Text = "InvCAPTCHA";
                        SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "Sorry, Invalid Card or PIN", ad.RsPathAndFileToWrite);
                        OK = "1";
                    }
                     */
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
            this.Size = new Size(980, 250);
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            this.Text = "Balance Extractor - " + AppName;
            SpecificRetryCntMax = 999;
            RetryCntMax = 30;
            SaveLoad.LoadSettingsFromRegistry(this);
            SaveLoad.LoadSettingsFromDB(this);
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
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T loaded; simulating with webservice data.");
            }
            ad = new GCGCommon.AllDetails(CLIrqFile, txtCAPTCHAPath.Text);
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

        public string DoGCGDelay(int pDelayAmnt, bool ResumetmrRunning)
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
                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA grabbing doc");
                    mshtml.IHTMLDocument2 document2 = IE.Document as mshtml.IHTMLDocument2;
                    mshtml.HTMLDocument document = IE.Document as mshtml.HTMLDocument;

                    //if (!document.activeElement.isTextEdit)
                    //{
                    //    MessageBox.Show("Active element is not a text-input system");
                    //}

                    IHTMLDocument2 doc2 = null;
                    IHTMLWindow2 htmlWindow2 = null;
                    if (CAPTCHAFrame == -1)
                    {
                        doc2 = document2;
                        //doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
                    }
                    else
                    {
                        htmlWindow2 = (IHTMLWindow2)document.frames.item(CAPTCHAFrame);
                        doc2 = CrossFrameIE.GetDocumentFromWindow(htmlWindow2);
                    }

                    GCGMethods.WriteTextBoxLog(txtLog, "SaveCAPTCHA making imgRange");
                    imgRange = (mshtml.IHTMLControlRange)((mshtml.HTMLBody)doc2.body).createControlRange();
                    foreach (mshtml.IHTMLImgElement img in doc2.images)
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
            string retVal = "-1";
            IE = new SHDocVw.InternetExplorer();
            IE.Visible = true;
            do
            {
                try
                {
                    DoGCGDelay(10, false);
                    IE.Navigate2(txtBaseURL.Text);
                    retVal = "1";
                }
                catch (Exception ex)
                {

                }
                Application.DoEvents();
            } while (retVal == "-1");
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

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            com.mc2techservices.gcg.WebService WS = new com.mc2techservices.gcg.WebService();
            WebProxy proxy = WebProxy.GetDefaultProxy();
            WS.Proxy = proxy;
            WS.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            WS.SaveWDDataForEXE01(AppName, txtCleanName.Text, txtBaseURL.Text, txtCardNumber.Text, txtCardPIN.Text, txtLogin.Text, txtPassword.Text, cmbSupportCode.Text, txtTimeout.Text, "CJMGCG");
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
        private void InspectIt(string test, string contains)
        {
            if (test.Contains(contains))
            {
                GCGMethods.WriteFile("C:\\test.txt", test, true);
            }
        }
        public void TypeIt(string whatToType)
        {
            string test1 = "";
            string test2 = "";
            string all = "";
            bool stop = false;
            int msgcurrpos = 0;
            do
            {
                try
                {
                    test1 = whatToType.Substring(msgcurrpos, 1);
                }
                catch (Exception ex)
                {
                    stop = true;
                    break;
                }
                if (test1 == "C")
                {
                    System.Diagnostics.Debug.WriteLine(test1);
                }
                if (test1 == "") return;
                if (test1 == "{")
                {
                    do
                    {
                        msgcurrpos++;
                        test2 = whatToType.Substring(msgcurrpos, 1);
                        if (test2 == "}")
                        {
                            all = "{" + all + "}";
                            break;
                        }
                        else
                        {
                            all = all + test2;
                        }
                    } while (true);
                }
                else
                {
                    all = test1;
                }

                //101-132 are uppercase
                string testchar = all.Substring(0, 1);
                byte[] asciiBytes = Encoding.ASCII.GetBytes(testchar);
                int testcharval = asciiBytes[0];
                if ((testcharval > 64) && (testcharval < 91))
                {
                    SendKeys.Send("+" + all);
                }
                else
                {
                    SendKeys.Send(all);
                }
                all = "";
                msgcurrpos++;
            } while (stop == false);
            all = "";
        }

        private string GrabCorrectPageOrFrame(string WhatToLookFor)
        {
            string OK;
            int SubFrame = GCGMethods.FindWhatFrameItsIn(IE, WhatToLookFor);
            if (SubFrame == -1)
            {
                System.Diagnostics.Debug.WriteLine("It's in the main page");
                SubFrame = 0;
                FrameDoc = GCGMethods.ConvertIEToIHTMLDocument(IE, SubFrame);
            }
            else if (SubFrame >= 0)
            {
                System.Diagnostics.Debug.WriteLine("It's in a frame");
                FrameDoc = GCGMethods.ConvertIEToIHTMLDocument(IE, SubFrame);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("SubFrame: " + SubFrame.ToString());
            }
            if (FrameDoc == null)
            {
                OK = "-1";
            }
            else
            {
                OK = "1";
            }
            return OK;
        }
    }
}
