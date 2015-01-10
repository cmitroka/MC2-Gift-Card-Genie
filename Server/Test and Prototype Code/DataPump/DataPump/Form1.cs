using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GCGCommon;
namespace DataPump
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void LoadSettings()
        {
            StreamReader sr = null;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                txtAppStaticDBPath.Text = MR.Read("APPSTATICDBPATH");
                GCGCommon.DB db = new GCGCommon.DB(txtAppStaticDBPath.Text);
                string[][] setting = db.GetMultiValuesOfSQL("SELECT URL FROM tblMerchants WHERE [DataPump]=true");
                int index = 0;
                do
                {
                    string temp = setting[index][0];
                    if (temp.Length<1) break;
                    ListViewItem lvi = new ListViewItem(temp);
                    listView1.Items.Add(temp);
                    index++;
                } while (true);

                //sr = new StreamReader(Application.StartupPath + "\\" + AppName + "_cfg.txt");
                //chkNeverAutoExit.Checked = Convert.ToBoolean(sr.ReadLine());
                sr.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                sr.Close();
            }
            catch (Exception ex1) { }
        }

    }
}
