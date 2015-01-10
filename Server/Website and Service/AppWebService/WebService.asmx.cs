﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.OleDb;
using System.IO;

namespace AppWebService
{
    /// <summary>
    /// Summary description for GCWebService
    /// </summary>
    [WebService(Namespace = "gcg.mc2techservices.com")]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod(Description = @"Will just return 'Hello'")]
        public string DiagSayHello()
        {
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return "Hello";
        }
        [WebMethod(Description = @"Will return 'Hello <name>'")]
        public string DiagSayHelloTo(string nameIn)
        {
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return "Hello " + nameIn;
        }

        [WebMethod(Description = @"")]
        public bool Login(string UserName, string Password)
        {
            bool retVal = false;
            if (UserName.ToUpper() == "CJM" && Password.ToUpper() == "GCG")
            {
                retVal = true;
            }
            if (UserName.ToUpper() == "BETA" && Password.ToUpper() == "BETA1")
            {
                retVal = true;
            }
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string GetURLForEXE(string pEXE, string pPassword)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetURLForEXE(pEXE, pPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string SaveURLForEXE(string pEXE, string pURL, string pPassword)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.SaveURLForEXE(pEXE, pURL, pPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string GetWDDataForEXE(string pEXE, string pPassword)
        {
            string retVal ="";
            BusinessLogic bl = new BusinessLogic();
            if (pPassword != "CJMGCG") return retVal;
            retVal = bl.GetWDDataForEXE(pEXE);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string ImageToOCR(byte[] dataIn)
        {
            try
            {
                string retVal = "";
                if (dataIn != null) //The image came from the web, where it was uploaded as C:\GCG\Tesseract-OCR\OCRthis.jpg
                {
                    MemoryStream ms = new MemoryStream(dataIn);
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    returnImage.Save(@"C:\GCG\Tesseract-OCR\OCRthis.jpg");
                }
                GCGCommon.SupportMethods.LaunchIt(@"C:\GCG\Tesseract-OCR\tesseract.exe", @"C:\GCG\Tesseract-OCR\OCRthis.jpg C:\GCG\Tesseract-OCR\Output digits", true, false);
                StreamReader sr = new StreamReader(@"C:\GCG\Tesseract-OCR\Output.txt");
                retVal = sr.ReadLine();
                sr.Close();
                File.Delete(@"C:\GCG\Tesseract-OCR\Output.txt");

                retVal = new String(retVal.Where(Char.IsDigit).ToArray());
                return retVal;
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        [WebMethod(MessageName = "SaveWDDataForEXE01")]
        public string SaveWDDataForEXE(string pEXE, string pURL, string pCardNum, string pCardPIN, string pCredLogin, string pCredPassword, string pPassword, string pCleanName, string pTimeout)
        {
            string retVal = "";
            if (pPassword != "CJMGCG") return retVal;
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.SaveWDDataForEXE(pEXE, pURL, pCardNum, pCardPIN, pCredLogin, pCredPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string SaveWDDataForEXE(string pEXE, string pURL, string pCardNum, string pCardPIN, string pCredLogin, string pCredPassword, string pPassword)
        {
            string retVal = "";
            if (pPassword != "CJMGCG") return retVal;
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.SaveWDDataForEXE(pEXE, pURL, pCardNum, pCardPIN, pCredLogin, pCredPassword);
            bl.CloseIt();
            return retVal;
        }

        [WebMethod(Description = @"")]
        public string AddMerchantRequest(string pMerchant, string pURL, string pCardNum, string pCardPIN, string pOtherInfo)
        {
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.AddMerchantRequest(pMerchant, pURL, pCardNum, pCardPIN, pOtherInfo, ip);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string GetSessionIDAndAdInfo(string UUID,string CardType)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetSessionIDAndAdInfo(UUID,CardType);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DownloadAllData()
        {
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.DownloadAllData();
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string NewRequest(string UUID, string SessionID, string CheckSum, string CardType, string CardNumber, string PIN, string Login, string Password)
        {
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.NewRequest(UUID, SessionID, CheckSum, CardType, CardNumber, PIN, Login, Password, ip);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string ArchiveNewVendorRequest(string UUID, string Merchant, string URL, string CardNumber, string CardPIN, string TimeLogged)
        {
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.ArchiveNewVendorRequest(UUID, Merchant, URL, CardNumber, CardPIN, TimeLogged);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string ContinueRequest(string pUUID, string pIDFileName, string pAnswer)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.ContinueRequest(pUUID, pIDFileName, pAnswer);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"gloPathToRqRs, gloPathToMerchantEXEs, gloCAPTCHAURLPrefix, gloWebserviceTimeout")]
        public string GetGlobalSettings()
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetGlobalSettings();
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string GetMerchantStatuses(string pSupportCode)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetMerchantStatuses(pSupportCode); //GetMerchantStatuses();
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GetMerchantCount()
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetMerchantCount();
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string CheckWebConfig()
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.CheckWebConfig();
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string RecordFeedback(string UUID, string Name, string ContactInfo, string Feedback)
        {
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.RecordFeedback(ip,UUID, Name, ContactInfo, Feedback);
            bl.CloseIt();
            return retVal;
        }
        //Replaces DoLogUser, CheckIfTrialIsExpired and
        [WebMethod(Description = @"This logs the user, then reponsed with http://gcg.mc2techservices.com/CAPTCHAs/@#@We're Upgrading!@#@0")]
        public string DoAppStartup(string UUID)
        {

            string retVal = "";
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.DoAppStartup(UUID, ip);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"This logs the user, then reponsed with http://gcg.mc2techservices.com/CAPTCHAs/@#@We're Upgrading!@#@0")]
        public string DoAppStartup2(string UUID, string Version)
        {
            string retVal = "";
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.DoAppStartup(UUID, Version, ip);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string LogPurchase(string SessionID, string CheckSum, string UUID, string PurchaseType)
        {
            string retVal = "";
            string ip = GetIPAddress();
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.LogPurchase(SessionID, CheckSum, UUID, ip, PurchaseType);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string LogConsideredBuying(string UUID, string Option)
        {
            string retVal = "";
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.LogConsideredBuying(UUID, Option);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string DeleteOldBackup(string UUID)
        {
            string retVal = "";
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.DeleteOldBackup(UUID);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string BackupData(string UUID, string AllData)
        {
            string retVal = "";
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.BackupData(UUID, AllData);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(Description = @"")]
        public string RetrieveData(string UUID, string RetrievalKey)
        {
            string retVal = "";
            BusinessLogic bl = new BusinessLogic();
            retVal = bl.RetrieveData(UUID, RetrievalKey);
            bl.CloseIt();
            return retVal;  //Sends back UUID and AllData
        }
        private string GetIPAddress()
        {
            string ip = "";
            ip = Context.Request.UserHostAddress;
            if (ip == string.Empty)
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return ip;
        }
        public string GetSupportedMerchants()
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.GetSupportedMerchants();
            bl.CloseIt();
            return retVal;
        }
        public string UUIDRestrictions(string UUID, string BorU, string Reason)
        {
            BusinessLogic bl = new BusinessLogic();
            string retVal = bl.UUIDRestrictions(UUID, BorU, Reason);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public OutputData AjaxGetMore(InputData input)
        {
            return new OutputData()
            {
                id = input.id,
                //message = new List { "it's work!", "OK!" },
                myInt = new int[] { input.myInt[0] + 1, input.myInt[1] + 1, 20, 75 },
                myComplexData = new InternalData() { blaBla = "haha", iii = new int[] { 1, 2, 3, 4, 5 } }
            };
        }
    }
    public class InternalData
    {
        public string blaBla { get; set; }
        public int[] iii { get; set; }
    }
    public class OutputData
    {
        public string id { get; set; }
        //public List message { get; set; }
        public int[] myInt { get; set; }
        public InternalData myComplexData { get; set; }
    }
    public class InputData
    {
        public string id { get; set; }
        public int[] myInt { get; set; }
        public InternalData data { get; set; }
    }

}
