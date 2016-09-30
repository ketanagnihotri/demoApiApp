using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Window
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

    }
}
