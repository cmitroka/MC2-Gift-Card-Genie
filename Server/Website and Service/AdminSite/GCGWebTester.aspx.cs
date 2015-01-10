using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class GCGWebTester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GCGWebWS ws = new GCGWebWS();
            string resp = ws.LogPurchase(txtSession.Text, "100", "164","GCGWebTester");
        }
    }
}