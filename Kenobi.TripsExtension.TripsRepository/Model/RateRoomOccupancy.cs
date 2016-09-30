using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class RateRoomOccupancy
    {
        [JsonProperty("adultCount")]
        public int AdultCount { get; set; }

        [JsonProperty("childCount")]
        public int ChildCount { get; set; }
    }
}
