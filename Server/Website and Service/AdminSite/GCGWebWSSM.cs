using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace AppAdminSite
{
    public static class GCGWebWSSM
    {
        public static string GCGKeyToGCGUsersID(string pGCGKey)
        {
            string retVal = "1";
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            //AppAdminSite.SQLHelper sqlh = new AppAdminSite.SQLHelper("GCGApp.mdb");
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT GCGUsersID FROM tblGCGUsers WHERE GCGKey=@P0", pGCGKey);
            if (CommonForWS.isDatasetBad(data)) return "-1";
            retVal = data[0][0];
            sqlh.CloseIt();
            return retVal;
        }
        public static string ValidateGCGLogin(string pGCGLogin)
        {
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
            string retVal = "1";
            if (pGCGLogin.Length < 3)
            {
                retVal = "-1" + POSDEL + "Sorry, the username has to be at least 3 characters long.";
                return retVal;
            }
            else if (pGCGLogin.Length > 32)
            {
                retVal = "-1" + POSDEL + "Sorry, the username cant be more than 32 characters long.";
                return retVal;
            }
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT Count(*) FROM tblGCGUsers WHERE GCGLogin=@P0", pGCGLogin);
            if (CommonForWS.isDatasetBad(data))
            {
                retVal = "-1" + POSDEL + "Sorry, a system error occured.";
                return retVal;
            }
            string pTemp = data[0][0];
            if (pTemp != "0")
            {
                retVal = "-1" + POSDEL + "Sorry, the username has already been taken.";
                return retVal;
            }
            return retVal;
        }
        public static string ValidateGCGPassword(string pGCGPassword)
        {
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
            string retVal = "1";
            if (pGCGPassword.Length < 4)
            {
                retVal = "-1" + POSDEL + "Sorry, the password has to be at least 4 characters long.";
            }
            else if (pGCGPassword.Length > 32)
            {
                retVal = "-1" + POSDEL + "Sorry, the password cant be more than 32 characters long.";
            }
            return retVal;
        }

        public static string[] GetLookupsCntData(string pGCGUsersID)
        {
            string[] retVal = new string[4];
            //0=AmntOfLookupsAllowed, 1=AmntOfSuccessfulLookups, 2=AmntOfUnsuccessfulLookups, 3=AmntOfLookupsRemaining
            int amntAllowed = 5;
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            string[][] overridedata = sqlh.GetMultiValuesOfSQL("Select [PurchaseType] from tblPurchases WHERE [GCGUsersID]=@P0", pGCGUsersID);
            int loopamnt = MVDataRowCount(overridedata);
            for (int i = 0; i < loopamnt; i++)
            {
                amntAllowed = amntAllowed + Convert.ToInt16(overridedata[i][0]);
            }
            retVal[0] = amntAllowed.ToString();

            string amntUsed = "0";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select [LookupCount] from tblSuccessfulLookupCount WHERE [GCGUsersID]=@P0", pGCGUsersID);
            amntUsed = data[0][0];
            if (amntUsed == "") amntUsed = "0";
            retVal[1] = amntUsed;

            string amntFailed = "0";
            string[][] data1 = sqlh.GetMultiValuesOfSQL("Select Count(*) from qryResponses WHERE [GCGUsersID]=@P0 AND ResponseType<>'GCBALANCE'", pGCGUsersID);
            amntFailed = data1[0][0];
            if (amntFailed == "") amntFailed = "0";
            retVal[2] = amntFailed;

            int amntRem = amntAllowed - Convert.ToInt16(amntUsed);
            retVal[3] = amntRem.ToString();

            return retVal;
        }
        private static int MVDataRowCount(string[][] temp)
        {
            int retVal = -1;
            string testVal = "X";
            try
            {
                testVal = temp[0][49];  //if this exists, it means we had no data.
            }
            catch (Exception)
            {
            }
            if (testVal == "X")
            {
                retVal = temp.GetUpperBound(0) + 1;
            }
            return retVal;
        }
        public static string[] GetConfigParams()
        {
            string[] retVal = new string[4];
            try
            {
                retVal[0] = System.Configuration.ConfigurationManager.AppSettings["PathToRqRs"].ToString();
            }
            catch (Exception)
            {
                retVal[0] = "";
            }
            try
            {
                retVal[1] = System.Configuration.ConfigurationManager.AppSettings["PathToMerchantEXEs"].ToString();
            }
            catch (Exception)
            {
                retVal[1]="";
            }
            try
            {
                retVal[2] = System.Configuration.ConfigurationManager.AppSettings["CAPTCHAURLPrefix"].ToString();
            }
            catch (Exception)
            {
                retVal[2]= "";
            }
            try
            {
                retVal[3] = System.Configuration.ConfigurationManager.AppSettings["WebserviceTimeout"].ToString();
            }
            catch (Exception)
            {
                retVal[3] = "";
            }
            return retVal;
        }

        public static string GetLegitValue(string pGCGKey, string pKey)
        {
            string retVal = ""; 
            try
            {
                string a = pGCGKey;  //865D4712AC2E968  //911115911353199
                string b = Regex.Replace(a, @"[^\d]", string.Empty);
                decimal c = Convert.ToDecimal(b);
                decimal d = (c / .0526M);  //This is the key
                retVal = d.ToString().Substring(0, 3);
            }
            catch (Exception ex)
            {
                retVal ="-1";
            }
            return retVal;
        }
        
        public static string IsKeyLegit(string pGCGKey, string pKey)
        {
            try
            {
                string a = pGCGKey;  //865D4712AC2E968  //911115911353199
                string b = Regex.Replace(a, @"[^\d]", string.Empty);
                decimal c = Convert.ToDecimal(b);
                decimal d = (c / .0526M);  //This is the key
                string legit = d.ToString().Substring(0, 3);
                if (pKey == legit)
                {
                    return "1";
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string InsertNewRequest(string pGCGUsersID, string pFileID, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword)
        {
            string retVal = "";
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblNewRequests (GCGUsersID, FileID, CardType, CardNumber, PIN, Login, [Password], TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4, @P5, @P6, @P7)", pGCGUsersID, pFileID, pCardType, pCardNumber, pPIN, pLogin, pPassword, DateTime.Now.ToString());
            retVal = temp.ToString();
            sqlh.CloseIt();
            return retVal;
        }
        public static string InsertFailedRequest(string pGCGKey, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword)
        {
            string retVal = "";
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblFailedRequests (GCGKey, CardType, CardNumber, PIN, Login, [Password], TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4, @P5, @P6)", pGCGKey, pCardType, pCardNumber, pPIN, pLogin, pPassword, DateTime.Now.ToString());
            retVal = temp.ToString();
            sqlh.CloseIt();
            return retVal;
        }

        public static string InsertReponse(string pGCGUsersID, string pFileID, string pResponseType, string pResponse)
        {
            string retVal = "";
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblResponses (GCGUsersID, FileID, ResponseType, Response, TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4)", pGCGUsersID, pFileID, pResponseType, pResponse, DateTime.Now.ToString());
            retVal = temp.ToString();
            sqlh.CloseIt();
            return retVal;
        }
        public static string InsertContinueRequest(string pGCGUsersID, string pFileID, string pAnswer)
        {
            string retVal = "";
            GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblContinueRequest (GCGUsersID, FileID, Answer, TimeLogged) VALUES  (@P0, @P1, @P2, @P3 )", pGCGUsersID, pFileID, pAnswer, DateTime.Now.ToString());
            retVal = temp.ToString();
            sqlh.CloseIt();
            return retVal;
        }
        public static bool WaitForResponseFileCreation(string fileToRead, int timeout)
        {
            bool retVal = false;
            int x = 0;
            do
            {
                x = x + 1;
                try
                {
                    retVal = System.IO.File.Exists(fileToRead);
                }
                catch (Exception ex0)
                {
                    Console.WriteLine(ex0.Message);
                }
                if (retVal == true) break;
                System.Threading.Thread.Sleep(1000);
            } while (x < timeout);
            return retVal;
        }
        public static string ProcessResponse(string fileToRead)
        {
            string retVal = "";
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
            System.IO.StreamReader sr = null;
            sr = new System.IO.StreamReader(fileToRead);
            string rsType = sr.ReadLine();
            string rsValue = sr.ReadLine();
            sr.Close();
            if (rsType.ToUpper() == GCGCommon.EnumExtensions.GCTypes.GCERR.ToString())
            {
                rsType = GCGCommon.EnumExtensions.GCTypes.GCBALANCEERR.ToString();
            }
            retVal = rsType + LINEDEL + rsValue;
            return retVal;
        }
        public static string InsertReponseUsingRetVal(string pGCGUsersID, string pFileID, string retValIn)
        {
            string[] pieces1 = GCGCommon.SupportMethods.SplitByString(retValIn, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
            string rsType = pieces1[0];
            string rsValue = pieces1[1];
            if (rsType.ToUpper() == GCGCommon.EnumExtensions.GCTypes.GCBALANCE.ToString())
            {

                string[] LookupsCntData = GetLookupsCntData(pGCGUsersID);
                int intSuccessfulLookups = Convert.ToInt16(LookupsCntData[1]) + 1;
                GCGCommon.SQLHelper sqlh = new GCGCommon.SQLHelper(GCGCommon.SQLHelper.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
                int temp1 = sqlh.ExecuteSQLParamed("DELETE FROM tblSuccessfulLookupCount WHERE GCGUsersID=@P0", pGCGUsersID);
                int temp2 = sqlh.ExecuteSQLParamed("INSERT INTO tblSuccessfulLookupCount (GCGUsersID,LookupCount,TimeLogged) VALUES (@P0,@P1,@P2)", pGCGUsersID, intSuccessfulLookups.ToString(), DateTime.Now.ToString());
                sqlh.CloseIt();
            }
            string OK = InsertReponse(pGCGUsersID, pFileID, rsType, rsValue);
            return OK;
        }
    }
}