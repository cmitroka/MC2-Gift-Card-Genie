using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GCG_Janitor
{
    public partial class Main : Form
    {
        string AutoRun = "";
        string StartInSystemTray = "";
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public Main()
        {
            InitializeComponent();
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Show", OnShow);
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "GC Janitor";
            trayIcon.Icon = new Icon(SystemIcons.Application, 400, 400);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

        }
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void SendToSystemtray()
        {
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = true;
            this.Hide();
            ShowInTaskbar = false;
        }
        private void GetFromSystemtray()
        {
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
            this.Show();
            ShowInTaskbar = true;
        }
        private void CheckForIISRESET()
        {
            bool DoIt = false;
            com.mc2techservices.gcg.WebService WS = new com.mc2techservices.gcg.WebService();
            try
            {
                string test = WS.CheckWebConfig();
            }
            catch (Exception ex)
            {
                DoIt=true;
            }
            if (DoIt==true)
            {
                try 
	            {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //process.StartInfo.FileName = @"C:\WINDOWS\system32\iisreset.exe";
                    process.StartInfo.FileName = "cmd";
                    process.StartInfo.Arguments = txtErrorHandler.Text;  ///C iisreset /STOP
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = false;
                    //process.StartInfo.RedirectStandardError = true;
                    //process.StartInfo.RedirectStandardOutput = true;
                    process.Start();
                    process.WaitForExit();
                    //GCGCommon.SupportMethods.LaunchIt(txtIISReset.Text,null,true,false);
                }
	            catch (Exception)
	            {
                    txtLog.Text = DateTime.Now.ToString() + " - Couldn't do IIS Reset";
	            }
            }
        }
        private void OnShow(object sender, EventArgs e)
        {
            GetFromSystemtray();
        }
        public Main(string[] commands)
        : this()
        {
            try
            {
                AutoRun = commands[0].ToString();
            }
            catch (Exception)
            {
            }
            try
            {
                StartInSystemTray = commands[1].ToString();
            }
            catch (Exception)
            {
            }
            if (AutoRun == "1")
            {
                tmrRunning.Enabled = true;
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (StartInSystemTray == "1")
            {
                SendToSystemtray();
            }
            string AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            int test = GCGCommon.SupportMethods.IsRunning(AppName);
            if (test > 1)
            {
                MessageBox.Show("Already Running");
                Application.Exit();
            }
            else
            {
                LoadConfig();
                RetitleToggle();
            }
            if (txtLocBatchRqRs.Text != "")
            {
                string rem = txtSecsDoPurge.Text;
                txtSecsDoPurge.Text = "0";
                FindAndPurgeOldFiles();
                txtSecsDoPurge.Text = rem;
            }
        }
        private void SaveConfig()
        {
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\Janitor";
            //MR.Write("txtCurrentCount", txtCurrentCount.Text);
            MR.Write("txtLaunchVisibly", txtLaunchVisibly.Text);
            MR.Write("txtLocBatchRqRs", txtLocBatchRqRs.Text);
            MR.Write("txtLocCAPTCHA", txtLocCAPTCHA.Text);
            MR.Write("txtSecsCheckBatch", txtSecsCheckBatch.Text);
            MR.Write("txtSecsDoPurge", txtSecsDoPurge.Text);
            MR.Write("txtErrorHandler", txtErrorHandler.Text);
        }

        private void LoadConfig()
        {
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\Janitor";
            //txtCurrentCount.Text = MR.Read("txtCurrentCount");
            txtLaunchVisibly.Text = MR.Read("txtLaunchVisibly");
            txtLocBatchRqRs.Text = MR.Read("txtLocBatchRqRs");
            txtLocCAPTCHA.Text = MR.Read("txtLocCAPTCHA");
            txtSecsCheckBatch.Text = MR.Read("txtSecsCheckBatch");
            txtSecsDoPurge.Text = MR.Read("txtSecsDoPurge");
            txtErrorHandler.Text = MR.Read("txtErrorHandler");
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

            SaveConfig();
        }

        private void tmrRunning_Tick(object sender, EventArgs e)
        {
            tmrRunning.Enabled = false;
            int cc = GetIntVal(txtCurrentCount);
            cc++;
            txtCurrentCount.Text = cc.ToString();
            int check=GetIntVal(txtSecsCheckBatch);
            if (cc>=check)
            {
                CheckForIISRESET();
                FindAndPurgeOldFiles();
                ResetTimer();
            }
            ExecAndDelBatchFiles();
            tmrRunning.Enabled = true;
        }
        private void FindAndPurgeOldFiles()
        {
            try
            {
                string[] filePaths;
                int sub;
                DateTime trueExpiredTime;
                try
                {
                    filePaths = Directory.GetFiles(txtLocBatchRqRs.Text + "\\");
                }
                catch (Exception)
                {
                    return;
                }

                foreach (string file in filePaths)
                {
                    try
                    {
                        DateTime creationTime = File.GetCreationTime(file);
                        DateTime expiredTime = DateTime.Now;
                        sub = GetIntVal(txtSecsDoPurge);
                        if (sub > 0) sub = (sub * -1);
                        trueExpiredTime = expiredTime.AddSeconds(sub);
                        if (creationTime < trueExpiredTime)
                        {
                            File.Delete(file);
                        }
                        else
                        {
                            Console.WriteLine("File too new");
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
                filePaths = Directory.GetFiles(txtLocCAPTCHA.Text + "\\");
                foreach (string file in filePaths)
                {
                    try
                    {
                        DateTime creationTime = File.GetCreationTime(file);
                        DateTime expiredTime = DateTime.Now;
                        sub = GetIntVal(txtSecsDoPurge);
                        if (sub > 0) sub = (sub * -1);
                        trueExpiredTime = expiredTime.AddSeconds(sub);
                        if (creationTime < trueExpiredTime)
                        {
                            File.Delete(file);
                        }
                        else
                        {
                            Console.WriteLine("File too new");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private void RetitleToggle()
        {
            if (tmrRunning.Enabled == false)
            {
                cmdToggle.Text = "Run";
            }
            else
            {
                cmdToggle.Text = "Stop";
            }
        }

        private void cmdToggle_Click(object sender, EventArgs e)
        {
            if (tmrRunning.Enabled == true)
            {
                tmrRunning.Enabled = false;
            }
            else
            {
                ResetTimer();
                tmrRunning.Enabled = true;
            }
            RetitleToggle();
        }
        private void ResetTimer()
        {
            txtCurrentCount.Text = "0";
            int cc = Convert.ToInt16(txtCurrentCount.Text);
        }
        private int GetIntVal(TextBox t)
        {
            int cc = 0;
            try
            {
                cc = Convert.ToInt16(t.Text);
            }
            catch (Exception) { }
            return cc;
        }

        private void ExecAndDelBatchFiles()
        {
            string errType = "";
            string[] FilePieces0;
            string[] FilePieces1;
            string[] filePaths;
            try
            {
                filePaths = Directory.GetFiles(txtLocBatchRqRs.Text + "\\");
            }
            catch (Exception)
            {
                return;
            }
            foreach (string file in filePaths)
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                FilePieces0=GCGCommon.SupportMethods.GetFilePathPieces(file);
                if (FilePieces0[0].ToUpper()=="GCG.CMD")
                {
                    //p.StartInfo.FileName = file;
                    //p.Start();
                    GCGCommon.SupportMethods.LaunchIt(file,null,true,false);
                }
                else if (FilePieces0[3].Contains("bat"))
                {
                    StreamReader sr = null;
                    string templine = "";
                    try
                    {
                        sr = new StreamReader(file);
                        templine = sr.ReadLine();
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return;
                    }
                    string[] temp0=GCGCommon.SupportMethods.SplitByString(templine, @""""+" " + @"""");
                    string fileName = temp0[0].Replace(@"""","");
                    string arguments = @"""" + temp0[1];
                    FilePieces1 = GCGCommon.SupportMethods.GetFilePathPieces(fileName);
                    p.StartInfo.FileName = fileName;
                    p.StartInfo.Arguments = arguments;
                    string commandLine=arguments;
                    string[] temp1=GCGCommon.SupportMethods.ParseArguments(arguments);
                    string OutputFile = temp1[0].Replace(@"rq.txt", "rs.txt");
                    if (FilePieces1[3].ToUpper() == txtLaunchVisibly.Text.ToUpper())
                    {
                        Console.WriteLine("launch visibly");
                    }
                    else if (txtLaunchVisibly.Text.ToUpper() == "ALL")
                    {
                        Console.WriteLine("launch visibly");
                    }
                    else
                    {
                        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    }
                    try
                    {
                        bool isStarted = p.Start();
                    }
                    catch (Exception ex)
                    {
                        errType=GCGCommon.JanitorTypes.JEXEMISSING.ToString();
                        GCGCommon.SupportMethods.WriteResponseFile(errType, "The executable for [" + fileName + "] was missing, or whatever came through for card type is wrong.", OutputFile);
                    }
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                SendToSystemtray();
            }
        }

    }

}
