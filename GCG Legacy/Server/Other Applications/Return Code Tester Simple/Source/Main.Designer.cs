namespace DVB
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmdForceExit = new System.Windows.Forms.Button();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessageToReturn = new System.Windows.Forms.TextBox();
            this.cmdSimulateGCG = new System.Windows.Forms.Button();
            this.cmdMakeWebFiles = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMerchEXEPath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAppStaticDBPath = new System.Windows.Forms.TextBox();
            this.txtRqRsPath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCAPTCHAPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tmrDelay = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MGoToBaseURL = new System.Windows.Forms.ToolStripMenuItem();
            this.shellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MOpenCAPTCHALocation = new System.Windows.Forms.ToolStripMenuItem();
            this.MOpenRqRSLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(962, 192);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(954, 166);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Execution";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Controls.Add(this.cmdForceExit);
            this.groupBox3.Controls.Add(this.cmdLogOut);
            this.groupBox3.Location = new System.Drawing.Point(2, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(946, 61);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(2, 10);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(846, 45);
            this.txtLog.TabIndex = 56;
            this.txtLog.TabStop = false;
            // 
            // cmdForceExit
            // 
            this.cmdForceExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdForceExit.Location = new System.Drawing.Point(854, 34);
            this.cmdForceExit.Name = "cmdForceExit";
            this.cmdForceExit.Size = new System.Drawing.Size(80, 21);
            this.cmdForceExit.TabIndex = 50;
            this.cmdForceExit.Text = "Force Exit";
            this.cmdForceExit.UseVisualStyleBackColor = true;
            this.cmdForceExit.Click += new System.EventHandler(this.cmdForceExit_Click);
            // 
            // cmdLogOut
            // 
            this.cmdLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLogOut.Location = new System.Drawing.Point(854, 11);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(80, 21);
            this.cmdLogOut.TabIndex = 49;
            this.cmdLogOut.Text = "View Log";
            this.cmdLogOut.UseVisualStyleBackColor = true;
            this.cmdLogOut.Click += new System.EventHandler(this.cmdViewLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.cmdSave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMessageToReturn);
            this.groupBox1.Controls.Add(this.cmdSimulateGCG);
            this.groupBox1.Controls.Add(this.cmdMakeWebFiles);
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 102);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "rsType to Return";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(2, 34);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(240, 20);
            this.txtType.TabIndex = 67;
            this.txtType.TabStop = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(516, 31);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(121, 23);
            this.cmdSave.TabIndex = 66;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "rsValue to Return";
            // 
            // txtMessageToReturn
            // 
            this.txtMessageToReturn.Location = new System.Drawing.Point(0, 76);
            this.txtMessageToReturn.Name = "txtMessageToReturn";
            this.txtMessageToReturn.Size = new System.Drawing.Size(934, 20);
            this.txtMessageToReturn.TabIndex = 64;
            this.txtMessageToReturn.TabStop = false;
            // 
            // cmdSimulateGCG
            // 
            this.cmdSimulateGCG.Location = new System.Drawing.Point(400, 31);
            this.cmdSimulateGCG.Name = "cmdSimulateGCG";
            this.cmdSimulateGCG.Size = new System.Drawing.Size(110, 23);
            this.cmdSimulateGCG.TabIndex = 63;
            this.cmdSimulateGCG.Text = "Process with GCG";
            this.cmdSimulateGCG.UseVisualStyleBackColor = true;
            this.cmdSimulateGCG.Click += new System.EventHandler(this.cmdSimulateGCG_Click);
            // 
            // cmdMakeWebFiles
            // 
            this.cmdMakeWebFiles.Location = new System.Drawing.Point(284, 31);
            this.cmdMakeWebFiles.Name = "cmdMakeWebFiles";
            this.cmdMakeWebFiles.Size = new System.Drawing.Size(110, 23);
            this.cmdMakeWebFiles.TabIndex = 62;
            this.cmdMakeWebFiles.Text = "Make Web Files";
            this.cmdMakeWebFiles.UseVisualStyleBackColor = true;
            this.cmdMakeWebFiles.Click += new System.EventHandler(this.cmdMakeWebFiles_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(954, 166);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Stuff";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtMerchEXEPath);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtAppStaticDBPath);
            this.groupBox5.Controls.Add(this.txtRqRsPath);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtCAPTCHAPath);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(937, 136);
            this.groupBox5.TabIndex = 54;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GC-Common Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "Merch EXE Path:";
            // 
            // txtMerchEXEPath
            // 
            this.txtMerchEXEPath.Location = new System.Drawing.Point(103, 105);
            this.txtMerchEXEPath.Name = "txtMerchEXEPath";
            this.txtMerchEXEPath.Size = new System.Drawing.Size(828, 20);
            this.txtMerchEXEPath.TabIndex = 61;
            this.txtMerchEXEPath.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "MDB Path:";
            // 
            // txtAppStaticDBPath
            // 
            this.txtAppStaticDBPath.Location = new System.Drawing.Point(103, 79);
            this.txtAppStaticDBPath.Name = "txtAppStaticDBPath";
            this.txtAppStaticDBPath.Size = new System.Drawing.Size(828, 20);
            this.txtAppStaticDBPath.TabIndex = 59;
            this.txtAppStaticDBPath.TabStop = false;
            // 
            // txtRqRsPath
            // 
            this.txtRqRsPath.Location = new System.Drawing.Point(103, 53);
            this.txtRqRsPath.Name = "txtRqRsPath";
            this.txtRqRsPath.Size = new System.Drawing.Size(828, 20);
            this.txtRqRsPath.TabIndex = 53;
            this.txtRqRsPath.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(35, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "RqRs Path:";
            // 
            // txtCAPTCHAPath
            // 
            this.txtCAPTCHAPath.Location = new System.Drawing.Point(103, 27);
            this.txtCAPTCHAPath.Name = "txtCAPTCHAPath";
            this.txtCAPTCHAPath.Size = new System.Drawing.Size(828, 20);
            this.txtCAPTCHAPath.TabIndex = 51;
            this.txtCAPTCHAPath.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "CAPTCHA Path:";
            // 
            // tmrDelay
            // 
            this.tmrDelay.Tick += new System.EventHandler(this.tmrDelay_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.navigationToolStripMenuItem,
            this.shellToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(962, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // navigationToolStripMenuItem
            // 
            this.navigationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MGoToBaseURL});
            this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
            this.navigationToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.navigationToolStripMenuItem.Text = "Navigation";
            // 
            // MGoToBaseURL
            // 
            this.MGoToBaseURL.Name = "MGoToBaseURL";
            this.MGoToBaseURL.Size = new System.Drawing.Size(150, 22);
            this.MGoToBaseURL.Text = "Go To Base URL";
            // 
            // shellToolStripMenuItem
            // 
            this.shellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MOpenCAPTCHALocation,
            this.MOpenRqRSLocation});
            this.shellToolStripMenuItem.Name = "shellToolStripMenuItem";
            this.shellToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.shellToolStripMenuItem.Text = "Shell";
            // 
            // MOpenCAPTCHALocation
            // 
            this.MOpenCAPTCHALocation.Name = "MOpenCAPTCHALocation";
            this.MOpenCAPTCHALocation.Size = new System.Drawing.Size(193, 22);
            this.MOpenCAPTCHALocation.Text = "Open CAPTCHA Location";
            // 
            // MOpenRqRSLocation
            // 
            this.MOpenRqRSLocation.Name = "MOpenRqRSLocation";
            this.MOpenRqRSLocation.Size = new System.Drawing.Size(193, 22);
            this.MOpenRqRSLocation.Text = "Open RqRS Location";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 216);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Balance Extractor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer tmrDelay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MGoToBaseURL;
        private System.Windows.Forms.ToolStripMenuItem shellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MOpenCAPTCHALocation;
        private System.Windows.Forms.ToolStripMenuItem MOpenRqRSLocation;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtAppStaticDBPath;
        public System.Windows.Forms.TextBox txtRqRsPath;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtCAPTCHAPath;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button cmdForceExit;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button cmdSimulateGCG;
        private System.Windows.Forms.Button cmdMakeWebFiles;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtMerchEXEPath;
        public System.Windows.Forms.TextBox txtMessageToReturn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtType;
    }
}

