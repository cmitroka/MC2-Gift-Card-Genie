using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class Metrics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLHelper s = null;
            try
            {
                s = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
                int temp1 = 0;
                int temp2 = 0;
                string[][] temp;
                string yesterday = DateTime.Today.AddDays(-1).ToShortDateString();
                string now = DateTime.Now.ToShortDateString();
                string tomorrow = DateTime.Today.AddDays(1).ToShortDateString();
                string t2 = DateTime.Today.ToString();
                temp = s.GetMultiValuesOfSQL("SELECT Amount FROM tblHistoricalData WHERE HistType='App Downloads'");
                temp1 = Convert.ToInt32(temp[0][0]);
                temp = s.GetMultiValuesOfSQL("SELECT Count(UDID) AS CountOfUDID1 FROM qryTopUsers");
                temp2 = Convert.ToInt32(temp[0][0]);
                temp1 = temp1 + temp2;
                lblTotalAppDownloads.Text = temp1.ToString();
                temp = s.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM tblUserLog WHERE TimeLogged>@P0 and UDID<>'WebsiteRequest'", t2);
                lblLogins.Text = temp[0][0];
                temp = s.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>@P0 and UDID<>'WebsiteRequest'", t2);
                lblRequests.Text = temp[0][0];
                temp = s.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>@P0 and UDID<>'WebsiteRequest' and ResponseType='GCBALANCE'", t2);
                lblSuccesses.Text = temp[0][0];
                string strtemp = (Convert.ToInt16(lblRequests.Text) - Convert.ToInt16(lblSuccesses.Text)).ToString();
                lblFailures.Text = strtemp;
            }
            catch (Exception)
            {
            }
            s.CloseIt();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SQLHelper s = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
                int st;
                //My Phone: 8BB2A5E9-FC0B-464F-8536-F24157FC4F4D, WebsiteRequest, My Mac:
                string InClause = "'8BB2A5E9-FC0B-464F-8536-F24157FC4F4D','WebsiteRequest','8D9E77C5-C41F-472F-945A-F30FB646AC54'";
                st = s.ExecuteSQLParamed("DELETE FROM tblUserLog WHERE UDID IN (" + InClause + ")");
                if (st == -1) System.Diagnostics.Debug.WriteLine("SQL Error");
                st = s.ExecuteSQLParamed("DELETE FROM tblResponses WHERE UDID IN (" + InClause + ")");
                st = s.ExecuteSQLParamed("DELETE FROM tblNewRequests WHERE UDID IN (" + InClause + ")");
                st = s.ExecuteSQLParamed("DELETE FROM tblContinueRequest WHERE UUID IN (" + InClause + ")");
            }
            catch (Exception)
            {
                //popup the restarter...
            }
        }
    }
}