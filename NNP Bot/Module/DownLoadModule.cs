using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NNP_Bot.Module
{
    public class DownLoadModule
    {
        private static readonly string DownloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
        public static void DownloadMusic(List<string> url,string filename)
        {
            Process youtubedl = new Process();
            ProcessStartInfo youtubedlinfo = new ProcessStartInfo()
            {
                FileName = "youtube-dl.exe",
                Arguments = url[1] + @" --extract-audio --audio-format mp3 -o D:\Music\"+ filename + ".%(ext)s",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            youtubedl = Process.Start(youtubedlinfo);
        }
    }
}
