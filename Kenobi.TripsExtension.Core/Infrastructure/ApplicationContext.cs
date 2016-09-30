using Kenobi.Common.Configuration;

namespace Kenobi.TripsExtension.Core.Infrastructure
{
    public static class ApplicationContext
    {
        static ApplicationContext()
        {
            ConfigurationProvider = new WebConfigConfigurationProvider();
        }

        private static WebConfigConfigurationProvider ConfigurationProvider { get; }
    }
}