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
    internal class EncryptionUtilTests
    {

        [Test]
        public void Encryption_ValidInput_ReturnsList()
        {
            EncryptionUtil.EncryptFileInPlace("F:\\file.txt");



        }
        [Test]
        public void Decryption_ValidInput_ReturnsList()
        {
            EncryptionUtil.DecryptFileInPlace("F:\\file.txt");

        }




    }
}
