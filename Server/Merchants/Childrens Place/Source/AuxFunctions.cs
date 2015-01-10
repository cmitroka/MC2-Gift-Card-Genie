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
        public static void SaveSettings(Main m)
        {

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(Application.StartupPath + "\\" + m.AppName + "_cfg.txt");
                sw.WriteLine(m.chkNeverAutoExit.Checked.ToString());
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                //MR.Write("CAPTCHAPath", txtCAPTCHAPath.Text);
                MR.Write("TestRqRsPath", m.txtTestRqRsPath.Text);
                MR.Write("AppStaticDBPath", m.txtAppStaticDBPath.Text);
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
        public static void LoadSettings(Main m)
        {
            m.SpecificRetryCntMax = 999;
            m.RetryCntMax = 25;
            StreamReader sr = null;
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                //txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                m.txtTestRqRsPath.Text = MR.Read("TestRqRsPath");
                m.txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
                //GCGCommon.DB db = new GCGCommon.DB(m.txtAppStaticDBPath.Text);
                //string[][] setting = db.GetMultiValuesOfSQL("SELECT URL,Timeout FROM tblMerchants WHERE EXE='" + m.AppName + "'");
                //m.txtBaseURL.Text = setting[0][0];

                com.mc2techservices.gcg.WebService WS = new com.mc2techservices.gcg.WebService();
                m.txtBaseURL.Text=WS.GetURLForEXE(m.AppName, "");



                m.txtTimeout.Text = "60";
                sr = new StreamReader(Application.StartupPath + "\\" + m.AppName + "_cfg.txt");
                m.chkNeverAutoExit.Checked = Convert.ToBoolean(sr.ReadLine());
                sr.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                sr.Close();
            }
            catch (Exception ex1) { }
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
            if (m.CLIrqFile == "")
            {

                m.CLIrqFile = m.txtTestRqRsPath.Text + "\\test-0rq-" + m.AppName + ".txt";
                m.CLIrqFile = m.CLIrqFile.Replace("\\\\", "\\");
                GCGMethods.WriteTextBoxLog(m.txtLog, "A CLIrqFile WASN'T LOADED; USING " + m.CLIrqFile);
                //txtLog.Text = "A CLIrqFile WASN'T LOADED; WHATEVERS AT " + CLIrqFile + " WILL BE USED";
            }
            m.ad = new GCGCommon.AllDetails(m.CLIrqFile, m.txtCAPTCHAPath.Text);
            string retVal = "1";
            StreamReader s = null;
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
            return retVal;
        }


    }
}
