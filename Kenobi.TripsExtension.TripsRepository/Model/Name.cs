using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Name
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("first")]
        public string FirstName { get; set; }

        [JsonProperty("middle")]
        public string MiddleName { get; set; }

        [JsonProperty("last")]
        public string LastName { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }

}
