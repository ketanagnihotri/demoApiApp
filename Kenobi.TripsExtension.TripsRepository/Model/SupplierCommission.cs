using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class SupplierCommission
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }
    }
}
