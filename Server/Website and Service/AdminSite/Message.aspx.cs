using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["DateMinusOne"] = DateTime.Today.AddDays(-1);
            Session["DateMinusSeven"] = DateTime.Today.AddDays(-7);
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            //string retVal = GCWS.GetDownloadCount();
            //Literal1.Text=retVal;
        }

        protected void AccessDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["TimeLogged"].Value = DateTime.Now.ToString();
        }
        protected void GridView1_RowCommand(object sender,
           System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());

        }
    }
}