using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebSocketReverseShellDotNet.utils
{
    internal class DataOperationHelper
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




    }
}
