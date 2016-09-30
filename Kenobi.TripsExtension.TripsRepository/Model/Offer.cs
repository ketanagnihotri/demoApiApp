using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Offer
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("fixedDiscountOffer")]
        public string FixedDiscountOffer { get; set; }

        [JsonProperty("percentageDiscountOffer")]
        public PercentageDiscountOffer PercentageDiscountOffer { get; set; }

        [JsonProperty("stayOffer")]
        public StayOffer StayOffer { get; set; }
    }
}
