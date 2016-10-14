using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
namespace CJMApp
{
    public class CJMAppWSBL
    {
        SQLHelper sqlh;
        public string gloHacker;
        public CJMAppWSBL()
        {
            sqlh = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
        }
        private string BuildHTMLTable(string SQLQueryIn)
        {
            string retVal = "";
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet dataset = new DataSet();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            command.Connection = sqlh.aConnection;
            command.CommandText = SQLQueryIn;
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(dataset, "RandomData");
            }
            catch (OleDbException)
            {
                System.Console.WriteLine("!");
            }
            int columns = dataset.Tables[0].Columns.Count;
            sb.Append("<table border=\"1\"><tr>");
            for (int i = 0; i < columns; i++)
            {
                sb.Append("<td style=\"background-color: #C0C0C0\">" + dataset.Tables[0].Columns[i] + "</td>");
            }
            sb.Append("</tr>");
            int pos = 0;
            do
            {
                DataRow row = null;
                try
                {
                    row = dataset.Tables["RandomData"].Rows[pos];
                    sb.Append("<tr>");
                }
                catch (Exception)
                {
                    break;
                }
                for (int i = 0; i < columns; i++)
                {
                    string valueread = "";
                    valueread = row[i].ToString();
                    sb.Append("<td>" + valueread + "</td>");
                }
                sb.Append("</tr>");
                pos++;
            }
            while (1 == 1);
            sb.Append("</table>");
            sqlh.CloseIt();
            retVal = sb.ToString();
            return retVal;
        }
        private string BuildJQMTable(string SQLQueryIn)
        {
            string retVal = "";
            retVal = BuildJQMTable(SQLQueryIn, "");
            return retVal;
        }
        private string BuildJQMTable(string SQLQueryIn, string pCustomizationName)
        {
            string retVal = "";
            /*
            int loc0 = SQLQueryIn.IndexOf("FROM ", StringComparison.OrdinalIgnoreCase);
            string pTableOrQuery = SQLQueryIn.Substring(loc0 + 5, SQLQueryIn.Length - (loc0 + 5));
            loc0= pTableOrQuery.IndexOf(" WHERE ", StringComparison.OrdinalIgnoreCase);
            if (loc0>0)
            {
                pTableOrQuery = pTableOrQuery.Substring(0, loc0);
            }
            if (pCustomizationName == "")
                pCustomizationName = pTableOrQuery;
            */
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet dataset = new DataSet();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            command.Connection = sqlh.aConnection;
            command.CommandText = SQLQueryIn;
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(dataset, "RandomData");
            }
            catch (OleDbException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            int columns=dataset.Tables[0].Columns.Count;
            sb.Append("<table data-role=\"table\" data-mode=\"columntoggle\" class=\"ui-responsive\"><thead><tr>");
            for (int i = 0; i < columns; i++)
            {
                sb.Append(" <th data-priority=\"" + i + "\">"+ dataset.Tables[0].Columns[i] +"</th>");
            }
            sb.Append("</tr></thead><tbody>");
            int pos = 0;
            do
            {
                DataRow row = null;
                try
                {
                    row = dataset.Tables["RandomData"].Rows[pos];
                    sb.Append("<tr>");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    break;                                        
                }
                for (int i = 0; i < columns; i++)
                {
                    string valueread = "";
                    valueread=row[i].ToString();
                    valueread= TryCustomization(SQLQueryIn, i, valueread);
                    sb.Append("<td>" + valueread  + "</td>");
                }
                sb.Append("</tr>");
                pos++;
            }
            while (1==1);
            sb.Append("</tbody></table>");
            sqlh.CloseIt();
            retVal = sb.ToString();
            return retVal;
        }


        private string TryCustomization(string pCustomizationName, int pIndexToCust, string pInintialVal)
        {
            //"SELECT * FROM qryRptDownloadSpecifics"
            //"SELECT * FROM qryRptPurchasesSpecific"
            //"SELECT * FROM qryRptManualLookupLog"
            //"SELECT * FROM qryRptRqRsLog"
            //
            bool LinkUID = false;
            if (pCustomizationName.ToUpper() == "SELECT * FROM qryRptDownloadSpecifics".ToUpper())
            {
                LinkUID = true;
            }
            else if (pCustomizationName.ToUpper() == "SELECT * FROM qryRptPurchasesSpecific".ToUpper())
            {
                LinkUID = true;
            }
            else if (pCustomizationName.ToUpper() == "SELECT * FROM qryRptManualLookupLog".ToUpper())
            {
                LinkUID = true;
            }
            else if (pCustomizationName.ToUpper() == "SELECT * FROM qryRptRqRsLog".ToUpper())
            {
                LinkUID = true;
            }
            if (LinkUID==true)
            {
                if (pIndexToCust == 0)
                {
                    pInintialVal = "<a href=\"javascript: BuildUserReport00(" + pInintialVal + ", 'User Report')\">" + pInintialVal + "</a>";
                }
            }
            return pInintialVal;
        }

        private string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
        public string GetAppLaunches()
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM AllUsage ORDER BY DateLogged DESC");
            return retVal;
        }
        public string GetAppLaunchesForToday()
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM rptAppLaunchesToday ORDER BY DateLogged DESC");
            return retVal;
        }
        public string RunUserReport(string pUserID)
        {
            string retVal = "";
            retVal = "Downloaded:<BR>";
            retVal = retVal + BuildJQMTable("SELECT Channel,[Date] FROM qryRptDownloadSpecifics WHERE GCGUsersID=" + pUserID);
            retVal = retVal + "<HR><HR>";
            retVal = retVal + "Purchase Details:<BR>";
            retVal = retVal + BuildJQMTable("SELECT Download, PurchaseType, Channel, DateLogged FROM qryPurchasedSingular WHERE GCGUsersID=" + pUserID);
            retVal = retVal + "<HR><HR>";
            retVal = retVal + "Lookup History:<BR>";
            retVal = retVal + BuildJQMTable("SELECT CardType, ResponseType, Response, TimeLogged FROM qryReportRqRsResultsV01 WHERE GCGUsersID=" + pUserID);
            retVal = retVal + "<HR><HR>";
            retVal = retVal + "Login History:<BR>";
            retVal = retVal + BuildJQMTable("SELECT GCGLogin,Channel,LookupCount,DateLogged FROM qryLogins WHERE GCGUsersID=" + pUserID);
            return retVal;
        }
        public string GetDownloadInfo()
        {
            string retVal = "";
            retVal = "Downloads in last 5 days by OS:<BR>";
            retVal = retVal + BuildJQMTable("SELECT * FROM rptDownloadsInLast5Days");
            retVal = retVal + "Downloads in last 5 days:<BR>";
            retVal = retVal + BuildJQMTable("SELECT * FROM rptDownloadsInLast5DaysTotal");
            return retVal;

        }
        public string GetPurchaseInfo()
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM qryPurchases ORDER BY DateLogged DESC");
            return retVal;
        }
        public string GetUsersByOS()
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM rptUsersByOS");
            return retVal;
        }
        public string GetClicks()
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM qryAdsClicked");
            return retVal;
        }

        public string RunQuery(string pFromWhere)
        {
            string retVal = "";
            retVal = BuildJQMTable("SELECT * FROM " + pFromWhere);
            return retVal;
        }
        
        public void CloseIt()
        {
            try
            {
                sqlh.CloseIt();
            }
            catch (Exception ex)
            {
            }
        }
    }
}