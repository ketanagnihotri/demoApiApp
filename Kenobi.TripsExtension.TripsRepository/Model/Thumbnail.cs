using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Thumbnail
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("imageCaption")]
        public string ImageCaption { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("horizontalResolution")]
        public double HorizontalResolution { get; set; }

        [JsonProperty("verticalResolution")]
        public double VerticalResolution { get; set; }
    }
}
