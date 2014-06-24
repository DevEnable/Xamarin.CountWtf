using Newtonsoft.Json;

namespace CounterWtf.Service.DataObjects
{
    public class ProjectSummary : Project
    {
        /// <summary>
        /// Gets or sets the total number of WTFs
        /// </summary>
        [JsonProperty("wtfCount")]
        public int WtfCount { get; set; }
    }
}