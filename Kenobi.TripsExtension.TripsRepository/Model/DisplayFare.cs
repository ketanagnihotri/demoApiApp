using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class DisplayFare
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("baseAmount")]
        public decimal BaseAmount { get; set; }


        [JsonProperty("totalMarkup")]
        public decimal TotalMarkup { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }

        [JsonProperty("discounts")]
        public List<Discount> Discounts { get; set; }

        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }
    }
}
