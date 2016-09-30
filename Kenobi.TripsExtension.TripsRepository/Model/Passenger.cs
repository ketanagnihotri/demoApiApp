using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Passenger
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }
            
        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }

        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public int Age { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("contactInfo")]
        public Contact ContactInfo { get; set; }

        [JsonProperty("passport")]
        public Passport Passport { get; set; }

        [JsonProperty("memberships")]
        public Memberships Memberships { get; set; }
    }
}
