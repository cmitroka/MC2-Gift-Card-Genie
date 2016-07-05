using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using WindowsInput;

namespace AutomationViaExternalApp
{
    public partial class Main : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        public class Win32
        {
            [DllImport("User32.Dll")]
            public static extern long SetCursorPos(int x, int y);

            [DllImport("User32.Dll")]
            public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;






        private enum GCTypes { GCBALANCE, GCBALANCEERR, GCNEEDSMOREINFO, GCTIMEOUT, GCCAPTCHA, GCERR, GCCUSTOM };
        string CLIrqFile, RsPathAndFileToWrite, AppName, AutoRun, RqOrRsType, whattotype;
        int DelayCnt, DelayAmnt, RetryCnt, RetryCntMax, SpecificRetryCnt, SecondsPassed, RqOrRsCnt, Instruction, whattotypeloc;
        bool whattotypecompleted;
        IntPtr MainWndHndl;
        object Whatever;
        public Main()
        {
            InitializeComponent();
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
        private void ProcessInstructions()
        {
            try
            {
                txtRetryCntr.Text = RetryCnt.ToString();
                string OK = "1";
                Instruction++;
                WriteTextBoxLog("Instruction " + Instruction.ToString() + " Running");
                if (RetryCnt >= RetryCntMax)
                {
                    txtResponse.Text = "N/A";
                    WriteResponseFile(GCTypes.GCERR.ToString(), "The retry count went above " + RetryCntMax, RsPathAndFileToWrite);
                    tmrRunning.Enabled = false;
                    tmrTimeout.Enabled = false;
                    MaybeExitApp();
                    return;
                }
                if (Instruction == 1)
                {
                    OK = AppLaunch();
                    if (OK=="1")
                    {
                        CJMFunctions.AdjustWindow(MainWndHndl, 0, 0, 800, 800);
                    }
                    HandleInstruction(OK);
                }
                else if (Instruction == 2)
                {
                    DoGCGDelay(80,true);
                    HandleInstruction(OK);
                }
                else if (Instruction == 3)
                {
                    SetForegroundWindowByHWND((int)MainWndHndl);
                    MouseMove(525, 387);
                    MouseClick();
                    DoHandleTyper(txtCardNumber.Text);
                    MouseMove(336, 465);
                    MouseClick();
                    DoGCGDelay(40, true);
                    MouseMove(465, 547);
                    MouseClick();
                    DoGCGDelay(40, true);
                    //InputSimulator.SimulateTextEntry(txtCardNumber.Text);
                    //AppKill();
                    //ApplicationExit();
                    HandleInstruction(OK);
                }
                else
                {
                    OK = "-1";
                    DoKeyCombos("CopyEverything");
                    string TextResponse = "";
                    string balanceResult = "";
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return;
                    }
                    string PlainTextResponse = Clipboard.GetText();
                    //GCGMethods.WriteFile("C:\\testo.txt", PlainTextResponse, true);

                    balanceResult = GetBalance(" is ", "Check", PlainTextResponse);
                    if (balanceResult == "")
                    {
                        SpecificRetryCnt++;
                        WriteTextBoxLog("SpecificRetryCnt: " + SpecificRetryCnt.ToString());
                        if (SpecificRetryCnt >= 10)
                        {
                            txtResponse.Text = "Error";
                            WriteResponseFile(GCTypes.GCCUSTOM.ToString(), "Sorry, we couldn't get the balance for some reason.", RsPathAndFileToWrite);
                            OK = "1";
                        }
                    }
                    else
                    {
                        txtResponse.Text = balanceResult;
                        WriteResponseFile(GCTypes.GCBALANCE.ToString(), balanceResult, RsPathAndFileToWrite);
                        OK = "1";
                        DoKeyCombos("CloseWindow");
                    }
                    if (OK == "1")
                    {
                        if (CLIrqFile == "") File.Delete(RsPathAndFileToWrite);
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
                WriteTextBoxLog("RetryCnt probably exceeded: " + ex.Message);
            }
        }
        private string AppKill()
        {
            string retVal="-1";
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            foreach (Process chrome in procsChrome)
            {
                // the chrome process must have a window
                if (chrome.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                else
                {
                    chrome.Kill();
                    retVal = "1";
                }
            }
            return retVal;
        }
        private string AppLaunch()
        {
            string retVal;
            string pURL=txtBaseURL.Text;
            //pURL = "http://www.msn.com";
            using (var process = new Process())
            {
                process.StartInfo.FileName = txtChromeLoc.Text;
                process.StartInfo.Arguments = "/new-window " + pURL;
                var proc=process.Start();
                try
                {
                    while (process.MainWindowHandle == IntPtr.Zero)
                    {
                        // Discard cached information about the process
                        // because MainWindowHandle might be cached.
                        process.Refresh();
                        Thread.Sleep(10);
                    }
                    MainWndHndl = process.MainWindowHandle;
                    retVal = "1";
                }
                catch
                {
                    retVal = "-1";
                    // The process has probably exited,
                    // so accessing MainWindowHandle threw an exception
                }


                Process[] procsChrome = Process.GetProcessesByName("chrome");
                foreach (Process chrome in procsChrome) {
                  // the chrome process must have a window
                    if (chrome.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }
                    else
                    {
                        retVal = "1";
                        MainWndHndl = chrome.MainWindowHandle;
                    }
                }
                return retVal;
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
        private void LoadSettingsFromDB()
        {
            try
            {
                CJMRegistry MR = new CJMRegistry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                CJMAccessDB db = new CJMAccessDB(txtAppStaticDBPath.Text);
                string[][] setting = db.GetMultiValuesOfSQL("SELECT URL,TestCardNum, TestCardPIN, TestLogin, TestPass FROM tblMerchants WHERE EXE='" + AppName + "'");
                txtBaseURL.Text = setting[0][0];
                txtCardNumber.Text = setting[0][1];
                txtCardPIN.Text = setting[0][2];
                txtLogin.Text = setting[0][3];
                txtPassword.Text = setting[0][4];
            }
            catch (Exception ex2)
            {
                WriteTextBoxLog("LoadSettingsFromDB error " + ex2.Message);
            }
        }
        private void LoadSettingsFromRegistry()
        {
            try
            {
                CJMRegistry MR = new CJMRegistry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
                txtChromeLoc.Text = MR.Read("CHROMEPATH");
                txtRqRsPath.Text = MR.Read("RQRSPATH");
            }
            catch (Exception ex2)
            {
                WriteTextBoxLog("LoadSettingsFromRegistry error " + ex2.Message);
            }
            if (txtAppStaticDBPath.Text == "") { WriteTextBoxLog("Missing SOFTWARE\\GCG Apps\\GC-Common\\APPSTATICDBPATH"); }
            if (txtChromeLoc.Text == "") { WriteTextBoxLog("Missing SOFTWARE\\GCG Apps\\GC-Common\\CHROMEPATH"); }
            if (txtRqRsPath.Text == "") { WriteTextBoxLog("Missing SOFTWARE\\GCG Apps\\GC-Common\\RQRSPATH"); }
        }

        private void WriteTextBoxLog(string topmessage)
        {
            string temp = txtLog.Text;
            string newmsg = topmessage + "\r\n" + txtLog.Text;
            txtLog.Text = newmsg;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Location = new Point(800, 0);
            this.Size = new Size(528, 438);
            AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            this.Text = "Balance Extractor - " + AppName;
            RetryCntMax = 45;
            LoadSettingsFromRegistry();
            LoadSettingsFromDB();
            LoadCLIrqFile();
            RsPathAndFileToWrite = CLIrqFile;
            txtAutorun.Text = AutoRun;
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
                WriteTextBoxLog("Attempting to load: " + CLIrqFile);
                StreamReader s = new StreamReader(CLIrqFile);
                string CardType = s.ReadLine();
                txtCardNumber.Text = s.ReadLine();
                txtCardPIN.Text = s.ReadLine();
                txtLogin.Text = s.ReadLine();
                txtPassword.Text = s.ReadLine();
                //txtLogin.Text = "temp1@mc2techservices.com";
                //txtPassword.Text = "Temppass1";
                s.Close();
                RqOrRsCnt = 0;
            }
            catch (Exception e)
            {
                faulted = true;
                //string eMessage = e.Message.Replace("\r\n", " --- ");
                //retVal = "LoadCLIrqFile() Error - " + eMessage;
                retVal = "No CLI Request File passed in; using test values from database.";
                CLIrqFile = txtRqRsPath.Text + "\\test-0rq-" + AppName + ".txt";
                CLIrqFile = CLIrqFile.Replace("\\\\", "\\");
                WriteTextBoxLog(retVal);
                retVal = "-1";
            }
            return retVal;
        }
        private string WriteResponseFile(string rsType, string rsValue, string PathOfFileToBuild)
        {
            string retVal = "1";
            RsPathAndFileToWrite = CLIrqFile.Replace("rq", "rs");
            System.IO.StreamWriter s = null;
            try
            {
                WriteTextBoxLog("Writing: " + RsPathAndFileToWrite);
                s = new System.IO.StreamWriter(RsPathAndFileToWrite);
                s.WriteLine(rsType);
                s.WriteLine(rsValue);
                //string message = "Response file <" + PathOfFileToBuild + "> written with rsType=" + rsType + "; rsValue=" + rsValue;
                //if (AppName != "") WriteToWindowsEventLog(AppName, message, "I");
                s.Close();
                retVal = "1";
            }
            catch (Exception ex)
            {
                retVal = "Error Writing - " + ex.Message;
                WriteTextBoxLog(retVal);
                retVal = "-1";
            }
            return retVal;
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
            RoughBalance = CJMFunctions.RoughExtract(startTag, endTag, allText);
            retval =  CJMFunctions.ReturnStandardBalance(RoughBalance);
            return retval;
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

        private void MaybeExitApp()
        {
            //if (chkNeverAutoExit.Checked == true) { return; }
            bool exit = true;
            if (AutoRun == "") { exit = false; }
            if (exit == true) { ApplicationExit(); }
        }
        private void ApplicationExit()
        {
            //Whatever.Quit
            SetForegroundWindowByHWND((int)MainWndHndl);
            DoKeyCombos("CloseWindow");
            Application.Exit();
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
            if (SecondsPassed >= 60)
            {
                txtResponse.Text = "Timeout";
                WriteResponseFile(GCTypes.GCTIMEOUT.ToString(), "Timeout", RsPathAndFileToWrite);
                tmrTimeout.Enabled = false;
                MaybeExitApp();
            }
        }
        private void tmrDelay_Tick(object sender, EventArgs e)
        {
            DelayCnt++;
            WriteTextBoxLog("Waiting " + DelayCnt.ToString() + " of " + DelayAmnt.ToString());
            if (DelayCnt >= DelayAmnt)
            {
                tmrDelay.Enabled = false;
                WriteTextBoxLog("Done Waiting");
            }
        }

        private void cmdRunRequest_Click(object sender, EventArgs e)
        {
            Instruction = 0;
            RetryCnt = 0;
            SpecificRetryCnt = 0;
            txtResponse.Text = "";
            tmrTimeout.Enabled = true;
            tmrRunning.Enabled = true;
        }

        private void cmdForceExit_Click(object sender, EventArgs e)
        {
            WriteResponseFile(GCTypes.GCCUSTOM.ToString(), "Forced to Exit", RsPathAndFileToWrite);
            DoKeyCombos("CloseWindow");
            ApplicationExit();
            return;
        }
        private void MouseMove(int pXPos, int pYPos)
        {
            /*
            Win32.POINT p = new Win32.POINT();
            p.x = Convert.ToInt16(pXPos);
            p.y = Convert.ToInt16(pYPos);
            Win32.ClientToScreen(this.Handle, ref p);
            Win32.SetCursorPos(p.x, p.y);
            */
            SetCursorPos(pXPos, pYPos);
        }
        private void MouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        private void SetForegroundWindowByHWND(int hWnd)
        {
            IntPtr x = (IntPtr)hWnd;
            //check if already has focus
            if (x == GetForegroundWindow()) return;
            //check if window is minimized
            if (IsIconic(x))
            {
                ShowWindow(x, Restore);
            }

            // Simulate a key press
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | 0, 0);
            //SetForegroundWindow(x);
            // Simulate a key release
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);
            SetForegroundWindow(x);
        }
        private void SetForegroundWindowByName(string NameOfWindow)
        {
            Process[] allprocs = Process.GetProcesses();
            foreach (Process proc in allprocs)
            {
                System.Diagnostics.Debug.WriteLine(proc.MainWindowTitle);
                if (proc.MainWindowTitle.Contains(NameOfWindow))
                {
                    SetForegroundWindowByHWND((int)proc.MainWindowHandle);
                    //return;
                }
            }
        }
        private string DoHandleTyper(string whatToType)
        {
            string OK = "1";
            whattotype = whatToType;
            tmrRunning.Enabled = false;
            tmrSendKeys.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (whattotypecompleted == false);
            tmrSendKeys.Enabled = false;
            tmrRunning.Enabled = true;
            return OK;
        }

        private void ChromeProcessViaURL()
        {
            // there are always multiple chrome processes, so we have to loop through all of them to find the
            // process with a Window Handle and an automation element of name "Address and search bar"
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            foreach (Process chrome in procsChrome)
            {
                // the chrome process must have a window
                if (chrome.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                // find the automation element
                AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);

                // manually walk through the tree, searching using TreeScope.Descendants is too slow (even if it's more reliable)
                AutomationElement elmUrlBar = null;
                try
                {
                    // walking path found using inspect.exe (Windows SDK) for Chrome 31.0.1650.63 m (currently the latest stable)
                    var elm1 = elm.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Google Chrome"));
                    if (elm1 == null) { continue; } // not the right chrome.exe
                    // here, you can optionally check if Incognito is enabled:
                    //bool bIncognito = TreeWalker.RawViewWalker.GetFirstChild(TreeWalker.RawViewWalker.GetFirstChild(elm1)) != null;
                    var elm2 = TreeWalker.RawViewWalker.GetLastChild(elm1); // I don't know a Condition for this for finding :(
                    var elm3 = elm2.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, ""));
                    var elm4 = elm3.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar));
                    elmUrlBar = elm4.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));
                }
                catch
                {
                    // Chrome has probably changed something, and above walking needs to be modified. :(
                    // put an assertion here or something to make sure you don't miss it
                    continue;
                }

