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

        public static void LoadSettingsFromRegistry(Main m)
        {
            try
            {
                GCGCommon.Registry MR = new GCGCommon.Registry();
                MR.SubKey = "SOFTWARE\\GCG Apps\\GC-Common";
                m.txtCAPTCHAPath.Text = MR.Read("CAPTCHAPath");
                m.txtRqRsPath.Text = MR.Read("RqRsPath");
                m.txtAppStaticDBPath.Text = MR.Read("AppStaticDBPath");
                m.txtMerchEXEPath.Text = MR.Read("MerchEXEPath");

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
                MR.Write("CAPTCHAPath", m.txtCAPTCHAPath.Text);
                MR.Write("RqRsPath", m.txtRqRsPath.Text);
                MR.Write("AppStaticDBPath", m.txtAppStaticDBPath.Text);
                MR.Write("MerchEXEPath", m.txtMerchEXEPath.Text);
            }
            catch (Exception ex)
            {
                GCGMethods.WriteTextBoxLog(m.txtLog, "SaveSettingsToRegistry error " + ex.Message);
            }
        }

    }
}
