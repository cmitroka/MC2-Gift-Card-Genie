using System;
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
            if (bl.gloHacker != "1")
            {
                retVal = bl.RegisterUserIns(pGCGLogin, pGCGPassword, pUsersName, pUsersEmail);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string DemoGCG(string pIP)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.DemoGCG(pIP);
            }
            bl.CloseIt();
            return retVal;
        }

        [WebMethod]
        public string ChangePassword(string pGCGKey, string pOldPassword, string pNewPassword)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.ChangePassword(pGCGKey, pOldPassword, pNewPassword);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod(MessageName = "GCGLogin")]
        public string GCGLogin(string pGCGLogin, string pGCGPassword)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GCGLogin(pGCGLogin, pGCGPassword);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string GCGLogUser(string pGCGKey, string pChannel)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GCGLogUser(pGCGKey, pChannel);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string CardBalSummarySel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.CardBalSummarySel(pGCGKey);
            }
            bl.CloseIt();
            //GCGCommon.SupportMethods.WriteFile("C:/Output.txt", retVal, true);
            return retVal;
        }
        [WebMethod]
        public string MyCardsDataSel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.MyCardsDataSel(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }


        [WebMethod]
        public string GetMyCards(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GetMyCards(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }

        [WebMethod]
        public string GetCardInfo()
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GetCardInfo();
            }
            bl.CloseIt();
            return retVal;
        }

        [WebMethod]
        public string GetSupportedCards(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GetSupportedCards(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string RUCardDataMod(string pGCGKey, string CardID, string CardType, string CardNumber, string CardPIN, string LastKnownBalance, string LastKnownBalanceDate, string pAction)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.RUCardDataMod(pGCGKey, CardID, CardType, CardNumber, CardPIN, LastKnownBalance, LastKnownBalanceDate, pAction);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string NewRequest(string pGCGKey, string pCardType, string pCardNumber, string pPIN)
        {
            string retVal = "";
            //retVal = "OUTOFLOOKUPS^)(OUT OF LOOKUPS";
            //retVal = "GCBALANCE^)($11.00";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.NewRequest(pGCGKey, pCardType, pCardNumber, pPIN);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string NewManualRequest(string pGCGKey, string pCardType, string pCardNumber, string pPIN)
        {
            string retVal = "";
            //retVal = "OUTOFLOOKUPS^)(OUT OF LOOKUPS";
            //retVal = "GCBALANCE^)($11.00";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.NewManualRequest(pGCGKey, pCardType, pCardNumber, pPIN);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string ContinueRequest(string pGCGKey, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.ContinueRequest(pGCGKey, pIDFileName, pAnswer);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string LogPurchase(string pGCGKey, string pPurchType, string pKey, string pChannel)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.LogPurchase(pGCGKey, pPurchType, pKey, pChannel);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string MyProfileSel(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.MyProfileSel(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string MyProfileSel00(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.MyProfileSel00(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }


        [WebMethod]
        public string GetMLParams(string pGCGKey)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.GetMLParams(pGCGKey);
            }
            bl.CloseIt();
            return retVal;
        }
        [WebMethod]
        public string SetMLParams(string pGCGKey, string pParams)
        {
            string retVal = "";
            GCGWebWSBL bl = new GCGWebWSBL();
            if (bl.gloHacker != "1")
            {
                retVal = bl.SetMLParams(pGCGKey, pParams);
            }
            bl.CloseIt();
            return retVal;
        }

    }
}
