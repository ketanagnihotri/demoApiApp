using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Voucher
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("supplierConfirmation")]
        public string SupplierConfirmation { get; set; }

        [JsonProperty("vendorConfirmation")]
        public string VendorConfirmation { get; set; }

        [JsonProperty("supplierCancellationNumber")]
        public string SupplierCancellationNumber { get; set; }

        [JsonProperty("vendorCancelationNumber")]
        public string VendorCancelationNumber { get; set; }

        [JsonProperty("issuedBy")]
        public string IssuedBy { get; set; }

        [JsonProperty("issuedOn")]
        public string IssuedOn { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
