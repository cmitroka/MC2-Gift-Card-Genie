using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace PhysicalInput
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
           // Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                Application.Run(new Form1(args));
                //RunScript(args[0].ToString());
            }
            else
            {
                Application.Run(new Form1());
            }
        }
        public static void RunScript(string ScriptInFile)
        {
            string wholeLine = "";
            string currCommand = "";
            string currParams = "";
            int loc0 = 0;
            int loc1 = 0;
            StreamReader sr = new StreamReader(ScriptInFile);
            while (sr.EndOfStream == false)
            {
                try
                {
                    wholeLine = "";
                    currCommand = "";
                    currParams = "";
                    loc0 = 0;
                    loc1 = 0;
                    wholeLine = sr.ReadLine();
                    System.Diagnostics.Debug.WriteLine(wholeLine);
                    loc0 = wholeLine.IndexOf("(");
                    currCommand = wholeLine.Substring(0, loc0);
                    loc1 = (wholeLine.Length - 2) - loc0;
                    currParams = wholeLine.Substring(loc0 + 1, loc1);
                    DoCommand(currCommand, currParams);
                }
                catch (Exception ex)
                {
                    //How do I report there was a problem???
                }
            }
            System.Diagnostics.Debug.WriteLine("Done");
            sr.Close();
            while (File.Exists(ScriptInFile))
            {
                try
                {
                    if (ScriptInFile.ToUpper().Contains("TESTSCRIPT")) break;
                    File.Delete(ScriptInFile);
                }
                catch (Exception ex)
                {
                }
            }
            Application.Exit();
        }
        private static void DoCommand(string theCommand, string theParams)
        {
            int temp0 = 0;
            int temp1 = 0;
            int tempX = 0;
            int tempY = 0;
            int tempCnvVal = 0;
            if (theCommand.Contains("Click"))
            {
                theParams = theParams.Replace(" ", "");
                temp0 = theParams.IndexOf(",");
                tempX = Convert.ToInt16(theParams.Substring(0, temp0));
                temp1 = theParams.Length - (temp0 + 1);
                tempY = Convert.ToInt16(theParams.Substring(temp0 + 1, temp1));
            }
            if (theCommand == "DoWait")
            {
                tempCnvVal = Convert.ToInt16(theParams);
                PhysicalInputLib.DoWait(tempCnvVal);
            }
            else if (theCommand == "DoTyping")
            {
                PhysicalInputLib.DoTyping(theParams);
            }
            else if (theCommand == "DoLeftClick")
            {
                PhysicalInputLib.DoLeftClick(tempX, tempY);
            }
            else if (theCommand == "DoLeftClickDown")
            {
                PhysicalInputLib.DoLeftClickDown(tempX, tempY);
            }
            else if (theCommand == "DoLeftClickUp")
            {
                PhysicalInputLib.DoLeftClickUp(tempX, tempY);
            }
            else if (theCommand == "DoRightClick")
            {
                PhysicalInputLib.DoRightClick(tempX, tempY);
            }
            else if (theCommand == "DoRightClickDown")
            {
                PhysicalInputLib.DoRightClickDown(tempX, tempY);
            }
            else if (theCommand == "DoRightClickUp")
            {
                PhysicalInputLib.DoRightClickUp(tempX, tempY);
            }
        }

    }
}
