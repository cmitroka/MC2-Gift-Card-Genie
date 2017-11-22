using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace DataProtector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args.Length != 0)
                    Application.Run(new Form1(args));
                else
                    Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                GCGCommon.SupportMethods.WriteToWindowsEventLog("DataProtector", ex.Message, "W");
            }
        }
    }
}
