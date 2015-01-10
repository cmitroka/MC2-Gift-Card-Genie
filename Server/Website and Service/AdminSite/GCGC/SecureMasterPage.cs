using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GCGC
{
    public class SecureMasterPage : System.Web.UI.MasterPage
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
                AuthTest = GCGCommon.SupportMethods.RetSessionVal("Authenticated");
            }
            if (AuthTest == "")
            {
                if (Request.Url.ToString().Contains("Default.aspx") == false) Response.Redirect("Default.aspx");
            }
            base.OnLoad(e);
        }
    }
}