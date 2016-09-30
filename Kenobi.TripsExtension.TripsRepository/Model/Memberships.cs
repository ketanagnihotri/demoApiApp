using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Memberships
    {
        [JsonProperty("hotelLoyalties")]
        public List<HotelLoyalty> HotelLoyalties { get; set; }
    }
}
