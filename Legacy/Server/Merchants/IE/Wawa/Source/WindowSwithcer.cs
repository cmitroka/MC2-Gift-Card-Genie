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

    public partial class WindowSwithcer : Form
    {
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;


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

        public WindowSwithcer()
        {
            InitializeComponent();
        }

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
