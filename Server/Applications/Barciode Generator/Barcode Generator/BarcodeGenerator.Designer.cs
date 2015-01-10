namespace BarcodeGenerator
{
    partial class BarcodeGenerator
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
            this.barcode1 = new IDAutomation.Windows.Forms.LinearBarCode.Barcode();
            this.cmdMakeBarcode = new System.Windows.Forms.Button();
            this.txtAlphanumerics = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSaveIt = new System.Windows.Forms.Button();
            this.chkInvisible = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSaveSettings = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.tmrAutostart = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barcode1
            // 
            this.barcode1.ApplyTilde = true;
            this.barcode1.BarHeightCM = 1F;
            this.barcode1.BearerBarHorizontal = 0;
            this.barcode1.BearerBarVertical = 0;
            this.barcode1.CaptionAbove = "";
            this.barcode1.CaptionBelow = "";
            this.barcode1.CaptionBottomAlignment = System.Drawing.StringAlignment.Center;
            this.barcode1.CaptionBottomColor = System.Drawing.Color.Black;
            this.barcode1.CaptionBottomSpace = 0.1F;
            this.barcode1.CaptionFontAbove = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.barcode1.CaptionFontBelow = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.barcode1.CaptionTopAlignment = System.Drawing.StringAlignment.Center;
            this.barcode1.CaptionTopColor = System.Drawing.Color.Black;
            this.barcode1.CaptionTopSpace = 0.1F;
            this.barcode1.CharacterGrouping = 0;
            this.barcode1.CheckCharacter = false;
            this.barcode1.CheckCharacterInText = true;
            this.barcode1.CODABARStartChar = "A";
            this.barcode1.CODABARStopChar = "B";
            this.barcode1.Code128Set = IDAutomation.Windows.Forms.LinearBarCode.Code128CharacterSets.Auto;
            this.barcode1.DataToEncode = "123456789012";
            this.barcode1.DoPaint = true;
            this.barcode1.FitControlToBarcode = true;
            this.barcode1.LeftMarginCM = 0.2F;
            this.barcode1.Location = new System.Drawing.Point(12, 12);
            this.barcode1.Name = "barcode1";
            this.barcode1.NarrowToWideRatio = 2F;
            this.barcode1.OneBitPerPixelImage = false;
            this.barcode1.PostnetHeightShort = 0.127F;
            this.barcode1.PostnetHeightTall = 0.3226F;
            this.barcode1.PostnetSpacing = 0.064F;
            this.barcode1.Resolution = IDAutomation.Windows.Forms.LinearBarCode.Resolutions.Printer;
            this.barcode1.ResolutionCustomDPI = 96F;
            this.barcode1.ResolutionPrinterToUse = "";
            this.barcode1.RotationAngle = IDAutomation.Windows.Forms.LinearBarCode.RotationAngles.Zero_Degrees;
            this.barcode1.ShowText = true;
            this.barcode1.ShowTextLocation = IDAutomation.Windows.Forms.LinearBarCode.HRTextPositions.Bottom;
            this.barcode1.Size = new System.Drawing.Size(197, 74);
            this.barcode1.SuppSeparationCM = 0.5F;
            this.barcode1.SymbologyID = IDAutomation.Windows.Forms.LinearBarCode.Symbologies.Code39;
            this.barcode1.TabIndex = 0;
            this.barcode1.TextFontColor = System.Drawing.Color.Black;
            this.barcode1.TextMarginCM = 0.1F;
            this.barcode1.TopMarginCM = 0.2F;
            this.barcode1.UPCESystem = "0";
            this.barcode1.WhiteBarIncrease = 0F;
            this.barcode1.XDimensionCM = 0.0298F;
            this.barcode1.XDimensionMILS = 11.7714F;
            // 
            // cmdMakeBarcode
            // 
            this.cmdMakeBarcode.Location = new System.Drawing.Point(338, 6);
            this.cmdMakeBarcode.Name = "cmdMakeBarcode";
            this.cmdMakeBarcode.Size = new System.Drawing.Size(133, 23);
            this.cmdMakeBarcode.TabIndex = 1;
            this.cmdMakeBarcode.Text = "Make Barcode";
            this.cmdMakeBarcode.UseVisualStyleBackColor = true;
            this.cmdMakeBarcode.Click += new System.EventHandler(this.cmdMakeBarcode_Click);
            // 
            // txtAlphanumerics
            // 
            this.txtAlphanumerics.Location = new System.Drawing.Point(223, 22);
            this.txtAlphanumerics.Name = "txtAlphanumerics";
            this.txtAlphanumerics.Size = new System.Drawing.Size(109, 20);
            this.txtAlphanumerics.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Alphanumerics:";
            // 
            // cmdSaveIt
            // 
            this.cmdSaveIt.Location = new System.Drawing.Point(338, 35);
            this.cmdSaveIt.Name = "cmdSaveIt";
            this.cmdSaveIt.Size = new System.Drawing.Size(133, 23);
            this.cmdSaveIt.TabIndex = 4;
            this.cmdSaveIt.Text = "Save Barcode";
            this.cmdSaveIt.UseVisualStyleBackColor = true;
            this.cmdSaveIt.Click += new System.EventHandler(this.cmdSaveIt_Click);
            // 
            // chkInvisible
            // 
            this.chkInvisible.AutoSize = true;
            this.chkInvisible.Location = new System.Drawing.Point(6, 19);
            this.chkInvisible.Name = "chkInvisible";
            this.chkInvisible.Size = new System.Drawing.Size(70, 17);
            this.chkInvisible.TabIndex = 5;
            this.chkInvisible.Text = "Invisible?";
            this.chkInvisible.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSaveSettings);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSavePath);
            this.groupBox1.Controls.Add(this.chkInvisible);
            this.groupBox1.Location = new System.Drawing.Point(12, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 70);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Settings";
            // 
            // cmdSaveSettings
            // 
            this.cmdSaveSettings.Location = new System.Drawing.Point(320, 12);
            this.cmdSaveSettings.Name = "cmdSaveSettings";
            this.cmdSaveSettings.Size = new System.Drawing.Size(133, 23);
            this.cmdSaveSettings.TabIndex = 8;
            this.cmdSaveSettings.Text = "Save Settings";
            this.cmdSaveSettings.UseVisualStyleBackColor = true;
            this.cmdSaveSettings.Click += new System.EventHandler(this.cmdSaveSettings_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Save Path:";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(72, 41);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(381, 20);
            this.txtSavePath.TabIndex = 6;
            // 
            // tmrAutostart
            // 
            this.tmrAutostart.Interval = 1;
            this.tmrAutostart.Tick += new System.EventHandler(this.tmrAutostart_Tick);
            // 
            // BarcodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 190);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSaveIt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAlphanumerics);
            this.Controls.Add(this.cmdMakeBarcode);
            this.Controls.Add(this.barcode1);
            this.Name = "BarcodeGenerator";
            this.Text = "Barcode Generator";
            this.Load += new System.EventHandler(this.BarcodeGenerator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IDAutomation.Windows.Forms.LinearBarCode.Barcode barcode1;
        private System.Windows.Forms.Button cmdMakeBarcode;
        private System.Windows.Forms.TextBox txtAlphanumerics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSaveIt;
        private System.Windows.Forms.CheckBox chkInvisible;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button cmdSaveSettings;
        private System.Windows.Forms.Timer tmrAutostart;
    }
}

