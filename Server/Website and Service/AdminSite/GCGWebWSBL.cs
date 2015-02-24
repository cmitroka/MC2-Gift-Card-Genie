using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AppAdminSite
{
    public class GCGWebWSBL
    {
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        string gloPathToRqRs;
        string gloPathToMerchantEXEs;
        string gloCAPTCHAURLPrefix;
        string gloWebserviceTimeout;
        public string gloHacker;
        int gloiWebserviceTimeout;
        GCGCommon.SQLHelper sqlh;

        public GCGWebWSBL()
        {
            sqlh = new GCGCommon.SQLHelper(GCGCommon.MDBBaseLoc.CurrentDomainBaseDirectory, "App_Data\\GCGApp.mdb");
            string[] ConfigParams = GCGWebWSSM.GetConfigParams();
            gloPathToRqRs = ConfigParams[0];
            gloPathToMerchantEXEs = ConfigParams[1];
            gloCAPTCHAURLPrefix = ConfigParams[2];
            gloWebserviceTimeout = ConfigParams[3];
            gloiWebserviceTimeout = Convert.ToInt16(gloWebserviceTimeout);

            string VisitorsIPAddr = GetUser_IP();
            DateTime adjDate = DateTime.Now.AddDays(-7);
            sqlh.ExecuteSQLParamed("DELETE * FROM tblTraffic WHERE DateLogged<@P0", adjDate.ToString());

            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT Count(*) FROM tblBlockedIPs WHERE BlockedIP=@P0", VisitorsIPAddr);
            string Amnt = data[0][0];
            int iAmnt = Convert.ToInt16(Amnt);
            if (iAmnt > 0)
            {
                gloHacker = "1";
                return;
            }
            sqlh.ExecuteSQLParamed("INSERT INTO tblTraffic (RecdIP, DateLogged) VALUES (@P0, @P1)", VisitorsIPAddr, DateTime.Now.ToString());
            adjDate = DateTime.Now.AddDays(-1);
            data = sqlh.GetMultiValuesOfSQL("SELECT Count(*) FROM tblTraffic WHERE RecdIP=@P0 AND DateLogged>@P1", VisitorsIPAddr, adjDate.ToString());
            Amnt = data[0][0];
            iAmnt = Convert.ToInt16(Amnt);
            if (iAmnt>99)
            {
                sqlh.ExecuteSQLParamed("INSERT INTO tblBlockedIPs (BlockedIP,DateLogged) VALUES (@P0,@P1)", VisitorsIPAddr, DateTime.Now.ToString());
                gloHacker="1";
            }
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

        public string DemoGCG(string pIP)
        {
            string retVal = "";
            string temp0=RegisterUserIns("", "", "", "");
            string[] temp1 = GCGCommon.SupportMethods.SplitByString(temp0, POSDEL);
            retVal = temp1[1];
            return retVal;
        }
        public string RegisterUserIns(string pGCGLogin, string pGCGPassword, string pUsersName, string pUsersEmail)
        {
            string retVal="";
            bool bDemo = false;
            string pGCGKey = "";
            string GCGID = "";
            string zpAll = pGCGLogin+"z"+pGCGPassword+"z"+pUsersName+"z"+pUsersEmail;
            if ((pGCGLogin == "") && (pGCGPassword == "") && (pUsersName == "") && (pUsersEmail == "")) bDemo = true;


            //MD5 md5Hash = MD5.Create();
            //string encUserPass = AppAdminSite.CSharp.MD5Lib.GetMd5Hash(UserPass);
            if (bDemo == true)
            {
                pGCGLogin = "GCGDemoUser" + GCGCommon.SupportMethods.CreateHexKey(20);
                System.Threading.Thread.Sleep(100);
                pGCGPassword = GCGCommon.SupportMethods.CreateHexKey(20);
            }
            else
            {
                pGCGLogin = pGCGLogin.Trim();
                pGCGPassword = pGCGPassword.Trim();
                pUsersName = pUsersName.Trim();
                pUsersEmail = pUsersEmail.Trim();
                retVal = GCGWebWSSM.ValidateGCGLogin(pGCGLogin);
                if (retVal != "1")
                {
                    return retVal;
                }
                retVal = GCGWebWSSM.ValidateGCGPassword(pGCGPassword);
                if (retVal != "1")
                {
                    return retVal;
                }
            }

            int OK = sqlh.ExecuteSQLParamed("INSERT INTO tblGCGUsers (GCGLogin, GCGPassword, DateLogged) VALUES (@P0, @P1,@P2)", pGCGLogin, pGCGPassword, DateTime.Now.ToString());
            if (OK != -1)
            {
                pGCGKey = GCGLogin(pGCGLogin, pGCGPassword);
                if (bDemo == true)
                {
                    retVal = "1" + POSDEL + pGCGKey;  // +"???" + zpAll;
                    return retVal;
                }
            }
            else
            {
                retVal = "-1"+POSDEL+"That username/password entry had a registration problem, try with other values.";
                return retVal;
            }


            if (pGCGKey != "")
            {
                GCGID = "-1";
                for (int i = 0; i < 6; i++)
                {
                    if (GCGID == "-1")
                    {
                        GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
                        System.Threading.Thread.Sleep(250);
                    }
                    else
                    {
                        OK = sqlh.ExecuteSQLParamed("INSERT INTO tblGCGUserInfo (GCGUsersID, UsersName, UsersEmail, DateLogged) VALUES (@P0,@P1,@P2,@P3)", GCGID, pUsersName, pUsersEmail, DateTime.Now.ToString());
                        if (OK == -1)
                        {
                            retVal = "-1" + POSDEL + "Your username and login were registered, but the other information you provided didn't.  Email as at service@mc2techservice with your additional information if you'd like.";
                            return retVal;
                        }
                        break;
                    } 
                }
            }
            retVal = "1" + POSDEL + pGCGKey + POSDEL + pGCGLogin + POSDEL + pGCGPassword;
            return retVal;
        }

        public string ChangePassword(string pGCGKey, string pOldPassword, string pNewPassword)
        {
            string retVal = "";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1") return retVal;
            if (pNewPassword.Length < 4) return "0" + POSDEL + "Your password has to be at least 4 characters.";
            if (pNewPassword.Length > 32) return "0" + POSDEL + "Your password cant be more than 32 characters.";
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM tblGCGUsers WHERE GCGUsersID=@P0 AND GCGPassword=@P1", GCGID, pOldPassword);
            if (CommonForWS.isDatasetBad(data))
            {
                return "0" + POSDEL + "The old password you gave didn't match our records, your password is unchanged.";
            }
            int temp1 = sqlh.ExecuteSQLParamed("UPDATE tblGCGUsers SET GCGPassword=@P0 WHERE GCGUsersID=@P1", pNewPassword, GCGID);
            if (temp1 == 1)
            {
                retVal = "1" + POSDEL + "Your password has been changed.";
            }
            else
            {
                retVal = "0" + POSDEL + "There was a problem changing your password.  Please try a different password.";
            }
            return retVal;
        }
        public string GCGLogin(string pGCGLogin, string pGCGPassword)
        {
            string retVal = "";
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT GCGUsersID FROM tblGCGUsers WHERE GCGLogin=@P0 AND GCGPassword=@P1", pGCGLogin, pGCGPassword);
            if (CommonForWS.isDatasetBad(data)) return "1234567890";
            string GCGID = data[0][0];
            string UpdatedKey = GCGCommon.SupportMethods.CreateHexKey(15);
            int temp1 = sqlh.ExecuteSQLParamed("UPDATE tblGCGUsers SET GCGKey=@P0, KeyTime='" + DateTime.Now.ToString() + "' WHERE GCGUsersID=@P1", UpdatedKey, GCGID);
            if (temp1 == 1)
            {
                retVal = UpdatedKey;
            }
            return retVal;
        }
        public string GCGLogUser(string pGCGKey, string pChannel)
        {
            string retVal = "";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            int temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblUserLog (GCGUsersID, Channel, DateLogged) VALUES (@P0,@P1,@P2)", GCGID, pChannel, DateTime.Now.ToString());
            return retVal;
        }
        
        public string CardBalSummarySel(string pGCGKey)
        {
            string retVal = "";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1") return retVal;
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM qryRUCardBalances WHERE GCGUsersID=@P0", GCGID);
            if (CommonForWS.isDatasetBad(data)) return null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string template = "";
            int max = data.Length;
            for (int i = 0; i < max; i++)
            {
                template = "";
                string CardType = data[i][0];
                string CardAmount = data[i][1];
                decimal temp1 = Convert.ToDecimal(CardAmount);
                CardAmount = String.Format("{0:C}", temp1);
                template = "" +
                "<li>" +
                "<h3>" + CardType + "</h3>" +
                "<p class=\"ui-li-aside\">" + CardAmount + "</p>" +
                "</li>";
                sb.Append(template);
            }
            retVal = sb.ToString();
            return retVal;
        }
        public string MyCardsDataSel(string pGCGKey)
        {
            string retVal = "";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT * FROM qryRUCardData WHERE GCGUsersID=@P0", GCGID);
            if (CommonForWS.isDatasetBad(data)) return null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string template = "";
            int max = data.Length;
            for (int i = 0; i < max; i++)
            {
                template = "";
                string CardInfoID = data[i][0];
                string CardType = data[i][2];
                string CardNumber = data[i][3];
                string CardPIN = data[i][4];
                string CardLogin = data[i][5];
                string CardPassword = data[i][6];
                string LastKnownBalance = data[i][7];
                string LastKnownBalanceDate = data[i][8];
                decimal temp1 = Convert.ToDecimal(data[i][10]);
                //LastKnownBalance = Convert.ToBase64CharArray data[i][10];
                LastKnownBalance = String.Format("{0:C}", temp1);
                string AllowAutolookup = data[i][11];
                string JSCardType = CardType.Replace("'", "\\'");
                string CardTypeID = GCGCommon.SupportMethods.RemoveNonAlphaNumericChars(CardType);
                string ConcatData = CardInfoID + POSDEL + CardType + POSDEL + CardNumber + POSDEL + CardPIN + POSDEL + CardLogin + POSDEL + CardPassword + POSDEL + LastKnownBalance + POSDEL + LastKnownBalanceDate + POSDEL + AllowAutolookup;
                template = "" +
                "<li>";
                if (AllowAutolookup == "1")
                {
                    template = template + "<a onclick=\"DoLoadAddModCardScreen(document.getElementById('MyCard" + CardInfoID + "').value, document.getElementById('" + CardTypeID + "').value)\">" +
                    "<h3>" + CardType + "</h3>";
                }
                else
                {
                    //template = template + "<a onclick=\"DoLoadAddModCardScreen(document.getElementById('MyCard" + CardInfoID + "').value, '~_~0~_~998~_~0~_~996~_~0')\">";
                    template = template + "<a href=\"javascript:DoLoadAddModCardScreen(document.getElementById('MyCard" + CardInfoID + "').value, '~_~3~_~998~_~0~_~996~_~0')\">" +
                    "<h3><span style=\"color: #FF3300\">" + CardType + "</span></h3>";
                }

                template = template + ""+
                    "<p>" + CardNumber + "</p>" +
                    //"</a>" +
                    //"<span class=\"ui-li-count\">12</span>" +
                "<p class=\"ui-li-aside\">" + LastKnownBalanceDate + "<br>" + LastKnownBalance + "</p>" +
                "<input id=\"MyCard" + CardInfoID + "\" type=\"hidden\" value=\"" + ConcatData + "\" />" +
                "</a>" +
                "</li>";
                sb.Append(template);
            }
            retVal = sb.ToString();
            return retVal;
        }
        public string GetSupportedCards(string pGCGKey)
        {
            string retVal = "";
            if (GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey) == "-1") return retVal;
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT CleanName, TrueCardNumMin, CardNumMax, TruePINMin, PINMax, showCreds FROM qryMerchantsSupported ORDER BY CleanName");
            if (GCGCommon.DB.isDatasetBad(data)) return null;
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
                string CardTypeID = GCGCommon.SupportMethods.RemoveNonAlphaNumericChars(CleanName);
                string ConcatData = CleanName + POSDEL + TrueCardNumMin + POSDEL + CardNumMax + POSDEL + TruePINMin + POSDEL + PINMax + POSDEL + showCreds;
                string FakeParam = "NA" + POSDEL + CleanName + POSDEL + "" + POSDEL + "" + POSDEL + "" + POSDEL + "";
                FakeParam = FakeParam.Replace("'", "\\'");
                ConcatData = ConcatData.Replace("'", "\\'");
                template = "" +
                "<li>" +
                "<a onclick=\"DoLoadAddModCardScreen('" + FakeParam + "\', document.getElementById('" + CardTypeID + "').value)\">" + CleanName +
                "<input id=\"" + CardTypeID + "\" type=\"hidden\" value=\"" + ConcatData + "\" />" +
                "</a>" +
                "</li>";
                //if (CleanName.Contains("Low"))
                //{
                //    GCGCommon.SupportMethods.WriteFile("C:\\template", template, true);
                //}
                sb.Append(template);
            }
            retVal = sb.ToString();
            return retVal;
        }
        public string RUCardDataMod(string pGCGKey, string CardID, string CardType, string CardNumber, string CardPIN, string CardLogin, string CardPass, string LastKnownBalance, string LastKnownBalanceDate)
        {
            string retVal = "";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1") return "Couldn't Save, invalid GCGID";
            int temp1 = -1;
            if (CardID == "NA")
            {
                //It's an insert
                temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblRUCardData (GCGUsersID,CardType,CardNumber,CardPIN, CardLogin, CardPassword, DateLogged) VALUES (@P0,@P1,@P2,@P3,@P4,@P5,@P6)", GCGID, CardType, CardNumber, CardPIN, CardLogin, CardPass, DateTime.Now.ToString());
            }
            else if (CardNumber == "")
            {
                //We're just updating the balance info
                temp1 = sqlh.ExecuteSQLParamed("UPDATE tblRUCardData SET LastKnownBalance=@P0, LastKnownBalanceDate=@P1 WHERE RUCardDataID=@P2 AND GCGUsersID=@P3", LastKnownBalance, DateTime.Now.ToString(), CardID, GCGID);
            }
            else if (CardNumber == "-1")
            {
                //We're deleting the card
                temp1 = sqlh.ExecuteSQLParamed("DELETE FROM tblRUCardData WHERE RUCardDataID=@P0 AND GCGUsersID=@P1", CardID, GCGID);
            }
            else
            {
                //We're updating the card with all the info we have
                if (CardType == "") return "Couldn't Save, No Card Type";
                temp1 = sqlh.ExecuteSQLParamed("UPDATE tblRUCardData SET CardType=@P0, CardNumber=@P1, CardPIN=@P2, CardLogin=@P3, CardPassword=@P4 WHERE RUCardDataID=@P5 AND GCGUsersID=@P6", CardType, CardNumber, CardPIN, CardLogin, CardPass, CardID, GCGID);
            }
            retVal = temp1.ToString();
            return retVal;
        }
        public string NewRequest(string pGCGKey, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword)
        {
            string retVal = "";
            string CAPTCHAAdditionalInfo = "";
            //pGCGKey = "FAIL";
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1")
            {
                GCGWebWSSM.InsertFailedRequest(pGCGKey, pCardType, pCardNumber, pPIN, pLogin, pPassword);
                return "Couldn't do request, invalid GCGID";
            }
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT GeneralNote FROM tblMerchants WHERE CleanName=@P0", pCardType);
            if (CommonForWS.isDatasetBad(data)==false) CAPTCHAAdditionalInfo = data[0][0];
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            string RqRsFileName = "";
            string rsValue = "";
            var testDate = DateTime.Now;
            RqRsFileName = testDate.ToString("yyyyMMdd_HHmmssfff");
            pCardNumber = pCardNumber.Replace(" ", "");
            pPIN = pPIN.Replace(" ", "");
            string[] LookupsCntData = GCGWebWSSM.GetLookupsCntData(GCGID);
            int AmntTest = Convert.ToInt16(LookupsCntData[3]);
            if (AmntTest <= 0)
            {
                rsType = "OUTOFLOOKUPS";
                rsValue = "Sorry, you're out of free lookups - please consider purchasing Gift Card Genie.";
                string OK = GCGWebWSSM.InsertNewRequest(GCGID, RqRsFileName, pCardType, "", "", "", "");
                OK = GCGWebWSSM.InsertReponse(GCGID, RqRsFileName, "OUTOFLOOKUPS", rsValue);
                retVal = rsType + LINEDEL + rsValue;
                return retVal;
            }
            try
            {
                string LogLevel = "2";
                bool FileMade = false;
                if (LogLevel == "1")
                {
                    string OK = GCGWebWSSM.InsertNewRequest(GCGID, RqRsFileName, pCardType, "", "", "", "");
                }
                else
                {
                    string OK = GCGWebWSSM.InsertNewRequest(GCGID, RqRsFileName, pCardType, pCardNumber, pPIN, pLogin, pPassword);
                }
                string MakeRqFile = gloPathToRqRs + "\\" + RqRsFileName + "-0rq.txt";
                string RsFileToRead = gloPathToRqRs + "\\" + RqRsFileName + "-0rs.txt";
                while (System.IO.File.Exists(MakeRqFile) == true)
                {
                    System.IO.File.Delete(MakeRqFile);
                }
                while (System.IO.File.Exists(RsFileToRead) == true)
                {
                    System.IO.File.Delete(RsFileToRead);
                }

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
                GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceCreated1", "NewRequest - Created ", "W");
                string filetomake = "";
                try
                {
                    filetomake = gloPathToRqRs + "\\" + RqRsFileName + "bat.txt";
                    s = new System.IO.StreamWriter(filetomake);
                    string pFileName = @"""" + gloPathToMerchantEXEs + "\\" + EXE + ".exe" + @"""";
                    string pArguments = @"""" + MakeRqFile + @""" 1";
                    s.Write(pFileName + " " + pArguments);
                    s.Close();
                    GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceCreated2", "NewRequest - Created " + filetomake, "I");
                }
                catch (Exception)
                {
                    GCGCommon.SupportMethods.WriteToWindowsEventLog("GCG WebserviceFailed", "NewRequest - Failed " + filetomake, "W");
                }

                FileMade = GCGWebWSSM.WaitForResponseFileCreation(RsFileToRead, gloiWebserviceTimeout);
                if (FileMade == true)
                {
                    retVal = GCGWebWSSM.ProcessResponse(RsFileToRead);
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
            GCGWebWSSM.InsertReponseUsingRetVal(GCGID, RqRsFileName, retVal);
            if (CAPTCHAAdditionalInfo != "")
            {
                retVal = retVal + LINEDEL + CAPTCHAAdditionalInfo;
            }
            return retVal;
        }
        public string LogPurchase(string pGCGKey, string pPurchType, string pKey, string pChannel)
        {
            string pErrorDescription = "";
            string pOtherInfo1 = "";
            string pOtherInfo2 = "";
            string pOtherInfo3 = "";
            string ErrorCode = "";
            int temp1 = -1;
            string retVal = "";
            string FileID = "";
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1")
            {
                pErrorDescription = "A purchase attempt failed.";
                pOtherInfo1 = "pGCGKey=" + pGCGKey + "   pPurchType=" + pPurchType + "   pKey=" + pKey + "   pChannel=" + pChannel;
                pOtherInfo2 = "Couldn't find a GCGUsersID based on the GCGKey";
                pOtherInfo3 = "";
                temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblErrorLog (ErrorDescription,OtherInfo1,OtherInfo2,OtherInfo3,DateLogged) VALUES (@P0,@P1,@P2,@P3,@P4)", pErrorDescription, pOtherInfo1, pOtherInfo2, pOtherInfo3, DateTime.Now.ToString());
                ErrorCode = ErrorCode + "2";
            }
            if (GCGWebWSSM.IsKeyLegit(pGCGKey, pKey) == "1")
            {
                //Insert the sale
                temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblPurchases (GCGUsersID,PurchaseType,Channel,DateLogged) VALUES (@P0,@P1,@P2,@P3)", GCGID, pPurchType, pChannel, DateTime.Now.ToString());
            }
            else
            {
                pErrorDescription = "A purchase attempt failed.";
                pOtherInfo1="pGCGKey="+pGCGKey+"   pPurchType="+pPurchType+"   pKey="+pKey;
                pOtherInfo2 = "Keys didn't match";
                pOtherInfo3 = GCGWebWSSM.GetLegitValue(pGCGKey, pKey);
                temp1 = sqlh.ExecuteSQLParamed("INSERT INTO tblErrorLog (ErrorDescription,OtherInfo1,OtherInfo2,OtherInfo3,DateLogged) VALUES (@P0,@P1,@P2,@P3,@P4)", pErrorDescription, pOtherInfo1, pOtherInfo2, pOtherInfo3, DateTime.Now.ToString());
                ErrorCode = ErrorCode + "3";
            }
            if (ErrorCode.Length == 0) ErrorCode = "1";
            retVal = ErrorCode;
            return retVal;
        }
        public string ContinueRequest(string pGCGKey, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            string FileID = "";
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1") return "Couldn't do continue request, invalid GCGID";
            try
            {
                string[] FileIDArr = GCGCommon.SupportMethods.SplitByString(pIDFileName, "-");
                FileID = FileIDArr[0];
                bool FileMade = false;
                string Good = GCGWebWSSM.InsertContinueRequest(GCGID, pIDFileName, pAnswer);
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
                FileMade = GCGWebWSSM.WaitForResponseFileCreation(RsFileToRead, gloiWebserviceTimeout);
                if (FileMade == true)
                {
                    retVal = GCGWebWSSM.ProcessResponse(RsFileToRead);
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
            string OK = GCGWebWSSM.InsertReponseUsingRetVal(GCGID, FileID, retVal);
            return retVal;
        }
        public string MyProfileSel(string pGCGKey)
        {
            string retVal = "";
            string FileID = "";
            string rsType = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString();
            string GCGID = GCGWebWSSM.GCGKeyToGCGUsersID(pGCGKey);
            if (GCGID == "-1") return "Couldn't do continue request, invalid GCGID";
            string[] LookupsCntData = GCGWebWSSM.GetLookupsCntData(GCGID);
            retVal=LookupsCntData[0]+LINEDEL+LookupsCntData[1]+LINEDEL+LookupsCntData[2]+LINEDEL+LookupsCntData[3];
            return retVal;
        }
    }
}