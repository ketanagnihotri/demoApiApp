namespace Kenobi.TripsExtension.Core.Infrastructure
{
    public static class Constants
    {
        public const string ApplicationName = "Kenobi.TripsExtension";
        public const string BookingWebHook = "BookingWebHook";
        public const string CancellationWebHook = "CancellationWebhook";
        public const string AppSettings = "appsettings";
        public const string TenantId = "oski-tenantId";
        public const string LogOnlyPolicy = "Log Only Policy";
        public const string ServiceLevel = "ServiceLevel";
        public const string SessionId = "SessionId";
        public const string ApiResource = "oski/api/hotel/v1/";
        public const string UrlKey = "ApiUrl";
        public const string BookingEvent = "hotelengine_booking";
        public const string CancellationEvent = "hotelengine_cancellation";
        public const string OskiTenantId = "oski-tenantId";
        public const string GetTripFolder = "GetTripFolder";
        public const string EventLogName = "Exception Logging Failure";
        public const string PosId = "PosId";

        public const string Count = "count";
        public const string Failure = "failure";
        public const string Latency = "latency";

        public const string TripsWrapperBookingWebHookCallCount = "tripswrapper.bookingwebhook.count";
        public const string TripsWrapperCancellationWebHookCallCount = "tripswrapper.cancellationwebhook.count";

        public const string TripsWrapperBookingWebHookCallLatency = "tripswrapper.bookingwebhook.latency";
        public const string TripsWrapperCancellationWebHookCallLatency = "tripswrapper.cancellationwebhook.latency";

        public const string TripsExtensionOskiGetTripCallCount = "oskigettrip.count";
        public const string TripsExtensionOskiGetTripCallLatency = "oskigettrip.latency";

        public const string TripsExtensionOskiUpdateTripCallCount = "oskiupdatetrip.count";
        public const string TripsExtensionOskiUpdateTripCallLatency = "oskiupdatetrip.latency";

        public const string TripsExtensionOskiGetTripFaultCount = "oskigettrip.fault";
        public const string TripsExtensionOskiUpdateTripFaultCount = "oskiupdatetrip.fault";
        public const string TripsWrapperBookingWebHookFaultCount = "tripswrapper.bookingwebhook.fault";
        public const string TripsWrapperCancellationWebHookFaultCount = "tripswrapper.cancellationwebhook.fault";
        public const string TripsExtensionLoggingFaultCount = "logging.fault";
        public const string TripsExtensionExceptionLoggingFaultCount = "exceptionlogging.fault";
        public const string MeterKeyPrefixMissing = "Meter key prefix cannot be null or empty";

        public static class Consul
        {
            public const string App = "kenobi_tripsextension";
        }
    }
}