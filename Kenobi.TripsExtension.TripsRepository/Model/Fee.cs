using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class Fee
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }
    }
}
