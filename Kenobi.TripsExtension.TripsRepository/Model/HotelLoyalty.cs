using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class HotelLoyalty
    {
        [JsonProperty("chainCode")]
        public string ChainCode { get; set; }

        [JsonProperty("no")]
        public string Number { get; set; }
    }
}
