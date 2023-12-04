using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class SystemCommandUtil
    {

        public static bool IsWindows { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

        public static bool RunPowerShell { get; } = false; // Adjust the condition as needed

        public static DirectoryInfo CurrentWorkingDir { get; } = new DirectoryInfo(Environment.CurrentDirectory);



    }
}
