using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class UUIDHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UUID = "";
            try 
	        {
                UUID = Page.Request["UUID"].ToString();		
	        }
	        catch (Exception ex)
	        {
	        }

            Button1.OnClientClick = "window.open('PopupAddMessage.aspx?UUIDIn=" + UUID +"', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=650,height=20');";
        }
    }
}