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
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.tmrResponseHandler = new System.Windows.Forms.Timer(this.components);
            this.tmrDelay = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRqRsPath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCAPTCHAPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdRunRequest = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAdditionalParam = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCAPTCHAAnswer = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmdForceExit = new System.Windows.Forms.Button();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.cmsSaveScript = new System.Windows.Forms.Button();
            this.cmdLoadScript = new System.Windows.Forms.Button();
            this.tmrRunning = new System.Windows.Forms.Timer(this.components);
            this.cmdGCBALANCE = new System.Windows.Forms.Button();
            this.cmdGCNEEDSMOREINFO = new System.Windows.Forms.Button();
            this.cmdGCCAPTCHA = new System.Windows.Forms.Button();
            this.cmdGCCUSTOM = new System.Windows.Forms.Button();
            this.cmdGCERR = new System.Windows.Forms.Button();
            this.cmdGCBALANCEERR = new System.Windows.Forms.Button();
            this.cmdGCTIMEOUT = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrResponseHandler
            // 
            this.tmrResponseHandler.Interval = 1;
            this.tmrResponseHandler.Tick += new System.EventHandler(this.tmrResponseHandler_Tick);
            // 
            // tmrDelay
            // 
            this.tmrDelay.Tick += new System.EventHandler(this.tmrDelay_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(812, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // txtRqRsPath
            // 
            this.txtRqRsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRqRsPath.Location = new System.Drawing.Point(112, 63);
            this.txtRqRsPath.Name = "txtRqRsPath";
            this.txtRqRsPath.Size = new System.Drawing.Size(686, 20);
            this.txtRqRsPath.TabIndex = 57;
            this.txtRqRsPath.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(44, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 58;
            this.label18.Text = "RqRs Path:";
            // 
            // txtCAPTCHAPath
            // 
            this.txtCAPTCHAPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCAPTCHAPath.Location = new System.Drawing.Point(112, 37);
            this.txtCAPTCHAPath.Name = "txtCAPTCHAPath";
            this.txtCAPTCHAPath.Size = new System.Drawing.Size(686, 20);
            this.txtCAPTCHAPath.TabIndex = 55;
            this.txtCAPTCHAPath.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "CAPTCHA Path:";
            // 
            // cmdRunRequest
            // 
            this.cmdRunRequest.Location = new System.Drawing.Point(696, 92);
            this.cmdRunRequest.Name = "cmdRunRequest";
            this.cmdRunRequest.Size = new System.Drawing.Size(100, 23);
            this.cmdRunRequest.TabIndex = 59;
            this.cmdRunRequest.TabStop = false;
            this.cmdRunRequest.Text = "Run Request";
            this.cmdRunRequest.UseVisualStyleBackColor = true;
            this.cmdRunRequest.Click += new System.EventHandler(this.cmdRunRequest_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(315, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Additional Param:";
            // 
            // txtAdditionalParam
            // 
            this.txtAdditionalParam.Location = new System.Drawing.Point(410, 89);
            this.txtAdditionalParam.Name = "txtAdditionalParam";
            this.txtAdditionalParam.Size = new System.Drawing.Size(148, 20);
            this.txtAdditionalParam.TabIndex = 62;
            this.txtAdditionalParam.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 61;
            this.label8.Text = "CAPTCHA Answer:";
            // 
            // txtCAPTCHAAnswer
            // 
            this.txtCAPTCHAAnswer.Location = new System.Drawing.Point(112, 89);
            this.txtCAPTCHAAnswer.Name = "txtCAPTCHAAnswer";
            this.txtCAPTCHAAnswer.Size = new System.Drawing.Size(148, 20);
            this.txtCAPTCHAAnswer.TabIndex = 60;
            this.txtCAPTCHAAnswer.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 285);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(812, 112);
            this.txtLog.TabIndex = 70;
            this.txtLog.TabStop = false;
            // 
            // cmdForceExit
            // 
            this.cmdForceExit.Location = new System.Drawing.Point(343, 259);
            this.cmdForceExit.Name = "cmdForceExit";
            this.cmdForceExit.Size = new System.Drawing.Size(80, 21);
            this.cmdForceExit.TabIndex = 69;
            this.cmdForceExit.Text = "Force Exit";
            this.cmdForceExit.UseVisualStyleBackColor = true;
            this.cmdForceExit.Click += new System.EventHandler(this.cmdForceExit_Click);
            // 
            // cmdLogOut
            // 
            this.cmdLogOut.Location = new System.Drawing.Point(429, 259);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(80, 21);
            this.cmdLogOut.TabIndex = 68;
            this.cmdLogOut.Text = "View Log";
            this.cmdLogOut.UseVisualStyleBackColor = true;
            this.cmdLogOut.Click += new System.EventHandler(this.cmdViewLog_Click);
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(16, 139);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(374, 81);
            this.txtScript.TabIndex = 71;
            this.txtScript.TabStop = false;
            // 
            // cmsSaveScript
            // 
            this.cmsSaveScript.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cmsSaveScript.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmsSaveScript.Location = new System.Drawing.Point(532, 197);
            this.cmsSaveScript.Name = "cmsSaveScript";
            this.cmsSaveScript.Size = new System.Drawing.Size(130, 23);
            this.cmsSaveScript.TabIndex = 72;
            this.cmsSaveScript.Text = "Save Script";
            this.cmsSaveScript.UseVisualStyleBackColor = false;
            this.cmsSaveScript.Click += new System.EventHandler(this.cmsSaveScript_Click);
            // 
            // cmdLoadScript
            // 
            this.cmdLoadScript.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cmdLoadScript.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdLoadScript.Location = new System.Drawing.Point(668, 197);
            this.cmdLoadScript.Name = "cmdLoadScript";
            this.cmdLoadScript.Size = new System.Drawing.Size(130, 23);
            this.cmdLoadScript.TabIndex = 73;
            this.cmdLoadScript.Text = "Load Script";
            this.cmdLoadScript.UseVisualStyleBackColor = false;
            this.cmdLoadScript.Click += new System.EventHandler(this.cmdLoadScript_Click);
            // 
            // tmrRunning
            // 
            this.tmrRunning.Tick += new System.EventHandler(this.tmrRunning_Tick);
            // 
            // cmdGCBALANCE
            // 
            this.cmdGCBALANCE.AutoSize = true;
            this.cmdGCBALANCE.Location = new System.Drawing.Point(396, 139);
            this.cmdGCBALANCE.Name = "cmdGCBALANCE";
            this.cmdGCBALANCE.Size = new System.Drawing.Size(130, 23);
            this.cmdGCBALANCE.TabIndex = 74;
            this.cmdGCBALANCE.Text = "GCBALANCE";
            this.cmdGCBALANCE.UseVisualStyleBackColor = true;
            this.cmdGCBALANCE.Click += new System.EventHandler(this.cmdGCBALANCE_Click);
            // 
            // cmdGCNEEDSMOREINFO
            // 
            this.cmdGCNEEDSMOREINFO.AutoSize = true;
            this.cmdGCNEEDSMOREINFO.Location = new System.Drawing.Point(532, 139);
            this.cmdGCNEEDSMOREINFO.Name = "cmdGCNEEDSMOREINFO";
            this.cmdGCNEEDSMOREINFO.Size = new System.Drawing.Size(130, 23);
            this.cmdGCNEEDSMOREINFO.TabIndex = 75;
            this.cmdGCNEEDSMOREINFO.Text = "GCNEEDSMOREINFO";
            this.cmdGCNEEDSMOREINFO.UseVisualStyleBackColor = true;
            this.cmdGCNEEDSMOREINFO.Click += new System.EventHandler(this.cmdGCNEEDSMOREINFO_Click);
            // 
            // cmdGCCAPTCHA
            // 
            this.cmdGCCAPTCHA.AutoSize = true;
            this.cmdGCCAPTCHA.Location = new System.Drawing.Point(668, 139);
            this.cmdGCCAPTCHA.Name = "cmdGCCAPTCHA";
            this.cmdGCCAPTCHA.Size = new System.Drawing.Size(130, 23);
            this.cmdGCCAPTCHA.TabIndex = 76;
            this.cmdGCCAPTCHA.Text = "GCCAPTCHA";
            this.cmdGCCAPTCHA.UseVisualStyleBackColor = true;
            this.cmdGCCAPTCHA.Click += new System.EventHandler(this.cmdGCCAPTCHA_Click);
            // 
            // cmdGCCUSTOM
            // 
            this.cmdGCCUSTOM.AutoSize = true;
            this.cmdGCCUSTOM.Location = new System.Drawing.Point(668, 168);
            this.cmdGCCUSTOM.Name = "cmdGCCUSTOM";
            this.cmdGCCUSTOM.Size = new System.Drawing.Size(130, 23);
            this.cmdGCCUSTOM.TabIndex = 79;
            this.cmdGCCUSTOM.Text = "GCCUSTOM";
            this.cmdGCCUSTOM.UseVisualStyleBackColor = true;
            this.cmdGCCUSTOM.Click += new System.EventHandler(this.cmdGCCUSTOM_Click);
            // 
            // cmdGCERR
            // 
            this.cmdGCERR.AutoSize = true;
            this.cmdGCERR.Location = new System.Drawing.Point(532, 168);
            this.cmdGCERR.Name = "cmdGCERR";
            this.cmdGCERR.Size = new System.Drawing.Size(130, 23);
            this.cmdGCERR.TabIndex = 78;
            this.cmdGCERR.Text = "GCERR";
            this.cmdGCERR.UseVisualStyleBackColor = true;
            this.cmdGCERR.Click += new System.EventHandler(this.cmdGCERR_Click);
            // 
            // cmdGCBALANCEERR
            // 
            this.cmdGCBALANCEERR.AutoSize = true;
            this.cmdGCBALANCEERR.Location = new System.Drawing.Point(396, 168);
            this.cmdGCBALANCEERR.Name = "cmdGCBALANCEERR";
            this.cmdGCBALANCEERR.Size = new System.Drawing.Size(130, 23);
            this.cmdGCBALANCEERR.TabIndex = 77;
            this.cmdGCBALANCEERR.Text = "GCBALANCEERR";
            this.cmdGCBALANCEERR.UseVisualStyleBackColor = true;
            this.cmdGCBALANCEERR.Click += new System.EventHandler(this.cmdGCBALANCEERR_Click);
            // 
            // cmdGCTIMEOUT
            // 
            this.cmdGCTIMEOUT.AutoSize = true;
            this.cmdGCTIMEOUT.Location = new System.Drawing.Point(396, 197);
            this.cmdGCTIMEOUT.Name = "cmdGCTIMEOUT";
            this.cmdGCTIMEOUT.Size = new System.Drawing.Size(130, 23);
            this.cmdGCTIMEOUT.TabIndex = 80;
            this.cmdGCTIMEOUT.Text = "GCTIMEOUT";
            this.cmdGCTIMEOUT.UseVisualStyleBackColor = true;
            this.cmdGCTIMEOUT.Click += new System.EventHandler(this.cmdGCTIMEOUT_Click);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 397);
            this.Controls.Add(this.cmdGCTIMEOUT);
            this.Controls.Add(this.cmdGCCUSTOM);
            this.Controls.Add(this.cmdGCERR);
            this.Controls.Add(this.cmdGCBALANCEERR);
            this.Controls.Add(this.cmdGCCAPTCHA);
            this.Controls.Add(this.cmdGCNEEDSMOREINFO);
            this.Controls.Add(this.cmdGCBALANCE);
            this.Controls.Add(this.cmdLoadScript);
            this.Controls.Add(this.cmsSaveScript);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.cmdForceExit);
            this.Controls.Add(this.cmdLogOut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAdditionalParam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCAPTCHAAnswer);
            this.Controls.Add(this.cmdRunRequest);
            this.Controls.Add(this.txtRqRsPath);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtCAPTCHAPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Balance Extractor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrTimeout;
        private System.Windows.Forms.Timer tmrDelay;
        public System.Windows.Forms.Timer tmrResponseHandler;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.TextBox txtRqRsPath;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtCAPTCHAPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdRunRequest;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtAdditionalParam;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtCAPTCHAAnswer;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button cmdForceExit;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        public System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Button cmsSaveScript;
        private System.Windows.Forms.Button cmdLoadScript;
        public System.Windows.Forms.Timer tmrRunning;
        private System.Windows.Forms.Button cmdGCBALANCE;
        private System.Windows.Forms.Button cmdGCNEEDSMOREINFO;
        private System.Windows.Forms.Button cmdGCCAPTCHA;
        private System.Windows.Forms.Button cmdGCCUSTOM;
        private System.Windows.Forms.Button cmdGCERR;
        private System.Windows.Forms.Button cmdGCBALANCEERR;
        private System.Windows.Forms.Button cmdGCTIMEOUT;

    }
}

