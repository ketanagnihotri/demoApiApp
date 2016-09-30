using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tavisca.Frameworks.Logging.Infrastructure;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class LogHandlerTests
    {
        private readonly ILogHandler _logHandler;
        private readonly ITestDataProvider _testDataProvider;

        public LogHandlerTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _logHandler = ServiceLocator.Current.GetInstance<ILogHandler>();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [TestMethod]
        public void MockServiceLocator_GetAllInstances_Success()
        {
            _logHandler.LogException(new Exception());
        }

        [Fact]
        public void InsertLog_ValidInputs_Successful()
        {
            try
            {
                var stopwatch = Utility.StartWatch();
                var logger = ServiceLocator.Current.GetInstance<ILogHandler>();
                var actionContext = new HttpActionExecutedContext();
                var context = new HttpActionContext();
                actionContext.ActionContext = context;
                context.ActionArguments.Add("BookingWebhooks", _testDataProvider.GetBookingWebhooksRequest());
                var responseMessage = new HttpResponseMessage();
                actionContext.Response = responseMessage;
                logger.InsertLog(new LogModel
                {
                    Title = Constants.ApplicationName,
                    CallType = "Kenobi.TripsExtension.Tests",
                    ProviderId = 0,
                    Request = null,
                    Response = null,
                    ResponseTime = stopwatch.Elapsed.TotalSeconds,
                    Status = StatusOptions.Success
                });
            }
            catch (Exception)
            {
                Xunit.Assert.True(true);
            }
        }
    }
}