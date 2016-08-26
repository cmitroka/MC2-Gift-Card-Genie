using System;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Automation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using mshtml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices;

namespace DVB
{
    public class DoUI
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
        public static void SetForegroundWindowByHWND(int hWnd)
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
            // Simulate a key release
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);
            SetForegroundWindow(x);
        }
        public static void MouseMove(int pXPos, int pYPos)
        {
            SetCursorPos(pXPos, pYPos);
        }
        public static void MouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        public static string ValidateBytesSize2(int X, int Y, int W, int H)
        {
            string OK = "-1";
            Bitmap bmp = null;
            bmp = GCGMethods.CaptureRegionAsBMP(X, Y, W, H);
            using (var ms = new MemoryStream()) // estimatedLength can be original fileLength
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // save image to stream in Jpeg format    
                OK = ms.Length.ToString();
            }
            return OK;

        }
        public static string ValidateBytesSize(int X, int Y, int W, int H, int SizeShouldBe, int WiggleRoom)
        {
            string OK = "-1";
            Bitmap bmp = null;
            bmp = GCGMethods.CaptureRegionAsBMP(X, Y, W, H);
            using (var ms = new MemoryStream()) // estimatedLength can be original fileLength
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // save image to stream in Jpeg format    
                string testVal = ms.Length.ToString();
                int exacttestval= Convert.ToInt16(testVal);
                //exacttestval = 1146;
                int testvalhigh = exacttestval + WiggleRoom;
                int testlowval = exacttestval - WiggleRoom;
                Console.WriteLine(testVal);
                if ((SizeShouldBe<= testvalhigh) && (SizeShouldBe >= testlowval))
                {
                    OK = "1";
                    return OK;
                }
            }
            return OK;
        }
        public static string ValidateBytesSize(string X, string Y, string H, string W, string SizeShouldBe, string WiggleRoom)
        {
            string OK = "-1";
            int iCapX = Convert.ToInt16(X);
            int iCapY = Convert.ToInt16(Y);
            int iCapW = Convert.ToInt16(W);
            int iCapH = Convert.ToInt16(H);
            OK = ValidateBytesSize(iCapX, iCapY, iCapW, iCapH, Convert.ToInt16(SizeShouldBe), Convert.ToInt16(WiggleRoom));
            return OK;
        }

        public static void DoMouseClick(int pXPos, int pYPos)
        {
            MouseMove(pXPos, pYPos);
            MouseClick();
        }
        public static void SetForegroundWindowByName(string NameOfWindow)
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

    }
}
