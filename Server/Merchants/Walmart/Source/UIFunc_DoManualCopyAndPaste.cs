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
    public static class UIFunc_DoManualCopyAndPaste
    {
        private static SHDocVw.InternetExplorer _IE;
        public static Main _m;
        public static int CopyPasteCnt = 0;
        public static string CopyAndPasteResult = "";
        public static Timer _t = null;

        public static string DoManualCopyAndPaste(Main m, SHDocVw.InternetExplorer IE)
        {
            _IE = IE;
            _m = m;
            _m.tmrRunning.Enabled = false;
            Timer t = new Timer();
            t.Interval = 100;
            t.Enabled = false;
            t.Tick += new System.EventHandler(tmrCopyPaste_Tick);
            _t = t;
            CopyPasteCnt = 0;
            CopyAndPasteResult = "";
            t.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (_t.Enabled == true);
            System.Diagnostics.Debug.WriteLine("Done.");
            _m.tmrRunning.Enabled = true;
            return CopyAndPasteResult;
        }
        public static void tmrCopyPaste_Tick(object sender, EventArgs e)
        {
            CopyPasteCnt++;
            mshtml.IHTMLDocument2 document2 = _IE.Document as mshtml.IHTMLDocument2;
            try
            {
                if (CopyPasteCnt == 1)
                {
                    document2.execCommand("SelectAll", false, null);
                    GCGMethods.WriteTextBoxLog(_m.txtLog, "tmrCopyPaste Select");
                    //System.Diagnostics.Debug.WriteLine("tmrCopyPaste Select");
                    _t.Interval = 500;
                }
                else if (CopyPasteCnt == 2)
                {
                    document2.execCommand("Copy", false, null);
                    GCGMethods.WriteTextBoxLog(_m.txtLog, "tmrCopyPaste Copy");
                    //System.Diagnostics.Debug.WriteLine("tmrCopyPaste Copy");
                    //The copy takes a while, maybe a second, otherwise it may exception
                    _t.Interval = 500;
                }
                else if (CopyPasteCnt == 3)
                {
                    CopyAndPasteResult = Clipboard.GetText();
                    GCGMethods.WriteTextBoxLog(_m.txtLog, "tmrCopyPaste SUCCESSFUL.");
                }
            }
            catch (Exception)
            {
                CopyAndPasteResult = "Failed";
                GCGMethods.WriteTextBoxLog(_m.txtLog, "tmrCopyPaste FAILED.");
                CopyPasteCnt = 4;
            }
            if (CopyPasteCnt > 3)
            {
                System.Diagnostics.Debug.WriteLine("tmrCopyPaste Resetting/Disabling");
                _t.Enabled = false;
                CopyPasteCnt = 0;
            }
        }

    }
}
