using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 项目管理模拟系统
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Welcome wel = new Welcome();
            wel.Show();
            wel.load();
            wel.Close();
            Application.Run(new Form1());
        }
    }
}
