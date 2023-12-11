using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model.broswer
{
    internal class CookieModel
    {

        public string? HostKey { get; set; }
        public string? Name { get; set; }
        public string? Cookie { get; set; }
        public string? Path { get; set; }
        public string? IsSecure { get; set; }
        public string? IsHttpOnly { get; set; }
        public string? CreationUtc { get; set; }
        public string? ExpiresUtc { get; set; }
        public string? LastAccessUtc { get; set; }
        public string? LastUpdatedUtc { get; set; }
        public string? BrowserProfile { get; set; }

        public CookieModel(string? HostKey, string? Name,
            string? CreationUtc, string? Path, string? IsSecure, string? IsHttpOnly
            , string? LastAccessUtc, string? LastUpdatedUtc, string? ExpiresUtc, string? Cookie, 
            string? BrowserProfile)
        {

            this.HostKey = HostKey;
            this.Name = Name;
            this.Cookie = Cookie;
            this.CreationUtc = CreationUtc;
            this.Path = Path;
            this.IsSecure = IsSecure;
            this.IsHttpOnly = IsHttpOnly;
            this.LastAccessUtc = LastAccessUtc;
            this.LastUpdatedUtc = LastUpdatedUtc;
            this.ExpiresUtc = ExpiresUtc;
            this.BrowserProfile = BrowserProfile;

        }


    }
}
