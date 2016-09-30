using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Errors.Hotel;
using Kenobi.Common.Metrics;
using Kenobi.Common.Translator.Json;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Microsoft.Practices.ServiceLocation;
using Tavisca.Frameworks.Logging.Infrastructure;
using Newtonsoft.Json;

namespace kenobi.TripsExtension.Filters
{

    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch = Utility.StartWatch();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            object request = null, response = null;
            var responseTime = _stopwatch.Elapsed.TotalSeconds;
            StatusOptions status = StatusOptions.Success;
            ILogHandler logger = ServiceLocator.Current.GetInstance<ILogHandler>();

            if (actionContext.ActionContext?.ActionArguments?.Values != null)
                request = actionContext.ActionContext.ActionArguments.Values.FirstOrDefault();

            var responseObject = actionContext.Response?.Content as ObjectContent;
            if (responseObject?.Value != null) response = responseObject.Value;

            if (response == null)
            {
                status = StatusOptions.Failure;
                response = GetResponseFromExceptionInfo(actionContext);
            }

            logger.InsertLog(new LogModel
            {
                Title = Constants.ApplicationName,
                CallType = GetCallType(actionContext),
                Request = request,
                Response = response,
                ResponseTime = responseTime,
                Status = status
            });
        }

        private static string GetResponseFromExceptionInfo(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
                return null;

            var baseApplicationException = actionExecutedContext.Exception as BaseApplicationException;

            var response = JsonConvert.SerializeObject(baseApplicationException != null ? baseApplicationException.GetErrorInfo() : new ErrorInfo(FaultCodes.Application, ErrorMessages.Application()), new ErrorInfoTranslator(), new InfoTranslator());
            return response;
        }

        private static string GetCallType(HttpActionExecutedContext actionContext)
        {
            var descriptor = actionContext.ActionContext.ActionDescriptor;
            var callType = $"{Constants.ApplicationName}.{descriptor?.ActionName}";
            return callType;
        }
    }
}