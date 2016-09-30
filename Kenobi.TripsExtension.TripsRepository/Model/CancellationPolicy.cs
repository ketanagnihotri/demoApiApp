using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class CancellationPolicy
    {

        [JsonProperty("text")]
        public string Text { get; set; }


        [JsonProperty("penaltyRules")]
        public List<PenaltyRule> PenaltyRules { get; set; }
    }
}
