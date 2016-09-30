using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class BookingRate:Rate
    {
        [JsonProperty("rooms")]
        public List<Room> Rooms { get; set; }

        [JsonProperty("roomBookings")]
        public List<RoomBooking> RoomBookings { get; set; }

    }
}
