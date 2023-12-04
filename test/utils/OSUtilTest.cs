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
    internal class OSUtilTest
    {


        [Test]
        public void systemInfo_ValidInput_ReturnsList()
        {
            ReverseShellInfoInitialMessage result = OSUtil.GetComputerInfo();

            Console.WriteLine(result);

            ClassicAssert.NotNull(result);

        }

        [Test]
        public void getTmp_ValidInput_ReturnsList()
        {
            string result = OSUtil.GetSystemTempDir();

            Console.WriteLine(result);

            ClassicAssert.NotNull(result);

        }



        [Test]
        public void RunFunctionInThread_ValidInput_ReturnsList()
        {

            string test = "a";
            OSUtil.RunFunctionInThreadAsync(() =>
            {
                test = "b";
                Console.WriteLine("hello world ");

            });
            
            Console.WriteLine(test);

        }


        



    }
}
