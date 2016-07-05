using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BarcodeGenerator
{
    public partial class BarcodeGenerator : Form
    {
        public BarcodeGenerator()
        {
            InitializeComponent();
            LoadConfig();
        }
        public BarcodeGenerator(string[] commands): this()
        {
            try
            {
                string AlphaNumerics = commands[0].ToString();
                string[] delim = { "=" };
                string[] pieces = AlphaNumerics.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                txtAlphanumerics.Text = pieces[1];
            }
            catch (Exception)
            {
            }
            tmrAutostart.Enabled = true;
        }


        private void cmdMakeBarcode_Click(object sender, EventArgs e)
        {
            ConvAlphanumericToBarcode();
        }

        private void cmdSaveIt_Click(object sender, EventArgs e)
        {
            SaveBarcode();
        }

        private void BarcodeGenerator_Load(object sender, EventArgs e)
        {
            if (tmrAutostart.Enabled == true)
            {
                if (chkInvisible.Checked == true)
                {
                    this.Location = new Point(5000, 5000);
                }
            }
        }
        private void SaveConfig()
        {
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\BarcodeGenerator";
            string chkInv = chkInvisible.Checked.ToString();
            MR.Write("chkInvisible", chkInv);
            MR.Write("txtSavePath", txtSavePath.Text);
        }
        private void ConvAlphanumericToBarcode()
        {
            barcode1.DataToEncode = txtAlphanumerics.Text;
        }
        private bool SaveBarcode()
        {
            bool retVal = false;
            barcode1.Resolution = IDAutomation.Windows.Forms.LinearBarCode.Resolutions.Custom;
            barcode1.ResolutionCustomDPI = 300; //Set the resolution
            barcode1.BarHeightCM = 1F;
            string savepath = txtSavePath.Text + "\\" + txtAlphanumerics.Text;
            try
            {
                StreamWriter sw = new StreamWriter(savepath);
                sw.Close();
            }
            catch (Exception ex)
            {
                GCGCommon.WindowsEventLog.WriteToWindowsEventLog("Barcode Generator", "Couldn't save <"+savepath+">; error is " + ex.Message,"E");
                return retVal;
            }
            do
            {
            try 
	            {	        
		            File.Delete(savepath);
	            }
	            catch (Exception ex1)
	            {
	            }
                
            } while (File.Exists(savepath));
            barcode1.SaveImageAs(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);

            Bitmap src = Image.FromFile(savepath) as Bitmap;
            Rectangle cropRect = new Rectangle(0, 20, src.Width, 150);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using(Graphics g = Graphics.FromImage(target))
            {
               g.DrawImage(src, new Rectangle(0,20, target.Width, target.Height),
                                cropRect,                        
                                GraphicsUnit.Pixel);
            }
            savepath = savepath = txtSavePath.Text + "\\" + txtAlphanumerics.Text + ".jpg";
            target.Save(savepath);
            return true;
        }
        private void LoadConfig()
        {
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\BarcodeGenerator";

            try { chkInvisible.Checked = Convert.ToBoolean(MR.Read("chkInvisible")); }
            catch (Exception e1) { }

            try { txtSavePath.Text= MR.Read("txtSavePath"); }
            catch (Exception e2) { }
            
        }

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void tmrAutostart_Tick(object sender, EventArgs e)
        {
            tmrAutostart.Enabled = false;
            this.Visible = false;
            ConvAlphanumericToBarcode();
            SaveBarcode();
            Application.Exit();
        }
    }
}
