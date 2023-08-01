using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using HttpUtility;

namespace SwitchBotController
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BtnAllOff_Click(object sender, EventArgs e)
        {
            HttpUtility.HttpUtility.Get(@"KEY");
        }

        private void BtnAllOn_Click(object sender, EventArgs e)
        {
            HttpUtility.HttpUtility.Get(@"KEY");
        }
    }
}
