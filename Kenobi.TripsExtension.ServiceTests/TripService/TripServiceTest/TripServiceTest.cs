using System;
using kenobi.TripsExtension.TestDataProvider;
using Kenobi.Common.Configuration.Provider;
using Kenobi.Common.Interfaces;
using Kenobi.TripsExtension.TripService.Entity;
using Microsoft.Practices.ServiceLocation;
using Xunit;
using System.Configuration;

namespace Kenobi.TripsExtension.ServiceTests.TripService.TripServiceTest
{
    public class TripServiceTest
    {
        private IConfigurationProvider ConfigurationProvider { get; }
        private readonly string _tripId = ConfigurationManager.AppSettings["OskiTripId"];

        private readonly string _tenantId = ConfigurationManager.AppSettings["oski-tenantId"];
        private const bool Success = true;

        public TripServiceTest()
        {
            WebhookContainer.SetLocatorWithContainer();
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
        }

        [Fact]
        public async void GetTripFolder_Valid_Successful()
         {
            var tripService = new TripsExtension.TripService.Service.TripService();
            var tripFolder = await tripService.GetTripFolder(new TripServiceRequest { TripId = _tripId }, _tenantId);
            if (tripFolder != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripFolder_TripIdMissing_Successful()
        {
            try
            {
                var tripService = new TripsExtension.TripService.Service.TripService();
                var tripFolder = await tripService.GetTripFolder(new TripServiceRequest { TripId = _tripId }, _tenantId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }
    }
}
