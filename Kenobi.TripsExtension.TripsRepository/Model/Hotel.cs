using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Hotel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("posId")]
        public string PosId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("supplierHotelId")]
        public string SupplierHotelId { get; set; }

        [JsonProperty("hotelId")]
        public string HotelId { get; set; }

        [JsonProperty("hotelName")]
        public string HotelName { get; set; }

        [JsonProperty("hotelAddress")]
        public Address HotelAddress { get; set; }

        [JsonProperty("hotelGeoCode")]
        public GeoCode HotelGeoCode { get; set; }

        [JsonProperty("stayPeriod")]
        public StayPeriod StayPeriod { get; set; }

        [JsonProperty("progress")]
        public string Progress { get; set; }

        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        [JsonProperty("leadPaxId")]
        public string LeadPaxId { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("supplierSessionId")]
        public string SupplierSessionId { get; set; }
    }
}
