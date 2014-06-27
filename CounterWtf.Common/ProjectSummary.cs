using Newtonsoft.Json;

namespace CounterWtf.Common
{
    public class ProjectSummary : Project
    {
        [JsonProperty("wtfCount")]
        public int WtfCount { get; set; }
    }
}
