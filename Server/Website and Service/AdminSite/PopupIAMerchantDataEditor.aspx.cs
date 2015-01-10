using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupIAMerchantDataEditor : SecurePage
    {
        static bool OK;
        protected void Page_Load(object sender, EventArgs e)
        {
            OK = false;
            if (Request.QueryString["IDIn"] == "-1")
            {
                DetailsView1.DefaultMode = DetailsViewMode.Insert;
            }
        }
        protected void AccessDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            OK = true;
        }
        protected void AccessDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            OK = true;
        }
        protected void AccessDataSource1_Unload(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(OK.ToString());
            if (OK == true)
            {
                Server.Transfer("PopupCloser.aspx");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "ABC", CloseAndRefresh("MerchantsData.aspx"));
            }
        }
        private string CloseAndRefresh()
        {
            return "<Script>window.opener.location.reload();window.close();</Script>";
        }
        private string CloseAndRefresh(string NavigateTo)
        {
            return "<Script>window.opener.location.href='" + NavigateTo + "';window.close();</Script>";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int OK = 1;
            Control c = DetailsView1.FindControl("IDLabel");
            Label l = (Label)c;
            if (l.Text == "")
            {
            }
            else
            {
                AccessDataSource1.DeleteCommand = "DELETE FROM tblMerchants WHERE ID=" + l.Text;
                OK = AccessDataSource1.Delete();
            }
            if (OK == 1)
            {
                Server.Transfer("PopupCloser.aspx");
            }
            System.Diagnostics.Debug.WriteLine(OK.ToString());
        }

    }
}