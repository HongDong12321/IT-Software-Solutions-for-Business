﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 전북_1.View;

namespace 전북_1
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Test());
            //Application.Run(new b_Main());
            //Application.Run(new c_MBTICheck());
            Application.Run(new d_ResultTest());
        }
    }
}
