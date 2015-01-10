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
        int SpecificRetryCnt, SpecificRetryCntMax;
        int RetryCnt, RetryCntMax;
        int Instruction;
        int DelayAmnt,DelayCnt;
        int CopyPasteCnt,GetCAPTCHACnt;
        string CopyAndPasteResult, GetAndSaveCAPTCHAResult;

        string tmrResponseHandlerSt;
        string PathToWriteCAPTCHA;
        string CAPTCHAName;
        mshtml.IHTMLControlRange imgRange;
        private SHDocVw.WebBrowser_V1 Web_V1; //Interface to expose ActiveX methods
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
            if (Instruction == 1)
            {
                webBrowser1.Navigate(txtBaseURL.Text);
            }
            else if (Instruction == 2)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.area, HTMLEnumAttributes.OuterHtml, "*%https://wbiprod.storedvalue.com/WBI/lookupservlet?language=en&amp;brand_id=staples", "");
                HandleInstruction(OK);
            }
            else if (Instruction == 3)
            {
                OK = DoHandleRqRs(GCTypes.GCCAPTCHA, "WBServlet?jsessionid");
                HandleInstruction(OK);
            }
            else if (Instruction == 4)
            {
                OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "cardNoH", txtCardNumber.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "pinNoH", txtCardPIN.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.input, HTMLEnumAttributes.name, "inCaptchaChars", txtCAPTCHAAnswer.Text);
                if (OK != "-1") OK = GCGMethods.SimInput2(FrameDoc, HTMLEnumTagNames.a, HTMLEnumAttributes.InnerText, "Check Balance", "");
                HandleInstruction(OK);
            }
            else
            {
                //RetryCntMax = 5;
                string balanceResult = "";
                string test = GCGMethods.GetHTML(FrameDoc);
                test = GCGMethods.GetPlainTextFromHTML(test);
                //GCGMethods.WriteFile("C:\\GetBalance.txt", test, true);
                string l4 = "9999";
                try
                {
                    int l4l = txtCardNumber.Text.Length - 4;
                    l4 = txtCardNumber.Text.Substring(l4l, 4);
                }
                catch (Exception ex)
                {
                }
                balanceResult = GetBalance("balance:", "look", test);
                if (balanceResult == "")
                {
                    //SpecificRetryCnt++;
                    //SpecificRetryCntMax = 3;
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
        private void Web_V1_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            Processed = true; //Stop event from being processed

            //Code to open in same window
            this.webBrowser1.Navigate(URL);

            //Code to open in new window instead of same window
            
            //Main Popup = new Main();
            //Popup.webBrowser1.Navigate(URL);
            //Popup.Show();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);
            Web_V1 = (SHDocVw.WebBrowser_V1)this.webBrowser1.ActiveXInstance;
            Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(Web_V1_NewWindow);
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
            SpecificRetryCntMax = 999;
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

        private string DoGCGDelay(int pDelayAmnt)
        {
            DelayAmnt = pDelayAmnt;
            DelayCnt = 0;
            tmrRunning.Enabled = false;
            tmrDelay.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (tmrDelay.Enabled==true);
            return "1";
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            GCGMethods.WriteTextBoxLog(txtLog,"Waiting " +DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                GCGMethods.WriteTextBoxLog(txtLog, "Done Waiting, enabling tmrRunning");
                //tmrRunning.Enabled = true;
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
            tmrRunning.Enabled = true;  //I don't use this anymore, but pretty sure this would mess stuff up.
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
                    doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
                    //mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
                    //IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(1);
                    //doc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);

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
                        /*
                        System.Diagnostics.Debug.WriteLine(bmp.Width.ToString());
                        System.Diagnostics.Debug.WriteLine(bmp.Height.ToString());
                        GCGMethods.ForceForegroundWindow(this.Handle);                            //or SnagIt to get the positions
                        DoGCGDelay(5);
                        bmp = GCGMethods.CaptureRegionAsBMP(220, 753, 200, 40);
                        */
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
            retval =GetBalance(startTag, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", allText);
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

        private string SetFocus(string SearchForInOuterHtml, GCGCommon.HTMLEnumTagNames htmltype)
        {
            string retVal = "-1";
            SearchForInOuterHtml = SearchForInOuterHtml.ToUpper();
            try
            {
                string whatitscalled = SearchForInOuterHtml.ToUpper();
                string a=htmltype.ToString();
                //col = webBrowser1.Document.GetElementsByTagName(a);  //can be hr, or input, or whatever
                HTMLDocument doc1 = null;
                IHTMLDocument2 doc = null;
                mshtml.HTMLDocument htmlDoc = (HTMLDocument)webBrowser1.Document.DomDocument;
                IHTMLWindow2 htmlWindow = (IHTMLWindow2)htmlDoc.frames.item(1);
                doc = CrossFrameIE.GetDocumentFromWindow(htmlWindow);
                doc1 = (HTMLDocument)doc;
                IHTMLElementCollection inputs = doc1.getElementsByTagName(htmltype.ToString());
                IHTMLElement2 focusTo = null;
                foreach (IHTMLElement input in inputs)
                {
                    if (input.outerHTML.ToUpper().Contains(SearchForInOuterHtml))
                    {
                        focusTo=(IHTMLElement2)input;
                        focusTo.focus();
                        retVal = "1";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retVal;
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
            tmrRunning.Enabled = false;
            tmrSendKeys.Enabled = true;
            msg = message;
            GCGMethods.WriteTextBoxLog(txtLog, "SendKeys Start");
            do
	        {
                Application.DoEvents();
	        } while (msgcurrpos<msg.Length);
            GCGMethods.WriteTextBoxLog(txtLog, "SendKeys Done");
            tmrSendKeys.Enabled = false;
            tmrRunning.Enabled = true;
            System.Diagnostics.Debug.Write("DONE.");
            return "1";
        }
        private void tmrSendKeys_Tick(object sender, EventArgs e)
        {
            string test1 = "";
            string test2 = "";
            string all = "";
            try
            {
                test1 = msg.Substring(msgcurrpos, 1);
            }
            catch (Exception ex){}
            if (test1 == "") return;
            if (test1 == "{")
            {
                do
                {
                    msgcurrpos++;
                    test2 = msg.Substring(msgcurrpos, 1);
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
            GCGMethods.WriteTextBoxLog(txtLog, "SendKeys.Send " + all);
            SendKeys.Send(all);
                       
            msgcurrpos++;
        }
        private void cmdLogOut_Click(object sender, EventArgs e)
        {
            GCGMethods.WriteFile("C:\\LogOut.txt", txtLog.Text, true);
        }
        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            SupportMethods.WriteResponseFile(GCGCommon.GCTypes.GCCUSTOM.ToString(), "There's a problem with your data, the merchants reporting 'invalid card info'", ad.RsPathAndFileToWrite);
            Application.Exit(); 
            return;
        }
    }
}
