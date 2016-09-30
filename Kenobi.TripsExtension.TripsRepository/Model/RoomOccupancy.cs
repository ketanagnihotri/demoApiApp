using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class RoomOccupancy
    {
        [JsonProperty("occupants")]
        public List<Occupant> Occupants { get; set; }
    }
}
