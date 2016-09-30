using System.Collections.Generic;
using Kenobi.TripsExtension.Entities.Contracts;
using Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace kenobi.TripsExtension.TestDataProvider.DataProviders
{
    public interface ITestDataProvider
    {
        Dictionary<string, string> GetDefaultHeaders();
        WebhooksRequest GetBookingWebhooksRequest();
        WebhooksRequest GetCancellationWebhooksRequest();
        TripFolder GetSwapTenantConfigurationRequest();
        string GetTenantId();
    }
}