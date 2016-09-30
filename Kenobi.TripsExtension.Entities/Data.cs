using Newtonsoft.Json;

namespace Kenobi.TripsExtension.Entities
{

    public class Data
    {
        [JsonProperty("tripId")]
        public string TripId { get; set; }

        [JsonProperty("bookingId")]
        public string BookingId { get; set; }
    }
}