using Newtonsoft.Json;

namespace CounterWtf.Common
{
	public class Project
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("createdBy")]
		public string CreatedBy { get; set; }

	}
}

