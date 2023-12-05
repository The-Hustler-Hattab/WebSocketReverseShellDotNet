using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentBuilder;
using Newtonsoft.Json;
namespace WebSocketReverseShellDotNet.model
{
    //[AutoGenerateBuilder]
    internal class CommandRestOutput
    {
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        private String? output { get; set; }
        
        [JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        private string? uuid { get; set; }

        [JsonConstructor]
        public CommandRestOutput(
       [JsonProperty("output")] string output,
       [JsonProperty("uuid")] string uuid)
        {
            this.output = output;
            this.uuid = uuid;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
