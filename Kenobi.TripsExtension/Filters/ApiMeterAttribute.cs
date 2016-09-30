using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Metrics;
using Kenobi.TripsExtension.Core.Infrastructure;

namespace Kenobi.TripsExtension.Filters
{
    public class ApiMeterAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        private readonly string _meterKeyPrefix;
        private IMeter _meter;

        public ApiMeterAttribute(string meterKeyPrefix)
        {
            if (string.IsNullOrEmpty(meterKeyPrefix))
                throw new ArgumentNullException(nameof(meterKeyPrefix), Constants.MeterKeyPrefixMissing);

            _meterKeyPrefix = meterKeyPrefix;
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            _meter = Metering.GetGlobalMeter();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            AddCountMetric(Constants.Count);       //meters the count of api call

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null && IsCriticalFault(actionExecutedContext.Exception))
                AddCountMetric(Constants.Failure);      //meters the failure count of the api call

            _stopwatch.Stop();
            AddTimerMetric(Constants.Latency, _stopwatch.ElapsedMilliseconds);  //meters the latency of the api call

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        private void AddCountMetric(string metricName)
        {
            var key = string.IsNullOrEmpty(metricName) ? _meterKeyPrefix : $"{_meterKeyPrefix}.{metricName}";
            _meter.Meter(key);
        }

        private void AddTimerMetric(string metricName, long milliseconds)
        {
            var key = string.IsNullOrEmpty(metricName) ? _meterKeyPrefix : $"{_meterKeyPrefix}.{metricName}";
            _meter.Timer(key, TimeSpan.FromMilliseconds(milliseconds));
        }

        private static bool IsCriticalFault(Exception exception)
        {
            var appEx = exception as BaseApplicationException;
            var isSystemException = appEx == null;
            var isCriticalAppException = appEx != null && (int)appEx.HttpStatusCode >= 500;
            return (isSystemException == true || isCriticalAppException == true);
        }
    }
}