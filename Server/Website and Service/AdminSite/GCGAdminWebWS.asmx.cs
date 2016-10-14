using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Web.Script.Services;

/// <summary>
    /// Summary description for GCGCGAdminWebWS
    /// </summary>
    [WebService(Namespace = "gcg.mc2techservices.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GCGAdminWebWS : WebService
    {
        [WebMethod(Description = @"Will just return 'Hello'")]
        public string DiagSayHello()
        {
            return "Hello";
        }
        [WebMethod]
        public string GetPurchaseLog()
        {
            string retVal = "";
            SQLHelper sqlh = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT UUID, MinOfTimeLogged, MaxOfPurchaseType FROM qryPurchasedWType");
            if (CommonForWS.isDatasetBad(data)) return null;
            int max = data.Length;
            string template = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<tbody>");
            for (int i = 0; i < max; i++)
            {
                template = "";
                string UUID = data[i][0];
                string MinOfTimeLogged = data[i][1];
                string MaxOfPurchaseType = data[i][2];
                template = "" +
                "<tr><td>" + (max-i) + "</td><td>" + UUID + "</td><td>" + MinOfTimeLogged + " </td><td>" + MaxOfPurchaseType + "</td></tr>";
                sb.Append(template);
            }
            //sb.Append("</tbody>");
            retVal = sb.ToString();
            sqlh.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GetRqRsLog()
        {
            string retVal = "";
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            SQLHelper sqlh = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT TOP 250 CardType, CardNumber, PIN, ResponseType, Response, TimeLogged, TrueAmnt FROM qryReportRqRsResultsV01 ORDER BY TimeLogged DESC");
            if (CommonForWS.isDatasetBad(data)) return null;
            int max = data.Length;
            string template = "";
            string bgcolor="";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<tbody>");
            for (int i = 0; i < max; i++)
            {
                template = "";

                string CardType = data[i][0];
                string CardNumber = ""; // data[i][1];
                string PIN = ""; // data[i][2];
                string ResponseType= data[i][3];
                string Response= data[i][4];
                string TimeLogged= data[i][5];
                string TrueAmnt = data[i][6];
                if (CommonForWS.isNumberOdd(i))
                    {
                        bgcolor="rgb(238, 238, 238)";
                    }
                    else
                    {
                        bgcolor = "gainsboro";
                    }
                template = "" +
                "<tr style=\"color: black; background-color: " + bgcolor + ";\"><td>" + CardType + "</td><td>" + CardNumber + "</td><td>" + PIN + "</td><td>" + ResponseType + "</td><td align=\"center\">" + Response + "</td><td>" + TimeLogged + "</td><td>" + TrueAmnt + "</td></tr>";
                sb.Append(template);
            }
            //sb.Append("</tbody>");
            retVal = sb.ToString();
            //GCGCommon.SupportMethods.WriteFile("C:\\out.txt",retVal,true);
            sqlh.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GetMetricsSummary()
        {
            string retVal = "";
            SQLHelper sqlh = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
            string[][] temp = null;
            int temp1 = 0;
            int temp2 = 0;
            temp = sqlh.GetMultiValuesOfSQL("SELECT Amount FROM tblHistoricalData WHERE HistType='App Downloads'");
            temp1 = Convert.ToInt32(temp[0][0]);
            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS CountOfUDID1 FROM qryTopUsers");
            temp2 = Convert.ToInt32(temp[0][0]);
            temp1 = temp1 + temp2;

            string l24hrdate = DateTime.Now.AddDays(-1).ToString();
            string currdate = DateTime.Now.ToShortDateString();
            string p5date = DateTime.Now.AddDays(-5).ToString();
            

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS CountOfUDID1 FROM tblUserLog WHERE TimeLogged>=#" + currdate + "#");
            string pLoginsToday = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT CountForDate FROM qryDownloadsByDate WHERE MDYDate>=#" + currdate + "#");
            string pDownloadsToday = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>=#" + currdate + "# and UDID<>'WebsiteRequest'");
            string pRequestsToday = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>=#" + currdate + "# and UDID<>'WebsiteRequest' and ResponseType='GCBALANCE'");
            string pSuccessesToday = temp[0][0];

            string pFailuresToday = (Convert.ToInt16(pRequestsToday) - Convert.ToInt16(pSuccessesToday)).ToString();

            if (pRequestsToday == "0") pRequestsToday = "-1";
            decimal pPctT = Convert.ToDecimal(pSuccessesToday) / Convert.ToDecimal(pRequestsToday);
            string sPctToday = String.Format("{0:0%}", pPctT);

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UUID) AS DLs FROM tblPurchases WHERE TimeLogged>=#" + currdate + "#");
            string pPurchasesToday = temp[0][0];

            retVal = "<table border=\"1\" style=\"width: 100%;\"><tr><td colspan=\"2\"><h2>On " + currdate + "</h2></td></tr><tr><td>Logins</td><td>" + pLoginsToday + "</td></tr><tr><td>Downloads</td><td>" + pDownloadsToday + "</td></tr><tr><td>Requests</td><td>" + pRequestsToday + "</td></tr><tr><td>Successes</td><td>" + pSuccessesToday + "</td></tr><tr><td>Failure</td><td>" + pFailuresToday + "</td></tr><tr><td>Success Percentage</td><td>" + sPctToday + "</td></tr><tr><td>Purchases</td><td>" + pPurchasesToday + "</td></tr></table>";

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM tblUserLog WHERE TimeLogged>#" + l24hrdate + "# and UDID<>'WebsiteRequest'");
            string pLogins = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>#" + l24hrdate + "# and UDID<>'WebsiteRequest'");
            string pRequests = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>#" + l24hrdate + "# and UDID<>'WebsiteRequest' and ResponseType='GCBALANCE'");
            string pSuccesses = temp[0][0];

            string pFailures = (Convert.ToInt16(pRequests) - Convert.ToInt16(pSuccesses)).ToString();

            if (pRequests == "0") pRequests = "-1";
            decimal pPct = Convert.ToDecimal(pSuccesses) / Convert.ToDecimal(pRequests);
            string sPct = String.Format("{0:0%}", pPct);

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UUID) AS DLs FROM tblPurchases WHERE TimeLogged>=#" + l24hrdate + "#");
            string pPurchases = temp[0][0];

            retVal = retVal + "<table border=\"1\" style=\"width: 100%;\"><tr><td colspan=\"2\"><h2>In the Past 24 Hours</h2></td></tr><tr><td>Logins</td><td>" + pLogins + "</td></tr><tr><td>Requests</td><td>" + pRequests + "</td></tr><tr><td>Successes</td><td>" + pSuccesses + "</td></tr><tr><td>Failure</td><td>" + pFailures + "</td></tr><tr><td>Success Percentage</td><td>" + sPct + "</td></tr><tr><td>Purchases</td><td>" + pPurchases + "</td></tr></table>";

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM tblUserLog WHERE TimeLogged>#" + p5date + "# and UDID<>'WebsiteRequest'");
            string pLoginsP5 = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>#" + p5date + "# and UDID<>'WebsiteRequest'");
            string pRequestsP5 = temp[0][0];

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UDID) AS UDIDcnt FROM qryReportRqRsResults WHERE TimeLogged>#" + p5date + "# and UDID<>'WebsiteRequest' and ResponseType='GCBALANCE'");
            string pSuccessesP5 = temp[0][0];

            string pFailuresP5 = (Convert.ToInt16(pRequestsP5) - Convert.ToInt16(pSuccessesP5)).ToString();

            if (pRequestsP5 == "0") pRequestsP5 = "-1";
            decimal pPctP5 = Convert.ToDecimal(pSuccessesP5) / Convert.ToDecimal(pRequestsP5);
            string sPctP5 = String.Format("{0:0%}", pPctP5);

            temp = sqlh.GetMultiValuesOfSQL("SELECT Count(UUID) AS DLs FROM tblPurchases WHERE TimeLogged>=#" + p5date + "#");
            string pPurchasesP5 = temp[0][0];

            retVal = retVal + "<table border=\"1\" style=\"width: 100%;\"><tr><td colspan=\"2\"><h2>In the Past 5 Days</h2></td></tr><tr><td>Logins</td><td>" + pLoginsP5 + "</td></tr><tr><td>Requests</td><td>" + pRequestsP5 + "</td></tr><tr><td>Successes</td><td>" + pSuccessesP5 + "</td></tr><tr><td>Failure</td><td>" + pFailuresP5 + "</td></tr><tr><td>Success Percentage</td><td>" + sPctP5 + "</td></tr><tr><td>Purchases</td><td>" + pPurchasesP5 + "</td></tr></table>";

            int length = 0;
            string pDBD = "";
            temp = sqlh.GetMultiValuesOfSQL("SELECT TOP 5 * FROM qryDownloadsByDate");
            length = temp.Length;
            for (int i = 0; i < length; i++)
            {
                pDBD = pDBD + "<tr><td>" + temp[i][0] + "</td><td>" + temp[i][1] + "</td></tr>";
            }

            length = 0;
            string pPBD = "";
            temp = sqlh.GetMultiValuesOfSQL("SELECT TOP 5 * FROM qryPurchasesByDate");
            length = temp.Length;
            for (int i = 0; i < length; i++)
            {
                pPBD = pPBD + "<tr><td>" + temp[i][0] + "</td><td>" + temp[i][1] + "</td></tr>";
            }


            retVal = retVal + "<table border=\"1\" style=\"width: 100%;\"><tr><td><h2>Downloads in Past 5 Days</h2></td><td><h2>Purchases in Past 5 Days</h2></td></tr><tr><td><table border=\"1\" style=\"width: 100%;\">" + pDBD + "</table></td><td><table border=\"1\" style=\"width: 100%;\">" + pPBD + "</table></td></tr></table>";
            return retVal;
        }
    }
