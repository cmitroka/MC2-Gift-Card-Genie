using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AppAdminSite
{
    public partial class SupportedUnsupported : System.Web.UI.Page
    {
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);

        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AppSite.AppWebReference.WebService GCWS = new AppSite.AppWebReference.WebService();
            //AppWebService.WebService GCWS = new AppWebService.WebService();
            string allsupported=GCWS.GetMerchantStatuses("Supported");
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("CleanName");
            dt.Columns.Add(dc);

            string[] allsupportedarray = GCGCommon.SupportMethods.SplitByString(allsupported, POSDEL);
            foreach (string supported in allsupportedarray)
            {
                DataRow dr = dt.NewRow();
                dr["CleanName"] = supported;
                dt.Rows.Add(dr);                
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();


            string allunsupported = GCWS.GetMerchantStatuses("Development");
            DataTable dtu = new DataTable();
            DataColumn dcu = new DataColumn("CleanName");
            dtu.Columns.Add(dcu);

            string[] allunsupportedarray = GCGCommon.SupportMethods.SplitByString(allunsupported, POSDEL);
            foreach (string unsupported in allunsupportedarray)
            {
                DataRow dr = dtu.NewRow();
                dr["CleanName"] = unsupported;
                dtu.Rows.Add(dr);
            }
            GridView2.DataSource = dtu;
            GridView2.DataBind();

        
        }
    }
}