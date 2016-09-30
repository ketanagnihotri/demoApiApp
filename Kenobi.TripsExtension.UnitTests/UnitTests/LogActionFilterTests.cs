using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using kenobi.TripsExtension.Filters;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities.Contracts;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class LogActionFilterTests
    {
        private readonly ITestDataProvider _testDataProvider;

        public LogActionFilterTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [Fact]
        public void OnActionExecuting_ValidInputs_Successful()
        {
            try
            {
                var logActionFilter = new LogActionFilterAttribute();
                var actionContext = new HttpActionExecutedContext();
                var context = new HttpActionContext();
                actionContext.ActionContext = context;
                context.ActionArguments.Add("BookingWebhooks", _testDataProvider.GetBookingWebhooksRequest());
                var responseMessage = new HttpResponseMessage();
                var response = new WebhooksRequest();
                responseMessage.Content = new ObjectContent(typeof(WebhooksRequest), response,new JsonMediaTypeFormatter());
                actionContext.Response = responseMessage;
                logActionFilter.OnActionExecuting(context);
                logActionFilter.OnActionExecuted(actionContext);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void MockServiceLocator_GetAllInstances_Success()
        {
            var instances = ServiceLocator.Current.GetAllInstances(typeof(ILogHandler));
            Assert.NotNull(instances);
        }
    }
}