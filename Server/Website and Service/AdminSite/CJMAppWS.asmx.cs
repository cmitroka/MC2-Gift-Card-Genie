using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace CJMApp
{
    /// <summary>
    /// Summary description for CJMAppWebService
    /// </summary>
    [WebService(Namespace = "CJMApp.mc2techservices.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CJMAppWS : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string MakeUserReport(string pUserID, string pUsername, string pPassword)
        {
            string retVal = "";
            if (BlockUser(pUsername, pPassword)) return "MakeUserReport Blocked";
            if (CredentialsOK(pUsername, pPassword))
                retVal = RunUserReport(pUserID);
            return retVal;
        }
        [WebMethod(EnableSession = true)]
        public string RunQuery(string pFromWhere, string pUsername, string pPassword)
        {
            string retVal = "";
            if (BlockUser(pUsername, pPassword)) return "RunQuery Blocked";
            if (CredentialsOK(pUsername, pPassword))
            {
                if (pFromWhere== "qryClicksPerDay" || pFromWhere == "qryClicksPerDaySpecifics")
                {
                    retVal = RunSecureQuery(pFromWhere, 1);
                }
                else
                {
                    retVal = RunSecureQuery(pFromWhere, 2);
                }
            }
            return retVal;
        }
        private string RunSecureQuery(string pFromWhere, int pDBVersion)
        {
            string retVal = "";
            CJMAppWSBL bl = new CJMAppWSBL(pDBVersion);
            retVal = bl.RunQuery(pFromWhere);
            bl.CloseIt();
            return retVal;
        }
        private string RunUserReport(string pUserID)
        {
            string retVal = "";
            CJMAppWSBL bl = new CJMAppWSBL(2);
            retVal = bl.RunUserReport(pUserID);
            bl.CloseIt();
            return retVal;
        }
        private bool CredentialsOK(string pUsername, string pPassword)
        {
            if ((pUsername.ToUpper() == "CJM") && (pPassword.ToUpper() == "GCGADMINPWD"))
            {
                return true;
            }
            return false;
        }
        private bool BlockUser(string pUsername, string pPassword)
        {
            UseSessionVars();
            string sVal = "0";
            int iVal = 0;
            try
            {
                string sTemp = HttpContext.Current.Session["Blocked"].ToString();
                if (sTemp == "1")
                {
                    return true;
                }
            }
            catch (Exception ex) { }

            if (CredentialsOK(pUsername,pPassword))
            {
                Session["Logins"] ="0";
                Session["Blocked"] = "0";
                return false;
            }
            else
            {
                try
                {
                    sVal = HttpContext.Current.Session["Logins"].ToString();
                }
                catch (Exception ex) { }
                iVal = Convert.ToInt16(sVal);
                iVal++;
                Session["Logins"] = iVal.ToString();
                if (iVal == 3)
                {
                    Session["Blocked"] = "1";
                }
            }
            return false;
        }
        private void UseSessionVars()
        {
            try
            {
                Console.WriteLine(Session["Logins"]);
            }
            catch (Exception ex)
            {
                Session.Add("Logins", "0");
            }
            try
            {
                Console.WriteLine(Session["Blocked"]);
            }
            catch (Exception ex)
            {
                Session.Add("Blocked", "0");
            }


        }
    }
}
