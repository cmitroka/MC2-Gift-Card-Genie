namespace DataPump
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
            this.label12 = new System.Windows.Forms.Label();
            this.txtAppStaticDBPath = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.cmdFunction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.URL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "AppStatic DB Path:";
            // 
            // txtAppStaticDBPath
            // 
            this.txtAppStaticDBPath.Enabled = false;
            this.txtAppStaticDBPath.Location = new System.Drawing.Point(121, 12);
            this.txtAppStaticDBPath.Name = "txtAppStaticDBPath";
            this.txtAppStaticDBPath.Size = new System.Drawing.Size(516, 20);
            this.txtAppStaticDBPath.TabIndex = 56;
            this.txtAppStaticDBPath.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.URL});
            this.listView1.Location = new System.Drawing.Point(12, 77);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(625, 97);
            this.listView1.TabIndex = 58;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 181);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(625, 100);
            this.webBrowser1.TabIndex = 59;
            // 
            // cmdFunction
            // 
            this.cmdFunction.Location = new System.Drawing.Point(278, 290);
            this.cmdFunction.Name = "cmdFunction";
            this.cmdFunction.Size = new System.Drawing.Size(75, 23);
            this.cmdFunction.TabIndex = 60;
            this.cmdFunction.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Load Time:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(91, 20);
            this.textBox1.TabIndex = 61;
            this.textBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Cycle Time:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(244, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(91, 20);
            this.textBox2.TabIndex = 63;
            this.textBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // URL
            // 
            this.URL.Text = "URL";
            this.URL.Width = 500;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 325);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmdFunction);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtAppStaticDBPath);
            this.Name = "Form1";
            this.Text = "DataPump";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAppStaticDBPath;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button cmdFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader URL;
    }
}

