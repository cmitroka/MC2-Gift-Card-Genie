using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GCGC
{
    public class SecurePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            string AuthTest = "";
            if (Request.Url.ToString().Contains("localhost"))
            {
                //AuthTest = CJMUtilities.WebAndNet.RetSessionVal("Authenticated");
                Session["Authenticated"]="true";
                AuthTest = "true";
            }
            else
            {
                AuthTest=GCGCommon.SupportMethods.RetSessionVal("Authenticated");
            }

            if (AuthTest == "")
            {
                if (Request.Url.ToString().Contains("PopupLogin.aspx") == false) Response.Redirect("PopupLogin.aspx");
            }
            foreach (Control ads in Page.Controls)
            {
                System.Diagnostics.Debug.WriteLine("A");
            }

            base.OnLoad(e);
        }
    }
}