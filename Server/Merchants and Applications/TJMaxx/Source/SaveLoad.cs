using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace DVB
{
    class SaveLoad
    {
        public static void SaveDataToTestFile(Main m)
        {
            string FileToSave = m.txtRqRsPath.Text + "\\test-0rq-" + m.AppName + ".txt";
            FileToSave = FileToSave.Replace("\\\\", "\\");
            try
            {
                StreamWriter s = new StreamWriter(FileToSave);
                s.WriteLine(m.AppName);
                s.WriteLine(m.txtCardNumber.Text);
                s.WriteLine(m.txtCardPIN.Text);
                s.WriteLine(m.txtLogin.Text);
                s.WriteLine(m.txtPassword.Text);
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "SaveDataToTestFile Error " + e.Message);
            }
        }

        public static void LoadDataFromTestFile(Main m)
        {
            string FileToLoad = m.txtRqRsPath.Text + "\\test-0rq-" + m.AppName + ".txt";
            FileToLoad = FileToLoad.Replace("\\\\", "\\");
            try
            {
                StreamReader s = new StreamReader(FileToLoad);
                string CardType = s.ReadLine();
                m.txtCardNumber.Text = s.ReadLine();
                m.txtCardPIN.Text = s.ReadLine();
                m.txtLogin.Text = s.ReadLine();
                m.txtPassword.Text = s.ReadLine();
                s.Close();
            }
            catch (Exception e)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "LoadDataFromTestFile Error " + e.Message);
            }
        }
        public static void LoadSettingsFromRegistry(Main m)
        {
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                string UseProxy = MR.Read("UseProxy");
                if (UseProxy == null) UseProxy="";
                if (UseProxy.ToUpper() == "TRUE")
                {
                    m.chkUseProxy.Checked = true;
                }
                else
                {
                    m.chkUseProxy.Checked = false;
                }
                m.txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                m.txtRqRsPath.Text = MR.Read("TestRqRsPath");
                m.txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
                m.txtChromePath.Text = MR.Read("ChromePath");
            }
            catch (Exception ex2)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "LoadSettingsFromRegistry error " + ex2.Message);
            }
        }

        public static void SaveSettingsToRegistry(Main m)
        {
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                MR.Write("UseProxy", m.chkUseProxy.Checked.ToString());
                MR.Write("CAPTCHAPath", m.txtCAPTCHAPath.Text);
                MR.Write("RqRsPath", m.txtRqRsPath.Text);
                MR.Write("AppStaticDBPath", m.txtAppStaticDBPath.Text);
                MR.Write("ChromePath", m.txtChromePath.Text);
            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "SaveSettingsToRegistry error " + ex.Message);
            }
        }

        public static void LoadSettingsFromDB(Main m)
        {
            try
            {
                GCGCommon.DB db = new GCGCommon.DB(m.txtAppStaticDBPath.Text);
                string[][] setting = db.GetMultiValuesOfSQL("SELECT CleanName,URL,Timeout,TestCardNum,TestCardPIN FROM tblMerchants WHERE EXE='" + m.AppName + "'");
                m.txtCleanName.Text = setting[0][0];
                m.txtBaseURL.Text = setting[0][1];
                m.txtTimeout.Text = setting[0][2];
                m.txtCardNumber.Text = setting[0][3];
                m.txtCardPIN.Text = setting[0][4];

            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "LoadSettingsFromDB error " + ex.Message);
            }
        }
        public static void SaveSettingsToDB(Main m)
        {
            try
            {
                GCGCommon.DB db = new GCGCommon.DB(m.txtAppStaticDBPath.Text);
                int retV = 0;
                retV = db.ExecuteSQLParamed("INSERT INTO tblMerchants (CleanName,URL,Timeout,TestCardNum,TestCardPIN,TestLogin,TestPass,EXE) VALUES (P0,P1,P2,P3,P4,P5,P6,P7);", m.txtCleanName.Text, m.txtBaseURL.Text, m.txtTimeout.Text, m.txtCardNumber.Text, m.txtCardPIN.Text, m.txtLogin.Text, m.txtPassword.Text, m.AppName);
                retV = db.ExecuteSQLParamed("UPDATE tblMerchants SET URL=P0, Timeout=P1, TestCardNum=P2, TestCardPIN=P3, TestLogin=P4, TestPass=P5, EXE=P6 WHERE CleanName=P7", m.txtBaseURL.Text, m.txtTimeout.Text, m.txtCardNumber.Text, m.txtCardPIN.Text, m.txtLogin.Text, m.txtPassword.Text, m.AppName, m.txtCleanName.Text);
            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "SaveSettingsToDB error " + ex.Message);
            }
        }

        /*
        public string GetWDDataForEXE(string pEXE)
        {
            string retVal = "";
            //, TestCardNum, TestCardPIN, TestLogin, TestPass 
            string[][] data = sqlh.GetMultiValuesOfSQL("SELECT URL, TestCardNum, TestCardPIN, TestLogin, TestPass FROM tblMerchants WHERE EXE=@P0", pEXE);
            retVal = data[0][0] + POSDEL + data[0][1] + POSDEL + data[0][2] + POSDEL + data[0][3] + POSDEL + data[0][4];
            return retVal;
        }
        public string SaveWDDataForEXE(string pEXE, string pURL, string pCardNum, string pCardPIN, string pCredLogin, string pCredPassword)
        {
            string retVal = "";
            int temp1 = sqlh.ExecuteSQLParamed("UPDATE tblMerchants SET URL=@P0, TestCardNum=@P1, TestCardPIN=@P2, TestLogin=@P3, TestPass=@P4 WHERE EXE=@P5", pURL, pCardNum,pCardPIN,pCredLogin,pCredPassword,pEXE);
            retVal = temp1.ToString();
            return retVal;
        }

        */


    }
}
