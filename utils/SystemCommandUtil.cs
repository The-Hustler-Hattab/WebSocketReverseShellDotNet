using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.service.factory;
using WebSocketReverseShellDotNet.service;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class SystemCommandUtil
    {

        public static bool IsWindows { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

        public static bool RunPowerShell { get; set; } = false; // Adjust the condition as needed

        public static DirectoryInfo CurrentWorkingDir { get; set; } = new DirectoryInfo(Environment.CurrentDirectory);


        public static String RunCommand(String command) {

            try
            {
                Command commandToExecute = CommandFactory.createCommand(command);

                return commandToExecute.ExecuteCommand(command);
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }


    }
}
