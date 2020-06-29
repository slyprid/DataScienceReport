using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataScienceReport
{
    public class GeolocationList
    {
        [JsonProperty("geolocations")]
        public List<Geolocation> Geolocations { get; set; }
    }
}