using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class RoomRate:Rate
    {
        [JsonProperty("room")]
        public Room Room { get; set; }
    }
}
