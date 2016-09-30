using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Tax
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
