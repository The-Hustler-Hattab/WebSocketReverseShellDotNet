using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class DataManuplationUtil
    {


        public static List<string> StringToList(string inputString, string delimiter)
        {

            try
            {
                // Split the input string by the delimiter and convert it to a list
                return new List<string>(inputString.Split(new[] { delimiter }, StringSplitOptions.None));
            }
            catch (Exception e)
            {
                // If an exception occurs, return a list with the original input string
                return new List<string> { inputString };
            }

        }

        public static string GetFirstNCharcters(string inputString, int length) 
        {
            if (inputString == null) return inputString;

            if (inputString.Length <= length) return inputString;

            return inputString.Substring(0, length);

            
        }

        public static List<string> SplitString(string input, int stringLength)
        {
            List<string> result = new List<string>();
            if (input == null)
            {
                return result;
            }

            int length = input.Length;
            int startIndex = 0;

            while (startIndex < length)
            {
                int endIndex = Math.Min(startIndex + stringLength, length);
                result.Add(input.Substring(startIndex, endIndex - startIndex));
                startIndex = endIndex;
            }

            return result;
        }

        public static string RemoveFirstCharacter(string input)
        {
            // Check if the string is not empty and has more than one character
            if (!string.IsNullOrEmpty(input) && input.Length > 1)
            {
                return input.Substring(1);
            }
            else
            {
                // Return the original string if it is empty or has only one character
                return input;
            }
        }

        public static string RemoveFirstLines(string input, int linesToRemove)
        {
            if (string.IsNullOrEmpty(input))
            {
                // Return the original string if it is null or empty
                return input;
            }

            // Split the string into lines
            string[] lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Check if there are at least specified lines to remove
            if (lines.Length > linesToRemove)
            {
                // Join the remaining lines starting from the specified line
                return string.Join(Environment.NewLine, lines, linesToRemove, lines.Length - linesToRemove);
            }
            else
            {
                // Return an empty string if there are fewer lines than specified
                return string.Empty;
            }
        }


        public static void addNewLine(StringBuilder stringBuilder, string newItem)
        {
            if (!System.String.IsNullOrEmpty(newItem.Trim()))
            {
                stringBuilder.AppendLine(newItem);
            }
        }

    }
}
