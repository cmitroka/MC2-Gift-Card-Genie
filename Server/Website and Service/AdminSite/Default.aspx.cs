using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            //AppAdminSite.GCWebService GCWS = new AppAdminSite.GCWebService();
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService(); 
            bool retVal = GCWS.Login(txtLogin.Text, txtPassword.Text);
            if (retVal==true)
            {
                Session["Authenticated"]="true";
                Server.Transfer("Metrics.aspx");
            }
        }
    }
}