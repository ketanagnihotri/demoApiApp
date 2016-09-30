using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class CheckinCheckoutPolicy
    {
        [JsonProperty("inTime")]
        public string InTime { get; set; }

        [JsonProperty("outTime")]
        public string OutTime { get; set; }

        [JsonProperty("days")]
        public List<string> Days { get; set; }
    }
}
