using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace AppSite
{
    public partial class GCGArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string foldPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string RealMDBPath = CJMUtilities.File.GoBackADirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            //MDBPath = Environment.CurrentDirectory;
            //MDBPath=Directory.GetCurrentDirectory();
            System.IO.FileInfo[] files = null;
            DirectoryInfo di = new DirectoryInfo(foldPath+"\\GCG");
            DirectoryInfo[] diarr=di.GetDirectories();
            DataTable dt = new DataTable();
            DataColumn dctitle = new DataColumn("Version");
            dt.Columns.Add(dctitle);
            foreach (DirectoryInfo d in diarr)
            {
                if (d.Name.ToUpper().Contains("ANDROID"))
                {
                    DataRow dr = dt.NewRow();
                    dr["Version"] = "<a href=\"http://gcg.mc2techservices.com/GCG/" + d.Name + "/GCG3.apk\">" + d.Name + "</a>"; //GCG/" + x + "XXX" + ";
                    dt.Rows.Add(dr);
                }
                else
                {
                    string x = d.Name.ToString();
                    string y = "itms-services://?action=download-manifest&url=http://gcg.mc2techservices.com/GCG/" + x + "/gcg.plist";
                    string temp = "<a href=\"" + y + "\">" + x + "</a>"; //GCG/" + x + "XXX" + "
                    System.Diagnostics.Debug.WriteLine(x);
                    DataRow dr = dt.NewRow();
                    dr["Version"] = temp;
                    dt.Rows.Add(dr);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
    }
}