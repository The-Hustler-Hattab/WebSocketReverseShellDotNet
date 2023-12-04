using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model
{
    internal class ManagerCommunicationModel
    {

        [JsonProperty("masterSessionId")]
        public string MasterSessionId { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonConstructor]
        public ManagerCommunicationModel(
            [JsonProperty("masterSessionId")] string masterSessionId,
            [JsonProperty("msg")] string msg,
            [JsonProperty("uuid")] string uuid,
            [JsonProperty("success")] bool success)
        {
            MasterSessionId = masterSessionId;
            Msg = msg;
            Success = success;
            Uuid = uuid;
        }


    }
}
