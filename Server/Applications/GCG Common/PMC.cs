using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GCGCommon
{
    public static class PMC
    {
        static int LineCnt = 0;
        public static string WriteMacro(string PathOfScript, string Script, bool KeepScript)
        {
            //MacroDel="~!~"
            string temp = SupportMethods.CreateAlphaNumericKey(6) + ".txt";  //testscript
            string completetemp = PathOfScript + "\\" + temp;
            string[] arr = Script.Split(new string[] { "~!~" }, StringSplitOptions.None);
            StreamWriter sw = new StreamWriter(completetemp);
            //Write header
            LineCnt=0;
            sw.WriteLine("[PMC Code]|F3||1|Window|1");
            foreach (string a in arr)
            {
                LineCnt++;
                string[] tmpcmd = a.Split(new string[] { "," }, StringSplitOptions.None);
                string LineToWrite = "";
                if (tmpcmd[0] == "LeftClick")
                {
                    LineToWrite = "|Left Click|Left, 1, |1|100|Click|||||";
                }
                else if (tmpcmd[0] == "RightClick")
                {
                    LineToWrite = "|Right Click|Right, 1, |1|100|Click|||||";
                }
                else if (tmpcmd[0] == "LeftDblClick")
                {
                    LineToWrite = "|Left Click|Left, 2, |1|100|Click|||||";
                }
                else if (tmpcmd[0] == "Pause")
                {
                    LineToWrite = "|[Pause]||1|" + tmpcmd[1] + "|Sleep|||||";
                }
                else if (tmpcmd[0] == "SendText")
                {
                    LineToWrite = "|[Text]|" + tmpcmd[1] + "|1|100|Send|||||";
                }
                else if (tmpcmd[0] == "SendKey")
                {
                    LineToWrite = "|" + tmpcmd[1] + "|{" + tmpcmd[1] + "}|" + tmpcmd[2] + "|100|Send|||||";
                }
                else if (tmpcmd[0] == "Move")
                {
                    LineToWrite = "|Move|" + tmpcmd[1] + "," + tmpcmd[2] + ", 0|1|100|Click|||||";
                }
                else if (tmpcmd[0] == "WinMove")
                {
                    LineToWrite = "|WinMove|" + tmpcmd[1] + "`, " + tmpcmd[2] + "`, " + tmpcmd[3] + "`, " + tmpcmd[4] + "|1|100|WinMove||" + tmpcmd[5] + "||";
                }
                else if (tmpcmd[0] == "FileDelete")
                {
                    LineToWrite = "|FileDelete|" + tmpcmd[1] + "|1|100|FileDelete||||";
                }
                else if ((tmpcmd[0] == "WinActivate")||(tmpcmd[0] == "WinClose"))  //WinActivate||1|333|WinActivate||Untitled - Notepad||
                {
                    LineToWrite = "|"+tmpcmd[0]+"||1|333|"+tmpcmd[0]+"||" + tmpcmd[1] + "||";
                }
                else if (tmpcmd[0] == "IEFocus") //|Method:Focus:Name||1|0|IECOM_Set|/atg/userprofiling/ProfileFormHandler.value.password:0|||
                {
                    LineToWrite = "|Method:Focus:Name||1|0|IECOM_Set|" + tmpcmd[1] + "|LoadWait||";
                }

                //|[Control]||1|0|ControlFocus|Edit1|ahk_class IEFrame||
                else if (tmpcmd[0] == "FocusOnAddressBar")
                {
                    LineToWrite = "|[Control]||1|0|ControlFocus|Edit1|ahk_class IEFrame||";
                }

                    //|Method:Focus:Name||1|0|IECOM_Set|/atg/userprofiling/ProfileFormHandler.value.password:0|||
                else if (tmpcmd[0] == "TypeText")
                {
                    LineToWrite = TypeText(tmpcmd[1], tmpcmd[2]);
                }

                if (tmpcmd[0] == "TypeText")
                {
                    if (LineToWrite.Length > 0)
                    {
                        sw.WriteLine(LineToWrite);
                    }
                    else
                    {
                        LineCnt--;
                    }
                }
                else
                {
                    if (LineToWrite.Length > 0)
                    {
                        sw.WriteLine(LineCnt.ToString() + LineToWrite);
                    }
                    else
                    {
                        LineCnt--;
                    }
                }




            }
            if (KeepScript == false)
            {
                //Write line to kill file after script is ran
                LineCnt++;
                sw.Write(LineCnt + "|FileDelete|" + completetemp + "|1|0|FileDelete||||");
            }
            sw.Close();
            return completetemp;
        }
        public static string WriteMacro(string PathOfScript, string Script)
        {
            string RetVal = WriteMacro(PathOfScript, Script, false);
            return RetVal;
        }
        public static int CloseWindow(int StartAtNumber)
        {
            int EndAtNumber = StartAtNumber;

            return EndAtNumber;
        }
        private static string TypeText(string WhatToType, string MSDelayBetweenChars)
        {
            string LineToWrite = "";
            string currChar = "";
            bool subOne = false;
            for (int i = 0; i < WhatToType.Length; i++)
            {
                subOne = true;
                currChar = WhatToType.Substring(i, 1);
                LineToWrite = LineToWrite + LineCnt.ToString() + "|[Text]|" + currChar + "|1|" + MSDelayBetweenChars + "|Send|||||\r\n";
                LineCnt++;
            }
            if (subOne == true)
            {
                LineToWrite = LineToWrite.Substring(0, LineToWrite.Length - 2);
                LineCnt--;
            }
            return LineToWrite;
        }

        public static string RunMacro(string Script)
        {
            string EXE = "C:\\Program Files\\MacroCreator\\MacroCreator.exe";
            System.Diagnostics.Debug.WriteLine("Launching " + EXE + " with arg " + Script);
            GCGCommon.SupportMethods.LaunchIt(EXE, Script + " -s"); // -s
            bool IsMacroRunning = true;
            while (IsMacroRunning == true)
            {
                if (!File.Exists(Script)) IsMacroRunning = false;
                Application.DoEvents();
            }
            System.Diagnostics.Debug.WriteLine("Done running " + Script);
            return "1";
        }

    }
}
/*
!=ALT ^=CTRL +=SHIFT #=WIN

SendText,WhatToSend
|[Text]|c|1|1000|Send|||||
|[Text]|!{F4}|1|0|Send||||

SendKey,WhatToSend,Amnt
|Enter|{Enter}|1|1000|Send|||||
|Down|{Down}|2|100|Send|||||

Pause,Milliseconds
|[Pause]||1|2000|Sleep|||||

Move,X,Y
|Move|35, 382, 0|1|0|Click|||||

LeftClick
|Left Click|Left, 1, |1|0|Click|||||

LeftDblClick
|Left Click|Left, 2, |1|0|Click|||||

WinMove,X,Y,H,W,TitleOfWindow
|WinMove|0`, 0`, 800`, 800|1|333|WinMove||Untitled - Notepad||

FileDelete,LocOfFile
|FileDelete|C:\Users\chmitro\Desktop\DeleteMe.txt|1|0|FileDelete||||
*/