using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class HttpUtil
    {

        public static FileInfo DownloadFile(string fileUrl, string destinationDirectory)
        {
            try
            {
                // Create URI object
                Uri uri = new Uri(fileUrl);

                // Create WebClient to download the file
                using (WebClient webClient = new WebClient())
                {
                    // Extract the file name from the URL
                    string fileName = Path.GetFileName(uri.LocalPath);

                    // Set the destination file path
                    string destinationPath = Path.Combine(destinationDirectory, fileName);

                    // Create the destination directory if it doesn't exist
                    Directory.CreateDirectory(destinationDirectory);

                    // Download the file
                    webClient.DownloadFile(uri, destinationPath);

                    /*Console.WriteLine("File downloaded to: " + destinationPath);*/

                    return new FileInfo(destinationPath);
                }
            }
            catch (Exception e)
            {
                /*Console.Error.WriteLine("Failed to download the file from " + fileUrl);*/
                throw new Exception("Failed to download the file", e);
            }
        }
    }
}
