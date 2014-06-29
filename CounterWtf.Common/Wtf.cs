using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace CounterWtf.Common
{
    public class Wtf
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }
    }
}
