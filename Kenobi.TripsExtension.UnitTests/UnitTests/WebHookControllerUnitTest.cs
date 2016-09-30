using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using kenobi.TripsExtension.Controllers;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.Common.Configuration;
using Kenobi.Common.Interfaces;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Microsoft.Practices.ServiceLocation;
using Moq;
using Xunit;
using Kenobi.TripsExtension.Core.Interfaces;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class WebHookControllerUnitTest
    {
        private WebHookController _webHookController;
        private IConfigurationProvider ConfigurationProvider { get; }
        private readonly ITestDataProvider _testDataProvider;
        private readonly string _oskiTenantId;

        public WebHookControllerUnitTest()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
            _oskiTenantId = ConfigurationProvider.GetString(Constants.AppSettings, Constants.OskiTenantId, "123456");
            SetMocking();
            SetHeaderContext();
        }

        private void SetMocking()
        {
            var mockBookingProvider = new Mock<IBookingWebhookProvider>();
            var mockCancellationProvider = new Mock<ICancellationWebhookProvider>();
            mockBookingProvider.Setup(provider => provider.InitBookingWebhook(It.IsAny<WebhooksRequest>(), It.IsAny<Dictionary<string, string>>())).
                Returns(GetWebResponse());
            mockCancellationProvider.Setup(provider => provider.InitCancellationWebhook(It.IsAny<WebhooksRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(GetWebResponse());
            _webHookController = new WebHookController(mockBookingProvider.Object, mockCancellationProvider.Object);
        }

        private async Task<WebhooksResponse> GetWebResponse()
        {
            return new WebhooksResponse()
            {
                Status = ResponseStatus.Success
            };
        }

        private void SetHeaderContext()
        {
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            var oskiTenantId = GetTenantId();
            AddRequestHeader(request, oskiTenantId);
            controllerContext.Request = request;
            _webHookController.ControllerContext = controllerContext;
        }

        private static void AddRequestHeader(HttpRequestMessage request, string oskiTenantId)
        {
            request.Headers.Add(Constants.OskiTenantId, oskiTenantId);
            request.Headers.Add(Constants.SessionId, Guid.NewGuid().ToString());
        }

        private static string GetTenantId()
        {
            var configurationProvider = new WebConfigConfigurationProvider();
            var oskiTenantId = configurationProvider.GetString(Constants.AppSettings, Constants.OskiTenantId, "123456");
            return oskiTenantId;
        }

        [Fact]
        private async void BookingWebhook_ValidInput_success()
        {
            var request = _testDataProvider.GetBookingWebhooksRequest();
            var response = await _webHookController.BookingWebhook(request);
            Assert.True(response.Status == ResponseStatus.Success);
        }

        [Fact]
        private async void CancellationWebHook_ValidInput_success()
        {
            var request = _testDataProvider.GetCancellationWebhooksRequest();
            var response = await _webHookController.CancellationWebhook(request);
            Assert.True(response.Status == ResponseStatus.Success);
        }
        [Fact]
        private void BookingWebhook_InValidInput_success()
        {
            try
            {
                _webHookController.BookingWebhook(null);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Fact]
        private void CancellationWebHook_InValidInput_success()
        {
            try
            {
                _webHookController.CancellationWebhook(null);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
    }
}
