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
        static string WDCredCleanName;
        static string WDCredSupportCode;
        static string WDCredTimeout;

        public static string LoadEverything(Main m)
        {
            string result = "1";
            m.SpecificRetryCntMax = 999;
            m.RetryCntMax = 30;
            SaveLoad.LoadSettingsFromRegistry(m);
            SaveLoad.LoadSettingsFromDB(m);
            //SaveLoad.LoadDataFromTestFile(m);
            result = LoadCLIrqFile(m);
            return result;
        }
        public static string LoadCLIrqFile(Main m)
        {
            string retVal = "1";
            bool faulted = false;
            try
            {
                StreamReader s = new StreamReader(m.CLIrqFile);
                string CardType = s.ReadLine();
                m.txtCardNumber.Text = s.ReadLine();
                m.txtCardPIN.Text = s.ReadLine();
                //m.txtLogin.Text = s.ReadLine();
                //m.txtPassword.Text = s.ReadLine();
                m.txtLogin.Text = "temp1@mc2techservices.com";
                m.txtPassword.Text = "Temppass1";
                s.Close();
            }
            catch (Exception e)
            {
                faulted = true;
                retVal = "LoadCLIrqFile() Error - " + e.Message;
            }

            if (faulted==true)
            {
                m.CLIrqFile = m.txtRqRsPath.Text + "\\test-0rq-" + m.AppName + ".txt";
                m.CLIrqFile = m.CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(m.txtLog, "A CLIrqFile WASN'T loaded; simulating with webservice data.");
            }
            m.ad = new GCGCommon.AllDetails(m.CLIrqFile, m.txtCAPTCHAPath.Text);
            return retVal;
        }


    }
}
