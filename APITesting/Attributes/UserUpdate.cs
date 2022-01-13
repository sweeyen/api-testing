using Newtonsoft.Json;
using System;

namespace APITesting.Attributes
{
    public partial class UserUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
