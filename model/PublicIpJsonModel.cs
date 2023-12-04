using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentBuilder;

namespace WebSocketReverseShellDotNet.model
{

    internal class PublicIpJsonModel
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonConstructor]
        public PublicIpJsonModel(string origin)
        {
            Origin = origin;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
