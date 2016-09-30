using System.Collections.Generic;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Errors.Hotel;
using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Entities;
using Kenobi.TripsExtension.Entities.Contracts;
using Microsoft.Practices.ServiceLocation;

namespace Kenobi.TripsExtension.Core.Validators
{
    public static class RequestValidator
    {
        public static ResponseStatus Validate(WebhooksRequest request, string action)
        {
            var validationError = Errors.ClientSide.ValidationFailure();

            if (request == null)
            {
                validationError.Info.Add(new Info(FaultCodes.InvalidRequest, FaultMessages.InvalidRequest));
                throw validationError;
            }

            ValidateWebhookData(request?.Data, validationError);

            if (request?.EventType != null && request?.EventType.ToLower().Trim() == Constants.CancellationEvent)
                ValidateBookingId(request?.Data?.BookingId, validationError);

            if (string.IsNullOrEmpty(request?.EventType))
            {
                validationError.Info.Add(new Info(FaultCodes.MissingEventTypeInWebHookRequest, FaultMessages.MissingEventTypeInWebHookRequest));
                throw validationError;
            }

            if(!ValidateEventType(request?.EventType, action))
            {
                validationError.Info.Add(new Info(FaultCodes.InvalidEventTypeInWebHookRequest, FaultMessages.InvalidEventTypeInWebHookRequest));
                throw validationError;
            }

            if (validationError.Info.Count == 0)
                return ResponseStatus.Success;

            throw validationError;
        }

        private static bool ValidateEventType(string eventType, string action)
        {
            if (eventType?.ToLower().Trim() == action)
                return true;
            return false;
        }   

        private static void ValidateWebhookData(Data data, BaseApplicationException validationError)
        {
            ValidateWebhookRequestData(data, validationError);
            ValidateTripId(data?.TripId, validationError);
        }

        private static void ValidateWebhookRequestData(Data data, BaseApplicationException validationError)
        {
            if (data == null)
            {
                validationError.Info.Add(new Info(FaultCodes.MissingDataInWebHookRequest, FaultMessages.MissingDataInWebHookRequest));
            }
        }

        private static void ValidateTripId(string tripId, BaseApplicationException validationError)
        {
            if (string.IsNullOrWhiteSpace(tripId))
            {
                validationError.Info.Add(new Info(FaultCodes.TripIdRequired, FaultMessages.TripIdRequired));
            }
        }

        private static void ValidateBookingId(string bookingId, BaseApplicationException validationError)
        {


            if (string.IsNullOrWhiteSpace(bookingId))
            {
                validationError.Info.Add(new Info(FaultCodes.MissingBookingId, FaultMessages.MissingBookingId));
            }
        }

        internal static ResponseStatus ValidateHeader(Dictionary<string, string> requestHeader)
        {
            var validationError = Errors.ClientSide.ValidationFailure();

            if (requestHeader == null)
            {
                validationError.Info.Add(new Info(FaultCodes.InvalidRequest, FaultMessages.InvalidRequest));
                throw validationError;
            }

            var tenantConfigProvider = ServiceLocator.Current.GetInstance<ITenantConfigurationProviderV1>();
            var tenantId = Utility.GetHeaderValueByKey(requestHeader, Constants.TenantId);

            if (string.IsNullOrWhiteSpace(tenantId) || !tenantConfigProvider.IsTlgTenant(tenantId))
                validationError.Info.Add(new Info(FaultCodes.InvalidAccountId, FaultMessages.InvalidAccountId));

            if (validationError.Info.Count == 0)
                return ResponseStatus.Success;

            throw validationError;
        }
    }
}
