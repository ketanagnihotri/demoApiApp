using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class BookingSearchQuery
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("posId")]
        public string PosId { get; set; }

        [JsonProperty("culture")]
        public string Culture { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("roomOccupancies")]
        public List<RoomOccupancy> RoomOccupancies { get; set; }

        [JsonProperty("stayPeriod")]
        public StayPeriod StayPeriod { get; set; }

        [JsonProperty("travellerCountryCodeOfResidence")]
        public string TravellerCountryCodeOfResidence { get; set; }

        [JsonProperty("travellerNationalityCode")]
        public string TravellerNationalityCode { get; set; }

        [JsonProperty("includeHotelsWithoutRates")]
        public bool IncludeHotelsWithoutRates { get; set; }
    }
}
