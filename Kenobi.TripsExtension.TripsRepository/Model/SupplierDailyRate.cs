using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class SupplierDailyRate
    {
        [JsonProperty("taxIncluded")]
        public bool TaxIncluded { get; set; }

        [JsonProperty("dailyRates")]
        public List<DailyRate> DailyRates { get; set; }
    }
}
