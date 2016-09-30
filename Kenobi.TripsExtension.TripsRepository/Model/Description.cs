using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class Description
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
