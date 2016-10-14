using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AppAdminSite
{
    public class BusinessLogic01
    {
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        SQLHelper sqlh;
        public BusinessLogic01()
        {
            sqlh = new SQLHelper(SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\App.mdb");;
        }
        public void CloseIt()
        {
            sqlh.CloseIt();
        }
        public string InitGCGWeb()
        {
            string retVal = "Error";
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT CleanName, TrueCardNumMin, CardNumMax, TruePINMin, PINMax, showCreds FROM qryMerchantsSupported ORDER BY CleanName");
            if (isDatasetBad(data)) return null;
            int max = data.Length;
            string template = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < max; i++)
            {
                template = "";
                string CleanName = data[i][0];
                string TrueCardNumMin = data[i][1];
                string CardNumMax = data[i][2];
                string TruePINMin = data[i][3];
                string PINMax = data[i][4];
                string showCreds = data[i][5];
                string CardTypeID = RemoveNonAlphaNumericChars(CleanName);
                string ConcatData = CleanName + POSDEL + TrueCardNumMin + POSDEL + CardNumMax + POSDEL + TruePINMin + POSDEL + PINMax + POSDEL + showCreds;
                template = "" +
                "<li>" +
                "<a onclick=\"SSAndLoadMerchNameAndValInfo(document.getElementById('" + CardTypeID + "').value)\">" + CleanName +
                "<input id=\"" + CardTypeID + "\" type=\"hidden\" value=\"" + ConcatData + "\" />" +
                "</a>"+
                "</li>";
                sb.Append(template);
            }
            retVal = sb.ToString();
            return retVal;
        }

        public string RegisterUserIns(string UserLogin, string UserPass)
        {
            string retVal = "Error";
            MD5 md5Hash = MD5.Create();
            string encUserPass=AppAdminSite.CSharp.MD5Lib.GetMd5Hash(UserPass);
            string myID=GCGCommon.SupportMethods.CreateHexKey(30);
            int OK = sqlh.ExecuteSQLParamed("INSERT INTO tblRegisteredUsers (GCGID,UserLogin, UserPass, TimeLogged) VALUES (@P0, @P1,@P2,@P3)", myID, UserLogin, encUserPass, DateTime.Now.ToString());
            if (OK != -1) retVal = myID;
            return retVal;
        }
        public string RegisterUserSel(string UserLogin, string UserPass)
        {
            string retVal = "Error";
            MD5 md5Hash = MD5.Create();
            string encUserPass = AppAdminSite.CSharp.MD5Lib.GetMd5Hash(UserPass);

            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT GCGID FROM tblRegisteredUsers WHERE UserLogin=@P0 AND UserPass=@P1", UserLogin, encUserPass);
            retVal = data[0][0];            
            return retVal;
        }
        public string RUCardDataSel(string GCGID)
        {
            string retVal = "Error";
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
            //GCGID = "AAA";
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM tblRUCardData WHERE GCGID=@P0", GCGID);
            if (isDatasetBad(data)) return null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string template = "";
            int max = data.Length;
            for (int i = 0; i < max; i++)
            {
                template = "";
                string CardID = data[i][0];
                string CardType = data[i][2];
                string CardNumber = data[i][3];
                string CardPIN = data[i][4];
                string CardLogin = data[i][5];
                string CardPass = data[i][6];
                string LastKnownBalance = data[i][7];
                string LastKnownBalanceDate = data[i][8];
                string ConcatData = CardID + POSDEL + CardType + POSDEL + CardNumber + POSDEL + CardPIN + POSDEL + CardLogin + POSDEL + CardPass;
                template= "" +
                "<li>" +
                //"<a href=\"#testarea1\">" +
                "<a onclick=\"SSAddModCard(document.getElementById('MyCard" + CardID + "').value)\">" +
                "<h3>" + CardType + "</h3>" +
                "<p>" + CardNumber + "</p>" +
                //"</a>" +
                //"<span class=\"ui-li-count\">12</span>" +
                "<p class=\"ui-li-aside\">" + LastKnownBalanceDate + "<br>" + LastKnownBalance + "</p>" +
                "<input id=\"MyCard"+CardID+"\" type=\"hidden\" value=\""+ConcatData+"\" />" +
                "</a>" +
                "</li>";
                sb.Append(template);                
            }
            retVal = sb.ToString();
            return retVal;
        }
        public string RUCardDataMod(string GCGID, string CardID, string CardType, string CardNumber, string CardPIN, string CardLogin, string CardPass, string LastKnownBalance, string LastKnownBalanceDate)
        {
            string retVal = "";
            int temp1 =-1;
            //GCGID = "AAA";
            if (CardID == "")
            {
                //It's an insert
                temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblRUCardData (GCGID,CardType,CardNumber,CardPIN, CardLogin, CardPass, TimeLogged) VALUES (@P0,@P1,@P2,@P3,@P4,@P5,@P6)", GCGID, CardType, CardNumber, CardPIN, CardLogin,CardPass, DateTime.Now.ToString());
            }
            else if (CardNumber == "")
            {
                //We're just updating the balance info
                temp1 = sqlh.ExecuteSQLParamed("UPDATE tblRUCardData SET LastKnownBalance=@P2, LastKnownBalanceDate=@P3 WHERE CardDataID=@P0 AND GCGID=@P1", CardID, GCGID, LastKnownBalance, LastKnownBalanceDate);
            }
            else if (CardNumber == "-1")
            {
                //We're deleting the card
                temp1 = sqlh.ExecuteSQLParamed("DELETE FROM tblRUCardData WHERE CardDataID=@P0 AND GCGID=@P1", CardID, GCGID);
            }
            else
            {
                //We're updating the card with all the info we have
                //temp1 = sqlh.ExecuteSQLParamed("UPDATE tblRUCardData SET CardNumber=@P0 WHERE CardDataID=@P1", CardNumber, CardID);
                temp1 = sqlh.ExecuteSQLParamed("UPDATE tblRUCardData SET CardType=@P0, CardNumber=@P1, CardPIN=@P2, CardLogin=@P3, CardPass=@P4 WHERE CardDataID=@P5 AND GCGID=@P6", CardType, CardNumber, CardPIN, CardLogin, CardPass, CardID, GCGID);
            }
            retVal = temp1.ToString();
            return retVal;
        }


        static bool OurCertificateValidation(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var actualCertificate = X509Certificate.CreateFromCertFile("example.com.cert");
            return certificate.Equals(actualCertificate);
        }
        static string RemoveNonAlphaNumericChars(string dataIn)
        {
            string retVal = "";
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
            retVal = rgx.Replace(dataIn, "");
            return retVal;
        }
        static bool isDatasetBad(string[][] dataIn)
        {
            if (dataIn == null) return true;
            try 
	        {
                if (dataIn[0][49] == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
	        }
	        catch (Exception)
	        {
                return false;
	        }
        }
    }
}