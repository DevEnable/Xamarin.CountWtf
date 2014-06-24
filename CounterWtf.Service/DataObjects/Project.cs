using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;

namespace CounterWtf.Service.DataObjects
{
    /// <summary>
    /// Represents a project that has WTF counters against it.
    /// </summary>
    public class Project : EntityData
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the user that created the project counter
        /// </summary>
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the WTFs that have occurred in this project.
        /// </summary>
        public ICollection<Wtf> Wtfs { get; set; }
    }
}