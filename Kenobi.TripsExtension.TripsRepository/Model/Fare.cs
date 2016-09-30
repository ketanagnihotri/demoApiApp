using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Fare
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("supplierId")]
        public string SupplierId { get; set; }

        [JsonProperty("supplierHotelId")]
        public string SupplierHotelId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("refundability")]
        public string Refundability { get; set; }

        [JsonProperty("onlineCancellable")]
        public bool OnlineCancellable { get; set; }

        [JsonProperty("perRoomCancellationPossible")]
        public bool PerRoomCancellationPossible { get; set; }

        [JsonProperty("onRequest")]
        public bool OnRequest { get; set; }

        [JsonProperty("voucherId")]
        public string VoucherId { get; set; }

        [JsonProperty("inclusions")]
        public List<string> Inclusions { get; set; }

        [JsonProperty("room")]
        public Room Room { get; set; }

        [JsonProperty("displayFare")]
        public DisplayFare DisplayFare { get; set; }

        [JsonProperty("policies")]
        public List<Policy> Policies { get; set; }

        [JsonProperty("boardBasis")]
        public BoardBasis BoardBasis { get; set; }

        [JsonProperty("cancellationPolicy")]
        public CancellationPolicy CancellationPolicy { get; set; }
    }
}
