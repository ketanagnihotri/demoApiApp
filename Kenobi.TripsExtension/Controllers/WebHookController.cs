using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using kenobi.TripsExtension.Filters;
using Kenobi.Common.Metrics;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Kenobi.TripsExtension.Filters;
using Microsoft.Practices.ServiceLocation;

namespace kenobi.TripsExtension.Controllers
{
    [RoutePrefix("oski/api/hotel/v1")]
    public class WebHookController : ApiController
    {
        private readonly IBookingWebhookProvider _bookingWebhookProvider;
        private readonly ICancellationWebhookProvider _cancellationWebhookProvider;
        private readonly IMeter _meter;
        public WebHookController()
        {
            _bookingWebhookProvider = ServiceLocator.Current.GetInstance<IBookingWebhookProvider>();
            _cancellationWebhookProvider = ServiceLocator.Current.GetInstance<ICancellationWebhookProvider>();
        }

        public WebHookController(IBookingWebhookProvider bookingWebhookProvider, ICancellationWebhookProvider cancellationWebhookProvider)
        {
            _bookingWebhookProvider = bookingWebhookProvider;
            _cancellationWebhookProvider = cancellationWebhookProvider;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public IHttpActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpPost]
        [Route("BookingWebhook")]
        [ApiMeter("bookingwebhook")]
        public async Task<WebhooksResponse> BookingWebhook(WebhooksRequest webhooksRequest)
        {
            var requestHeader = Utility.GetRequestHeaders(Request?.Headers);

            if (!Utility.ValidateWebHookHeadres(requestHeader) ||
                !Utility.ValidateWebHookRequest(webhooksRequest, Constants.BookingEvent))
                return Utility.GetResponse(ResponseStatus.Failure);
            var response = await _bookingWebhookProvider.InitBookingWebhook(webhooksRequest, requestHeader);
            return response;
        }

        [HttpPost]
        [Route("CancellationWebhook")]
        [ApiMeter("cancellationwebhook")]
        public async Task<WebhooksResponse> CancellationWebhook(WebhooksRequest webhooksRequest)
        {
            var requestHeader = Utility.GetRequestHeaders(Request?.Headers);

            if (!Utility.ValidateWebHookHeadres(requestHeader) || !Utility.ValidateWebHookRequest(webhooksRequest, Constants.CancellationEvent))
                return Utility.GetResponse(ResponseStatus.Failure);

            var response = await _cancellationWebhookProvider.InitCancellationWebhook(webhooksRequest, requestHeader);
            return response;
        }
    }
}