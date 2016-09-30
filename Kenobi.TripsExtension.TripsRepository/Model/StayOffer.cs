using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class StayOffer
    {
        [JsonProperty("stayNights")]
        public int StayNights { get; set; }

        [JsonProperty("freeNights")]
        public int FreeNights { get; set; }
    }
}
