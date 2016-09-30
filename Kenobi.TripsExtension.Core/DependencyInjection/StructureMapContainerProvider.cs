using Kenobi.Common.Configuration;
using Kenobi.Common.Interfaces;
using Kenobi.Common.RequestHeaderProvider;
using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.TripsExtension.Core.BusinessLogic;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Kenobi.TripsExtension.TripsRepository.Interface;
using Kenobi.TripsExtension.TripService.Interface;
using StructureMap;
using Tavisca.Frameworks.Logging;
using ConsulConfiguration = Kenobi.Common.ConsulConfiguration;

namespace Kenobi.TripsExtension.Core.DependencyInjection
{
    public static class StructureMapContainerProvider
    {
        public static Container GetContainer()
        {
            var registry = new Registry();
            SetUpDependencies(registry);
            return new Container(registry);
        }

        private static void SetUpDependencies(Registry registry)
        {
            registry.For<ILogHandler>().Use<LogHandler>();
            registry.For<IEventEntry>().Use<EventEntry>();
            registry.For<IExceptionEntry>().Use<ExceptionEntry>();
            registry.For<ILogger>().Use<Logger>();
            registry.For<IRequestHeaderProvider>().Use<RequestHeaderProvider>();
            registry.For<IBookingWebhookProvider>().Use<BookingWebhookProvider>();
            registry.For<ICancellationWebhookProvider>().Use<CancellationWebhookProvider>();
            registry.For<ITenantConfigurationProviderV1>().Use<TenantConfigurationProviderV1>();
            registry.For<ISwapTenantConfigurationProvider>().Use<SwapTenantConfigurationProvider>();
            registry.For<ITripService>().Use<TripService.Service.TripService>();
            registry.For<ITripRepository>().Use<TripsRepository.Repository.TripsRepository>();
            registry.For<IConfigurationProvider>().Use<WebConfigConfigurationProvider>();
            registry.For<ITenantXmlProvider>().Use<FileXmlProvider>().SelectConstructor(() => new FileXmlProvider());
            registry.For<ConsulConfiguration.IConfigurationProvider>().Use(new ConsulConfiguration.ConfigurationProvider(Constants.Consul.App));
            registry.For<ITenantXmlProvider>().Use<ConsulXmlProvider>().SelectConstructor(() => new ConsulXmlProvider());
        }
    }
}