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
        private string SessionId { get; set; }
        private string InitialMessage { get; set; }

        [JsonConstructor]
        public InitialConnectionMessageModel(
            [JsonProperty("sessionId")] string sessionId,
            [JsonProperty("initialMessage")] string initialMessage)
        {
            SessionId = sessionId;
            InitialMessage = initialMessage;
        }
    }
}
