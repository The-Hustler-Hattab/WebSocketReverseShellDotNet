using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model.broswer;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.utils
{
    [TestFixture]
    internal class ExcelTest
    {

        [Test]
        public void Excel_ValidInput_ReturnsList()
        {
            Dictionary<string, List<object>> dataDictionary = new Dictionary<string, List<object>>();

            dataDictionary["HistoryModel"] = new List<object>
        {
            new HistoryModel ("1","1","1","1") ,
            new HistoryModel ("2","2","2","2")

        };

            dataDictionary["LoginModel"] = new List<object>
        {
            new LoginModel ("1","1","1","","","",""),
            new LoginModel ("2","2","2","","","","")
        };

            string filePath = "F:\\file.xlsx";

            FileInfo excelFileInfo = Excel.ConvertToExcel(dataDictionary, filePath);

            Console.WriteLine(excelFileInfo.FullName);


        }
    }
}
