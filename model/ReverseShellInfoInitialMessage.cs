using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentBuilder;
using WebSocketReverseShellDotNet.FluentBuilder;


namespace WebSocketReverseShellDotNet.model
{
    [AutoGenerateBuilder]
    public class ReverseShellInfoInitialMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? osName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? osVersion { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? osArch { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? userName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? userHome { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? userCurrentWorkingDir { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? userLanguage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? userPublicIp { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? malwareType { get; set; }

        public string? reply { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


    }




}
