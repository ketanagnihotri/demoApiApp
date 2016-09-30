using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class TopLeft
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("@long")]
        public double Longitude { get; set; }
    }
}
