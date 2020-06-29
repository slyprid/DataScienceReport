using Newtonsoft.Json;

namespace DataScienceReport
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonIgnore]
        public string Country { get; set; }
    }
}