using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class BookingTransaction
    {
        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("providerTransactionId")]
        public string ProviderTransactionId { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("paymentBreakup")]
        public List<PaymentBreakup> PaymentBreakup { get; set; }
    }
}
