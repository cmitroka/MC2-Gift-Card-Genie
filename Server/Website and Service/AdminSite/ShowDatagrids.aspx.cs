using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAdminSite
{
    public partial class ShowDatagrids : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["DateMinusOne"] = DateTime.Today.AddDays(-1);
            Session["DateMinusSeven"] = DateTime.Today.AddDays(-7);
            AppAdminSite.WebService GCWS = new AppAdminSite.WebService();
            //com.mc2techservices.gcg.WebService GCWS = new com.mc2techservices.gcg.WebService();
            //string retVal = GCWS.GetDownloadCount();
            //Literal1.Text=retVal;
            string test="";
            try 
	        {	        
                test=Page.Request["CardType"].ToString();
                AccessDataSource1.SelectParameters.Add("CardType",test);
                AccessDataSource1.SelectCommand = "SELECT * FROM [qryReportRqRsResults] WHERE CardType=@CardType ORDER BY [TimeLogged] desc";		
	        }
	        catch (Exception ex)
	        {
		
	        }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected string ValidateString(object String)
        {
            string retVal = "";
            if ((String.ToString().Length > 10))
            {
                retVal= String.ToString().Substring(0, 10) + " ..."; 
            }
            else
            {
                retVal= String.ToString();
            }
            return retVal;
        }
        protected string MDBPath()
        {
            string retVal = "";
            retVal = "~/App_Data/App.mdb";
            return retVal;
        }

        protected void AccessDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
        }


    }
}