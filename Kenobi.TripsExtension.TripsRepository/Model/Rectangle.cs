using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Rectangle
    {
        [JsonProperty("topLeft")]
        public TopLeft TopLeft { get; set; }

        [JsonProperty("bottomRight")]
        public BottomRight BottomRight { get; set; }
    }
}
