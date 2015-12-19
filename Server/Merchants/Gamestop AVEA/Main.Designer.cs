namespace AutomationViaExternalApp
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.cmdRunRequest = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtCardPIN = new System.Windows.Forms.TextBox();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChromeLoc = new System.Windows.Forms.TextBox();
            this.txtRqRsPath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAppStaticDBPath = new System.Windows.Forms.TextBox();
            this.txtBaseURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTimeoutMonitor = new System.Windows.Forms.TextBox();
            this.txtRetryCntr = new System.Windows.Forms.TextBox();
            this.cmdForceExit = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.tmrRunning = new System.Windows.Forms.Timer(this.components);
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.tmrDelay = new System.Windows.Forms.Timer(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tmrSendKeys = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtAutorun = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtResponse);
            this.groupBox1.Controls.Add(this.cmdRunRequest);
            this.groupBox1.Location = new System.Drawing.Point(400, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 64);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 78;
            this.label11.Text = "Resp:";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(47, 38);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(59, 20);
            this.txtResponse.TabIndex = 77;
            this.txtResponse.TabStop = false;
            // 
            // cmdRunRequest
            // 
            this.cmdRunRequest.Location = new System.Drawing.Point(6, 12);
            this.cmdRunRequest.Name = "cmdRunRequest";
            this.cmdRunRequest.Size = new System.Drawing.Size(100, 21);
            this.cmdRunRequest.TabIndex = 76;
            this.cmdRunRequest.TabStop = false;
            this.cmdRunRequest.Text = "Run Request";
            this.cmdRunRequest.UseVisualStyleBackColor = true;
            this.cmdRunRequest.Click += new System.EventHandler(this.cmdRunRequest_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtLogin);
            this.groupBox2.Controls.Add(this.txtCardPIN);
            this.groupBox2.Controls.Add(this.txtCardNumber);
            this.groupBox2.Location = new System.Drawing.Point(6, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 64);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(197, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 78;
            this.label7.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(259, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(119, 20);
            this.txtPassword.TabIndex = 77;
            this.txtPassword.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Login:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Card PIN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Card #:";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(259, 12);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(119, 20);
            this.txtLogin.TabIndex = 73;
            this.txtLogin.TabStop = false;
            // 
            // txtCardPIN
            // 
            this.txtCardPIN.Location = new System.Drawing.Point(65, 38);
            this.txtCardPIN.Name = "txtCardPIN";
            this.txtCardPIN.Size = new System.Drawing.Size(122, 20);
            this.txtCardPIN.TabIndex = 72;
            this.txtCardPIN.TabStop = false;
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(65, 12);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(122, 20);
            this.txtCardNumber.TabIndex = 71;
            this.txtCardNumber.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtChromeLoc);
            this.groupBox3.Controls.Add(this.txtRqRsPath);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtAppStaticDBPath);
            this.groupBox3.Location = new System.Drawing.Point(6, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 91);
            this.groupBox3.TabIndex = 82;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Chrome Path:";
            // 
            // txtChromeLoc
            // 
            this.txtChromeLoc.Location = new System.Drawing.Point(78, 39);
            this.txtChromeLoc.Name = "txtChromeLoc";
            this.txtChromeLoc.Size = new System.Drawing.Size(420, 20);
            this.txtChromeLoc.TabIndex = 85;
            // 
            // txtRqRsPath
            // 
            this.txtRqRsPath.Location = new System.Drawing.Point(78, 65);
            this.txtRqRsPath.Name = "txtRqRsPath";
            this.txtRqRsPath.Size = new System.Drawing.Size(420, 20);
            this.txtRqRsPath.TabIndex = 86;
            this.txtRqRsPath.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 87;
            this.label18.Text = "RqRs Path:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 81;
            this.label12.Text = "MDB Path:";
            // 
            // txtAppStaticDBPath
            // 
            this.txtAppStaticDBPath.Location = new System.Drawing.Point(78, 13);
            this.txtAppStaticDBPath.Name = "txtAppStaticDBPath";
            this.txtAppStaticDBPath.Size = new System.Drawing.Size(420, 20);
            this.txtAppStaticDBPath.TabIndex = 80;
            this.txtAppStaticDBPath.TabStop = false;
            // 
            // txtBaseURL
            // 
            this.txtBaseURL.Location = new System.Drawing.Point(78, 13);
            this.txtBaseURL.Name = "txtBaseURL";
            this.txtBaseURL.Size = new System.Drawing.Size(420, 20);
            this.txtBaseURL.TabIndex = 82;
            this.txtBaseURL.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Base URL:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.txtLog);
            this.groupBox4.Location = new System.Drawing.Point(6, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 117);
            this.groupBox4.TabIndex = 83;
            this.groupBox4.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(486, 96);
            this.txtLog.TabIndex = 57;
            this.txtLog.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtAutorun);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtTimeoutMonitor);
            this.groupBox5.Controls.Add(this.txtRetryCntr);
            this.groupBox5.Controls.Add(this.cmdForceExit);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Location = new System.Drawing.Point(6, 250);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(510, 39);
            this.groupBox5.TabIndex = 84;
            this.groupBox5.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(136, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 57;
            this.label17.Text = "Timeout:";
            // 
            // txtTimeoutMonitor
            // 
            this.txtTimeoutMonitor.Location = new System.Drawing.Point(190, 12);
            this.txtTimeoutMonitor.Name = "txtTimeoutMonitor";
            this.txtTimeoutMonitor.Size = new System.Drawing.Size(44, 20);
            this.txtTimeoutMonitor.TabIndex = 56;
            this.txtTimeoutMonitor.TabStop = false;
            this.txtTimeoutMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRetryCntr
            // 
            this.txtRetryCntr.Location = new System.Drawing.Point(78, 13);
            this.txtRetryCntr.Name = "txtRetryCntr";
            this.txtRetryCntr.Size = new System.Drawing.Size(44, 20);
            this.txtRetryCntr.TabIndex = 53;
            this.txtRetryCntr.TabStop = false;
            this.txtRetryCntr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdForceExit
            // 
            this.cmdForceExit.Location = new System.Drawing.Point(403, 11);
            this.cmdForceExit.Name = "cmdForceExit";
            this.cmdForceExit.Size = new System.Drawing.Size(80, 21);
            this.cmdForceExit.TabIndex = 55;
            this.cmdForceExit.Text = "Force Exit";
            this.cmdForceExit.UseVisualStyleBackColor = true;
            this.cmdForceExit.Click += new System.EventHandler(this.cmdForceExit_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 54;
            this.label16.Text = "Rerty Cnrt:";
            // 
            // tmrRunning
            // 
            this.tmrRunning.Interval = 1000;
            this.tmrRunning.Tick += new System.EventHandler(this.tmrRunning_Tick);
            // 
            // tmrTimeout
            // 
            this.tmrTimeout.Interval = 1000;
            this.tmrTimeout.Tick += new System.EventHandler(this.tmrTimeout_Tick);
            // 
            // tmrDelay
            // 
            this.tmrDelay.Tick += new System.EventHandler(this.tmrDelay_Tick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.txtBaseURL);
            this.groupBox6.Location = new System.Drawing.Point(6, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(510, 37);
            this.groupBox6.TabIndex = 85;
            this.groupBox6.TabStop = false;
            // 
            // tmrSendKeys
            // 
            this.tmrSendKeys.Interval = 10;
            this.tmrSendKeys.Tick += new System.EventHandler(this.tmrSendKeys_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(249, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Autorun:";
            // 
            // txtAutorun
            // 
            this.txtAutorun.Location = new System.Drawing.Point(303, 13);
            this.txtAutorun.Name = "txtAutorun";
            this.txtAutorun.Size = new System.Drawing.Size(44, 20);
            this.txtAutorun.TabIndex = 58;
            this.txtAutorun.TabStop = false;
            this.txtAutorun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 415);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button cmdRunRequest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtLogin;
        public System.Windows.Forms.TextBox txtCardPIN;
        public System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtBaseURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtAppStaticDBPath;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTimeoutMonitor;
        private System.Windows.Forms.TextBox txtRetryCntr;
        private System.Windows.Forms.Button cmdForceExit;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Timer tmrRunning;
        private System.Windows.Forms.Timer tmrTimeout;
        public System.Windows.Forms.TextBox txtRqRsPath;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Timer tmrDelay;
        private System.Windows.Forms.TextBox txtChromeLoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Timer tmrSendKeys;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAutorun;
    }
}

