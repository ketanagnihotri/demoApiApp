using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class AgencyFare
    {
        [JsonProperty("markups")]
        public List<Markup> Markups { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("baseAmount")]
        public decimal BaseAmount { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }

        [JsonProperty("discounts")]
        public List<Discount> Discounts { get; set; }

        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }
    }
}
