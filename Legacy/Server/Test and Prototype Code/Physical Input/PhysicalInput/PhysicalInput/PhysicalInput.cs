using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PhysicalInput
{
    class PhysicalInput
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Send a series of key presses to the Calculator application. 
        private void button1_Click(object sender, EventArgs e)
        {
            // Get a handle to the Calculator application. The window class 
            // and window name were obtained using the Spy++ tool.
            IntPtr calculatorHandle = FindWindow("CalcFrame", "Calculator");

            // Verify that Calculator is a running process. 
            if (calculatorHandle == IntPtr.Zero)
            {
                MessageBox.Show("Calculator is not running.");
                return;
            }

            // Make Calculator the foreground application and send it  
            // a set of calculations.
            SetForegroundWindow(calculatorHandle);
            SendKeys.SendWait("111");
            SendKeys.SendWait("*");
            SendKeys.SendWait("11");
            SendKeys.SendWait("=");
        }
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            // Send the enter key to the button, which raises the click  
            // event for the button. This works because the tab stop of  
            // the button is 0.
            SendKeys.Send("{ENTER}");
        }

    }
}
