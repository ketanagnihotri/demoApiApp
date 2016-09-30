using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Contact
    {
        [JsonProperty("phoneNos")]
        public List<Phone> Phones { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("emails")]
        public List<string> Emails { get; set; }
     
    }
}
