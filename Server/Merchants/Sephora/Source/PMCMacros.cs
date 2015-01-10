﻿using System;
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
        public static void DoMacro1(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "WinActivate,Balance Extractor - " + m.AppName + "~!~" +
                "Pause,100~!~" +
                "Move,393,512~!~" +
                "LeftClick"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }
        public static void DoMacro2(Main m)
        {
            /*
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "WinActivate,Balance Extractor - " + m.AppName + "~!~" +
                "Pause,100~!~" +
                "Move,859,511~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "TypeText," + m.txtCardNumber.Text + ",100~!~" +
                "Pause,1000~!~" +
                "SendText,{TAB}~!~" +
                "TypeText," + m.txtCardPIN.Text + ",100~!~" +
                "Pause,1000~!~" +
                "SendText,{TAB}~!~"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
             */ 
        }
        public static void DoMacroMcDonalds(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "WinActivate,Balance Extractor - " + m.AppName + "~!~" +
                "Pause,1000~!~" +
                "Move,170,210~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "SendText,^a~!~" +
                "Pause,250~!~" +
                "SendText," + m.txtCardNumber.Text + "{TAB}{ENTER}"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }
        public static void DoMacroSephora(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "WinActivate,Balance Extractor - " + m.AppName + "~!~" +
                "SendText,{TAB}{TAB}{ENTER}");    
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }
        public static void DoMacro4(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,5000~!~" +
                "WinMove,0,0,950,850,The Cheesecake Factory - Gift Cards - Google Chrome~!~" +
                "Pause,1000~!~" +
                "Move,937,781~!~" +
                "LeftClick~!~" +
                "Pause,500~!~" +
                "Move,680,690~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "SendText," + m.txtCardNumber.Text + "{TAB}" + m.txtCardPIN.Text + "{TAB}{ENTER}~!~" +
                "Pause,5000~!~" +
                "SendText,^a~!~" +
                "Pause,100~!~" +
                "SendText,^c~!~" +
                "Pause,200~!~" +
                "SendText,!{F4}"
                );
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }
        public static void DoMacroSubway(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,100~!~" +
                "Move,469,413~!~" +
                "LeftClick");
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }
        public static void DoMacro7Eleven(Main m)
        {
            m.tmrRunning.Enabled = false;
            string FileToUse = GCGCommon.PMC.WriteMacro(m.txtRqRsPath.Text, "" +
                "Pause,1000~!~" +
                "Move,947,663~!~" +
                "LeftClick~!~" +
                "Pause,500~!~" +
                "Move,420,422~!~" +
                "LeftClick~!~" +
                "Pause,100~!~" +
                "SendText," + m.txtCardNumber.Text + "{TAB}{TAB}" + m.txtCAPTCHAAnswer.Text + "{TAB}{ENTER}~!~" +
                "Pause,5000~!~" +
                "SendText,^a~!~" +
                "Pause,100~!~" +
                "SendText,^c~!~" +
                "Pause,200");
            GCGCommon.PMC.RunMacro(FileToUse);
            System.Diagnostics.Debug.WriteLine("DoMacro0 Done");
            m.tmrRunning.Enabled = true;
        }

    }
}
