using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class PercentageDiscountOffer
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("appliedOn")]
        public string AppliedOn { get; set; }
    }
}
