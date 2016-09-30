using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Rate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("voucherIds")]
        public List<string> VoucherIds { get; set; }

        [JsonProperty("isPrepaid")]
        public bool IsPrepaid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("supplierId")]
        public string SupplierId { get; set; }

        [JsonProperty("supplierHotelId")]
        public string SupplierHotelId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("refundability")]
        public string Refundability { get; set; }

        [JsonProperty("depositRequired")]
        public bool DepositRequired { get; set; }

        [JsonProperty("guaranteeRequired")]
        public bool GuaranteeRequired { get; set; }

        [JsonProperty("allGuestInfoRequired")]
        public bool AllGuestInfoRequired { get; set; }

        [JsonProperty("roomType")]
        public string RoomType { get; set; }

        [JsonProperty("additionalCharges")]
        public List<string> AdditionalCharges { get; set; }

        [JsonProperty("onlineCancellable")]
        public bool OnlineCancellable { get; set; }

        [JsonProperty("perRoomCancellationPossible")]
        public bool PerRoomCancellationPossible { get; set; }

        [JsonProperty("onRequest")]
        public bool OnRequest { get; set; }

        [JsonProperty("inclusions")]
        public List<string> Inclusions { get; set; }

        [JsonProperty("allowedCreditCards")]
        public List<string> AllowedCreditCards { get; set; }

        [JsonProperty("displayFare")]
        public DisplayFare DisplayFare { get; set; }

        [JsonProperty("rateBreakup")]
        public RateBreakup RateBreakup { get; set; }

        [JsonProperty("supplierDailyRates")]
        public SupplierDailyRate SupplierDailyRates { get; set; }

        [JsonProperty("supplierDailyFares")]
        public SupplierDailyFare supplierDailyFares { get; set; }

        [JsonProperty("rackRate")]
        public Money RackRate { get; set; }

        [JsonProperty("policies")]
        public List<Policy> Policies { get; set; }

        [JsonProperty("boardBasis")]
        public BoardBasis BoardBasis { get; set; }

        [JsonProperty("offer")]
        public Offer Offer { get; set; }

        [JsonProperty("cancellationPolicy")]
        public CancellationPolicy CancellationPolicy { get; set; }

        [JsonProperty("supplierCommissions")]
        public List<SupplierCommission> SupplierCommissions { get; set; }

        [JsonProperty("packageSaving")]
        public Money PackageSaving { get; set; }

        [JsonProperty("roomCancellations")]
        public List<Money> RoomCancellations { get; set; }
    }
}
