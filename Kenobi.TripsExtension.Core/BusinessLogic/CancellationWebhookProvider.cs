using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Kenobi.Common.Interfaces;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Kenobi.TripsExtension.TripService.Entity;
using Kenobi.TripsExtension.TripService.Interface;
using Microsoft.Practices.ServiceLocation;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Kenobi.Common;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Metrics;

namespace Kenobi.TripsExtension.Core.BusinessLogic
{
    internal class CancellationWebhookProvider : ICancellationWebhookProvider
    {
        private readonly ITripService _tripService;
        private IConfigurationProvider ConfigurationProvider { get; }
        private readonly string _apiUrl;
        private IMeter _meter;

        public CancellationWebhookProvider() : this(ServiceLocator.Current.GetInstance<ITripService>())
        {
            _meter = Metering.GetGlobalMeter();
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
            _apiUrl = ConfigurationProvider.GetString(Constants.AppSettings, Constants.UrlKey) + Constants.ApiResource;
        }

        private CancellationWebhookProvider(ITripService service)
        {
            _tripService = service;
        }

        public async Task<WebhooksResponse> InitCancellationWebhook(WebhooksRequest request,
            Dictionary<string, string> headers)
        {
            WebhooksResponse response = new WebhooksResponse() { Status = ResponseStatus.Failure };
            var watch = Utility.StartWatch();
            var client = new HttpClient(_apiUrl + Constants.CancellationWebHook, headers);
            if (request != null)
            {
                var tenantId = Utility.GetHeaderValueByKey(headers, Constants.TenantId);
                var tripFolderSaveRq = GetTripFolderSaveRq(request, tenantId).Result;
                _meter.Meter(Constants.TripsWrapperCancellationWebHookCallCount);
                TripFolderSaveRS cancellationResponse;
                var meterWatch = new Stopwatch();
                meterWatch.Start();
                try
                {
                    cancellationResponse = client.Post<TripFolderSaveRQ, TripFolderSaveRS>(tripFolderSaveRq);
                }
                catch (Exception ex)
                {
                    if (IsCriticalFault(ex))
                        _meter.Meter(Constants.TripsWrapperCancellationWebHookFaultCount);

                    throw ex;
                }
                _meter.Meter(Constants.TripsWrapperCancellationWebHookCallLatency, Convert.ToUInt64(meterWatch.Elapsed.TotalSeconds));
                if (cancellationResponse?.TripFolder != null)
                {
                    response.TripId = cancellationResponse.TripFolder.ConfirmationNumber;
                    response.Status = ResponseStatus.Success;
                }
                Utility.LogRequestResponse(tripFolderSaveRq, cancellationResponse, watch.Elapsed.TotalSeconds,
                    Constants.CancellationWebHook, Tavisca.Frameworks.Logging.Infrastructure.StatusOptions.Success);
            }
            watch.Stop();
            return response;
        }

        private async Task<TripFolderSaveRQ> GetTripFolderSaveRq(WebhooksRequest request, string tenantId)
        {

            if (request != null)
                return new TripFolderSaveRQ
                {
                    TripFolder = await GetTripFolder(request.Data?.TripId, tenantId),
                    SessionId = Utility.GetCurrentSessionId()
                };
            return new TripFolderSaveRQ();
        }

        private async Task<TripFolder> GetTripFolder(string tripId, string tenantId)
        {
            try
            {
                var meterWatch = new Stopwatch();
                meterWatch.Start();
                _meter.Meter(Constants.TripsExtensionOskiGetTripCallCount);
                var folder = await _tripService.GetTripFolder(GetTripServiceRequest(tripId), tenantId);
                _meter.Meter(Constants.TripsExtensionOskiGetTripCallLatency, Convert.ToUInt64(meterWatch.Elapsed.TotalSeconds));
                Utility.LogRequestResponse(tripId, folder, meterWatch.Elapsed.TotalSeconds, Constants.GetTripFolder, Tavisca.Frameworks.Logging.Infrastructure.StatusOptions.Success);
                return folder;
            }
            catch (Exception ex)
            {
                if (IsCriticalFault(ex))
                    _meter.Meter(Constants.TripsExtensionOskiGetTripFaultCount);
                throw ex;
            }
        }

        private static TripServiceRequest GetTripServiceRequest(string tripId)
        {
            return new TripServiceRequest
            {
                TripId = tripId
            };
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