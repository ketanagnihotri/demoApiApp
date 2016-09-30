using System.Threading.Tasks;
using Kenobi.Common.ConsulConfiguration;
using Microsoft.Practices.ServiceLocation;
using static System.String;

namespace Kenobi.TripsExtension.TripsRepository.Util
{
    internal static class Config
    {
        private static readonly IConfigurationProvider ConfigurationProvider;

        static Config()
        {
            ConfigurationProvider = ServiceLocator.Current.GetInstance<IConfigurationProvider>();
        }

        private static string GetValueFromConfig(string section, string key)
        {
            var value =  Task.Run(() => ConfigurationProvider.GetGlobalConfigurationAsStringAsync(section, key)).GetAwaiter().GetResult();
            return value;
        }

        internal static string OskiTripsBookingDetailsUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.OskiTripsBookingDetailsUrl);

        internal static string TripsPutUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsPutUrl);

        internal static string TripsUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsUrl);

        internal static string TripsPassengerUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsPassengerUrl);

        internal static string TripsTransactionUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsTransactionUrl);

        internal static string TripsVouchersUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsVouchersUrl);

        internal static string TripsNotesUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsNotesUrl);
        internal static string TripsHotelUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsHotelUrl);

        internal static string TripsHotelByBookingIdUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripsHotelByBookingIdUrl);

        internal static string HotelBookingPassengersUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelBookingPassengersUrl);

        internal static string HotelBookingFaresUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelBookingFaresUrl);

        internal static string HotelBookingRateUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelBookingRateUrl);

        internal static string HotelBookingVoucherUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelBookingVoucherUrl);

        internal static string HotelSearchQueryUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelSearchQueryUrl);

        internal static string HotelBookingTransactionUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.HotelBookingTransactionUrl);

        internal static string BookedHotelDetailsUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.BookedHotelDetailsUrl);
        internal static string AgencyFareUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.AgencyFareUrl);

        internal static string TripSupplierCardUrl => GetValueFromConfig(Constants.Consul.AppSettings, Constants.AppSettings.TripSupplierCardUrl);

        #region tripFolderSettings
        public static string CreatorEmail => GetValueFromConfig(Constants.Consul.Default, Constants.Default.CreatorEmail);
        public static string CreatorFirstName => GetValueFromConfig(Constants.Consul.Default, Constants.Default.CreatorFirstName);
        public static string CreatorLastName => GetValueFromConfig(Constants.Consul.Default, Constants.Default.CreatorLastName);
        public static string OwnerEmail => GetValueFromConfig(Constants.Consul.Default, Constants.Default.OwnerEmail);

        internal static string GetTenantId
        {
            get
            {
                var tenantId = GetValueFromConfig(Constants.Consul.Default, Constants.Default.OskiTenantId);
                return tenantId ?? "123456";
            }
        }

        public static string OwnerFirstName
        {
            get
            {
                var val = GetValueFromConfig(Constants.Consul.Default, Constants.Default.OwnerFirstName);
                return val;
            }

        }

        public static string OwnerLastName
        {
            get
            {
                var val = GetValueFromConfig(Constants.Consul.Default, Constants.Default.OwnerLastName);
                return val;
            }

        }
        internal static string RuleGroupId
        {
            get
            {
                var ruleGroupId = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RuleGroupId);

                return IsNullOrEmpty(ruleGroupId)
                    ? "511"
                    : ruleGroupId;
            }
        }

        internal static string ExRate
        {
            get
            {
                var exRate = GetValueFromConfig(Constants.Consul.Default, Constants.Default.ExRate);
                return IsNullOrEmpty(exRate)
                    ? "1"
                    : exRate;
            }
        }

        internal static string RuleGroupName
        {
            get
            {
                var ruleGroupName = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RuleGroupName);
                return IsNullOrEmpty(ruleGroupName)
                    ? "Pay in Cash"
                    : ruleGroupName;
            }
        }

        internal static string RuleGroupType
        {
            get
            {
                var ruleGroupType = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RuleGroupType);
                return IsNullOrEmpty(ruleGroupType)
                    ? "Purchase"
                    : ruleGroupType;
            }
        }

        internal static string RuleGroupTypeId
        {
            get
            {
                var ruleGroupTypeId = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RuleGroupTypeId);

                return IsNullOrEmpty(ruleGroupTypeId)
                    ? "7"
                    : ruleGroupTypeId;
            }
        }

        internal static string AccountId
        {
            get
            {
                var accountId = GetValueFromConfig(Constants.Consul.Default, Constants.Default.AccountId);
                return IsNullOrEmpty(accountId)
                    ? "7"
                    : accountId;
            }
        }

        internal static string ClientId
        {
            get
            {
                var clientId = GetValueFromConfig(Constants.Consul.Default, Constants.Default.ClientId);

                return IsNullOrEmpty(clientId)
                    ? "103"
                    : clientId;
            }
        }

        internal static string Branch
        {
            get
            {
                var branch = GetValueFromConfig(Constants.Consul.Default, Constants.Default.Branch);

                return IsNullOrEmpty(branch)
                    ? "711"
                    : branch;
            }
        }

        public static string RequesterCode
        {
            get
            {
                var requesterCode = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RequesterCode);

                return IsNullOrEmpty(requesterCode)
                    ? "123456"
                    : requesterCode;
            }
        }

        public static string RequesterPosDk
        {
            get
            {
                var requesterPosDk = GetValueFromConfig(Constants.Consul.Default, Constants.Default.RequesterPosDk);

                return IsNullOrEmpty(requesterPosDk)
                    ? "123456"
                    : requesterPosDk;
            }
        }

        public static string CreditcardNameonCard
        {
            get
            {
                var firstName = GetValueFromConfig(Constants.Consul.Default, Constants.Default.CreditcardNameonCard);

                return IsNullOrEmpty(firstName)
                    ? "FirstName"
                    : firstName;
            }
        }

        public static string DisplayCurrency
        {
            get
            {
                var displayCurrency = GetValueFromConfig(Constants.Consul.Default, Constants.Default.DisplayCurrency);
                return IsNullOrEmpty(displayCurrency)
                    ? "USD"
                    : displayCurrency;
            }
        }


        public static string OUCAddress
        {
            get
            {
                var address = GetValueFromConfig(Constants.Consul.Default, Constants.Default.OUCAddress);
                return IsNullOrEmpty(address)
                    ? "400 Duke Drive|Franklin|37067"
                    : address;
            }
        }
        public static string OUCCompleteAddress
        {
            get
            {
                var address = GetValueFromConfig(Constants.Consul.Default, Constants.Default.OUCCompleteAddress);
                return IsNullOrEmpty(address)
                    ? "400 Duke Drive Franklin TN 37067 US"
                    : address;
            }
        }



        #endregion
    }
}
