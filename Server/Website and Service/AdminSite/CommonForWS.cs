using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class CommonForWS
{
    public static bool isNumberOdd(int numIn)
    {
        if (numIn % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool isDatasetBad(string[][] dataIn)
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
    public static string RemoveNonAlphaNumericChars(string dataIn)
    {
        string retVal = "";
        System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
        retVal = rgx.Replace(dataIn, "");
        return retVal;
    }

}
