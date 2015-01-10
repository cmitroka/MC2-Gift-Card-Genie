namespace PhysicalInput
{
    partial class Form1
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
            this.cmdSetFocusTest = new System.Windows.Forms.Button();
            this.monitor = new System.Windows.Forms.Timer(this.components);
            this.txtx = new System.Windows.Forms.TextBox();
            this.txty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrAutorun = new System.Windows.Forms.Timer(this.components);
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdSetFocusTest
            // 
            this.cmdSetFocusTest.Location = new System.Drawing.Point(9, 10);
            this.cmdSetFocusTest.Name = "cmdSetFocusTest";
            this.cmdSetFocusTest.Size = new System.Drawing.Size(110, 23);
            this.cmdSetFocusTest.TabIndex = 3;
            this.cmdSetFocusTest.Text = "Set Focus Test";
            this.cmdSetFocusTest.UseVisualStyleBackColor = true;
            this.cmdSetFocusTest.Click += new System.EventHandler(this.cmdSetFocusTest_Click);
            // 
            // monitor
            // 
            this.monitor.Enabled = true;
            this.monitor.Tick += new System.EventHandler(this.monitor_Tick);
            // 
            // txtx
            // 
            this.txtx.Location = new System.Drawing.Point(10, 63);
            this.txtx.Name = "txtx";
            this.txtx.Size = new System.Drawing.Size(55, 20);
            this.txtx.TabIndex = 4;
            // 
            // txty
            // 
            this.txty.Location = new System.Drawing.Point(71, 63);
            this.txty.Name = "txty";
            this.txty.Size = new System.Drawing.Size(48, 20);
            this.txty.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "SHIFT = +, CTRL = ^, ALT = %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Maximize = alt + spacebar and then X.";
            // 
            // tmrAutorun
            // 
            this.tmrAutorun.Enabled = true;
            this.tmrAutorun.Interval = 1;
            this.tmrAutorun.Tick += new System.EventHandler(this.tmrAutorun_Tick);
            // 
            // txtFocus
            // 
            this.txtFocus.Location = new System.Drawing.Point(126, 11);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Size = new System.Drawing.Size(251, 20);
            this.txtFocus.TabIndex = 8;
            this.txtFocus.Text = "Jasc Paint Shop Pro - Image1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(160, 63);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(217, 78);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "DoWait(1000)\r\nDoLeftClick(124, 887)\r\nDoWait(1000)\r\nDoTyping(% X)\r\nDoTyping(This i" +
                "s a test)\r\nDoLeftClickDown(272,147)\r\nDoLeftClickUp(611, 147)\r\nDoTyping(^a)\r\nDoTy" +
                "ping(^c)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 249);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtFocus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txty);
            this.Controls.Add(this.txtx);
            this.Controls.Add(this.cmdSetFocusTest);
            this.Name = "Form1";
            this.Text = "GCG Macro Player";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSetFocusTest;
        private System.Windows.Forms.Timer monitor;
        private System.Windows.Forms.TextBox txtx;
        private System.Windows.Forms.TextBox txty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrAutorun;
        private System.Windows.Forms.TextBox txtFocus;
        private System.Windows.Forms.TextBox textBox1;
    }
}

