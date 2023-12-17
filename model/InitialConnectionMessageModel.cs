using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model
{
    internal class InitialConnectionMessageModel
    {
        public string sessionId;

        public string GetSessionId()
        {
            return sessionId;
        }

        public void SetSessionId(string value)
        {
            sessionId = value;
        }

        public string aes256Key;

        public string InitialMessage;



        [JsonConstructor]
        public InitialConnectionMessageModel(
            [JsonProperty("sessionId")] string sessionId,
            [JsonProperty("initialMessage")] string initialMessage,
            [JsonProperty("aes256Key")] string aes256Key
            
            )
        {
            this.sessionId = sessionId;
            this.InitialMessage = initialMessage;
            this.aes256Key = aes256Key;
        }
    }
}
