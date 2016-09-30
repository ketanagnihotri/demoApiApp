using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Rates
    {
        [JsonProperty("perRoomRates")]
        public List<RoomRate> RoomRates { get; set; }

        [JsonProperty("perBookingRates")]
        public List<BookingRate> BookingRates { get; set; }
    }
}
