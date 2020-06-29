using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataScienceReport
{
    public class IpAddressList
    {
        [JsonProperty("addresses")]
        public List<string> Addresses { get; set; }

        public IpAddressList(List<string> input)
        {
            Addresses = input;
        }
    }
}