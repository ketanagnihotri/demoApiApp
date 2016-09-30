using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class PenaltyRule
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("valueType")]
        public string ValueType { get; set; }

        [JsonProperty("window")]
        public Window Window { get; set; }
    }
}
