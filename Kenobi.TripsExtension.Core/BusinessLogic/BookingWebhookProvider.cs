using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Kenobi.Common;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Interfaces;
using Kenobi.Common.Metrics;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Kenobi.TripsExtension.TripService.Entity;
using Kenobi.TripsExtension.TripService.Interface;
using Microsoft.Practices.ServiceLocation;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using static System.String;

namespace Kenobi.TripsExtension.Core.BusinessLogic
{
    internal class BookingWebhookProvider : IBookingWebhookProvider
    {
        private readonly ITripService _tripService;

        //TODO: Use ConsulConfiguration.ConfigurationProvider instead of  Kenobi.Common.Interfaces IConfigurationProvider.
        private IConfigurationProvider ConfigurationProvider { get; }
        private readonly ISwapTenantConfigurationProvider _swapTenantConfigurationProvider;
        private readonly string _apiUrl;
        private readonly IMeter _meter;

        public BookingWebhookProvider() : this(ServiceLocator.Current.GetInstance<ITripService>())
        {
            _meter = Metering.GetGlobalMeter();
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
            _swapTenantConfigurationProvider = ServiceLocator.Current.GetInstance<ISwapTenantConfigurationProvider>();
            _apiUrl = ConfigurationProvider.GetString(Constants.AppSettings, Constants.UrlKey) + Constants.ApiResource;
        }
        private BookingWebhookProvider(ITripService service)
        {
            _tripService = service;
        }

        public async Task<WebhooksResponse> InitBookingWebhook(WebhooksRequest request, Dictionary<string, string> headers)
        {
            WebhooksResponse response = new WebhooksResponse() {Status = ResponseStatus.Failure};
            var watch = Utility.StartWatch();

            if (request != null)
            {
                var tenantId = Utility.GetHeaderValueByKey(headers, Constants.TenantId);
                var tripFolderSaveRq = GetTripFolderSaveRq(request, tenantId).Result;

                if (tripFolderSaveRq != null && !IsNullOrEmpty(tenantId))
                {
                    _swapTenantConfigurationProvider.SwapTenantConfiguration(tripFolderSaveRq.TripFolder, tenantId);
                    _meter.Meter(Constants.TripsWrapperBookingWebHookCallCount);
                    var client = new HttpClient(_apiUrl + Constants.BookingWebHook, headers);
                    TripFolderSaveRS bookingResponse;
                    var meterWatch = new Stopwatch();
                    meterWatch.Start();
                    try
                    {
                        bookingResponse = client.Post<TripFolderSaveRQ, TripFolderSaveRS>(tripFolderSaveRq);
                    }
                    catch (Exception ex)
                    {
                        if (IsCriticalFault(ex))
                            _meter.Meter(Constants.TripsWrapperBookingWebHookFaultCount);

                        throw ex;
                    }
                    _meter.Meter(Constants.TripsWrapperBookingWebHookCallLatency, Convert.ToUInt64(meterWatch.Elapsed.TotalSeconds));
                    meterWatch.Stop();
                    if (bookingResponse?.TripFolder != null)
                    {
                        response.TripId = bookingResponse.TripFolder.ConfirmationNumber;
                        response.OrderId =
                            bookingResponse.TripFolder?.Pos?.AdditionalInfo?.Find(
                                stateBag => stateBag.Name == "TripOrderID")?.Value;
                        response.OskiTripId = request?.Data?.TripId;
                        response.Status = ResponseStatus.Success;
                        UpdateOskiWithClassicTripId(request.Data?.TripId, bookingResponse.TripFolder, tenantId);
                    }

                    Utility.LogRequestResponse(tripFolderSaveRq, response, watch.Elapsed.TotalSeconds, Constants.BookingWebHook, Tavisca.Frameworks.Logging.Infrastructure.StatusOptions.Success);
                }
            }
            watch.Stop();
            return response;
        }

        private void UpdateOskiWithClassicTripId(string oskiTripId, TripFolder tripFolder, string tenantId)
        {
            try
            {
                _meter.Meter(Constants.TripsExtensionOskiUpdateTripCallCount);
                var meterWatch = new Stopwatch();
                meterWatch.Start();
                _tripService.UpdateOskiWithClassicTripId(oskiTripId, tripFolder, tenantId);
                _meter.Meter(Constants.TripsExtensionOskiUpdateTripCallLatency, Convert.ToUInt64(meterWatch.Elapsed.TotalSeconds));
                meterWatch.Stop();
            }
            catch (Exception e)
            {
                if (IsCriticalFault(e))
                    _meter.Meter(Constants.TripsExtensionOskiUpdateTripFaultCount);
                Utility.LogException(e);
            }
        }

        private async Task<TripFolderSaveRQ> GetTripFolderSaveRq(WebhooksRequest request, string tenantId)
        {
            return new TripFolderSaveRQ
            {
                TripFolder = await GetTripFolder(request?.Data?.TripId, tenantId),
                SessionId = Utility.GetCurrentSessionId()
            };
        }

        private async Task<TripFolder> GetTripFolder(string tripId, string tenantId)
        {
            try
            {
                _meter.Meter(Constants.TripsExtensionOskiGetTripCallCount);
                var meterWatch = new Stopwatch();
                meterWatch.Start();
                var folder = await _tripService.GetTripFolder(GetTripServiceRequest(tripId), tenantId);
               
                _meter.Meter(Constants.TripsExtensionOskiGetTripCallLatency, Convert.ToUInt64(meterWatch.Elapsed.TotalSeconds));
                Utility.LogRequestResponse(tripId, folder, meterWatch.Elapsed.TotalSeconds, Constants.GetTripFolder,
                   Tavisca.Frameworks.Logging.Infrastructure.StatusOptions.Success);
                meterWatch.Stop();
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