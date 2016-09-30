using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.Common.ConsulConfiguration;

namespace kenobi.TripsExtension.TestDataProvider.DataProviders
{
    class WebConfigurationProvider : IConfigurationProvider
    {
        public Task<string> GetGlobalConfigurationAsStringAsync(string section, string key)
        {
            return Task.FromResult(ConfigurationManager.AppSettings[key]);
        }

        public Task<NameValueCollection> GetGlobalConfigurationAsNameValueCollectionAsync(string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetGlobalConfigurationAsync<T>(string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetGlobalConfigurationAsStringAsync(string applicationName, string section, string key)
        {
            return Task.FromResult(ConfigurationManager.AppSettings[key]);
        }

        public Task<NameValueCollection> GetGlobalConfigurationAsNameValueCollectionAsync(string applicationName, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetGlobalConfigurationAsync<T>(string applicationName, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTenantConfigurationAsStringAsync(string tenantId, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<NameValueCollection> GetTenantConfigurationAsNameValueCollectionAsync(string tenantId, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetTenantConfigurationAsync<T>(string tenantId, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTenantConfigurationAsStringAsync(string tenantId, string applicationName, string section, string key)
        {
            throw new NotImplementedException();
        }

        public Task<NameValueCollection> GetTenantConfigurationAsNameValueCollectionAsync(string tenantId, string applicationName, string section,
            string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetTenantConfigurationAsync<T>(string tenantId, string applicationName, string section, string key)
        {
            throw new NotImplementedException();
        }
    }
}
