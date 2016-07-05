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
            this.tmrRunning = new System.Windows.Forms.Timer(this.components);
            this.txtLog = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdGo = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAdditionalParam = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCAPTCHAAnswer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCardBalance = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtCardPIN = new System.Windows.Forms.TextBox();
            this.cmdRunRequest = new System.Windows.Forms.Button();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAppStaticDBPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestRqRsPath = new System.Windows.Forms.TextBox();
            this.chkNeverAutoExit = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCAPTCHAPath = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaseURL = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.tmrResponseHandler = new System.Windows.Forms.Timer(this.components);
            this.tmrDelay = new System.Windows.Forms.Timer(this.components);
            this.tmrCopyPaste = new System.Windows.Forms.Timer(this.components);
            this.txtMaxProg = new System.Windows.Forms.TextBox();
            this.tmrGetCAPTCHA = new System.Windows.Forms.Timer(this.components);
            this.tmrPageDone = new System.Windows.Forms.Timer(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtIsBusy = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCntr = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrRunning
            // 
            this.tmrRunning.Interval = 1;
            this.tmrRunning.Tick += new System.EventHandler(this.tmrRunning_Tick);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLog.Location = new System.Drawing.Point(8, 628);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(722, 45);
            this.txtLog.TabIndex = 14;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(8, 164);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(953, 458);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtAdditionalParam);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCAPTCHAAnswer);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCardBalance);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.txtCardPIN);
            this.groupBox1.Controls.Add(this.cmdRunRequest);
            this.groupBox1.Controls.Add(this.txtCardNumber);
            this.groupBox1.Location = new System.Drawing.Point(11, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(947, 73);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // cmdGo
            // 
            this.cmdGo.Location = new System.Drawing.Point(475, 17);
            this.cmdGo.Name = "cmdGo";
            this.cmdGo.Size = new System.Drawing.Size(34, 23);
            this.cmdGo.TabIndex = 37;
            this.cmdGo.Text = "Go";
            this.cmdGo.UseVisualStyleBackColor = true;
            this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(566, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Additional Param:";
            // 
            // txtAdditionalParam
            // 
            this.txtAdditionalParam.Location = new System.Drawing.Point(661, 45);
            this.txtAdditionalParam.Name = "txtAdditionalParam";
            this.txtAdditionalParam.Size = new System.Drawing.Size(150, 20);
            this.txtAdditionalParam.TabIndex = 38;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(833, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Resp:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(557, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "CAPTCHA Answer:";
            // 
            // txtCAPTCHAAnswer
            // 
            this.txtCAPTCHAAnswer.Location = new System.Drawing.Point(661, 19);
            this.txtCAPTCHAAnswer.Name = "txtCAPTCHAAnswer";
            this.txtCAPTCHAAnswer.Size = new System.Drawing.Size(150, 20);
            this.txtCAPTCHAAnswer.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(272, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Password:";
            // 
            // txtCardBalance
            // 
            this.txtCardBalance.Location = new System.Drawing.Point(874, 46);
            this.txtCardBalance.Name = "txtCardBalance";
            this.txtCardBalance.Size = new System.Drawing.Size(59, 20);
            this.txtCardBalance.TabIndex = 25;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(334, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 20);
            this.txtPassword.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Login:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Card PIN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Card #:";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(334, 19);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(150, 20);
            this.txtLogin.TabIndex = 23;
            // 
            // txtCardPIN
            // 
            this.txtCardPIN.Location = new System.Drawing.Point(67, 45);
            this.txtCardPIN.Name = "txtCardPIN";
            this.txtCardPIN.Size = new System.Drawing.Size(150, 20);
            this.txtCardPIN.TabIndex = 22;
            // 
            // cmdRunRequest
            // 
            this.cmdRunRequest.Location = new System.Drawing.Point(834, 17);
            this.cmdRunRequest.Name = "cmdRunRequest";
            this.cmdRunRequest.Size = new System.Drawing.Size(100, 23);
            this.cmdRunRequest.TabIndex = 0;
            this.cmdRunRequest.Text = "Run Request";
            this.cmdRunRequest.UseVisualStyleBackColor = true;
            this.cmdRunRequest.Click += new System.EventHandler(this.cmdRunRequest_Click);
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(67, 19);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(150, 20);
            this.txtCardNumber.TabIndex = 21;
            // 
            // tmrTimeout
            // 
            this.tmrTimeout.Interval = 1000;
            this.tmrTimeout.Tick += new System.EventHandler(this.tmrTimeout_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.cmdGo);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtAppStaticDBPath);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtTestRqRsPath);
            this.groupBox2.Controls.Add(this.chkNeverAutoExit);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtCAPTCHAPath);
            this.groupBox2.Controls.Add(this.cmdSave);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTimeout);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtBaseURL);
            this.groupBox2.Location = new System.Drawing.Point(11, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(947, 79);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(626, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 13);
            this.label12.TabIndex = 55;
            this.label12.Text = "AppStatic DB Path:";
            // 
            // txtAppStaticDBPath
            // 
            this.txtAppStaticDBPath.Location = new System.Drawing.Point(733, 48);
            this.txtAppStaticDBPath.Name = "txtAppStaticDBPath";
            this.txtAppStaticDBPath.Size = new System.Drawing.Size(200, 20);
            this.txtAppStaticDBPath.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "TestRqRs Path:";
            // 
            // txtTestRqRsPath
            // 
            this.txtTestRqRsPath.Location = new System.Drawing.Point(413, 45);
            this.txtTestRqRsPath.Name = "txtTestRqRsPath";
            this.txtTestRqRsPath.Size = new System.Drawing.Size(200, 20);
            this.txtTestRqRsPath.TabIndex = 52;
            // 
            // chkNeverAutoExit
            // 
            this.chkNeverAutoExit.AutoSize = true;
            this.chkNeverAutoExit.Location = new System.Drawing.Point(645, 18);
            this.chkNeverAutoExit.Name = "chkNeverAutoExit";
            this.chkNeverAutoExit.Size = new System.Drawing.Size(100, 17);
            this.chkNeverAutoExit.TabIndex = 51;
            this.chkNeverAutoExit.Text = "Never Auto Exit";
            this.chkNeverAutoExit.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "CAPTCHA Path:";
            // 
            // txtCAPTCHAPath
            // 
            this.txtCAPTCHAPath.Location = new System.Drawing.Point(101, 45);
            this.txtCAPTCHAPath.Name = "txtCAPTCHAPath";
            this.txtCAPTCHAPath.Size = new System.Drawing.Size(211, 20);
            this.txtCAPTCHAPath.TabIndex = 49;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(802, 18);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(132, 23);
            this.cmdSave.TabIndex = 48;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(515, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Timeout:";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(569, 19);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(44, 20);
            this.txtTimeout.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Base URL:";
            // 
            // txtBaseURL
            // 
            this.txtBaseURL.Location = new System.Drawing.Point(101, 19);
            this.txtBaseURL.Name = "txtBaseURL";
            this.txtBaseURL.Size = new System.Drawing.Size(368, 20);
            this.txtBaseURL.TabIndex = 44;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(901, 628);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(60, 20);
            this.txtStatus.TabIndex = 27;
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
            // tmrCopyPaste
            // 
            this.tmrCopyPaste.Tick += new System.EventHandler(this.tmrCopyPaste_Tick);
            // 
            // txtMaxProg
            // 
            this.txtMaxProg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxProg.Location = new System.Drawing.Point(901, 653);
            this.txtMaxProg.Name = "txtMaxProg";
            this.txtMaxProg.Size = new System.Drawing.Size(60, 20);
            this.txtMaxProg.TabIndex = 28;
            // 
            // tmrGetCAPTCHA
            // 
            this.tmrGetCAPTCHA.Tick += new System.EventHandler(this.tmrGetCAPTCHA_Tick);
            // 
            // tmrPageDone
            // 
            this.tmrPageDone.Interval = 500;
            this.tmrPageDone.Tick += new System.EventHandler(this.tmrPageDone_Tick);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(866, 631);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Curr:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(865, 656);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Max:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(750, 631);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "Is Busy:";
            // 
            // txtIsBusy
            // 
            this.txtIsBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIsBusy.Location = new System.Drawing.Point(800, 628);
            this.txtIsBusy.Name = "txtIsBusy";
            this.txtIsBusy.Size = new System.Drawing.Size(60, 20);
            this.txtIsBusy.TabIndex = 31;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(765, 656);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Cnrt:";
            // 
            // txtCntr
            // 
            this.txtCntr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCntr.Location = new System.Drawing.Point(800, 653);
            this.txtCntr.Name = "txtCntr";
            this.txtCntr.Size = new System.Drawing.Size(60, 20);
            this.txtCntr.TabIndex = 33;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 673);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtCntr);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtIsBusy);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtMaxProg);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Main";
            this.Text = "Balance Extractor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrRunning;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCardBalance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtCardPIN;
        private System.Windows.Forms.Button cmdRunRequest;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Timer tmrTimeout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAdditionalParam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCAPTCHAAnswer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkNeverAutoExit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCAPTCHAPath;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaseURL;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Timer tmrResponseHandler;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTestRqRsPath;
        private System.Windows.Forms.Timer tmrDelay;
        private System.Windows.Forms.Timer tmrCopyPaste;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAppStaticDBPath;
        private System.Windows.Forms.TextBox txtMaxProg;
        private System.Windows.Forms.Timer tmrGetCAPTCHA;
        private System.Windows.Forms.Timer tmrPageDone;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtIsBusy;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCntr;
        private System.Windows.Forms.Button cmdGo;
    }
}

