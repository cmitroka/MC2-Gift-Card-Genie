using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAdminSite
{
    public static class GCGReponseHandler 
    {
        public static string HandleRs(string rsType, string rsDetails)
        {
            string retVal;
            string[] rsPieces;
            string LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.LINEDEL);
            string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.EnumExtensions.Delimiters.POSDEL);
            rsPieces = GCGCommon.SupportMethods.SplitByString(rsDetails, POSDEL);
            if (rsType == GCGCommon.EnumExtensions.GCTypes.GCCAPTCHA.ToString())
            {
                retVal = "PopupAnswerCAPTCHA.aspx";
            }
            //else if (rsType == GCGCommon.EnumExtensions.GCTypes.GCBALANCE.ToString())
            //{
            //    retVal = ""; //Do javascript popup
            //}
            else if (rsType == GCGCommon.EnumExtensions.GCTypes.GCNEEDSMOREINFO.ToString())
            {
                retVal = "PopupAnswerMoreInfo.aspx";
            }
            else
            {
                retVal = "PopupResponse.aspx";
            }
            if (retVal != "") retVal = retVal + "?rsType=" + rsType + "&rsVal=" + rsDetails;
            return retVal;
        }
    }
}