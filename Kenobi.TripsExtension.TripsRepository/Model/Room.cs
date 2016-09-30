using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Room
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("maxOccupancy")]
        public int MaxOccupancy { get; set; }

        [JsonProperty("smokingPreference")]
        public string SmokingPreference { get; set; }

        [JsonProperty("roomViews")]
        public List<string> RoomViews { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rateRoomOccupancy")]
        public RateRoomOccupancy RateRoomOccupancy { get; set; }

        [JsonProperty("bedDetails")]
        public List<BedDetail> BedDetails { get; set; }

        [JsonProperty("roomTypeCode")]
        public string RoomTypeCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
