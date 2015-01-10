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
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdReloadRequest = new System.Windows.Forms.Button();
            this.txtForceExit = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTimeoutMonitor = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtRetryCntr = new System.Windows.Forms.TextBox();
            this.cmdForceExit = new System.Windows.Forms.Button();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSupportCode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCleanName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.txtBaseURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtRqRsPath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCAPTCHAPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tmrRunning = new System.Windows.Forms.Timer(this.components);
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.tmrResponseHandler = new System.Windows.Forms.Timer(this.components);
            this.tmrDelay = new System.Windows.Forms.Timer(this.components);
            this.tmrSendKeys = new System.Windows.Forms.Timer(this.components);
            this.tmrGetCAPTCHA = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(967, 421);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(959, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Execution";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.cmdUpdate);
            this.groupBox3.Controls.Add(this.cmdReloadRequest);
            this.groupBox3.Controls.Add(this.txtForceExit);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtTimeoutMonitor);
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Controls.Add(this.txtRetryCntr);
            this.groupBox3.Controls.Add(this.cmdForceExit);
            this.groupBox3.Controls.Add(this.cmdLogOut);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(0, 331);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(950, 61);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(844, 33);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(100, 21);
            this.cmdUpdate.TabIndex = 55;
            this.cmdUpdate.TabStop = false;
            this.cmdUpdate.Text = "Update Data";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdReloadRequest
            // 
            this.cmdReloadRequest.Location = new System.Drawing.Point(844, 10);
            this.cmdReloadRequest.Name = "cmdReloadRequest";
            this.cmdReloadRequest.Size = new System.Drawing.Size(100, 21);
            this.cmdReloadRequest.TabIndex = 54;
            this.cmdReloadRequest.TabStop = false;
            this.cmdReloadRequest.Text = "Reload Data";
            this.cmdReloadRequest.UseVisualStyleBackColor = true;
            this.cmdReloadRequest.Click += new System.EventHandler(this.cmdReloadRequest_Click);
            // 
            // txtForceExit
            // 
            this.txtForceExit.Location = new System.Drawing.Point(619, 34);
            this.txtForceExit.Name = "txtForceExit";
            this.txtForceExit.Size = new System.Drawing.Size(220, 20);
            this.txtForceExit.TabIndex = 53;
            this.txtForceExit.TabStop = false;
            this.txtForceExit.Text = "Please try again.";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(741, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 52;
            this.label17.Text = "Timeout:";
            // 
            // txtTimeoutMonitor
            // 
            this.txtTimeoutMonitor.Location = new System.Drawing.Point(795, 10);
            this.txtTimeoutMonitor.Name = "txtTimeoutMonitor";
            this.txtTimeoutMonitor.Size = new System.Drawing.Size(44, 20);
            this.txtTimeoutMonitor.TabIndex = 51;
            this.txtTimeoutMonitor.TabStop = false;
            this.txtTimeoutMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLog.Location = new System.Drawing.Point(6, 10);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(521, 45);
            this.txtLog.TabIndex = 38;
            this.txtLog.TabStop = false;
            // 
            // txtRetryCntr
            // 
            this.txtRetryCntr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetryCntr.Location = new System.Drawing.Point(683, 11);
            this.txtRetryCntr.Name = "txtRetryCntr";
            this.txtRetryCntr.Size = new System.Drawing.Size(44, 20);
            this.txtRetryCntr.TabIndex = 47;
            this.txtRetryCntr.TabStop = false;
            this.txtRetryCntr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdForceExit
            // 
            this.cmdForceExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdForceExit.Location = new System.Drawing.Point(533, 33);
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
            this.cmdLogOut.Location = new System.Drawing.Point(533, 10);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(80, 21);
            this.cmdLogOut.TabIndex = 49;
            this.cmdLogOut.Text = "View Log";
            this.cmdLogOut.UseVisualStyleBackColor = true;
            this.cmdLogOut.Click += new System.EventHandler(this.cmdViewLog_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(620, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 48;
            this.label16.Text = "Rerty Cnrt:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSupportCode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtCleanName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTimeout);
            this.groupBox1.Controls.Add(this.txtBaseURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
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
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(950, 102);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // cmbSupportCode
            // 
            this.cmbSupportCode.FormattingEnabled = true;
            this.cmbSupportCode.Items.AddRange(new object[] {
            "",
            "Supported",
            "Unsupported"});
            this.cmbSupportCode.Location = new System.Drawing.Point(712, 45);
            this.cmbSupportCode.Name = "cmbSupportCode";
            this.cmbSupportCode.Size = new System.Drawing.Size(100, 21);
            this.cmbSupportCode.TabIndex = 61;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(670, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "Clean Name:";
            // 
            // txtCleanName
            // 
            this.txtCleanName.Location = new System.Drawing.Point(744, 76);
            this.txtCleanName.Name = "txtCleanName";
            this.txtCleanName.Size = new System.Drawing.Size(190, 20);
            this.txtCleanName.TabIndex = 59;
            this.txtCleanName.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(714, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Timeout:";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(768, 19);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(44, 20);
            this.txtTimeout.TabIndex = 57;
            this.txtTimeout.TabStop = false;
            // 
            // txtBaseURL
            // 
            this.txtBaseURL.Location = new System.Drawing.Point(67, 76);
            this.txtBaseURL.Name = "txtBaseURL";
            this.txtBaseURL.Size = new System.Drawing.Size(477, 20);
            this.txtBaseURL.TabIndex = 50;
            this.txtBaseURL.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Base URL:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(565, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 41;
            this.button1.TabStop = false;
            this.button1.Text = "Go To Base URL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cmdGo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(455, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Additional Param:";
            // 
            // txtAdditionalParam
            // 
            this.txtAdditionalParam.Location = new System.Drawing.Point(550, 45);
            this.txtAdditionalParam.Name = "txtAdditionalParam";
            this.txtAdditionalParam.Size = new System.Drawing.Size(150, 20);
            this.txtAdditionalParam.TabIndex = 38;
            this.txtAdditionalParam.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(834, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Resp:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(446, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "CAPTCHA Answer:";
            // 
            // txtCAPTCHAAnswer
            // 
            this.txtCAPTCHAAnswer.Location = new System.Drawing.Point(550, 19);
            this.txtCAPTCHAAnswer.Name = "txtCAPTCHAAnswer";
            this.txtCAPTCHAAnswer.Size = new System.Drawing.Size(150, 20);
            this.txtCAPTCHAAnswer.TabIndex = 30;
            this.txtCAPTCHAAnswer.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Password:";
            // 
            // txtCardBalance
            // 
            this.txtCardBalance.Location = new System.Drawing.Point(875, 49);
            this.txtCardBalance.Name = "txtCardBalance";
            this.txtCardBalance.Size = new System.Drawing.Size(59, 20);
            this.txtCardBalance.TabIndex = 25;
            this.txtCardBalance.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(289, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 20);
            this.txtPassword.TabIndex = 28;
            this.txtPassword.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 22);
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
            this.txtLogin.Location = new System.Drawing.Point(289, 19);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(150, 20);
            this.txtLogin.TabIndex = 23;
            this.txtLogin.TabStop = false;
            // 
            // txtCardPIN
            // 
            this.txtCardPIN.Location = new System.Drawing.Point(67, 45);
            this.txtCardPIN.Name = "txtCardPIN";
            this.txtCardPIN.Size = new System.Drawing.Size(150, 20);
            this.txtCardPIN.TabIndex = 22;
            this.txtCardPIN.TabStop = false;
            // 
            // cmdRunRequest
            // 
            this.cmdRunRequest.Location = new System.Drawing.Point(834, 17);
            this.cmdRunRequest.Name = "cmdRunRequest";
            this.cmdRunRequest.Size = new System.Drawing.Size(100, 23);
            this.cmdRunRequest.TabIndex = 0;
            this.cmdRunRequest.TabStop = false;
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
            this.txtCardNumber.TabStop = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 108);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(950, 217);
            this.webBrowser1.TabIndex = 37;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(959, 395);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Stuff";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtRqRsPath);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtCAPTCHAPath);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(937, 63);
            this.groupBox5.TabIndex = 54;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GCG Janitor (Common) Settings";
            // 
            // txtRqRsPath
            // 
            this.txtRqRsPath.Location = new System.Drawing.Point(407, 27);
            this.txtRqRsPath.Name = "txtRqRsPath";
            this.txtRqRsPath.Size = new System.Drawing.Size(211, 20);
            this.txtRqRsPath.TabIndex = 53;
            this.txtRqRsPath.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(339, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "RqRs Path:";
            // 
            // txtCAPTCHAPath
            // 
            this.txtCAPTCHAPath.Location = new System.Drawing.Point(103, 27);
            this.txtCAPTCHAPath.Name = "txtCAPTCHAPath";
            this.txtCAPTCHAPath.Size = new System.Drawing.Size(211, 20);
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
            // tmrResponseHandler
            // 
            this.tmrResponseHandler.Interval = 1;
            this.tmrResponseHandler.Tick += new System.EventHandler(this.tmrResponseHandler_Tick);
            // 
            // tmrDelay
            // 
            this.tmrDelay.Tick += new System.EventHandler(this.tmrDelay_Tick);
            // 
            // tmrSendKeys
            // 
            this.tmrSendKeys.Interval = 10;
            // 
            // tmrGetCAPTCHA
            // 
            this.tmrGetCAPTCHA.Tick += new System.EventHandler(this.tmrGetCAPTCHA_Tick);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 421);
            this.Controls.Add(this.tabControl1);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdForceExit;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtRetryCntr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCardBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdRunRequest;
        private System.Windows.Forms.Timer tmrTimeout;
        private System.Windows.Forms.Timer tmrDelay;
        private System.Windows.Forms.Timer tmrSendKeys;
        private System.Windows.Forms.Timer tmrGetCAPTCHA;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTimeoutMonitor;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtCAPTCHAPath;
        public System.Windows.Forms.TextBox txtRqRsPath;
        public System.Windows.Forms.TextBox txtLog;
        public System.Windows.Forms.TextBox txtAdditionalParam;
        public System.Windows.Forms.TextBox txtCAPTCHAAnswer;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.TextBox txtLogin;
        public System.Windows.Forms.TextBox txtCardPIN;
        public System.Windows.Forms.TextBox txtCardNumber;
        public System.Windows.Forms.Timer tmrRunning;
        public System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.TextBox txtBaseURL;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Timer tmrResponseHandler;
        private System.Windows.Forms.TextBox txtForceExit;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtCleanName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtTimeout;
        public System.Windows.Forms.ComboBox cmbSupportCode;
        public System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdReloadRequest;

    }
}

