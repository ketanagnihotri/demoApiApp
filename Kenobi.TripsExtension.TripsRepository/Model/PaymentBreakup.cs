using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class PaymentBreakup
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("voucherIds")]
        public List<string> VoucherIds { get; set; }

        [JsonProperty("bookingId")]
        public string BookingId { get; set; }
    }
}
