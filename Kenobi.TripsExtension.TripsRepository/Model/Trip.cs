using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Trip
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("posId")]
        public string PosId { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdOn")]
        public string CreatedOn { get; set; }

        [JsonProperty("modifiedOn")]
        public string ModifiedOn { get; set; }

    }

    public class OskiTripUpdateReq
    {
        [JsonProperty("__id")]
        public string Id { get; set; }

        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("cnx_orderid")]
        public string CNX_OrderId { get; set; }

        [JsonProperty("cnx_tripid")]
        public string CNX_TripId { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }
    }

    public class OskiTripUpdateRes
    {

        [JsonProperty("__id")]
        public string Id { get; set; }

        [JsonProperty("__owner")]
        public string Owner { get; set; }

        [JsonProperty("__typeId")]
        public string TypeId { get; set; }

        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("__utcCreatedOn")]
        public string UtcCreatedOn { get; set; }

        [JsonProperty("__utcLastUpdatedOn")]
        public string UtcLastUpdatedOn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("posId")]
        public string PosId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("CNX_OrderId")]
        public string CNXOrderId { get; set; }

        [JsonProperty("CNX_TripId")]
        public string CNXTripId { get; set; }

        [JsonProperty("__tags")]
        public int[] Tags { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }
    }
}
    