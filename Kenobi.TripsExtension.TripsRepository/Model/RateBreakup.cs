using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class RateBreakup
    {
        [JsonProperty("baseFare")]
        public Money BaseFare { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }

        [JsonProperty("markups")]
        public List<Markup> Markups { get; set; }

        [JsonProperty("discounts")]
        public List<Discount> Discounts { get; set; }

        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }
    }
}
