using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.utils
{
    [TestFixture]
    internal class DataOperationHelperTest
    {

        [Test]
        public void StringToList_ValidInput_ReturnsList()
        {
            // Arrange
            string inputString = "item1,item2,item3";
            string delimiter = ",";

            // Act
            List<string> result = DataManuplationUtil.StringToList(inputString, delimiter);
            /*Console.WriteLine(result.ToString()); */
            /*result.ForEach(x => Console.WriteLine(x));*/
            // Assert
            CollectionAssert.AreEquivalent(new[] { "item1", "item2", "item3" }, result);
        }


        [Test]
        public void GetFirstNCharcters_ValidInput_ReturnsList()
        {
            // Arrange
            string? inputString = "some name string";
            int length= 3;

            // Act
            string result = DataManuplationUtil.GetFirstNCharcters(inputString, length);
            /*Console.WriteLine(result);*/


            ClassicAssert.AreEqual(result, "som");
            inputString = "so";

            result = DataManuplationUtil.GetFirstNCharcters(inputString, length);
            /*Console.WriteLine(result);*/
            ClassicAssert.AreEqual(result, "so");

            inputString = null;

            result = DataManuplationUtil.GetFirstNCharcters(inputString, length);
            /*Console.WriteLine(result);*/
            ClassicAssert.AreEqual(result, null);


        }
        [Test]
        public void SplitString_ValidInput_ReturnsList()
        {
            // Arrange
            string? inputString = "some name string";
            int length = 3;

            // Act
            List<string> result = DataManuplationUtil.SplitString(inputString, length);
            /*result.ForEach(x => Console.WriteLine(x));*/


            CollectionAssert.AreEquivalent(new[] { "som", "e n", "ame", " st", "rin","g" }, result);

        }
        [Test]
        public void RemoveFirstCharacter_ValidInput_ReturnsList()
        {
            string? inputString = "some name string";


            string result = DataManuplationUtil.RemoveFirstCharacter(inputString);
            /*Console.WriteLine(result);*/
            ClassicAssert.AreEqual(result, "ome name string");

        }

        [Test]
        public void RemoveFirstLines_ValidInput_ReturnsList()
        {
            string? inputString = "some name string\nyooo\nnooo\nsooo";


            string result = DataManuplationUtil.RemoveFirstLines(inputString,1);
            /*Console.WriteLine(result);*/
            ClassicAssert.AreEqual(result, "yooo\r\nnooo\r\nsooo");


            result = DataManuplationUtil.RemoveFirstLines(inputString, 2);
            /*Console.WriteLine(result);*/
            ClassicAssert.AreEqual(result, "nooo\r\nsooo");

        }


    }
}
