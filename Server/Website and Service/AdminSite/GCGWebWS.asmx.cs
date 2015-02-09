﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Web.Script.Services;

namespace AppAdminSite
{
    /// <summary>
    /// Summary description for GCGWebWS
    /// </summary>
    [WebService(Namespace = "gcg.mc2techservices.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GCGWebWS : System.Web.Services.WebService
    {
        string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
        string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string RegisterUserIns(string pGCGLogin, string pGCGPassword, string pUsersName, string pUsersEmail)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.RegisterUserIns(pGCGLogin, pGCGPassword, pUsersName, pUsersEmail);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string DemoGCG(string pIP)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.DemoGCG(pIP);
            bl.CloseIt();
            return retVal;
        }

        [WebMethod]
        public string ChangePassword(string pGCGKey, string pOldPassword, string pNewPassword)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal=bl.ChangePassword(pGCGKey, pOldPassword, pNewPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GCGLogin(string pGCGLogin, string pGCGPassword)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.GCGLogin(pGCGLogin, pGCGPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string CardBalSummarySel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.CardBalSummarySel(pGCGKey);
            bl.CloseIt();
            //GCGCommon.SupportMethods.WriteFile("C:/Output.txt", retVal, true);
            return retVal;
        }
        [WebMethod]
        public string MyCardsDataSel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.MyCardsDataSel(pGCGKey);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GetSupportedCards(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.GetSupportedCards(pGCGKey);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string RUCardDataMod(string pGCGKey, string CardID, string CardType, string CardNumber, string CardPIN, string CardLogin, string CardPass, string LastKnownBalance, string LastKnownBalanceDate)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.RUCardDataMod(pGCGKey,CardID,CardType,CardNumber,CardPIN,CardLogin,CardPass,LastKnownBalance,LastKnownBalanceDate);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string NewRequest(string pGCGKey, string pCardType, string pCardNumber, string pPIN, string pLogin, string pPassword)
        {
            string retVal = "";
            //retVal = "OUTOFLOOKUPS^)(OUT OF LOOKUPS";
            //retVal = "GCBALANCE^)($11.00";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.NewRequest(pGCGKey, pCardType, pCardNumber, pPIN, pLogin, pPassword);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string ContinueRequest(string pGCGKey, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.ContinueRequest(pGCGKey, pIDFileName, pAnswer);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string LogPurchase(string pGCGKey, string pPurchType, string pKey, string pChannel)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.LogPurchase(pGCGKey, pPurchType, pKey, pChannel);
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string MyProfileSel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            retVal = bl.MyProfileSel(pGCGKey);
            bl.CloseIt();
            return retVal;
        }

    }
}