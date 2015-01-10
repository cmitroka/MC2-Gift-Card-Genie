using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace PhysicalInput
{
    public partial class Form1 : Form
    {
        int GloX;
        int GloY;
        int Ins;
        string ScriptInFile = "";
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string[] commands)
            : this()
        {
            try
            {
                ScriptInFile = commands[0].ToString();
            }
            catch (Exception)
            {
            }
        }
        private void SetFocus()
        {
            IntPtr calculatorHandle = FindWindow(null, txtFocus.Text);

            // Verify that Calculator is a running process. 
            if (calculatorHandle == IntPtr.Zero)
            {
                MessageBox.Show(txtFocus.Text + " couldn't be found." + Environment.NewLine + "Some examples are: Jasc Paint Shop Pro - Image1, Untitled - Notepad, Calculator");
                return;
            }
            // Make Calculator the foreground application
            SetForegroundWindow(calculatorHandle);
        }
        private void Test1(object sender, EventArgs e)
        {
            PhysicalInputLib.DoLeftClick(124,887);
            PhysicalInputLib.DoWait(2000);
            PhysicalInputLib.DoTyping("^a");
            PhysicalInputLib.DoWait(2000);
            PhysicalInputLib.DoTyping("{DELETE}");
            PhysicalInputLib.DoWait(1000);
            PhysicalInputLib.DoTyping("1234567890");
            System.Diagnostics.Debug.WriteLine("A");
        }

        private void Test2(object sender, EventArgs e)
        {
            PhysicalInputLib.DoLeftClick(124, 887);
            PhysicalInputLib.DoWait(1000);
            PhysicalInputLib.DoTyping("% X");
            PhysicalInputLib.DoTyping("This is a test");
            PhysicalInputLib.DoTyping("1");
            PhysicalInputLib.DoWait(1000);
            PhysicalInputLib.DoLeftClickDown(272,147);
            PhysicalInputLib.DoLeftClickUp(611, 147);
            PhysicalInputLib.DoTyping("^a");
            PhysicalInputLib.DoTyping("^c");
            System.Diagnostics.Debug.WriteLine("A");
        }

        private void monitor_Tick(object sender, EventArgs e)
        {
            int[] x = PhysicalInputLib.GetCursorPositionInt();
            txtx.Text = x[0].ToString();
            txty.Text = x[1].ToString();
        }
        public void RunScript()
        {
            this.Visible = false;
            string wholeLine = "";
            string currCommand = "";
            string currParams = "";
            int loc0 = 0;
            int loc1 = 0;
            StreamReader sr = new StreamReader(ScriptInFile);
            while (sr.EndOfStream==false)
            {
                try
                {
                    wholeLine = "";
                    currCommand = "";
                    currParams = "";
                    loc0 = 0;
                    loc1 = 0;
                    wholeLine = sr.ReadLine();
                    System.Diagnostics.Debug.WriteLine(wholeLine);
                    loc0 = wholeLine.IndexOf("(");
                    currCommand = wholeLine.Substring(0, loc0);
                    loc1 = (wholeLine.Length - 2) - loc0;
                    currParams = wholeLine.Substring(loc0 + 1, loc1);
                    DoCommand(currCommand, currParams);
                }
                catch (Exception ex)
                {
                    //How do I report there was a problem???
                }
            }
            System.Diagnostics.Debug.WriteLine("Done");
            sr.Close();
            while (File.Exists(ScriptInFile))
            {
                try
                {
                    if (ScriptInFile.ToUpper().Contains("TESTSCRIPT")) break;
                    File.Delete(ScriptInFile);
                }
                catch (Exception ex)
                {
                }
            }
            Application.Exit(); 
        }
        private void DoCommand(string theCommand, string theParams)
        {
            int temp0 = 0;
            int temp1 = 0;
            int tempX = 0;
            int tempY = 0;
            int tempCnvVal=0;
            if (theCommand.Contains("Click"))
            {
                theParams = theParams.Replace(" ", "");
                temp0 = theParams.IndexOf(",");
                tempX = Convert.ToInt16(theParams.Substring(0, temp0));
                temp1 = theParams.Length - (temp0 + 1);
                tempY = Convert.ToInt16(theParams.Substring(temp0 + 1, temp1));
            }
            if (theCommand == "DoWait")
            {
                tempCnvVal=Convert.ToInt16(theParams);
                PhysicalInputLib.DoWait(tempCnvVal);
            }
            else if (theCommand == "DoTyping")
            {
                PhysicalInputLib.DoTyping(theParams);
            }
            else if (theCommand == "DoLeftClick")
            {
                PhysicalInputLib.DoLeftClick(tempX, tempY);
            }
            else if (theCommand == "DoLeftClickDown")
            {
                PhysicalInputLib.DoLeftClickDown(tempX, tempY);
            }
            else if (theCommand == "DoLeftClickUp")
            {
                PhysicalInputLib.DoLeftClickUp(tempX, tempY);
            }
            else if (theCommand == "DoRightClick")
            {
                PhysicalInputLib.DoRightClick(tempX, tempY);
            }
            else if (theCommand == "DoRightClickDown")
            {
                PhysicalInputLib.DoRightClickDown(tempX, tempY);
            }
            else if (theCommand == "DoRightClickUp")
            {
                PhysicalInputLib.DoRightClickUp(tempX, tempY);
            }
        }
        private void tmrAutorun_Tick(object sender, EventArgs e)
        {
            tmrAutorun.Enabled = false;
            if (ScriptInFile != "")
            {
                RunScript();
            }
        }

        private void cmdSetFocusTest_Click(object sender, EventArgs e)
        {
            SetFocus();
        }
    }
}
