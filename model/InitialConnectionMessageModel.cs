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
        private string sessionId;

        public string GetSessionId()
        {
            return sessionId;
        }

        public void SetSessionId(string value)
        {
            sessionId = value;
        }

        private string InitialMessage;



        [JsonConstructor]
        public InitialConnectionMessageModel(
            [JsonProperty("sessionId")] string sessionId,
            [JsonProperty("initialMessage")] string initialMessage)
        {
            SetSessionId(sessionId);
            InitialMessage = initialMessage;
        }
    }
}
