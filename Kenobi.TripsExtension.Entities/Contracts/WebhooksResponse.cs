using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.Entities.Contracts
{
    public class WebhooksResponse
    {

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("ErrorList")]
        public List<ErrorInfo> ErrorList { get; set; }

        [JsonProperty("tripId")]
        public string TripId { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("oskiTripId")]
        public string OskiTripId { get; set; }

    }

    public class ErrorInfo
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp => DateTime.Now;

    }
}