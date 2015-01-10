namespace DataProtector
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
            this.cmdReadData = new System.Windows.Forms.Button();
            this.cmdDecryptFiles = new System.Windows.Forms.Button();
            this.txtMDBLoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEncFileLoc = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cmdReadData
            // 
            this.cmdReadData.Location = new System.Drawing.Point(15, 62);
            this.cmdReadData.Name = "cmdReadData";
            this.cmdReadData.Size = new System.Drawing.Size(181, 44);
            this.cmdReadData.TabIndex = 0;
            this.cmdReadData.Text = "Run";
            this.cmdReadData.UseVisualStyleBackColor = true;
            this.cmdReadData.Click += new System.EventHandler(this.cmdReadData_Click);
            // 
            // cmdDecryptFiles
            // 
            this.cmdDecryptFiles.Location = new System.Drawing.Point(399, 62);
            this.cmdDecryptFiles.Name = "cmdDecryptFiles";
            this.cmdDecryptFiles.Size = new System.Drawing.Size(181, 44);
            this.cmdDecryptFiles.TabIndex = 1;
            this.cmdDecryptFiles.Text = "Decrypt Files";
            this.cmdDecryptFiles.UseVisualStyleBackColor = true;
            this.cmdDecryptFiles.Click += new System.EventHandler(this.cmdDecryptFiles_Click);
            // 
            // txtMDBLoc
            // 
            this.txtMDBLoc.Location = new System.Drawing.Point(125, 10);
            this.txtMDBLoc.Name = "txtMDBLoc";
            this.txtMDBLoc.Size = new System.Drawing.Size(455, 20);
            this.txtMDBLoc.TabIndex = 2;
            this.txtMDBLoc.Text = "C:\\GCG\\Gift Card Genie\\App_Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "MDB Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enc File Location";
            // 
            // txtEncFileLoc
            // 
            this.txtEncFileLoc.Location = new System.Drawing.Point(125, 36);
            this.txtEncFileLoc.Name = "txtEncFileLoc";
            this.txtEncFileLoc.Size = new System.Drawing.Size(455, 20);
            this.txtEncFileLoc.TabIndex = 4;
            this.txtEncFileLoc.Text = "C:\\Users\\chmitro\\Desktop\\EncodedFiles";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 113);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEncFileLoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMDBLoc);
            this.Controls.Add(this.cmdDecryptFiles);
            this.Controls.Add(this.cmdReadData);
            this.Name = "Form1";
            this.Text = "End Dec Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdReadData;
        private System.Windows.Forms.Button cmdDecryptFiles;
        private System.Windows.Forms.TextBox txtMDBLoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEncFileLoc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

