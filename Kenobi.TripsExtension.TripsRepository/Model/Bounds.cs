using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Bounds
    {
        [JsonProperty("circle")]
        public Circle Circle { get; set; }

        [JsonProperty("rectangle")]
        public Rectangle Rectangle { get; set; }
    }
}
