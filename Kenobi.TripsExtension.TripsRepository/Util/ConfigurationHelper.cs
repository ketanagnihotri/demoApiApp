using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Kenobi.Common.TenantConfiguration.Entities;
using static System.String;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Util
{
    internal class ConfigurationHelper
    {
        private static Client _client;
        private static KenobiTenant _tenant;
        private static decimal _displayAmount;
        private static string _displayCurrency;

        public ConfigurationHelper(string tenantId)
        {
            _client = TenantConfigurationHelper.GetTenantConfiguration(tenantId);
            if (_client?.KenobiTenants != null)
                _tenant = _client.KenobiTenants.Find(t => t.KenobiTenantId == tenantId);
        }
        internal static List<proxy.StateBag> GetSupplierSideAdditionalData(int paymentOptionCount, decimal fareQuoteAmount, string displayCurrency, decimal exchangeRate)
        {
            _displayAmount = fareQuoteAmount;
            _displayCurrency = displayCurrency;
            return new List<proxy.StateBag>
            {
                new proxy.StateBag {Name = "PaymentOptions", Value = GetPaymentOptionsXml(paymentOptionCount).ToString()},
                new proxy.StateBag {Name = "PaymentBreakup", Value = GetPaymentBreakupXml(paymentOptionCount).ToString()},
                new proxy.StateBag {Name = "RuleGroupId", Value = _tenant.RuleGroupId},
                new proxy.StateBag {Name = "EX_RATE", Value = exchangeRate.ToString()}
            };
        }


        private static XElement GetPaymentOptionsXml(int paymentOptionCount)
        {
            var xeOption = new XElement("ArrayOfPaymentOption");

            for (var i = 0; i < paymentOptionCount; i++)
                xeOption.Add(GetPaymentOptionXml());
            return xeOption;
        }

        private static XElement GetPaymentOptionXml()
        {
            var xeOption = new XElement("PaymentOption");

            xeOption.Add(new XElement("AddToFareQuote", null));
            xeOption.Add(new XElement("Attributes", null));
            xeOption.Add(new XElement("CrossOffFareQuote", null));
            xeOption.Add(GetCurrencyOptions());
            xeOption.Add(new XElement("DiscountAmount", "0"));
            xeOption.Add(new XElement("FareQuote", _displayAmount.ToString(CultureInfo.InvariantCulture)));
            xeOption.Add(new XElement("FareQuoteAdjustment", ""));
            xeOption.Add(new XElement("Fees", null));
            xeOption.Add(new XElement("IncrementAmount", ""));
            xeOption.Add(new XElement("IncrementRoundingType", "None"));
            xeOption.Add(new XElement("IsDeposit", "false"));
            xeOption.Add(new XElement("MarkupAmount", "0"));
            xeOption.Add(new XElement("MaxValue", ""));
            xeOption.Add(new XElement("AddToFareQuote", "True"));
            xeOption.Add(new XElement("ProgramCurrencyMinimum", ""));
            xeOption.Add(new XElement("ProgramCurrencyMinimumTypeDescription", "Numeric"));
            xeOption.Add(new XElement("ProgramCurrencyMinimumTypeId", "1"));
            xeOption.Add(new XElement("Rank", "200"));
            xeOption.Add(new XElement("RequiresBinValidation", "false"));
            xeOption.Add(new XElement("RequiresOrderhistoryValidation", "false"));
            xeOption.Add(new XElement("RewardsAvailable", null));
            xeOption.Add(new XElement("RuleGroupDescription", ""));
            xeOption.Add(new XElement("RuleGroupId", _tenant.RuleGroupId));
            xeOption.Add(new XElement("RuleGroupName", Config.RuleGroupName));
            xeOption.Add(new XElement("RuleGroupType", Config.RuleGroupType));
            xeOption.Add(new XElement("RuleGroupTypeId", Config.RuleGroupTypeId));
            xeOption.Add(new XElement("ShortfallFactor", ""));
            xeOption.Add(new XElement("ShortfallFactorTypeDescription", "Multiply"));
            xeOption.Add(new XElement("ShortfallFactorTypeID", "2"));
            return xeOption;
        }
        private static XElement GetCurrencyOptions()
        {
            var xeCurrencyOptions = new XElement("CurrencyOptions");
            xeCurrencyOptions.Add(GetCurrencyOption());
            return xeCurrencyOptions;
        }

        private static XElement GetCurrencyOption()
        {
            var xeCurrencyOption = new XElement("CurrencyOption");
            xeCurrencyOption.Add(GetCurrencies());
            return xeCurrencyOption;
        }

        private static XElement GetCurrencies()
        {
            var xeCurrencies = new XElement("Currencies");
            xeCurrencies.Add(GetCurrency());
            return xeCurrencies;
        }

        private static XElement GetCurrency()
        {
            var xeCurrency = new XElement("Currency");
            xeCurrency.Add(new XElement("Amount", _displayAmount.ToString(CultureInfo.InvariantCulture)));
            xeCurrency.Add(new XElement("CrossOffAmount", "0"));
            xeCurrency.Add(new XElement("DecimalPlaces", "0"));
            xeCurrency.Add(new XElement("PointCalculationFactor", "0"));
            xeCurrency.Add(new XElement("Type", _displayCurrency));
            return xeCurrency;
        }

        private static XElement GetPaymentBreakupXml(int paymentOptionCount)
        {

            var xeSelectedPaymentOptions = new XElement("SelectedPaymentOptions");

            for (var i = 0; i < paymentOptionCount; i++)
                xeSelectedPaymentOptions.Add(GetPaymentBreakupPaymentXml());

            return xeSelectedPaymentOptions;
        }

        private static XElement GetPaymentBreakupPaymentXml()
        {
            var xeOption = new XElement("PaymentOption");
            xeOption.Add(new XElement("RuleGroupId", _tenant.RuleGroupId));
            xeOption.Add(new XElement("RuleGroupType", Config.RuleGroupType));
            xeOption.Add(new XElement("RuleGroupTypeId", Config.RuleGroupTypeId));
            xeOption.Add(new XElement("PointsRedeemed", "0.00"));
            xeOption.Add(new XElement("RedemptionCashCost", "0.00"));
            xeOption.Add(new XElement("ProgramCurrencyRequired", "0"));
            xeOption.Add(new XElement("ProgramCurrencyLabel", "USD"));
            xeOption.Add(new XElement("UnitPointsPrice", "0"));
            xeOption.Add(new XElement("MaxValue", "0"));
            xeOption.Add(new XElement("ClientPrice", "0"));
            xeOption.Add(new XElement("RewardQuantity", "0"));
            xeOption.Add(new XElement("Attributes", ""));
            xeOption.Add(new XElement("Fees", ""));
            xeOption.Add(new XElement("IsInvoice", "True"));
            return xeOption;
        }


        internal static List<proxy.StateBag> GetPosAdditionalInfo()
        {

            if (_client != null && _tenant != null)
            {
                return new List<proxy.StateBag>
                {
                    new proxy.StateBag { Name = "DisplayProgramCurrencyAsDecimal", Value = "N" },
                    new proxy.StateBag { Name = "DealerUrl", Value = "" },
                    new proxy.StateBag { Name = "AccountId", Value = Config.AccountId },
                    new proxy.StateBag { Name = "UserId", Value = _client.UserId },
                    new proxy.StateBag { Name = "ThemeID", Value = "BaseRedemption" },
                    new proxy.StateBag { Name = "_POSDKField_", Value = "19" },
                    new proxy.StateBag { Name = "CorporateId", Value = "529511" },
                    new proxy.StateBag { Name = "Branch", Value = _tenant?.Branch ?? Config.Branch },
                    new proxy.StateBag { Name = "ProfileId", Value = _client.ClientId+"_"+Guid.NewGuid()},
                    new proxy.StateBag { Name = "ClientId", Value = _client.ClientId.ToString() },
                    new proxy.StateBag { Name = "AgentId", Value = _tenant.AgentId.ToString() },
                    new proxy.StateBag { Name = "TransitCode", Value = _tenant.TransitCode },
                    new proxy.StateBag { Name = "ProgramId", Value = _tenant.ProgramId.ToString() },
                    new proxy.StateBag { Name = "ProgramCode", Value = _tenant.ProgramCode },
                    new proxy.StateBag { Name = "IsOrderFeeRefundable", Value = "10043335" },
                    new proxy.StateBag { Name = "ClearCommerceOrderID", Value = "" },
                    new proxy.StateBag { Name = "SiteUrl", Value = "BaseRedemption" },
                    new proxy.StateBag { Name = "OrderId", Value = "10043335" },
                    new proxy.StateBag { Name = "AutoRecapId", Value = "10043335047" },
                    new proxy.StateBag { Name = "OrderConfirmationCode", Value = "0" },
                    new proxy.StateBag { Name = "OrderAPIRewardsAvailableBalance", Value = "0" },
                    new proxy.StateBag { Name = "OrderAPIAvailableBalance", Value = "0" },
                    new proxy.StateBag { Name = "OrderAPIRawBalance", Value = "0" },

                    new proxy.StateBag { Name = "ClientProgramGroupId", Value =_tenant.ClientProgramGroupId},
                    new proxy.StateBag { Name = "RuleGroupId", Value =_tenant.RuleGroupId}

                };
            }
            return null;
        }

        internal static string GetOwnerUserName()
        {
            return _client != null ? Concat(_client.ClientId, Guid.NewGuid().ToString()) : Empty;
        }
        internal static string GetTenantRuleId()
        {
            return _tenant != null ? _tenant.RuleGroupId : string.Empty;
        }
        internal static string GetCreatorUserName()
        {
            return _client != null ? Concat(_client.ClientId, Guid.NewGuid().ToString()) : Empty;
        }


        internal static List<proxy.StateBag> GetUserAdditionalInfo()
        {
            return new List<proxy.StateBag>
            {
                new proxy.StateBag { Name = "AgencyName", Value = "Connexions" },
                new proxy.StateBag { Name = "CompanyName", Value = "Connexions" },
                new proxy.StateBag { Name = "UserType", Value = "" },
                new proxy.StateBag { Name = "UserOrderCreateDate", Value = DateTime.Now.ToString(CultureInfo.InvariantCulture)}
            };
        }
    }
}
