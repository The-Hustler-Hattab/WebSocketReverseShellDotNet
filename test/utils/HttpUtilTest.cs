﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.utils
{
    [TestFixture]
    internal class HttpUtilTest
    {

        [Test]
        public void download_ValidInput_ReturnsList()
        {


            FileInfo result = HttpUtil.DownloadFile("https://www.aznews.az/storage/2019/11/07/fil-1.jpg", "./");


            /*Console.WriteLine(result.FullName);*/

        }


    }
}
