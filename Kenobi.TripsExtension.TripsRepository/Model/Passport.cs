using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Passport
    {
        [JsonProperty("no")]
        public string Number { get; set; }

        [JsonProperty("placeOfIssue")]
        public string PlaceOfIssue { get; set; }

        [JsonProperty("dateOfIssue")]
        public string DateOfIssue { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }
    }
}
