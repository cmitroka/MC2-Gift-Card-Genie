namespace GCG_Janitor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLaunchVisibly = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSecsDoPurge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLocCAPTCHA = new System.Windows.Forms.TextBox();
            this.txtSecsCheckBatch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocBatchRqRs = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCurrentCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdToggle = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtErrorHandler = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrRunning
            // 
            this.tmrRunning.Interval = 1000;
            this.tmrRunning.Tick += new System.EventHandler(this.tmrRunning_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLaunchVisibly);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSecsDoPurge);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtLocCAPTCHA);
            this.groupBox1.Controls.Add(this.txtSecsCheckBatch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLocBatchRqRs);
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 130);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // txtLaunchVisibly
            // 
            this.txtLaunchVisibly.Location = new System.Drawing.Point(290, 97);
            this.txtLaunchVisibly.Name = "txtLaunchVisibly";
            this.txtLaunchVisibly.Size = new System.Drawing.Size(238, 20);
            this.txtLaunchVisibly.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Launch this visible (Example: 5 Guys, ALL):";
            // 
            // txtSecsDoPurge
            // 
            this.txtSecsDoPurge.Location = new System.Drawing.Point(463, 71);
            this.txtSecsDoPurge.Name = "txtSecsDoPurge";
            this.txtSecsDoPurge.Size = new System.Drawing.Size(65, 20);
            this.txtSecsDoPurge.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Purge files older than ??? seconds:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Location of CAPTCHA files";
            // 
            // txtLocCAPTCHA
            // 
            this.txtLocCAPTCHA.Location = new System.Drawing.Point(184, 45);
            this.txtLocCAPTCHA.Name = "txtLocCAPTCHA";
            this.txtLocCAPTCHA.Size = new System.Drawing.Size(344, 20);
            this.txtLocCAPTCHA.TabIndex = 8;
            // 
            // txtSecsCheckBatch
            // 
            this.txtSecsCheckBatch.Location = new System.Drawing.Point(184, 71);
            this.txtSecsCheckBatch.Name = "txtSecsCheckBatch";
            this.txtSecsCheckBatch.Size = new System.Drawing.Size(65, 20);
            this.txtSecsCheckBatch.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Check for files every ??? seconds:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Location of batch, rq, and rs files";
            // 
            // txtLocBatchRqRs
            // 
            this.txtLocBatchRqRs.Location = new System.Drawing.Point(184, 19);
            this.txtLocBatchRqRs.Name = "txtLocBatchRqRs";
            this.txtLocBatchRqRs.Size = new System.Drawing.Size(344, 20);
            this.txtLocBatchRqRs.TabIndex = 4;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(196, 24);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 32);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCurrentCount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 67);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Global Variables";
            // 
            // txtCurrentCount
            // 
            this.txtCurrentCount.Enabled = false;
            this.txtCurrentCount.Location = new System.Drawing.Point(12, 41);
            this.txtCurrentCount.Name = "txtCurrentCount";
            this.txtCurrentCount.Size = new System.Drawing.Size(120, 20);
            this.txtCurrentCount.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Timer for checking rq/rs:";
            // 
            // cmdToggle
            // 
            this.cmdToggle.Location = new System.Drawing.Point(335, 24);
            this.cmdToggle.Name = "cmdToggle";
            this.cmdToggle.Size = new System.Drawing.Size(120, 32);
            this.cmdToggle.TabIndex = 7;
            this.cmdToggle.Text = "trmRuning";
            this.cmdToggle.UseVisualStyleBackColor = true;
            this.cmdToggle.Click += new System.EventHandler(this.cmdToggle_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 266);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(538, 53);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RqRs Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 19);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(506, 20);
            this.txtLog.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtErrorHandler);
            this.groupBox4.Location = new System.Drawing.Point(12, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(538, 53);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Webserver Error Handler (/C iisreset)";
            // 
            // txtErrorHandler
            // 
            this.txtErrorHandler.Location = new System.Drawing.Point(12, 19);
            this.txtErrorHandler.Name = "txtErrorHandler";
            this.txtErrorHandler.Size = new System.Drawing.Size(506, 20);
            this.txtErrorHandler.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 331);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdToggle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "GCG Janitor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrRunning;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSecsCheckBatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocBatchRqRs;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCurrentCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSecsDoPurge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLocCAPTCHA;
        private System.Windows.Forms.Button cmdToggle;
        private System.Windows.Forms.TextBox txtLaunchVisibly;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtErrorHandler;
    }
}

