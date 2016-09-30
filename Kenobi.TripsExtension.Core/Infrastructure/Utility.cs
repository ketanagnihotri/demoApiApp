using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using Kenobi.Common.RequestHeaderProvider;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Core.Validators;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using Tavisca.Frameworks.Logging.Infrastructure;
using static System.String;

namespace Kenobi.TripsExtension.Core.Infrastructure
{
    public static class Utility
    {
        private static readonly ILogHandler Logger = ServiceLocator.Current.GetInstance<ILogHandler>();

        public static string GetCurrentSessionId()
        {
            var headers = HttpContext.Current?.Request.Headers;
            string sessionId = null;
            if (!IsNullOrEmpty(headers?.Get(Constants.SessionId)))
                sessionId = headers.GetValues(Constants.SessionId)?.First();
            return sessionId ?? Guid.NewGuid().ToString();
        }
        public static void LogRequestResponse(object request, object response, double duration,string callType, StatusOptions status)
        {
            Logger.InsertLog(new LogModel
            {
                Title = Constants.ApplicationName,
                CallType = callType,
                ProviderId = 0,
                Request = request,
                Response = response,
                ResponseTime = duration,
                SessionId = GetCurrentSessionId(),
                Status = status
            });
        }
        public static void LogException(Exception e)
        {
            Logger.LogException(e);
        }
        public static Stopwatch StartWatch()
        {
            var watch = new Stopwatch();
            watch.Start();
            return watch;
        }

        public static void LogEntry(object request, object response, string callType, Dictionary<string, string> requestHeaders, long responseTime, StatusOptions status)
        {
            Logger.InsertLog(new LogModel
            {
                Title = Constants.ApplicationName,
                CallType = callType,
                ProviderId = 0,
                Request = request,
                Response = response,
                ResponseTime = responseTime,
                Status = status
            });
        }

        public static string GetHeaderValueByKey(Dictionary<string, string> requestHeader, string key)
        {
            return requestHeader?.FirstOrDefault(t => key != null && t.Key != null &&
                        String.Equals(t.Key, key, StringComparison.CurrentCultureIgnoreCase)).Value;
        }

        public static Dictionary<string, string> GetRequestHeaders(HttpRequestHeaders headers)
        {
            var headersProvider = new RequestHeaderProvider();
            var requestHeader = headersProvider.GetRequestHeader(headers);
            return requestHeader;
        }
        public static WebhooksResponse GetResponse(ResponseStatus status)
        {
            return new WebhooksResponse { Status = status };
        }

        public static bool ValidateWebHookHeadres(Dictionary<string, string> requestHeader)
        {
            return RequestValidator.ValidateHeader(requestHeader) == ResponseStatus.Success;
        }
        public static bool ValidateWebHookRequest(WebhooksRequest webhooksRequest, string action)
        {
            return RequestValidator.Validate(webhooksRequest, action) == ResponseStatus.Success;
        }
    }
}