using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Parsers;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Markup = Kenobi.TripsExtension.TripsRepository.Model.Markup;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public class MarkupsParser
    {
        public static proxy.Money GetMarkup(Rate rate, AgencyFare agencyfare)
        {
            var totalMarkUp =  new proxy.Money
            {
                BaseEquivAmount = GetAgencyMarkup(agencyfare),
                BaseEquivCurrency = agencyfare.Currency,
                Currency = rate.RateBreakup.BaseFare.Currency,
                Amount = GetSupplierMarkUp(rate),
                DisplayAmount = GetDisplayMarkUp(rate, agencyfare),
                DisplayCurrency = rate.DisplayFare.Currency
            };

            return totalMarkUp;
        }

        private static decimal GetDisplayMarkUp(Rate rate, AgencyFare agencyfare)
        {
            if (rate.DisplayFare.Currency.Equals(rate?.RateBreakup?.BaseFare?.Currency))
                return GetSupplierMarkUp(rate);

            if (agencyfare.Currency.Equals(rate?.DisplayFare?.Currency, StringComparison.InvariantCultureIgnoreCase) ==
                false
                || (agencyfare.Markups != null && agencyfare.Markups.Count > 0))
            {
                
                decimal exchangeRate = BaseFareParser.GetExchangeRate(rate,agencyfare);

                var totalMarkup = exchangeRate*GetAgencyMarkup(agencyfare);

                return totalMarkup;
            }

            return rate.DisplayFare.BaseAmount;
        }

        private static decimal GetSupplierMarkUp(Rate rate)
        {
            var totalMarkUp = rate.RateBreakup?.Markups.Sum(markup => Convert.ToDecimal(markup.Amount));
            return totalMarkUp ?? 0;
        }

        private static decimal GetAgencyMarkup(AgencyFare agencyfare)
        {
            return agencyfare.Markups.Sum(markup => Convert.ToDecimal(markup.Amount));
        }

        public static List<proxy.Markup> GetMarkups(Rate rate, AgencyFare agencyfare)
        {
            if (rate.RateBreakup.Markups != null && rate.RateBreakup.Markups.Count > 0)
            {
                return rate.RateBreakup.Markups.ConvertAll(
                    markup => new proxy.Markup
                    {
                        BaseEquivAmount = GetAgencyMarkupAmount(markup, rate, agencyfare),
                        BaseEquivCurrency = GetAgencyMarkupCurrency(markup, rate, agencyfare),
                        Amount = markup.Amount,
                        Currency = markup.Currency,
                        DisplayAmount = GetDisplayMarkupAmount(markup, rate),
                        DisplayCurrency = GetDisplayCurrency(markup, rate),
                        Type = markup.Description
                    });
            }
            return null;

        }

        private static string GetDisplayCurrency(Markup markup, Rate rate)
        {
            if (markup.Currency.Equals(rate.DisplayFare.Currency))
                return markup.Currency;

            return rate.DisplayFare.Currency;
        }

        private static decimal GetDisplayMarkupAmount(Markup markup, Rate rate)
        {
            if (markup.Currency.Equals(rate.DisplayFare.Currency))
                return markup.Amount;

            decimal totalSupplierFare = BaseFareParser.GetTotalSupplierFare(rate.RateBreakup);

            decimal totalDisplayFare = BaseFareParser.GetTotalDisplayFare(rate.DisplayFare);

            decimal exchangeRate = totalSupplierFare/totalDisplayFare;

            return markup.Amount/exchangeRate;
        }

        private static string GetAgencyMarkupCurrency(Markup markup, Rate rate, AgencyFare agencyfare)
        {
            if (markup.Currency.Equals(agencyfare.Currency))
                return markup.Currency;

            return agencyfare.Currency;
        }

        private static decimal GetAgencyMarkupAmount(Markup markup, Rate rate, AgencyFare agencyfare)
        {
            if (markup.Currency.Equals(agencyfare.Currency))
                return markup.Amount;

            decimal exchangeRate = BaseFareParser.GetExchangeRate(rate, agencyfare);

            return markup.Amount/exchangeRate;
        }

    }
}
