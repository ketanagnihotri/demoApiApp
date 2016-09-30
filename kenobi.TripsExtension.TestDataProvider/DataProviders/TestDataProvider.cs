using System.Collections.Generic;
using Kenobi.TripsExtension.Entities.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Kenobi.Common.Configuration;
using Kenobi.TripsExtension.Core.Infrastructure;

namespace kenobi.TripsExtension.TestDataProvider.DataProviders
{
    internal class TestDataProvider : ITestDataProvider
    {
        public Dictionary<string, string> GetDefaultHeaders()
        {
            return new Dictionary<string, string>
            {
                {"oski-tenantId", "hhq163t340"},
                {"accept-language", "en-US"},
                {"oski-correlationId", "111ffada-eaaf-4e6d-a6cd-4c0972c86607"},
                {"oski-apiKey", "3af131ac-e096-4143-b06f-ead600a02a7d"},
                {"oski-userToken", "demoUser"},
                {"oski-posId", "101"}
            };
        }

        public WebhooksRequest GetBookingWebhooksRequest()
        {
            var json = new JsonFileReader().GetResourceTextFile("kenobi.TripsExtension.TestDataProvider.DataProviders.SampleData.BookingWebhooksContract.json");
            var jsonData = JsonConvert.DeserializeObject<WebhooksRequest>(json, GetJsonSerializerSettings());
            return jsonData;
        }

        public WebhooksRequest GetCancellationWebhooksRequest()
        {
            var json = new JsonFileReader().GetResourceTextFile("kenobi.TripsExtension.TestDataProvider.DataProviders.SampleData.CancellationWebhooksContract.json");
            var jsonData = JsonConvert.DeserializeObject<WebhooksRequest>(json, GetJsonSerializerSettings());
            return jsonData;
        }

        public TripFolder GetSwapTenantConfigurationRequest()
        {
            return new TripFolder
            {
                Pos = new PointOfSale
                {
                    PosId = 51,
                    AdditionalInfo = new List<StateBag>
                    {
                        new StateBag()
                        {
                            Name = Constants.PosId,
                            Value = "51"
                        }
                    }
                },
                Products = new List<TripProduct>
                {
                    GetHotelTripProduct()
                }
            };
        }

        public string GetTenantId()
        {
            var configurationProvider = new WebConfigConfigurationProvider();
            var oskiTenantId = configurationProvider.GetString(Constants.AppSettings, Constants.OskiTenantId, "123456");
            return oskiTenantId;
        }

        private static HotelTripProduct GetHotelTripProduct()
        {
            return new HotelTripProduct
            {
                HotelItinerary = new HotelItinerary
                {
                    HotelFareSource = new HotelFareSource
                    {
                        Id = 101
                    }
                }
            };
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
        }
    }
}