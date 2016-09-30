using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class SupplierDailyFare
    {
        [JsonProperty("taxIncluded")]
        public bool TaxIncluded { get; set; }

        [JsonProperty("dailyFares")]
        public List<DailyFare> DailyFares { get; set; }
    }
}
