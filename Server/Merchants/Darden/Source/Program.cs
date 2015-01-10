﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DVB
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
            if (args.Length != 0)
                Application.Run(new Main(args));
            else
                Application.Run(new Main());
        }
    }
}
