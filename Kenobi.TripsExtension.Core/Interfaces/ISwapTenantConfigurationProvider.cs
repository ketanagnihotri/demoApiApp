

namespace Kenobi.TripsExtension.Core.Interfaces
{
    public interface ISwapTenantConfigurationProvider
    {
        Tavisca.TravelNxt.TripDetailsService.Proxy.TripFolder SwapTenantConfiguration(Tavisca.TravelNxt.TripDetailsService.Proxy.TripFolder webhooksRequest, string tenantId);
    }
}