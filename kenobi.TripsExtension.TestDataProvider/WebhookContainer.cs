using System;
using System.Collections.Generic;
using System.Linq;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.Common.Configuration;
using Kenobi.Common.Interfaces;
using Kenobi.Common.RequestHeaderProvider;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using Tavisca.Frameworks.Logging;
using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.TripsExtension.Core.BusinessLogic;
using Kenobi.TripsExtension.TripService.Interface;
using Kenobi.TripsExtension.TripService.Service;
using Kenobi.TripsExtension.TripsRepository.Interface;
using Kenobi.TripsExtension.TripsRepository.Repository;
using ConsulConfiguration = Kenobi.Common.ConsulConfiguration;

namespace kenobi.TripsExtension.TestDataProvider
{
    public static class WebhookContainer
    {
        public static void SetLocatorWithContainer()
        {
            var smContainer = GetStructureMapContainer();
            ServiceLocator.SetLocatorProvider(() => new BookingWebhookLocator(smContainer));
            ServiceLocator.SetLocatorProvider(() => new CancellationWebhookLocator(smContainer));
        }

        private static Container GetStructureMapContainer()
        {
            var container = new Container(c =>
            {
                c.For<IEventEntry>().Use<EventEntry>();
                c.For<IExceptionEntry>().Use<ExceptionEntry>();
                c.For<ILogger>().Use<Logger>().SelectConstructor(() => new Logger());
                c.For<ITenantConfigurationProviderV1>().Use<TenantConfigurationProviderV1>();
                c.For<ILogHandler>().Use<LogHandler>();
                c.For<IBookingWebhookProvider>().Use<BookingWebhookProvider>();
                c.For<ITestDataProvider>().Use<DataProviders.TestDataProvider>();
                c.For<ICancellationWebhookProvider>().Use<CancellationWebhookProvider>();
                c.For<IRequestHeaderProvider>().Use<RequestHeaderProvider>();
                c.For<ISwapTenantConfigurationProvider>().Use<SwapTenantConfigurationProvider>();
                c.For<ITripService>().Use<TripService>();
                c.For<ITripRepository>().Use<TripsRepository>();
                c.For<IConfigurationProvider>().Use<WebConfigConfigurationProvider>();
                c.For<ITenantXmlProvider>().Use<FileXmlProvider>().SelectConstructor(() => new FileXmlProvider());
                //c.For<ConsulConfiguration.IConfigurationProvider>().Use(new ConsulConfiguration.ConfigurationProvider(Constants.Consul.App));
                c.For<ConsulConfiguration.IConfigurationProvider>().Use(new WebConfigurationProvider());
            });
            return container;
        }
    }

    internal class BookingWebhookLocator : ServiceLocatorImplBase
    {
        private readonly Container _container;

        public BookingWebhookLocator(Container container)
        {
            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? _container.GetInstance(serviceType)
                : _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }

    internal class CancellationWebhookLocator : ServiceLocatorImplBase
    {
        private readonly Container _container;

        public CancellationWebhookLocator(Container container)
        {
            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrWhiteSpace(key) ? _container.GetInstance(serviceType) : _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}