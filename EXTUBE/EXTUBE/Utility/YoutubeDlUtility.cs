using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EXTUBE;

namespace EXTUBE.Utility
{
    public class YoutubeDlUtility
    {
        const string YOUTUBE_DL_PATH = @"youtube-dl.exe";
        const string YOUTUBE_API_APP_PATH = @"YoutubeApiApp.exe";
        private string absoluteYoutubeDlDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        private string config = "";
        private string configTemplate = "{0}  -o \"{1}\\%(title)s-%(id)s.%(ext)s\"";
        private string configMp4 = " -f 'bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext=mp4]/best'";

        private string url = "";
        // private string videoPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

        private string downloadPath = "";

        public YoutubeDlUtility(string url)
        {
            this.url = url;
        }
        public YoutubeDlUtility(string url, string downloadPath)
        {
            this.url = url;
            this.downloadPath = downloadPath;
            //config += this.url + " -o \"" + downloadPath + "\\%(title)s-%(id)s.%(ext)s\"";
            config = string.Format(configTemplate, this.url, this.downloadPath);
        }

        public void Run()
        {
            Task.Run(() => KickYoutubeDl());
            Task.Run(() => KickYoutubeCommentDl());
        }

        private void KickYoutubeDl()
        {
            string res = string.Empty;
            string absoluteYoutubeDlPath = Path.Combine(absoluteYoutubeDlDirectory, YOUTUBE_DL_PATH);
            try
            {
                Directory.CreateDirectory(absoluteYoutubeDlDirectory);
                byte[] youtubeDlBin = Properties.Resources.youtube_dl;
                using (FileStream fs = new FileStream(absoluteYoutubeDlPath, FileMode.Create))
                    fs.Write(youtubeDlBin, 0, youtubeDlBin.Length);

                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = absoluteYoutubeDlPath;
                processInfo.CreateNoWindow = true;
                processInfo.Arguments = $"{this.url} {this.config}";
                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                }

            }
            finally
            {
                File.Delete(absoluteYoutubeDlPath);
                Directory.Delete(absoluteYoutubeDlDirectory, true);
            }
        }

        private void KickYoutubeCommentDl()
        {
            string res = string.Empty;
            string absoluteYoutubeCommentDlPath = Path.Combine(absoluteYoutubeDlDirectory, YOUTUBE_API_APP_PATH);
            try
            {
                string videoId = "";
                Match match = Regex.Match(this.url, @"\?v=.+");
                videoId = Regex.Replace(match.Value, @"\?v=", "");
                Directory.CreateDirectory(absoluteYoutubeDlDirectory);
                byte[] youtubeApiAppBin = Properties.Resources.YoutubeApiApp;
                using (FileStream fs = new FileStream(absoluteYoutubeCommentDlPath, FileMode.Create))
                    fs.Write(youtubeApiAppBin, 0, youtubeApiAppBin.Length);
                
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = absoluteYoutubeCommentDlPath;
                //processInfo.UseShellExecute = false;
                //processInfo.RedirectStandardOutput = true;
                //processInfo.RedirectStandardInput = false;
                //processInfo.CreateNoWindow = true;
                processInfo.Arguments = $"{videoId} 1 {downloadPath}";
                using (Process process = Process.Start(processInfo))
                {
                    //res += process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                //File.AppendAllText(Path.Combine(downloadPath, "videoIdComment.txt"), res);
            }
            finally
            {
                File.Delete(absoluteYoutubeCommentDlPath);
                //Directory.Delete(absoluteYoutubeDlDirectory);
            }
        }
    }
}
