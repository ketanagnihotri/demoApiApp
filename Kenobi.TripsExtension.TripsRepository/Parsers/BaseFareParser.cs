using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public class BaseFareParser
    {
        public static proxy.BaseFare GetBaseFare(Rate rate, AgencyFare agencyfare)
        {
            var baseFare = new proxy.BaseFare
            {
                Amount = Convert.ToDecimal(rate?.RateBreakup?.BaseFare?.Amount),
                Currency = rate?.RateBreakup?.BaseFare?.Currency,
                BaseEquivAmount = agencyfare.BaseAmount,
                BaseEquivCurrency = agencyfare.Currency,
                DisplayAmount = GetDisplayAmount(rate, agencyfare),
                DisplayCurrency = rate?.DisplayFare?.Currency,
              //  UsdEquivAmount = agencyfare.BaseAmount
            };
            return baseFare;
        }
        private static decimal GetDisplayAmount(Rate rate, AgencyFare agencyfare)
        {
            if (rate.DisplayFare.Currency.Equals(rate?.RateBreakup?.BaseFare?.Currency))
                return Convert.ToDecimal(rate?.RateBreakup?.BaseFare?.Amount);

            if (agencyfare.Currency.Equals(rate?.DisplayFare?.Currency, StringComparison.InvariantCultureIgnoreCase) ==
                false
                || agencyfare.BaseAmount != rate?.DisplayFare?.BaseAmount
                || (agencyfare.Markups != null && agencyfare.Markups.Count > 0))
            {
                decimal exchangeRate = GetExchangeRate(rate, agencyfare);

                var baseFare = exchangeRate*agencyfare.BaseAmount;

                return baseFare;
            }

            return rate.DisplayFare.BaseAmount;
        }

        public static decimal GetExchangeRate(Rate rate, AgencyFare agencyfare)
        {
            decimal totalDisplayFare = GetTotalDisplayFare(rate?.DisplayFare);

            decimal totalAgencyFare = GetTotalAgencyFare(agencyfare);

            return totalDisplayFare/totalAgencyFare;
        }


        public static decimal GetTotalAgencyFare(AgencyFare agencyfare)
        {
            decimal totalFare = agencyfare.BaseAmount + GetTotalTaxes(agencyfare.Taxes) + GetTotalFees(agencyfare.Fees) +
                                GetTotalMarkup(agencyfare.Markups);
            return totalFare;
        }

        public static decimal GetTotalDisplayFare(DisplayFare displayFare)
        {
            decimal totalFare = displayFare.BaseAmount + GetTotalTaxes(displayFare.Taxes) +
                                GetTotalFees(displayFare.Fees);
            return totalFare;
        }

        public static decimal GetTotalSupplierFare(RateBreakup rateBreakup)
        {
            decimal totalFare = Convert.ToDecimal(rateBreakup.BaseFare.Amount) + GetTotalTaxes(rateBreakup.Taxes) +
                                GetTotalFees(rateBreakup.Fees) + GetTotalMarkup(rateBreakup.Markups);
            return totalFare;
        }

        private static decimal GetTotalFees(List<Fee> fees)
        {
            return fees.Sum(fee => fee.Amount);
        }

        private static decimal GetTotalMarkup(List<Markup> markups)
        {
            return markups.Sum(markup => markup.Amount);
        }

        private static decimal GetTotalTaxes(List<Tax> taxes)
        {
            return taxes.Sum(fee => fee.Amount);
        }
    }
}
