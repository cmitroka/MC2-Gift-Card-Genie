using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace DVB
{

    public partial class Typer : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        private const int ALT = 0xA4;
        private const int VK_MENU=0x12;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();






        private static string whattotype;
        private static string whattotypeall;
        private static int whattotypeloc;
        private static bool whattotypecompleted;
        public Typer()
        {
            InitializeComponent();
            whattotypeloc = 0;
        }
        public string TypeIt(string WhatToType)
        {
            timer1.Enabled = true;
            whattotype = WhatToType;
            whattotypecompleted = false;
            do
            {
                Application.DoEvents();
            } while (whattotypecompleted==false);
            return "1";
        }

        private void timer1_Tick(object sender, EventArgs e)
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
                timer1.Enabled = false;
                return;
            }
            if (test1 == "")
            {
                whattotypecompleted = true;
                timer1.Enabled = false;
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
                    if (whattotypeall=="{BACKTAB}")
                    {
                        SendKeys.Send("+{TAB}");
                    }
                    else
                    {
                        SendKeys.Send(whattotypeall);
                        //char v=ConvertStringValToChar("A");
                        //keybd_event(VK_MENU, 0xb8, KEYUP, 0);
                        //keybd_event((byte)VkKeyScan(v),0x9e,0 , 0); // ‘A’ Press
                        //keybd_event((byte)VkKeyScan(v), 0x9e, KEYUP, 0); // ‘A’ Release
                    }
                }
                catch (Exception ex)
                {
                }
            }
            whattotypeall = "";
            whattotypeloc++;
        }
        public static char ConvertStringValToChar(String ch) 
        {
            char retval;
            char[] charray = ch.ToCharArray();
            retval=charray[0];
            return retval;
        }

        //public static Keys ConvertCharToVirtualKey(String ch) {
        //    Keys retval;
        //    char[] charray = ch.ToCharArray();
        //    foreach (char c in charray)
        //    {
        //        short vkey = VkKeyScan(c);
        //        retval = (Keys)(vkey & 0xff);
        //        int modifiers = vkey >> 8;
        //        if ((modifiers & 1) != 0) retval |= Keys.Shift;
        //        if ((modifiers & 2) != 0) retval |= Keys.Control;
        //        if ((modifiers & 4) != 0) retval |= Keys.Alt;		 
        //    }
        //    return retval;
        //}
        public void SetForegroundWindowByHWND(int hWnd)
        {
            IntPtr x = (IntPtr)hWnd;
            //check if already has focus
            if (x == GetForegroundWindow())  return;
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
        public void SetForegroundWindowByName(string NameOfWindow)
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
