using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.BusinessLogic;
using Kenobi.TripsExtension.Core.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class BookingWebhookProviderUnitTests
    {
        private static ITestDataProvider _testDataProvider;

        public BookingWebhookProviderUnitTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [Fact]
        public void BookingWebhookProvider_ValidInputs_Successful()
        {
            IBookingWebhookProvider bookingWebhookProvider = new BookingWebhookProvider();
            var response = bookingWebhookProvider.InitBookingWebhook(_testDataProvider.GetBookingWebhooksRequest(), _testDataProvider.GetDefaultHeaders());
            Assert.NotNull(response);
        }
        [Fact]
        public void BookingWebhookProvider_InValidInputs_Successful()
        {
            IBookingWebhookProvider bookingWebhookProvider = new BookingWebhookProvider();
            var response = bookingWebhookProvider.InitBookingWebhook(null, null);
            Assert.NotNull(response);
        }
    }
}