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
using EXTUBE.Utility;
using System.IO;
using System.Text.RegularExpressions;

namespace EXTUBE.Forms
{
    public partial class MainMenuForm : MetroForm
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btn_button_Click(object sender, EventArgs e)
        {
            string url = this.TextBoxUrlPath.Text;
            if (url == string.Empty) return;
            string downloadPath = this.TextBoxDownloadFolderPath.Text;
            YoutubeDlUtility youtubeDlUtility;
            if (downloadPath == string.Empty) youtubeDlUtility = new YoutubeDlUtility(url);
            else youtubeDlUtility = new YoutubeDlUtility(url, downloadPath);
            youtubeDlUtility.Run();
        }

        private void DownloadFolderSelectButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                FileName = "Folder Selection",
                Filter = "Folder|.",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.TextBoxDownloadFolderPath.Text = Path.GetDirectoryName(ofd.FileName);
                }
            }
        }
    }
}
