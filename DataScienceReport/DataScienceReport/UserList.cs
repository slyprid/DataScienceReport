using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataScienceReport
{
    public class UserList
    {
        [JsonProperty("userdata")]
        public List<User> Users { get; set; }
    }
}