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
        private static string whattotype;
        private static string whattotypeall;
        private static int whattotypeloc;
        private static bool whattotypecompleted;

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
            try
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
                    IE = new SHDocVw.InternetExplorer();
                    IE.Visible = true;
                    IntPtr x = (IntPtr)IE.HWND;
                    GCGCommon.SupportMethods.AdjustWindow(x, 0, 0, 800, 800);
                    //ShowWindow((IntPtr)IE.HWND, 3);
                    //IE.Navigate2(txtBaseURL.Text);
                }
                else if (Instruction == 2)
                {
                    IE.Navigate2(txtBaseURL.Text);
                    HandleInstruction(OK);
                }
                else if (Instruction == 3)
                {
                    //DoUI.SetForegroundWindowByHWND(IE.HWND);
                    //DoGCGDelay(30, true);
                    //DoUI.DoMouseClick(314, 158);
                    //DoGCGDelay(10, true);
                    //DoUI.DoMouseClick(390, 432);
                    IHTMLDocument2 x = GCGMethods.ConvertIEToIHTMLDocument2(IE, -1);
                    OK = GCGMethods.SimInput(x, GCGMethods.HTMLTagNames.Zbutton, GCGMethods.HTMLAttributes.ZOuterText, "Check now >", "");
                    DoGCGDelay(10, true);
                    HandleInstruction(OK);
                }
                else if (Instruction == 4)
                {
                    DoHandleTyper("{TAB}"+txtCardNumber.Text + "{TAB}" + txtCardPIN.Text + "{TAB}");
                    //OK = DoHandleCAPTCHARqRs("CaptchaView?"); //image?c= //kaptcha?
                    //HandleInstruction(OK);
                }
                else if (Instruction == 5)
                {
                    DoHandleTyper(txtCardNumber.Text + "{TAB}" + txtCardPIN.Text + "{TAB}{ENTER}");
                    //OK = DoHandleCAPTCHARqRs("CaptchaView?"); //image?c= //kaptcha?
                    //HandleInstruction(OK);
                }
                else if (Instruction == 999)
                {
                    DoUI.SetForegroundWindowByHWND(IE.HWND);
                    int frame = -1;
                    IHTMLDocument2 x = GCGMethods.ConvertIEToIHTMLDocument2(IE, frame);
                    OK = GCGMethods.ElementExists(x, GCGMethods.HTMLTagNames.Zinput, GCGMethods.HTMLAttributes.Zname, "giftCardNumber");
                    if (OK == "-1")
                    {
                        HandleInstruction(OK);
                        return;
                    }
                    OK = SetFocusOnElement("giftCardNumber", GCGMethods.ByNameOrID.Name, frame);
                    if (OK == "-1")
                    {
                        HandleInstruction(OK);
                        return;
                    }
                    DoHandleTyper(txtCardNumber.Text + "{TAB}" + txtCardPIN.Text + "{TAB}{ENTER}");
                    //int SubFrame = GCGMethods.FindWhatFrameItsIn(IE, "ctl00$BodyContent$tbCardNumber");
                    //IHTMLDocument2 FrameDoc = GCGMethods.ConvertIEToIHTMLDocument2(IE, SubFrame);
                    //OK = GCGMethods.SimInput(FrameDoc, GCGMethods.HTMLTagNames.Zinput, GCGMethods.HTMLAttributes.Zname, "ctl00$BodyContent$tbCardNumber", txtCardNumber.Text);
                    //if (OK != "-1") OK = GCGMethods.SimInput(FrameDoc, GCGMethods.HTMLTagNames.Zinput, GCGMethods.HTMLAttributes.Zname, "ctl00$BodyContent$ucCaptcha$tbHipAlphaNumeric", txtCAPTCHAAnswer.Text);
                    //if (OK != "-1") OK = GCGMethods.SimInput(FrameDoc, GCGMethods.HTMLTagNames.Zbutton, GCGMethods.HTMLAttributes.Zid, "ctl00_BodyContent_cont", "");
                    HandleInstruction(OK);
                }
                else
                {
                    OK = "-1";
                    string testi = "";
                    string testo = "";
                    string balanceResult = "";
                    IHTMLDocument2 IHTMLDocument2 = GCGMethods.ConvertIEToIHTMLDocument2(IE, -1);
                    string test = GCGMethods.GetHTMLFromIHTMLDocument2(IHTMLDocument2);
                    //string test = GCGMethods.GetHTML(FrameDoc);
                    try
                    {
                        //test = GCGMethods.GetPlainTextFromHTML(test);
                        testi = IHTMLDocument2.body.innerHTML;
                        testo = IHTMLDocument2.body.outerHTML;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return;
                    }
                    //GCGMethods.WriteFile("C:\\test.txt", test, true);
                    //GCGMethods.WriteFile("C:\\testi.txt", testi, true);
                    //GCGMethods.WriteFile("C:\\testo.txt", testo, true);
                    string p1 = "", p2 = "", p3 = "", p4 = "", p5 = "";
                    try
                    {
                        p1 = txtCardNumber.Text.Substring(19, 4);
                    }
                    catch (Exception ex)
                    {
                    }
                    testi = SupportMethods.RemoveNonVisibleChars(testi);
                    //GCGMethods.WriteFile("C:\\testi.txt", testi, true);
                    balanceResult = GetBalance("Balance$", "USD", testi);
                    if (balanceResult == "")
                    {
                        OK = "-1";
                        string VendErr1 = "The PIN is invalid.";
                        string VendErr2 = "Please check your numbers, we got back a 0.00 balance.";
                        string VendErr3 = "you have reached your request limit";
                        if (test.Contains(VendErr1))
                        {
                            txtCardBalance.Text = "VE1";
                            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr1, ad.RsPathAndFileToWrite);
                            OK = "1";
                        }
                        else if (test.Contains(VendErr2))
                        {
                            txtCardBalance.Text = "VE2";
                            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr1, ad.RsPathAndFileToWrite);
                            OK = "1";
                        }
                        else if (test.Contains(VendErr3))
                        {
                            txtCardBalance.Text = "VE3";
                            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), VendErr3, ad.RsPathAndFileToWrite);
                            OK = "1";
                        }
                        //if (OK == "-1") HandleInstruction(OK);  //TRY INST 5 AGAIN?
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
            catch (Exception ex)
            {
                string err = ex.Message;
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
            RetryCntMax = 7;
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
                if (t.IsDisposed == true)
                {
                    stop++;
                }
                if (t.Enabled == false)
                {
                    stop++;
                }
            } while (stop == 0);
            tmrRunning.Enabled = true;
            return OK;

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
                GCGMethods.WriteTextBoxLog(txtLog, "A CLIrqFile WASN'T loaded; using " + AppName + "_LastRun.txt");
                
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
            bool exit = true;
            if (AutoRun == "") { exit = false; }
            if (exit == true) { ApplicationExit(); }
        }
        private void ApplicationExit()
        {
            try
            {
                IE.Quit();
            }
            catch (Exception ex)
            {
            }
            Application.Exit();
            Environment.Exit(1);
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
            txtTicks.Text = DelayCnt.ToString();
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
                    GCGMethods.WriteTextBoxLog(txtLog, ad.NextRqPathAndFileToRead + Environment.NewLine + "Waiting for the file above...");
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
        private void tmrSendKeys_Tick(object sender, EventArgs e)
        {
            string test1 = "";
            string test2 = "";
            string whattotypeall = "";
            try
            {
                test1 = whattotype.Substring(whattotypeloc, 1);
            }
            catch (Exception ex)
            {
                whattotypecompleted = true;
                tmrSendKeys.Enabled = false;
                return;
            }
            if (test1 == "")
            {
                whattotypecompleted = true;
                tmrSendKeys.Enabled = false;
                return;
            }
            if (test1 == "{")
            {
                do
                {
                    whattotypeloc++;
                    test2 = whattotype.Substring(whattotypeloc, 1);
                    if (test2 == "}")
                    {
                        whattotypeall = "{" + whattotypeall + "}";
                        break;
                    }
                    else
                    {
                        whattotypeall = whattotypeall + test2;
                    }
                } while (true);
            }
            else
            {
                whattotypeall = test1;
            }

            //101-132 are uppercase
            string testchar = whattotypeall.Substring(0, 1);
            byte[] asciiBytes = Encoding.ASCII.GetBytes(testchar);
            int testcharval = asciiBytes[0];
            if ((testcharval > 64) && (testcharval < 91))
            {
                try
                {
                    SendKeys.Send("+" + whattotypeall);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    if (whattotypeall == "{BACKTAB}")
                    {
                        SendKeys.Send("+{TAB}");
                        tmrSendKeys.Interval = 100;
                    }
                    else
                    {
                        SendKeys.Send(whattotypeall);
                        if (whattotypeall == "{TAB}")
                        {
                            tmrSendKeys.Interval = 100;
                        }
                        else
                        {
                            tmrSendKeys.Interval = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            whattotypeall = "";
            whattotypeloc++;
            Random rnd = new Random();
            tmrSendKeys.Interval = rnd.Next(1, 500);
            tmrSendKeys.Interval = 1;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MSaveLastRun_Click(object sender, EventArgs e)
        {
            SaveLastRun();
        }

        private void MLoadLastRun_Click(object sender, EventArgs e)
        {
            LoadLastRun();
        }

        private void MOpenCAPTCHALocation_Click(object sender, EventArgs e)
        {
            GCGMethods.LaunchItWArg("explorer", txtCAPTCHAPath.Text);
        }
        private void MOpenRqRSLocation_Click(object sender, EventArgs e)
        {
            GCGMethods.LaunchItWArg("explorer", txtRqRsPath.Text);
        }


        private void MGoToBaseURLwIE_Click(object sender, EventArgs e)
        {
            GCGMethods.LaunchItWArg("iexplore", txtBaseURL.Text);
        }

        private void MGoToBaseURLwChrome_Click(object sender, EventArgs e)
        {
            GCGMethods.LaunchItWArg("chrome", txtBaseURL.Text);
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
                        if (pByNameOrID.ToString()=="Id")
                        {
                            parentWindow.execScript("document.getElementById('" + pElement + "').focus();", "javascript");
                        }
                        else
                        {
                            parentWindow.execScript("document.getElementsByName('" + pElement + "')[0].focus();", "javascript");
                        }
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
        private void SetForegroundWindowByName(string NameOfWindow)
        {
            Process[] allprocs = Process.GetProcesses();
            foreach (Process proc in allprocs)
            {
                System.Diagnostics.Debug.WriteLine(proc.MainWindowTitle);
                if (proc.MainWindowTitle.Contains(NameOfWindow))
                {
                    DoUI.SetForegroundWindowByHWND((int)proc.MainWindowHandle);
                    //return;
                }
            }
        }
        private void MouseMove(int pXPos, int pYPos)
        {
            DoUI.MouseMove(pXPos, pYPos);
        }

    }
}
