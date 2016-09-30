using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using kenobi.TripsExtension.Filters;
using kenobi.TripsExtension.TestDataProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class ExceptionFilterTests
    {

        public ExceptionFilterTests()
        {
            WebhookContainer.SetLocatorWithContainer();
        }

        [TestMethod]
        public void OnException_ValidInputs_Successful()
        {
            var exceptionFilter = new LogExceptionFilterAttribute();
            var actionContext = new HttpActionExecutedContext();
            var context = new HttpActionContext();
            actionContext.ActionContext = context;
            actionContext.Exception = new Exception();
            exceptionFilter.OnException(actionContext);
        }
    }
}