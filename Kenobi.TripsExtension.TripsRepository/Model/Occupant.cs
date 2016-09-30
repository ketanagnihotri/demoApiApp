using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Occupant
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
