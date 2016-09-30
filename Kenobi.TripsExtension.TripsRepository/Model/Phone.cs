using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Phone
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("num")]
        public string Number { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("ext")]
        public string Extension { get; set; }

        [JsonProperty("areaCode")]
        public string AreaCode { get; set; }
    }
}
