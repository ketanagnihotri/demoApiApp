using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class HotelChain
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
