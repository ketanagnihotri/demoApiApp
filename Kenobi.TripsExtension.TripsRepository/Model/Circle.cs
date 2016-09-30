using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Circle
    {
        [JsonProperty("center")]
        public Center Center { get; set; }

        [JsonProperty("radiusKm")]
        public decimal RadiusKm { get; set; }
    }
}
