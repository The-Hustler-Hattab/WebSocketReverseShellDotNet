using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.service
{
    internal interface EncryptionStrategy
    {
        void ProcessFile(string path);

    }
}
