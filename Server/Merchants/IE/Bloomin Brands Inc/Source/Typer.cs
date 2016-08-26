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

        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);
        
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
                    }
                }
                catch (Exception ex)
                {
                }
            }
            whattotypeall = "";
            whattotypeloc++;
        }

        public void switchWindow(string NameOfWindow)
        {
            Process[] allprocs = Process.GetProcesses();
            foreach (Process proc in allprocs)
            {
                System.Diagnostics.Debug.WriteLine(proc.MainWindowTitle);
                if (proc.MainWindowTitle.Contains(NameOfWindow))
                {
                    SwitchToThisWindow(proc.MainWindowHandle, false);
                    //return;
                }
            }
        }
        public void switchWindow(int hwnd)
        {
            IntPtr x = (IntPtr)hwnd;
            SwitchToThisWindow(x, false);
        }
    }
}
