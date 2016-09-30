using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.Common.TenantConfiguration.Entities;

namespace Kenobi.TripsExtension.TripsRepository.Util
{
    public static class TenantConfigurationHelper
    {
        public static Client GetTenantConfiguration(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId)) return null;

            ITenantConfigurationProviderV1 provider = new TenantConfigurationProviderV1();
             var client= provider.GetClientByTenantId(tenantId);
            return client;
        }
    }
}
