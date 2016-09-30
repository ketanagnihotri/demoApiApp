using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class StayPeriod
    {
        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }
    }
}
