using Newtonsoft.Json;

namespace DataScienceReport
{
    public class Geolocation
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}