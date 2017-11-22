using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GCG_Common_Registry_Config_Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
            int x = MR.ValueCount();
            int offset = 0;
            for (int i = 0; i < x; i++)
            {
                string[] keyValPair = MR.Read(i);

                offset = 10 + ((i + 1) * 30);
                Label lb = new Label();
                lb.Location = new System.Drawing.Point(10, offset);
                lb.Name = "label" + i;
                lb.Text = keyValPair[0];
                lb.Size = new System.Drawing.Size(100, 20);
                this.Controls.Add(lb);

                TextBox tb = new TextBox();
                tb.Location = new System.Drawing.Point(150, offset);
                tb.Name = "textBox"+i;
                tb.Text = keyValPair[1];
                tb.Size = new System.Drawing.Size(100, 20);
                this.Controls.Add(tb);
            }
            //string a = MR.Read("TestRqRsPath");
            //MR.Write("CAPTCHAPath", a);
        }
    }
}
