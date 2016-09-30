using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class RoomBooking
    {
        [JsonProperty("rateRefId")]
        public string RateRefId { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }
        [JsonProperty("supplierConfirmationNumber")]
        public string SupplierConfirmationNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
