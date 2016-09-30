using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class BookingFare
    {
        [JsonProperty("perRoomFares")]
        public List<Fare> RoomFares { get; set; }

        [JsonProperty("perBookingFares")]
        public List<Fare> BookingFares { get; set; }
    }
}
