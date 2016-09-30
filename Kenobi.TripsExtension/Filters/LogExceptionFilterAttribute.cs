using System.Web.Http.Filters;
using Kenobi.Common.Metrics;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;

namespace kenobi.TripsExtension.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ILogHandler logger = new LogHandler();
            logger.LogException(actionExecutedContext.Exception);
        }
    }
}