                // make sure it's valid
                if (elmUrlBar == null)
                {
                    // it's not..
                    continue;
                }

                // elmUrlBar is now the URL bar element. we have to make sure that it's out of keyboard focus if we want to get a valid URL
                if ((bool)elmUrlBar.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
                {
                    continue;
                }

                // there might not be a valid pattern to use, so we have to make sure we have one
                AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                if (patterns.Length == 1)
                {
                    string ret = "";
                    try
                    {
                        ret = ((ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0])).Current.Value;
                    }
                    catch { }
                    if (ret != "")
                    {
                        // must match a domain name (and possibly "https://" in front)
                        if (Regex.IsMatch(ret, @"^(https:\/\/)?[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,4}).*$"))
                        {
                            // prepend http:// to the url, because Chrome hides it if it's not SSL
                            if (!ret.StartsWith("http"))
                            {
                                ret = "http://" + ret;
                            }
                            Console.WriteLine("Open Chrome URL found: '" + ret + "'");
                        }
                    }
                    continue;
                }
            }
        }
 
        private void DoKeyCombos(string Combo)
        {
            //
            //DoKeyCombos("CloseWindow")
            //DoKeyCombos("CopyEverything")
            if (Combo == "CopyEverything")
            {
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                DoGCGDelay(3, true);
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_C);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
            }
            if (Combo == "CloseWindow")
            {
                InputSimulator.SimulateKeyDown(VirtualKeyCode.MENU);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.F4);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.MENU);
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

                
            /*    
            //SHIFT=+, CTRL=^, ALT=%
            else if (test1 == "+")
            {
                InputSimulator.SimulateKeyDown(VirtualKeyCode.SHIFT);
            }
            else if (test1 == "^")
            {
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
            }
            else if (test1 == "%")
            {
                //InputSimulator.SimulateKeyDown(VirtualKeyCode.);
            }
            */
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
            //SHIFT=+, CTRL=^, ALT=%
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
        }

    }
}
