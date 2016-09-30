using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class HotelDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("contentSupplierFamily")]
        public string ContentSupplierFamily { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("geoCode")]
        public GeoCode GeoCode { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("websiteUrl")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("descriptions")]
        public List<Description> Descriptions { get; set; }

        [JsonProperty("hotelCurrencyCode")]
        public string HotelCurrencyCode { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("areaAttractions")]
        public List<AreaAttraction> AreaAttractions { get; set; }

        [JsonProperty("policies")]
        public List<Policy> Policies { get; set; }

        [JsonProperty("amenities")]
        public List<Amenity> Amenities { get; set; }

        [JsonProperty("checkinCheckoutPolicy")]
        public List<CheckinCheckoutPolicy> CheckinCheckoutPolicy { get; set; }

        [JsonProperty("hotelChain")]
        public HotelChain HotelChain { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("supportsPrepaidRates")]
        public bool SupportsPrepaidRates { get; set; }

        [JsonProperty("supportsPostpaidRates")]
        public bool SupportsPostpaidRates { get; set; }
    }
}
