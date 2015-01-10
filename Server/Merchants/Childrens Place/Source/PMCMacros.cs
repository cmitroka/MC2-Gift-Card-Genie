using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVB
{
    public static class PMCMacros
    {
        public static void DoMacro0(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "WinActivate,Balance Extractor - " + m.AppName + "~!~" +
                "Pause,100~!~" +
                "Move,368,545~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "SendText," + m.txtCardNumber.Text
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }

    }
}
