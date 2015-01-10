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
using SHDocVw;
using GCGCommon;

namespace DVB
{
    public static class CAPTCHALib
    {
        public static string DoHandleCAPTCHARqRs(Main m, int X, int Y)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "WinActivate,Balance Extractor - " + m.AppName +"~!~" +
                "Pause,100~!~" +
                "Move," + X + "," + Y + "~!~" +
                "Pause,100~!~" +
                "RightClick~!~" +
                "Pause,100~!~" +
                "SendText,s~!~" +
                "Pause,2500~!~" +
                "SendText,^c~!~" +
                "Pause,100~!~" +
                "SendText," + m.txtCAPTCHAPath.Text + "~!~" +
                "Pause,100~!~" +
                "SendText,\\^v~!~"+
                "Pause,100~!~" +
                "SendText,{ENTER}~!~"+
                "Pause,100~!~" +
                "SendText,y{ENTER}~!~"

                //"SendText," + ad.CAPTCHAPathAndFileToWrite +"{ENTER}~!~"+
                //"Pause,500~!~" +
                //"SendText,y{ENTER}~!~"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
                GCGCommon.SupportMethods.WriteResponseFile(m.ad.GCCAPTCHA, m.ad.NextRxFileWOExt, m.ad.RsPathAndFileToWrite);
                m.tmrResponseHandlerSt = "";
                m.tmrResponseHandler.Enabled = true;

            System.Diagnostics.Debug.WriteLine("DoMacro1 Done");
            try
            {
                string renamefile = m.txtCAPTCHAPath.Text + "\\" + Clipboard.GetText();
                File.Delete(m.ad.CAPTCHAPathAndFileToWrite);
                File.Move(renamefile, m.ad.CAPTCHAPathAndFileToWrite);
            }
            catch (Exception)
            {
            }
            return "1";

        }
    }
}
