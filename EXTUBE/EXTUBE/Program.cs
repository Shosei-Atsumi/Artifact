using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXTUBE.Forms;

namespace EXTUBE
{
    static class Program
    {
        private static MainForm mainForm;

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mainForm = new MainForm();
                Application.Run();
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
