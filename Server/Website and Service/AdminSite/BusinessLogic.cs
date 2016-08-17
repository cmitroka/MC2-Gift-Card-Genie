using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AppAdminSite
{
    public class BusinessLogic
    {
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        string gloPathToMerchantEXEs = "";
        string gloPathToRqRs;
        string gloCAPTCHAURLPrefix = "";
        string LogLevel = "";
        int gloWebserviceTimeout = 0;
        string errArea = "";
        SQLHelper sqlh;
        public BusinessLogic()
        {
            GetGlobalSettings();
            sqlh = new SQLHelper("App.mdb");
            LogLevel = GetSystemParam("LogLevel");
            LogLevel = LogLevel;

        }
        public void CloseIt()
        {
            sqlh.CloseIt();
        }
        public string NewRequest(string pUUID, string pSessionID, string pCheckSum, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword, string pIP)
        {
            string pFileName = "";
            string pArguments = "";
            string retVal = "";
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            string RqRsFileName = "";
            string rsValue = "";
            var testDate = DateTime.Now;
            RqRsFileName = testDate.ToString("yyyyMMdd_HHmmssfff");
            pCardNumber = pCardNumber.Replace(" ", "");
            pPIN = pPIN.Replace(" ", "");
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT Count(IP) AS CountOfIP FROM tblBlockedIPs WHERE IP=@P0", pIP);
            string IPBockedCnt = data[0][0];
            if (IPBockedCnt != "0")
            {
                rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSBLOCKEDIP.ToString();
                rsValue = "IP Has been blocked";
                retVal = rsType + LINEDEL + rsValue;
                return retVal;
            }

            string sAmntTest = GetAmntOfLookupsRemaining(pUUID);
            int AmntTest = Convert.ToInt16(sAmntTest);
            if (AmntTest <= 0)
            {
                rsType = GCGCommon.EnumExtensions.GCTypes.GCCUSTOM.ToString();
                rsValue = "Sorry, you're out of free lookups - please consider purchasing Gift Card Genie.";
                string OK = InsertNewRequest(pUUID, RqRsFileName, pCardType, "", "", "", "");
                OK = InsertReponse(pUUID, RqRsFileName, "OUTOFLOOKUPS", rsValue);
                retVal = rsType + LINEDEL + rsValue;
                return retVal;
            }

            string[][] data2 = sqlh.GetMultiValuesOfSQL("SELECT UUID, Reason FROM tblBlockedUUIDs WHERE UUID=@P0", pUUID);
            string UUIDBlocked = data2[0][0];
            if (UUIDBlocked.Length > 0)
            {
                rsType = GCGCommon.EnumExtensions.GCTypes.GCCUSTOM.ToString();
                string Reason = data2[0][1];
                if (Reason.Length == 0) Reason = "There seems to be a problem with your GCG copy, please contact us via email.";
                retVal = rsType + LINEDEL + Reason;
                return retVal;
            }

            if (IsRequestValid(pSessionID, pCheckSum, pIP) == false)
            {
                rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSINVALIDSESSION.ToString();
                rsValue = "Invalid Session";
                retVal = rsType + LINEDEL + rsValue;
                return retVal;
            }
            try
            {
                string TestForValid = SetGlobalVariables();
                if (TestForValid != "") return rsType + LINEDEL + "We seem to have a setup problem, try again or send us an email.";
                bool FileMade = false;

                string isLookupManual = "0";
                data = sqlh.GetMultiValuesOfSQL("SELECT IsLookupManual FROM qryMerchantsSupported WHERE CleanName=@P0", pCardType);
                if (CommonForWS.isDatasetBad(data) == false) isLookupManual = data[0][0];
                if (isLookupManual == "1")
                {
                    string tempRqRsFileName = GCGCommon.SupportMethods.CreateHexKey(20);
                    rsType = "MANUALLOOKUP";
                    rsValue = "0.00";
                    string tempRetVal = rsType + GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL) + rsValue;
                    string OK = InsertNewRequest(pUUID, tempRqRsFileName, pCardType, pCardNumber, pPIN, "", "");
                    InsertReponseUsingRetVal(pUUID, tempRqRsFileName, tempRetVal);
                    PossiblyLogSuccessfulLookup(tempRetVal, pUUID);
                    return "REDIRECTING" + "^)(" + "Your being taken to the merchants website to complete the lookup...";
                }
                else
                {
                    if (LogLevel == "1")
                    {
                        string OK = InsertNewRequest(pUUID, RqRsFileName, pCardType, "", "", "", "");
                    }
                    else
                    {
                        string OK = InsertNewRequest(pUUID, RqRsFileName, pCardType, pCardNumber, pPIN, pLogin, pPassword);
                    }
                }


                string MakeRqFile = gloPathToRqRs + "\\" + RqRsFileName + "-0rq.txt";
                string RsFileToRead = gloPathToRqRs + "\\" + RqRsFileName + "-0rs.txt";
                GCGCommon.AllDetails ad = new GCGCommon.AllDetails(MakeRqFile, gloCAPTCHAURLPrefix);
                Console.WriteLine(ad.CAPTCHAPathAndFileToWrite);
                while (System.IO.File.Exists(MakeRqFile) == true)
                {
                    System.IO.File.Delete(MakeRqFile);
                }
                while (System.IO.File.Exists(RsFileToRead) == true)
                {
                    System.IO.File.Delete(RsFileToRead);
                }


                //SQLHelper s1 = new SQLHelper("MerchantsAndSettings.mdb");
                //string[][] data1 = s1.GetMultiValuesOfSQL("Select [EXE] from tblMerchants WHERE [CleanName]=@P0", pCardType);
                string[][] data1 = sqlh.GetMultiValuesOfSQL("Select [EXE] from tblMerchants WHERE [CleanName]=@P0", pCardType);
                string EXE = data1[0][0];


                if (EXE == "")
                {
                    EXE = pCardType;
                }
                System.IO.StreamWriter s = new System.IO.StreamWriter(MakeRqFile);
                s.WriteLine(EXE);
                s.WriteLine(pCardNumber);
                s.WriteLine(pPIN);
                s.WriteLine(pLogin);
                s.WriteLine(pPassword);
                s.Close();
                //GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceCreated1", "NewRequest - Created ", "W");
                string filetomake = "";
                try
                {
                    filetomake = gloPathToRqRs + "\\" + RqRsFileName + "bat.txt";
                    s = new System.IO.StreamWriter(filetomake);
                    pFileName = @"""" + gloPathToMerchantEXEs + "\\" + EXE + ".exe" + @"""";
                    pArguments = @"""" + MakeRqFile + @""" 1";
                    s.Write(pFileName + " " + pArguments);
                    s.Close();
                    //GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceCreated2", "NewRequest - Created " + filetomake, "I");
                }
                catch (Exception)
                {
                    //GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceFailed", "NewRequest - Failed " + filetomake, "W");
                }
                /*
                System.IO.StreamWriter s2 = new System.IO.StreamWriter("C:\\gcg\\out.txt");
                s2.WriteLine(pFileName + " " + pArguments);
                s2.Close();
                if (GCGCommon.SupportMethods.IsRunning("GCG Janitor") != 1)
                {
                    rsType = GCGCommon.EnumExtensions.JanitorTypes.JNOTRUNNING.ToString();
                    rsValue = "The request can't be processed because the executer isn't running";
                    retVal = rsType + LINEDEL + rsValue;
                    return retVal;
                }
                */
                int x = 0;
                do
                {
                    x = x + 1;
                    try
                    {
                        FileMade = System.IO.File.Exists(RsFileToRead);
                    }
                    catch (Exception ex0)
                    {
                        Console.WriteLine(ex0.Message);
                    }
                    if (FileMade == true) break;
                    System.Threading.Thread.Sleep(1000);
                } while (x < gloWebserviceTimeout);
                FileMade = WaitForResponseFileCreation(RsFileToRead);
                if (FileMade == true)
                {
                    retVal = ProcessResponse(RsFileToRead, pUUID);
                    if (retVal.Contains(GCGCommon.EnumExtensions.JanitorTypes.JEXEMISSING.ToString()))
                    {
                        AddMerchantRequest(pCardType, GCGCommon.EnumExtensions.JanitorTypes.JEXEMISSING.ToString(), pCardNumber, pPIN, pUUID, pIP);
                    }
                }
                else
                {
                    retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSTIMEOUT.ToString() + LINEDEL + "Webservice Timed Out";
                }
            }
            catch (Exception ex)
            {
                rsValue = ex.Message;
                retVal = rsType + LINEDEL + rsValue;
            }
            InsertReponseUsingRetVal(pUUID, RqRsFileName, retVal);
            PossiblyLogSuccessfulLookup(retVal, pUUID);
            return retVal;

        }

        public string GetSessionIDAndAdInfo(string UUID, string CardType)
        {
            string retVal = "";
            string SessionID = GetSessionID(UUID);
            string AdInfo = GetAdInfo(CardType);
            retVal = SessionID + POSDEL + AdInfo;
            return retVal;
        }
        private string GetAdInfo(string CardType)
        {
            string retVal = "";
            if (CardType == "")
            {
                retVal = "" + POSDEL;
            }
            else
            {
                string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM tblAdInfo WHERE Merchant=@P0", CardType);
                retVal = data[0][2] + POSDEL + data[0][3];
            }
            return retVal;
        }
        public string ArchiveNewVendorRequest(string UUID, string Merchant, string URL, string CardNumber, string CardPIN, string TimeLogged)
        {
            string retVal = "";
            sqlh.ExecuteSQLParamed("INSERT INTO tblArchivedMerchantRequests (Merchant, URL, CardNum, CardPIN, OtherInfo, TimeLogged) VALUES (@P1, @P2, @P3, @P4, @P5, @P6)", Merchant, URL, CardNumber, CardPIN, UUID, TimeLogged);
            return retVal;
        }
        public string GetMerchantStatuses(string pSupportCode)
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM tblMerchants WHERE SupportCode=@P0 ORDER BY CleanName", pSupportCode);
            int test = data.GetUpperBound(0);
            for (int i = 0; i <= test; i++)
            {
                retVal = retVal + data[i][1] + POSDEL;
            }
            return retVal;
        }

        private string GetSessionID(string UUID)
        {
            string retVal = "";
            string temp = "";
            try
            {
                var testDate = DateTime.Now;
                temp = testDate.ToString("yyyyMMdd_HHmmssfff");
                string MakeFile = gloPathToRqRs + "\\" + temp;
                System.IO.StreamWriter s = new System.IO.StreamWriter(MakeFile);
                s.Close();
                retVal = temp;
            }
            catch (Exception ex)
            {
                //GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG BusinessLogic", "GetSessionID - Couldn't make Session ID - " + ex.Message, "W");
            }
            return retVal;
        }
        public string ContinueRequest(string pUDID, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            string FileID = "";
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            try
            {
                string[] FileIDArr = GCGCommon.SupportMethods.SplitByString(pIDFileName, "-");
                FileID = FileIDArr[0];
                //string pIDFileName2=GCGCommon.AllDetailsMethods.GetNextRxFileWOExt(pIDFileName);
                //string TestForValid = SetGlobalVariables();
                //if (TestForValid != "") return rsType + LINEDEL+"We seem to have a setup problem, try again or send us an email.";
                bool FileMade = false;
                string Good = InsertContinueRequest(pUDID, pIDFileName, pAnswer);
                string MakeRqFile = gloPathToRqRs + "\\" + pIDFileName.Replace("rx", "rq") + ".txt";
                string RsFileToRead = gloPathToRqRs + "\\" + pIDFileName.Replace("rx", "rs") + ".txt";
                while (System.IO.File.Exists(MakeRqFile) == true)
                {
                    System.IO.File.Delete(MakeRqFile);
                }
                while (System.IO.File.Exists(RsFileToRead) == true)
                {
                    System.IO.File.Delete(RsFileToRead);
                }
                System.IO.StreamWriter s = new System.IO.StreamWriter(MakeRqFile);
                s.WriteLine(pAnswer);
                s.Close();
                FileMade = WaitForResponseFileCreation(RsFileToRead);
                if (FileMade == true)
                {
                    retVal = ProcessResponse(RsFileToRead, pUDID);
                }
                else
                {
                    retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSTIMEOUT.ToString() + LINEDEL + "Webservice Timed Out";
                }
            }
            catch (Exception ex)
            {
                retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString() + LINEDEL + ex.Message;
            }

            string OK = InsertReponseUsingRetVal(pUDID, FileID, retVal);
            return retVal;
        }

        public string CheckWebConfig()
        {
            string retVal = SetGlobalVariables();
            return retVal;
        }
        public string GetGlobalSettings()
        {
            string retVal = "";
            string OK = SetGlobalVariables();
            retVal = gloPathToRqRs + POSDEL + gloPathToRqRs + POSDEL + gloPathToMerchantEXEs + POSDEL + gloCAPTCHAURLPrefix + POSDEL + gloWebserviceTimeout;
            return retVal;
        }
        private bool WaitForResponseFileCreation(string fileToRead)
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
            } while (x < gloWebserviceTimeout);
            return retVal;
        }
        private bool IsRequestValid(string SessionID, string Checksum, string IP)
        {
            bool retVal = false;
            GetGlobalSettings();
            FileInfo f = new FileInfo(gloPathToRqRs + "\\" + SessionID);
            if (f.Exists == true)
            {
                f.Delete();
                //Now check to see if the checksum is right...

                string IncomingChannel = GCGCommon.SupportMethods.ValidateSession(SessionID, Checksum);
                if (IncomingChannel != "")
                {
                    retVal = true;
                }
                else
                {
                    sqlh.ExecuteSQLParamed("INSERT INTO tblBlockedIPs (IP, TimeLogged) VALUES (@P1, @P2)", IP, DateTime.Now.ToString());
                    retVal = false;
                }
            }
            else
            {
                //strange request, really shouldn't occur... log it.  Pretty much means the session expired
                retVal = false;
            }
            return retVal;
        }

        public string ProcessResponse(string fileToRead, string UDID)
        {
            string retVal = "";
            System.IO.StreamReader sr = null;
            sr = new System.IO.StreamReader(fileToRead);
            string rsType = sr.ReadLine();
            string rsValue = sr.ReadLine();
            sr.Close();
            if (rsType.ToUpper() == GCGCommon.EnumExtensions.GCTypes.GCERR.ToString())
            {
                rsType = GCGCommon.EnumExtensions.GCTypes.GCBALANCEERR.ToString();
                //rsType = GCGCommon.EnumExtensions.GCTypes.GCCUSTOM.ToString();
            }
            retVal = rsType + LINEDEL + rsValue;
            return retVal;
        }
        private string PossiblyLogSuccessfulLookup(string rsTypeAndInfo, string UDID)
        {
            string retVal = "";
            string[] pieces1 = GCGCommon.SupportMethods.SplitByString(rsTypeAndInfo, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
            string rsType = pieces1[0];
            string rsValue = pieces1[1];
            if ((rsType.ToUpper() == GCGCommon.EnumExtensions.GCTypes.GCBALANCE.ToString()) || rsType.ToUpper() == "MANUALLOOKUP")
            {
                int intSuccessfulLookups = GetAmntOfSuccessfulLookups(UDID) + 1;
                int temp1 = sqlh.ExecuteSQLParamed("DELETE FROM tblSuccessfulLookupCount WHERE UDID=@P0", UDID);
                int temp2 = sqlh.ExecuteSQLParamed("INSERT INTO tblSuccessfulLookupCount (UDID,LookupCount,TimeLogged) VALUES (@P0,@P1,@P2)", UDID, intSuccessfulLookups.ToString(), DateTime.Now.ToString());
            }
            return retVal;

        }

        private string SetGlobalVariables()
        {
            string error = "";
            /*
            static string gloPathToMerchantEXEs = "";
            static string gloPathToRqRs = "";
            static string gloCAPTCHAURLPrefix = "";
            static int gloWebserviceTimeout = 0;
            */

            try
            {
                gloPathToRqRs = System.Configuration.ConfigurationManager.AppSettings["PathToRqRs"].ToString();
            }
            catch (Exception)
            {
                error = "PathToRqRs, ";
            }
            try
            {
                gloPathToMerchantEXEs = System.Configuration.ConfigurationManager.AppSettings["PathToMerchantEXEs"].ToString();
            }
            catch (Exception)
            {
                error += "PathToMerchantEXEs, ";
            }
            try
            {
                gloCAPTCHAURLPrefix = System.Configuration.ConfigurationManager.AppSettings["CAPTCHAURLPrefix"].ToString();
            }
            catch (Exception)
            {
                error += "CAPTCHAURLPrefix, ";
            }
            try
            {
                gloWebserviceTimeout = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["WebserviceTimeout"].ToString());
            }
            catch (Exception)
            {
                error += "WebserviceTimeout, ";
            }
            if (error.Length > 0)
            {
                string newerror = error.Substring(0, error.Length - 2);
                newerror += " not configured";
                error = newerror;
            }
            return error;
        }

        public string RecordFeedback(string IP, string UDID, string Name, string ContactInfo, string Feedback)
        {
            string retVal = "";
            //OleDbCommand cmdIns = new OleDbCommand(InsertQuery, sqlh.aConnection);
            OleDbCommand cmdID = new OleDbCommand("Select @@IDENTITY", sqlh.aConnection);
            string ID = null;
            try
            {
                string sql = "INSERT INTO tblFeedback (UDID, Name, ContactInfo, Feedback, IP, TimeLogged) VALUES  (@UDID, @NameP, @ContactInfo, @Feedback, @IP, @TimeLogged);";
                OleDbCommand cmdIns = new OleDbCommand(sql, sqlh.aConnection);
                cmdIns.Connection = sqlh.aConnection;
                //cmdIns.CommandType = CommandType.Text;
                cmdIns.CommandText = sql;
                cmdIns.Parameters.AddWithValue("UDID", UDID);
                cmdIns.Parameters.AddWithValue("NameP", Name);
                cmdIns.Parameters.AddWithValue("ContactInfo", ContactInfo);
                cmdIns.Parameters.AddWithValue("Feedback", Feedback);
                cmdIns.Parameters.AddWithValue("IP", IP); //HttpContext.Current.Request.UserHostAddress
                cmdIns.Parameters.AddWithValue("TimeLogged", DateTime.Now.ToString());
                int OK = -1;
                try
                {
                    OK = cmdIns.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    retVal = ex.Message;
                    return "E11 - " + retVal;
                }
                try
                {
                    ID = cmdID.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    retVal = ex.Message;
                    return "E12 - " + retVal;
                }
            }
            catch (Exception ex1)
            {
                return "E13 - " + ex1.Message;
            }
            try
            {
                SendMail(Name,ContactInfo,Feedback);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            //finally { sqlh.aConnection.Close(); }
            return ID;
        }
        public string GetSuccessfulRqCount(string pUDID)
        {
            string retVal = "0";
            string[][] overridedata = sqlh.GetMultiValuesOfSQL("Select [UUID] from tblPurchaseOverrides WHERE [UUID]=@P0", pUDID);
            retVal = overridedata[0][0];
            if (retVal != "") return "0";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select [SuccessfulLookups] from qryUDIDBalanceLookups WHERE [UDID]=@P0", pUDID);
            retVal = data[0][0];
            if (retVal == "") retVal = "0";
            return retVal;
        }
        public string GetCAPTCHAURLinfo()
        {
            string retVal = "";
            string test = SetGlobalVariables();
            if (test.Length == 0) return gloCAPTCHAURLPrefix;
            return retVal;
        }
        public string GetMerchantCount()
        {
            string retVal = "";
            try
            {
                string[][] data = sqlh.GetMultiValuesOfSQL("Select count(*) from qryMerchantsSupported");
                retVal = data[0][0];
            }
            catch (Exception)
            {
            }
            return retVal;
        }
        public string DownloadAllData()
        {
            string retVal = "";
            string tempLine = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                //SQLHelper s1 = new SQLHelper("MerchantsAndSettings.mdb");
                //OleDbDataAdapter da = new OleDbDataAdapter("Select * from qryMerchantsSupported", s1.aConnection);
                OleDbDataAdapter da = new OleDbDataAdapter("Select * from qryMerchantsSupported", sqlh.aConnection);
                da.Fill(ds, "RandomData");
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    string URLToUse = dr["URL"].ToString();
                    if (URLToUse.Contains("%"))  //IOS <5 doesn't handle this well...
                    {
                        URLToUse = "";
                    }
                    //tempLine = dr["CleanName"].ToString() + POSDEL + dr["CleanPhone"].ToString() + POSDEL + URLToUse + POSDEL + dr["showCardNum"].ToString() + POSDEL + dr["showCardPIN"].ToString() + POSDEL + dr["showCreds"].ToString() + POSDEL + dr["reqReg"].ToString() + POSDEL + dr["TrueCardNumMin"].ToString() + POSDEL + dr["CardNumMax"].ToString() + POSDEL + dr["TruePINMin"].ToString() + POSDEL + dr["PINMax"].ToString() + POSDEL + dr["GeneralNote"].ToString() + LINEDEL;
                    tempLine = dr["CleanName"].ToString() + POSDEL + "" + POSDEL + URLToUse + POSDEL + dr["showCardNum"].ToString() + POSDEL + dr["showCardPIN"].ToString() + POSDEL + "0" + POSDEL + "0" + POSDEL + dr["TrueCardNumMin"].ToString() + POSDEL + dr["CardNumMax"].ToString() + POSDEL + dr["TruePINMin"].ToString() + POSDEL + dr["PINMax"].ToString() + POSDEL + dr["GeneralNote"].ToString() + LINEDEL;
                    sb.Append(tempLine);
                }
            }
            catch (OleDbException ex)
            {
                retVal = ex.Message;
                return retVal;
            }
            retVal = sb.ToString();
            return retVal;
        }
        /*
        public string GetSystemMessage()
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select [Value] from tblSystemParams where Key='SystemMessage'");
            retVal = data[0][0];
            return retVal;
        }
        */
        public string DownloadAllDataV2()
        {
            string retVal = "";
            string tempLine = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                //SQLHelper s1 = new SQLHelper("MerchantsAndSettings.mdb");
                //OleDbDataAdapter da = new OleDbDataAdapter("Select * from qryMerchantsSupported", s1.aConnection);
                OleDbDataAdapter da = new OleDbDataAdapter("Select * from qryMerchantsSupported", sqlh.aConnection);
                da.Fill(ds, "RandomData");
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    string URLToUse = dr["URL"].ToString();
                    /*
                    if (URLToUse.Contains("%"))  //IOS <5 doesn't handle this well...
                    {
                        URLToUse = "";
                    }
                    */
                    tempLine = dr["CleanName"].ToString() + POSDEL + URLToUse + POSDEL + dr["showCardNum"].ToString() + POSDEL + dr["showCardPIN"].ToString() + POSDEL + dr["TrueCardNumMin"].ToString() + POSDEL + dr["CardNumMax"].ToString() + POSDEL + dr["TruePINMin"].ToString() + POSDEL + dr["PINMax"].ToString() + POSDEL + dr["IsLookupManual"].ToString() + POSDEL + dr["GeneralNote"].ToString() + LINEDEL;
                    sb.Append(tempLine);
                }
            }
            catch (OleDbException ex)
            {
                retVal = ex.Message;
                return retVal;
            }
            retVal = sb.ToString();
            return retVal;
        }

        public string GetSystemParam(string pKey)
        {
            string retVal = "";
            try
            {
                string[][] data = sqlh.GetMultiValuesOfSQL("Select [Value] from tblSystemParams where Key=@P0", pKey);
                retVal = data[0][0];
            }
            catch (Exception)
            {
            }
            return retVal;
        }


        public string GetMessage(string pUUID)
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select id, Message from tblMessages where UUID=@P0 AND Sent=0",pUUID);
            retVal = data[0][1];
            string ID = data[0][0];
            if (retVal != "")
            {
                int temp1 = sqlh.ExecuteSQLParamed("UPDATE tblMessages SET Sent=-1, TimeSentToUser='" + DateTime.Now.ToString() + "' WHERE ID=@P0", ID);
            }
            return retVal;
        }
        public string BackupData(string UUID, string AllData)
        {
            string retVal = "";
            string key = CreateAlphaNumericKey(16);
            int temp1 = sqlh.ExecuteSQLParamed("DELETE FROM tblBackedUpData WHERE UUID=@P0", UUID);
            int temp2 = sqlh.ExecuteSQLParamed("INSERT INTO tblBackedUpData (UUID,BackedUpData,RetrievalKey,TimeLogged) VALUES (@P0,@P1,@P2,@P3)", UUID, AllData, key, DateTime.Now.ToString());
            //key = "a";
            return key;
        }
        public string RetrieveData(string UUID, string RetrievalKey)
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select UUID, BackedUpData from tblBackedUpData where RetrievalKey=@P0",RetrievalKey);
            string RetrievedUUID = data[0][0];
            string AllData = data[0][1];
            retVal = RetrievedUUID + LINEDEL + AllData;
            return retVal;
        }

        private string CreateAlphaNumericKey(int length)
        {
            Random r = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int rndInt;
            string retVal = "";
            //char[] chars = new char[61];
            //chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            char[] chars = new char[15];
            chars = "ABCDEF1234567890".ToCharArray();            
            for (int i = 0; i < length; i++)
            {
                rndInt = r.Next(0, 15);
                char c = chars[rndInt];
                string s = c.ToString();
                sb.Append(s);
            }
            retVal = sb.ToString();
            //CJMUtilities.File.QuickWrite("C:\\out.txt", retVal);
            return retVal;
        }
        /*
        public string GetDBLastUpdated()
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select [Value] from tblSystemParams where Key='DBLastUpdated'");
            DateTime dt = Convert.ToDateTime(data[0][0]);
            retVal = dt.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            return retVal;
        }
        */
        private string FormatDate(string _Date)
        {
            DateTime Dt = DateTime.Now;
            IFormatProvider mFomatter = new System.Globalization.CultureInfo("en-US");
            Dt = DateTime.Parse(_Date, mFomatter);
            return Dt.ToString("yyyy-MM-dd");
        }

        public string LogUser(string UDID, string Version, string IP)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblUserLog (UDID, Version, IP, TimeLogged) VALUES  (@P0, @P1, @P2, @P3)", UDID,Version, IP, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }

        public void AppendTextToFile(string StatusMessage, string FinalLoc)
        {
            errArea = "AppendTextToFile - 0";
            System.IO.FileInfo f;
            string tempFileName = "C:\\" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string tempLogTime = DateTime.Now.ToLongTimeString();
            errArea = "AppendTextToFile - 1";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(tempFileName);
            sw.WriteLine(StatusMessage);
            errArea = "AppendTextToFile - 2";
            f = new System.IO.FileInfo(FinalLoc);
            if (f.Exists == true)
            {
                errArea = "AppendTextToFile - 3";
                System.IO.StreamReader sr = new System.IO.StreamReader(FinalLoc);
                sw.Write(tempLogTime + " - " + sr.ReadToEnd());
                sr.Close();
            }
            sw.Close();
            errArea = "AppendTextToFile - 4";
            f = new System.IO.FileInfo(tempFileName);
            errArea = "AppendTextToFile - 5; FinalLoc=" + FinalLoc;
            f.CopyTo(FinalLoc, true);
            errArea = "AppendTextToFile - 6";
            f.Delete();
        }
        private string InsertReponseUsingRetVal(string pUDID, string pFileID, string retValIn)
        {
            string[] pieces1 = GCGCommon.SupportMethods.SplitByString(retValIn, GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL));
            string rsType = pieces1[0];
            string rsValue = pieces1[1];
            string OK = InsertReponse(pUDID, pFileID, rsType, rsValue);
            return OK;

        }
        private string InsertReponse(string pUDID, string pFileID, string pResponseType, string pResponse)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblResponses (UDID, FileID, ResponseType, Response, TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4)", pUDID, pFileID, pResponseType, pResponse, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }
        private string InsertNewRequest(string pUDID, string pFileID, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblNewRequests (UDID, FileID, CardType, CardNumber, PIN, Login, [Password], TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4, @P5, @P6, @P7)", pUDID, pFileID, pCardType, pCardNumber,pPIN,pLogin,pPassword, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }
        private string InsertContinueRequest(string pUUID, string pFileID, string pAnswer)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblContinueRequest (UUID, FileID, Answer, TimeLogged) VALUES  (@P0, @P1, @P2, @P3 )", pUUID, pFileID, pAnswer, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }
        public string AddMerchantRequest(string pMerchant, string pURL, string pCardNum, string pCardPIN, string pOtherInfo, string pIP)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblAddMerchantRequest (Merchant, URL, CardNum, CardPIN, OtherInfo, IP, TimeLogged) VALUES  (@P0, @P1, @P2, @P3, @P4, @P5, @P6)", pMerchant, pURL, pCardNum, pCardPIN, pOtherInfo, pIP, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }
        public string DoAppStartup(string UDID, string IP)
        {
            string retVal = "";
            retVal = DoAppStartup(UDID, "<1.2", IP);
            return retVal;
        }

        public string DeleteOldBackup(string UUID)
        {
            string retVal = "";
            int temp = sqlh.ExecuteSQLParamed("DELETE FROM tblBackedUpData where RetrievalKey=@P0", UUID);
            retVal = temp.ToString();
            return retVal;

        }
        public string DoAppStartup(string UDID,string pVersion, string IP)
        {
            string retVal = "";
            string temp1 = LogUser(UDID,pVersion, IP);
            string temp2 = GetCAPTCHAURLinfo();
            string temp3 = GetMessage(UDID);
            if (temp3 == "") temp3 = GetSystemParam("SystemMessage");//GetSystemMessage();
            
            string temp4 = "";
            temp4 = GetAmntOfLookupsRemaining(UDID);
            //temp4 = GetSuccessfulRequestCount(UDID);
            string temp5 = GetSystemParam("DBLastUpdated");
            string temp6 = GetSystemParam("AmntForADollar");
            string temp7 = GetSystemParam("CostForApp");
            retVal = temp2 + POSDEL + temp3 + POSDEL + temp4 + POSDEL + temp5 + POSDEL + temp6 + POSDEL + temp7;
            return retVal;
        }

        private int MVDataRowCount(string[][] temp)
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
                retVal=temp.GetUpperBound(0) + 1;
            }
            return retVal;
        }
        public string GetAmntOfLookupsRemaining(string pUDID)
        {
            string retVal = "0";
            int amntAllowed = GetAmntOfLookupsAvail(pUDID);
            int amntUsed = GetAmntOfSuccessfulLookups(pUDID);
            int amntRem = amntAllowed - amntUsed;
            retVal = amntRem.ToString();
            return retVal;
        }
        public int GetAmntOfSuccessfulLookups(string pUDID)
        {
            int amntUsed = 0;
            string retVal = "0";
            string[][] data = sqlh.GetMultiValuesOfSQL("Select [LookupCount] from tblSuccessfulLookupCount WHERE [UDID]=@P0", pUDID);
            retVal = data[0][0];
            if (retVal == "") retVal = "0";
            amntUsed = Convert.ToInt16(retVal);
            return amntUsed;
        }
        public int GetAmntOfLookupsAvail(string pUDID)
        {
            int amntAllowed = 2;
            string[][] overridedata = sqlh.GetMultiValuesOfSQL("Select [PurchaseType] from tblPurchases WHERE [UUID]=@P0", pUDID);
            int loopamnt = MVDataRowCount(overridedata);
            for (int i = 0; i < loopamnt; i++)
            {
                amntAllowed = amntAllowed + Convert.ToInt16(overridedata[i][0]);
            }
            return amntAllowed;
        }
        public string LogConsideredBuying(string UUID, string Option)
        {
            string retVal = "";
            int temp;
            temp = sqlh.ExecuteSQLParamed("INSERT INTO tblConsideredBuying (UUID, PurchOption, TimeLogged) VALUES (@P0,@P1,@P2)", UUID, Option, DateTime.Now.ToString());
            retVal = temp.ToString();
            return retVal;
        }
        public string LogPurchase(string pSessionID, string pCheckSum, string pUUID, string pIP, string pPurchaseType)
        {
            string retVal = "";

            int temp;
            if (IsRequestValid(pSessionID, pCheckSum, pIP) == true)
            {
                if (pPurchaseType == "3")
                {
                    temp = sqlh.ExecuteSQLParamed("INSERT INTO tblAdsClicked (UUID, Details, DateLogged) VALUES (@P0,@P1,@P2)", pUUID, "Banner", DateTime.Now.ToString());
                }
                if (pPurchaseType == "5")
                {
                    temp = sqlh.ExecuteSQLParamed("INSERT INTO tblAdsClicked (UUID, Details, DateLogged) VALUES (@P0,@P1,@P2)", pUUID, "Interstitial", DateTime.Now.ToString());
                }
                //Leave in for now...
                if (pPurchaseType == "GiftCardGenie001")
                {
                    pPurchaseType="1000";
                }
                if (pPurchaseType == "15for1")
                {
                    pPurchaseType = "16";
                }
                temp = sqlh.ExecuteSQLParamed("INSERT INTO tblPurchases (UUID, TimeLogged, PurchaseType) VALUES (@P0,@P1,@P2)", pUUID, DateTime.Now.ToString(), pPurchaseType);
                retVal = temp.ToString();
            }
            return retVal;
        }

        private bool SendMail(string name,string email, string comment)
        {
            var fromAddress = new MailAddress("temp1@mc2techservices.com", "GCG Temp Email");
            var toAddress = new MailAddress("service@mc2techservices.com", "GCG Support Page");
            const string fromPassword = "temppass";
            const string subject = "Feedback";
            string body = "Name: " + name + Environment.NewLine + "Email: " + email + Environment.NewLine + comment;
            var smtp = new SmtpClient
            {
                //Host ="smtp.googlemail.com" Port=465
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    smtp.Send(message);
                    System.Diagnostics.Debug.WriteLine("Message sent!");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return true;
        }
        public string GetSupportedMerchants()
        {
            string retVal = "";
            string tempLine = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("Select CleanName, TestCardNum, TestCardPIN from qryMerchantsSupported", sqlh.aConnection);
                da.Fill(ds, "RandomData");
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    tempLine = dr["CleanName"].ToString() + POSDEL + dr["TestCardNum"].ToString() + POSDEL + dr["TestCardPIN"].ToString() + LINEDEL;
                    sb.Append(tempLine);
                }
            }
            catch (OleDbException ex)
            {
                retVal = ex.Message;
                return retVal;
            }
            retVal = sb.ToString();
            return retVal;
        }
        public string UUIDRestrictions(string UUID, string BorU,string Reason)
        {
            string retVal="";
            if (BorU == "B")
            {
                int temp = sqlh.ExecuteSQLParamed("INSERT INTO tblBlockedUUIDs (UUID, Reason, TimeLogged) VALUES  (@P0, @P1, @P2)", UUID,Reason, DateTime.Now.ToString());
                retVal = temp.ToString();
                return retVal;
            }
            else if (BorU == "U")
            {
                int temp = sqlh.ExecuteSQLParamed("DELETE FROM tblBlockedUUIDs WHERE UUID=@P0", UUID);
                retVal = temp.ToString();
                return retVal;
            }
            return retVal;
        }
        static bool OurCertificateValidation(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var actualCertificate = X509Certificate.CreateFromCertFile("example.com.cert");
            return certificate.Equals(actualCertificate);
        }
        public string GetURLForEXE(string pEXE, string pPassword)
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT URL FROM tblMerchants WHERE EXE=@P0", pEXE);
            //retVal = data[0][1];
            string URL = data[0][0];
            retVal = URL;
            return retVal;
        }
        public string SaveURLForEXE(string pEXE, string pURL, string pPassword)
        {
            string retVal = "";
            int temp1 = sqlh.ExecuteSQLParamed("UPDATE tblMerchants SET URL=@P0 WHERE EXE=@P1", pURL, pEXE);
            retVal=temp1.ToString();
            return retVal;
        }
        public string GetWDDataForEXE(string pEXE)
        {
            string retVal = "";
            //, TestCardNum, TestCardPIN, TestLogin, TestPass 
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT URL, TestCardNum, TestCardPIN, TestLogin, TestPass, CleanName, SupportCode, Timeout FROM tblMerchants WHERE EXE=@P0", pEXE);
            retVal = data[0][0] + POSDEL + data[0][1] + POSDEL + data[0][2] + POSDEL + data[0][3] + POSDEL + data[0][4] + POSDEL + data[0][5] + POSDEL + data[0][6] + POSDEL + data[0][7];
            return retVal;
        }
        public string SaveWDDataForEXE(string pEXE, string pCleanName, string pURL, string pCardNum, string pCardPIN, string pCredLogin, string pCredPassword, string pSupportCode, string pTimeout)
        //string pEXE, string pURL, string pCardNum, string pCardPIN, string pCredLogin, string pCredPassword
        {
            string retVal = "";
            int retValInt = -1;
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT Count(*) FROM tblMerchants WHERE EXE=@P0", pEXE);
            int InsTest=Convert.ToInt16(data[0][0]);
            if (InsTest == 0)
            {
                retValInt = sqlh.ExecuteSQLParamed("INSERT INTO tblMerchants (URL, TestCardNum, TestCardPIN, TestLogin, TestPass, CleanName, SupportCode, Timeout, EXE) VALUES (@P0, @P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8)", pURL, pCardNum, pCardPIN, pCredLogin, pCredPassword, pCleanName, pSupportCode, pTimeout, pEXE);
            }
            else
            {
                retValInt = sqlh.ExecuteSQLParamed("UPDATE tblMerchants SET URL=@P0, TestCardNum=@P1, TestCardPIN=@P2, TestLogin=@P3, TestPass=@P4, CleanName=@P5, SupportCode=@P6, Timeout=@7 WHERE EXE=@P8", pURL, pCardNum, pCardPIN, pCredLogin, pCredPassword, pCleanName, pSupportCode, pTimeout, pEXE);
            }
            retVal = retValInt.ToString();
            return retVal;
        }
    }
}