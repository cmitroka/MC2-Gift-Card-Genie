using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class PopupRequestError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblRsType.Text = Page.Request["rsType"]; ;
                lblRs.Text = Page.Request["rsVal"]; ;
            }
            catch (Exception)
            {
                lblRsType.Text = "No rsType";
                lblRs.Text = "No rsVal";
                
            }
        }
    }
}