using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class BoardBasis
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amountIncludedInRate")]
        public bool AmountIncludedInRate { get; set; }
    }
}
