using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class SupplierCard
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("issuedBy")]
        public string IssuedBy { get; set; }

        [JsonProperty("nameOnCard")]
        public string NameOnCard { get; set; }

    }
}
