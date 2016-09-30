using System.Net;
using System.Threading.Tasks;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class CancellationWebhookProviderUnitTests
    {
        private static ITestDataProvider _testDataProvider;
        public CancellationWebhookProviderUnitTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [Fact]
        public void CancellationWebhook_ValidInputs_Successful()
        {
            ICancellationWebhookProvider cancellationWebhookProvider = ServiceLocator.Current.GetInstance<ICancellationWebhookProvider>();
            var response = cancellationWebhookProvider.InitCancellationWebhook(_testDataProvider.GetCancellationWebhooksRequest(), _testDataProvider.GetDefaultHeaders());
            Assert.NotNull(response);
        }

        [Fact]
        public async void CancellationWebhook_InValidInputs_Successful()
        {
            ICancellationWebhookProvider cancellationWebhookProvider =
                ServiceLocator.Current.GetInstance<ICancellationWebhookProvider>();

            var response = await cancellationWebhookProvider.InitCancellationWebhook(null, null);

            Assert.True(response.Status == ResponseStatus.Failure);
        }
    }
}