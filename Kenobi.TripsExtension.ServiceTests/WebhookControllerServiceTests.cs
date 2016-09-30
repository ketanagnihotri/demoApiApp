using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.Common;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Interfaces;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Entities.Contracts;
using Microsoft.Practices.ServiceLocation;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Xunit;

namespace Kenobi.TripsExtension.ServiceTests
{
    public class WebHookControllerServiceTest
    {
        private IConfigurationProvider ConfigurationProvider { get; }
        private readonly ITestDataProvider _testDataProvider;
        private readonly string _apiUrl;
        private HttpClient _client;

        public WebHookControllerServiceTest()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
            _apiUrl = ConfigurationProvider.GetString(Constants.AppSettings, Constants.UrlKey) + Constants.ApiResource;
        }

        [Fact]
        private void InitBookingWebHookRequest_ValidInput_Success()
        {
            _client = new HttpClient(_apiUrl + Constants.BookingWebHook, _testDataProvider.GetDefaultHeaders());
            var request = _testDataProvider.GetBookingWebhooksRequest();
            var response = _client.Post<WebhooksRequest, TripFolderSaveRS>(request);
            Assert.NotNull(response);
        }

        [Fact]
        private void InitBookingWebHookRequest_InValidInput_Success()
        {
            _client = new HttpClient(_apiUrl + Constants.BookingWebHook, _testDataProvider.GetDefaultHeaders());
            var baseApplicationException = Assert.Throws<BaseApplicationException>(() => _client.Post<WebhooksRequest, TripFolderSaveRS>(null));
            Assert.NotNull(baseApplicationException);
            Assert.NotNull(baseApplicationException.ErrorCode);
            Assert.NotNull(baseApplicationException.ErrorMessage);
        }

        [Fact]
        private void InitCancellationWebHookRequest_ValidInput_Success()
        {
            _client = new HttpClient(_apiUrl + Constants.CancellationWebHook, _testDataProvider.GetDefaultHeaders());
            var request = _testDataProvider.GetCancellationWebhooksRequest();
            var response = _client.Post<WebhooksRequest, TripFolderSaveRS>(request);
            Assert.NotNull(response);
        }

        [Fact]
        private void InitCancellationWebHookRequest_InValidInput_Success()
        {
            _client = new HttpClient(_apiUrl + Constants.CancellationWebHook, _testDataProvider.GetDefaultHeaders());
            var baseApplicationException = Assert.Throws<BaseApplicationException>(() => _client.Post<WebhooksRequest, TripFolderSaveRS>(null));
            Assert.NotNull(baseApplicationException);
            Assert.NotNull(baseApplicationException.ErrorCode);
            Assert.NotNull(baseApplicationException.ErrorMessage);
        }
    }
}
