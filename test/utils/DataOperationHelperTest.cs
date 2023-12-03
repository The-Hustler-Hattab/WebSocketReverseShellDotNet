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
            List<string> result = DataOperationHelper.StringToList(inputString, delimiter);
            Console.WriteLine(result.ToString()); 
            result.ForEach(x => Console.WriteLine(x));
            // Assert
            CollectionAssert.AreEquivalent(new[] { "item1", "item2", "item3" }, result);
        }
    }
}
