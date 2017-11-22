using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PainDotNetAutomate
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = Process.GetProcessesByName("PaintDotNet").FirstOrDefault();
            if (p != null)
            {
                SetForegroundWindow(p.MainWindowHandle); //Set the Paint.NET application at front
                SendKeys.SendWait("%^(v)"); //^(o) will sends the Ctrl+O key to the application. 
                SendKeys.SendWait("^+(s)"); //^(o) will sends the Ctrl+O key to the application. 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile("C:\\temp.png");
            pictureBox1.BackgroundImage.Save((@"C:\tempnew.jpg"));
        }
    }
}
