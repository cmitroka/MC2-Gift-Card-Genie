using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class DailyUUIDs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessDataSource1.SelectCommand = "SELECT * FROM [qryDailyUUIDs] WHERE ([ShortDate] = '" + DateTime.Now.ToShortDateString() + "') order by MaxOfTimeLogged desc";
        }
    }
}