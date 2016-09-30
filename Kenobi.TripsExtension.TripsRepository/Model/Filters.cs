using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Filters
    {
        [JsonProperty("minHotelPrice")]
        public decimal? MinHotelPrice { get; set; }

        [JsonProperty("maxHotelPrice")]
        public decimal? MaxHotelPrice { get; set; }

        [JsonProperty("minHotelRating")]
        public decimal? MinHotelRating { get; set; }

        [JsonProperty("maxHotelRating")]
        public decimal? MaxHotelRating { get; set; }

        [JsonProperty("hotelChains")]
        public List<string> HotelChains { get; set; }
    }
}
