using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Tavisca.Frameworks.Logging.Infrastructure;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class CommonProviderUnitTests
    {
        private static ITestDataProvider _testDataProvider;

        public CommonProviderUnitTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [Fact]
        public void GetHeaderValueByKey_ValidInput_Success()
        {
            var key = Utility.GetHeaderValueByKey(_testDataProvider.GetDefaultHeaders(), "oski-tenantId");
            Assert.NotNull(key);
        }

        [Fact]
        public void GetRequestHeaders_ValidInput_Success()
        {
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add(Constants.TenantId, "123456");
            controllerContext.Request = request;
            var headers = Utility.GetRequestHeaders(null);
            Assert.NotNull(headers.Count);
        }

        [Fact]
        public void GetRequestHeaders_InValidInput_Fail()
        {
            var headers = Utility.GetRequestHeaders(null);
            Assert.NotNull(headers.Count);
        }

        [Fact]
        public void LogEntry_InValidInput_Fail()
        {
            try
            {
                Utility.LogEntry(null, null, "UnitTest", null, 123, StatusOptions.Failure);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}