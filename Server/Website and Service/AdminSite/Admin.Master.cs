using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class AdminMaster : SecureMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (Control ads in Page.Controls)
            {
                System.Diagnostics.Debug.WriteLine(ads.ID);
            }
        }
    }
}
