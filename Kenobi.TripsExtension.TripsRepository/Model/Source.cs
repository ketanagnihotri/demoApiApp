using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Source
    {
        [JsonProperty("selectedSupplier")]
        public string SelectedSupplier { get; set; }

        [JsonProperty("suppliers")]
        public List<string> Suppliers { get; set; }
    }
}
