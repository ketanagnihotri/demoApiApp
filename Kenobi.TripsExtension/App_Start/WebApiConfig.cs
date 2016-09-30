using System.Web.Http;
using kenobi.TripsExtension.Filters;
using Kenobi.Common.Metrics;
using Kenobi.Common.WebApiHandler;
using Kenobi.TripsExtension.Core.DependencyInjection;
using Kenobi.TripsExtension.Core.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace kenobi.TripsExtension
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            SetAppSerializerSettings();
            SetAppDependencyInjection();
            SetAppFilters(config);
        }

        private static void SetAppDependencyInjection()
        {
            var locatorProvider = new StructureMapServiceLocator(StructureMapContainerProvider.GetContainer());
            ServiceLocator.SetLocatorProvider(() => locatorProvider);
        }

        private static void SetAppFilters(HttpConfiguration config)
        {
            Metering.Initialize(Constants.Consul.App);
            config.MessageHandlers.Add(new ActionContextHandler(Constants.ApplicationName));
            config.Filters.Add(new ExceptionResponseFilterAttribute());
            config.Filters.Add(new LogActionFilterAttribute());
            config.Filters.Add(new LogExceptionFilterAttribute());
        }

        private static void SetAppSerializerSettings()
        {
            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
        }
    }
}