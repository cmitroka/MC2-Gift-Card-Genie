using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;
using GCGCommon;

public static class StaticStuff
{
    public static string gloDelimiter = "~!~";
    public static string gloRxPath;
    public static void SetStaticStuff()
    {       
        GetgloRxPath();
    }
    private static void GetgloRxPath()
    {
        string retVal = "";
        string tempVal = "";
        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        GCGCommon.com.mc2techservices.gcg.WebService ws = new GCGCommon.com.mc2techservices.gcg.WebService();
        //localhost.WebService ws = new localhost.WebService();
        try
        {
            tempVal = ws.GetGlobalSettings();
            if (tempVal == "") tempVal = "-2"; else tempVal = "1;" + tempVal;

        }
        catch (Exception ex)
        {
            tempVal = "-1;" + ex.Message;
        }
        if (tempVal.Substring(0, 1) == "-")
        {
            try
            {
                WebProxy proxy = WebProxy.GetDefaultProxy();
                ws.Proxy = proxy;
                ws.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                tempVal = ws.GetGlobalSettings();
                if (tempVal == "") tempVal = "0"; else tempVal = "1;" + tempVal;
            }
            catch (Exception ex)
            {
                tempVal = "-1;" + ex.Message;
            }
        }
        string[] arr0 = tempVal.Split(new string[] { ";" }, StringSplitOptions.None);
        if (arr0[0] == "1")
        {
            string[] arr1 = arr0[1].Split(new string[] { "~_~" }, StringSplitOptions.None);
            gloRxPath = arr1[1];
        }
    }

}
