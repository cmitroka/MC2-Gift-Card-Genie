using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using mshtml;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DVB
{
    public static class AuxFunctions
    {
        static string WDURL;
        static string WDCardNum;
        static string WDCardPin;
        static string WDCredLogin;
        static string WDCredPass;
        public static void SaveSettings(Main m)
        {

            StreamWriter sw = null;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                //MR.Write("CAPTCHAPath", txtCAPTCHAPath.Text);
                MR.Write("TestRqRsPath", m.txtRqRsPath.Text);
                //MR.Write("AppStaticDBPath", m.txtAppStaticDBPath.Text);
            }
            catch (Exception ex)
            {
            }
            try
            {
                sw.Close();
            }
            catch (Exception ex1) { }
        }
        public static string LoadEverything(Main m)
        {
            string result = "1";
            LoadSettings(m);
            LoadCommonConfig(m);
            result = AuxFunctions.LoadCLIrqFile(m);
            return result;
        }
        public static void LoadSettings(Main m)
        {
            m.SpecificRetryCntMax = 999;
            m.RetryCntMax = 15;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                m.txtRqRsPath.Text = MR.Read("TestRqRsPath");
                try
                {
                    com.mc2techservices.gcg.WebService WS = new com.mc2techservices.gcg.WebService();
                    string WDData = WS.GetWDDataForEXE(m.AppName, "");
                    WDData = WS.GetWDDataForEXE(m.AppName, "CJMGCG");
                    string POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.Delimiters.POSDEL);
                    string[] WDDataArray = GCGCommon.SupportMethods.SplitByString(WDData, POSDEL);
                    WDURL = WDDataArray[0];
                    WDCardNum = WDDataArray[1];
                    WDCardPin = WDDataArray[2];
                    WDCredLogin = WDDataArray[3];
                    WDCredPass = WDDataArray[4];
                    m.txtBaseURL.Text = WDURL;
                }
                catch (Exception ex2) 
                {
                    string tempFile = Application.StartupPath + "\\" + m.AppName + "_temp.txt";
                    GCGMethods.WriteTextBoxLog(m.txtLog, "Couldn't connect to webservice!");
                }

                m.txtTimeout.Text = "60";
            }
            catch (Exception ex)
            {
            }
        }
        public static void LoadCommonConfig(Main m)
        {
            GCGCommon.Registry MR = new GCGCommon.Registry();
            MR.SubKey = "SOFTWARE\\GCG Apps\\Janitor";
            //txtCurrentCount.Text = MR.Read("txtCurrentCount");
            m.txtRqRsPath.Text = MR.Read("txtLocBatchRqRs");
            m.txtCAPTCHAPath.Text = MR.Read("txtLocCAPTCHA");
        }
        public static string LoadCLIrqFile(Main m)
        {
            string retVal = "1";
            StreamReader s = null;
            if (m.CLIrqFile == "")
            {
                m.txtCardNumber.Text = WDCardNum;
                m.txtCardPIN.Text = WDCardPin;
                m.txtLogin.Text = WDCredLogin;
                m.txtPassword.Text = WDCredPass;
                m.CLIrqFile = m.txtRqRsPath.Text + "\\test-0rq-" + m.AppName + ".txt";
                m.CLIrqFile = m.CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(m.txtLog, "A CLIrqFile WASN'T loaded; simulating with " + m.CLIrqFile);
            }
            else
            {
                try
                {
                    s = new StreamReader(m.CLIrqFile);
                    string CardType = s.ReadLine();
                    m.txtCardNumber.Text = s.ReadLine();
                    m.txtCardPIN.Text = s.ReadLine();
                    m.txtLogin.Text = s.ReadLine();
                    m.txtPassword.Text = s.ReadLine();
                    s.Close();
                }
                catch (Exception e)
                {
                    retVal = "LoadCLIrqFile() Error - " + e.Message;
                }

            }
            m.ad = new GCGCommon.AllDetails(m.CLIrqFile, m.txtCAPTCHAPath.Text);
            return retVal;
        }


    }
}
