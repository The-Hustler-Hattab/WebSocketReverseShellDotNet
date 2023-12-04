using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.utils
{
    [TestFixture]
    internal class S3UploadutilTest
    {

        [Test]
        public void uploadToS3_ValidInput_ReturnsList()
        {

            FileInfo fileToUpload = new FileInfo("/.GamingRoot");

            //            String res = await S3Uploadutil.UploadToS3Async(fileToUpload);
            string resultMessage =  S3Uploadutil.UploadToS3(fileToUpload);
            Console.WriteLine(resultMessage);
        }
    }
}
