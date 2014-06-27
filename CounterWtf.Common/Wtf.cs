using System;
using Newtonsoft.Json;

namespace CounterWtf.Common
{
    public class Wtf
    {
        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
