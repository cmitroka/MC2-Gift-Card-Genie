using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using HtmlAgilityPack;
using System.Runtime.InteropServices;

namespace GCGCommon
{
    public static class SupportMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hwnd, bool enabled);

        //Initialization
        private const uint SHOWWINDOW = 0x0040;

        public static bool AdjustWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight)
        {
            bool OK=MoveWindow(hWnd, X, Y, nWidth, nHeight,true);
            return OK;
        }
        public static void UnhideWindow()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "notepad";
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = Process.Start(info);
            p.WaitForInputIdle();
            IntPtr HWND = FindWindow(null, "Untitled - Notepad");

            System.Threading.Thread.Sleep(1000);

            ShowWindow(HWND, 5);
            EnableWindow(HWND, true);
        }
        public static void AdjustWindow(string NameOfProcess)
        {
            Process[] processes = Process.GetProcesses(".");
            foreach (var process in processes)
            {
                var handle = process.MainWindowHandle;
                Int64 ihandle = 0;
                try
                {
                    string shandle = handle.ToString();
                    ihandle = Convert.ToInt64(shandle);
                }
                catch (Exception ex)
                {
                }
                if (ihandle > 0)
                {
                    if (process.ProcessName == "devenv")
                    {
                        System.Diagnostics.Debug.WriteLine("1");
                    }
                    else if (process.ProcessName == "iexplore")
                    {
                        //process.CloseMainWindow();
                        System.Diagnostics.Debug.WriteLine("2");
                        //bool OK = MoveWindow(handle, 40, 40, 400, 400, false);
                        SetWindowPos(handle, 0, 0, 0, 0, 0, 0);
                        System.Diagnostics.Debug.WriteLine("1");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("3");
                        bool OK = MoveWindow(handle, 0, 0, 800, 400, true);
                    }
                }
            }
        }

        public static string GetWebUUID()
        {
            string tempFileName = "WR" + DateTime.Now.ToString("ssfff");
            return tempFileName;
        }
        public static string CreateAlphaNumericKey(int length)
        {
            Random r = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int rndInt;
            string retVal = "";
            char[] chars = new char[61];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            for (int i = 0; i < length; i++)
            {
                rndInt = r.Next(0, 62);
                char c = chars[rndInt];
                string s = c.ToString();
                sb.Append(s);
            }
            retVal = sb.ToString();
            //CJMUtilities.File.QuickWrite("C:\\out.txt", retVal);
            return retVal;
        }

        public static string ReturnTextOnlyFromHTML(string HTMLin)
        {
            string retVal = "";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLin);
            var text = doc.DocumentNode.SelectNodes("//body//text()").Select(node => node.InnerText);
            StringBuilder output = new StringBuilder();
            foreach (string line in text)
            {
                output.AppendLine(line);
            }
            retVal = output.ToString();
            return retVal;
            //string textOnly = HttpUtility.HtmlDecode(output.ToString());
        }
        public static string GetChecksum(string SessionID)
        {
            string retVal = "";
            try
            {
                string temp = "";
                int len = SessionID.Length;
                string tempi = SessionID.Substring(len - 4, 4);
                int temp2 = Convert.ToInt16(tempi);
                int temp3 = temp2 * 316;
                temp = temp3.ToString();
                len = temp.Length;
                temp = temp.Substring(len - 2, 2);
                retVal=temp;
            }
            catch (Exception)
            {
                retVal = "-1";
            }
            return retVal;
        }
        public static string EncUDID(string UDIDin)
        {
            UDIDin = "a19bc64";
            string c1 = UDIDin.Substring(0, 1);
            string c2 = UDIDin.Substring(1, 1);
            string c3 = UDIDin.Substring(2, 1);
            string c4 = UDIDin.Substring(3, 1);
            string c5 = UDIDin.Substring(4, 1);
            return "";
        }
        //needed for old stuff start
        public static void PurgeOldLogFile(string pLP)
        {
            try
            {
                System.IO.FileInfo f = new System.IO.FileInfo(pLP);
                f.Delete();
                f = null;
            }
            catch (Exception ex)
            {

            }
        }
        public static void LogStatus(string StatusMessage, string PathOfLogFile)
        {
            string tempFileName = "";
            System.IO.StreamWriter sw = null;
            try
            {
                System.IO.FileInfo f;
                string LogMsg = DateTime.Now.ToLongTimeString() + " - " + StatusMessage;
                string FinalLoc = PathOfLogFile;
                //string FinalLoc = Application.StartupPath + "\\" + AppName + "_runlog.txt";
                tempFileName = "C:\\" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                sw = new System.IO.StreamWriter(tempFileName);
                sw.WriteLine(LogMsg);
                f = new System.IO.FileInfo(FinalLoc);
                if (f.Exists == true)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(FinalLoc);
                    sw.Write(sr.ReadToEnd());
                    sr.Close();
                }
                sw.Close();
                f = new System.IO.FileInfo(tempFileName);
                f.CopyTo(FinalLoc, true);
                f.Delete();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    sw.Close();
                    System.IO.File.Delete(tempFileName);
                }
                catch (Exception ex2)
                {

                }
            }
        }
        //needed for old stuff end
        
        public static string GetAnswerFromFile(string PathToCLIFile)
        {
            string retVal = "";
            System.IO.StreamReader s = null;
            try
            {
                s = new System.IO.StreamReader(PathToCLIFile);
                retVal = s.ReadLine();
                //System.Windows.Forms.MessageBox.Show("retVal=" + retVal);
                s.Close();
            }
            catch (Exception ex)
            {
                retVal = GetAnswerFromFile(PathToCLIFile);
                //retVal = "GetAnswerFromFileError";
            }
            return retVal;
        }
        public static string WriteResponseFile(string AppName, string rsType, string rsValue, string PathOfFileToBuild)
        {
            string retVal = "1";
            System.IO.StreamWriter s = null;
            try
            {
                retVal = TrimQuotesFromBandE(PathOfFileToBuild);
                s = new System.IO.StreamWriter(retVal);
                s.WriteLine(rsType);
                s.WriteLine(rsValue);
                string message="Response file <"+PathOfFileToBuild+"> written with rsType="+rsType+"; rsValue="+rsValue;
                if (AppName != "") WriteToWindowsEventLog(AppName, message, "I");
                s.Close();
            }
            catch (Exception ex)
            {
                retVal = "WriteResponseFile(string rsType, string rsValue) Error - " + ex.Message;
            }
            return retVal;
        }
        public static string WriteResponseFile(string rsType, string rsValue, string PathOfFileToBuild)
        {
            string retVal = "1";
            retVal = WriteResponseFile("", rsType, rsValue, PathOfFileToBuild);
            return retVal;
        }
        public static void WriteToWindowsEventLog(string AppName, string WhatToLog, string WorI)
        {
            if (WorI.ToUpper() == "W")
            {
                System.Diagnostics.EventLog.WriteEntry("GC-" + AppName, AppName, System.Diagnostics.EventLogEntryType.Warning, 234);
            }
            else
            {
                System.Diagnostics.EventLog.WriteEntry("GC-" + AppName, AppName);
            }
        }
        public static string TrimQuotesFromBandE(string lineIn)
        {
            string retVal = "";
            int cntr = 0;
            try
            {
                string bChar = lineIn.Substring(0, 1);
                if (bChar == @"""")
                {
                    retVal = lineIn.Substring(1, lineIn.Length - 1);
                    cntr++;
                }
                string eChar = lineIn.Substring(lineIn.Length - 1, 1);
                if (eChar == @"""")
                {
                    retVal = retVal.Substring(0, retVal.Length - 1);
                    cntr++;
                }
            }
            catch (Exception ex)
            {
            }

            if (cntr != 2)
            {
                retVal = lineIn;
            }
            return retVal;
        }
        public static string[] GetFilePathPieces(string fileLocation)
        {
            string[] retme = new string[5];
            try
            {
                retme[0] = System.IO.Path.GetFileName(fileLocation);
            }
            catch (Exception ex0) { }
            try
            {
                retme[1] = System.IO.Path.GetDirectoryName(fileLocation);
            }
            catch (Exception ex1) { }
            try
            {
                retme[2] = System.IO.Path.GetExtension(fileLocation);
            }
            catch (Exception ex2) { }
            try
            {
                retme[3] = System.IO.Path.GetFileNameWithoutExtension(fileLocation);
            }
            catch (Exception ex3) { }
            try
            {
                retme[4] = System.IO.Path.GetPathRoot(fileLocation);
            }
            catch (Exception ex4) { }
            return retme;
        }
        public static string[] SplitByString(this string OriginalText, string ParseWith)
        {
            return OriginalText.Split(new string[] { ParseWith }, StringSplitOptions.None);
        }
        public static string[] ParseArguments(string commandLine)
        {
            char[] parmChars = commandLine.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }
            return (new string(parmChars)).Split('\n');
        }
        public static string RetSessionVal(string WhatToGet)
        {
            string retVal = "";
            GCGCommon.SessionTool s = new SessionTool();
            retVal = s.RetSessionVal(WhatToGet);
            return retVal;
        }
        public static string LaunchIt(string exe, string args, bool WaitForExit, bool LaunchInvisible)
        {
            string retVal = "";
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = exe;
                if (args == null) args = "";
                if (args.Length > 0)
                {
                    p.StartInfo.Arguments = args;
                }
                if (LaunchInvisible == true)
                {
                    p.StartInfo.CreateNoWindow = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                else
                {
                    p.StartInfo.CreateNoWindow = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                }
                Console.WriteLine(p.StartInfo.FileName);
                bool isStarted = p.Start();
                if (isStarted == false) { return exe + " couldn't start"; }
                if (WaitForExit == true)
                {
                    p.WaitForExit();
                }
                retVal = "1";
            }
            catch (Exception ex)
            {
                retVal = "-1";
                return ex.Message;
            }
            return retVal;
        }
        public static string LaunchIt(string exe, string args)
        {
            string retVal = "";
            retVal = LaunchIt(exe, args, false,false);
            return retVal;            
        }
        public static string LaunchIt(string EXE)
        {
            string retVal = "0";
            LaunchIt(EXE, "");
            return "";
        }

        public static int IsRunning(string pProcessName)
        {
            int processcount = 0;
            Process[] pname = Process.GetProcessesByName(pProcessName);
            foreach (Process theprocess in pname)
            {
                if (theprocess.ProcessName == pProcessName)
                {
                    processcount++;
                }
            }
            return processcount;
        }
        public static void KillProcess(string NameOfProcess)
        {
            //Tools | Internet Options | Advanced | Browsing | Enable automatice crash recovery*
            System.Diagnostics.Process[] Ps = System.Diagnostics.Process.GetProcessesByName(NameOfProcess);
            foreach (System.Diagnostics.Process process in Ps)
            {
                process.Kill();
            }
        }
        public static string CreateHexKey(int length)
        {
            Random r = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int rndInt;
            string retVal = "";
            //char[] chars = new char[61];
            //chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            char[] chars = new char[15];
            chars = "ABCDEF1234567890".ToCharArray();
            for (int i = 0; i < length; i++)
            {
                rndInt = r.Next(0, 15);
                char c = chars[rndInt];
                string s = c.ToString();
                sb.Append(s);
            }
            retVal = sb.ToString();
            //CJMUtilities.File.QuickWrite("C:\\out.txt", retVal);
            return retVal;
        }
        public static bool WriteFile(string TextFileLocation, string WhatToWrite, bool OpenAfterwards)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(TextFileLocation);
                sw.WriteLine(WhatToWrite);
                sw.Close();
                if (OpenAfterwards == true)
                {
                    System.Diagnostics.Process notePad = new System.Diagnostics.Process();
                    notePad.StartInfo.FileName = "notepad.exe";
                    notePad.StartInfo.Arguments = TextFileLocation;
                    notePad.Start();
                }
                return true;
            }
            catch (Exception) { return false; }
        }

    }
}
