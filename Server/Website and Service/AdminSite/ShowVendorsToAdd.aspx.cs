using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class ShowVendorsToAdd : System.Web.UI.Page
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[4].Text); //UUID
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[5].Text); //Merc
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[6].Text); //JEXEMISSING
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[7].Text); //Card No
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[8].Text); //PIN
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[index].Cells[9].Text); //Time Logged
            string UUID = (GridView1.Rows[index].Cells[4].Text);
            string Merch = (GridView1.Rows[index].Cells[5].Text);
            string Url = (GridView1.Rows[index].Cells[6].Text);
            string CardNo = (GridView1.Rows[index].Cells[7].Text);
            string CardPIN = (GridView1.Rows[index].Cells[8].Text);
            string TimeLogged = (GridView1.Rows[index].Cells[9].Text);
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            GCWS.ArchiveNewVendorRequest(UUID,Merch,Url,CardNo,CardPIN,TimeLogged);
        }
    }
}