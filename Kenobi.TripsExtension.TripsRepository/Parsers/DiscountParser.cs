using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public class DiscountParser
    {
        public static List<proxy.Discount> GetDiscount(Rate rate, AgencyFare agencyfare)
        {
            if (agencyfare.Discounts != null && agencyfare.Discounts.Count > 0)
            {
                return agencyfare.Discounts.ConvertAll(
                    discount =>
                        new proxy.Discount
                        {
                            Amount = GetDiscountAmountBySource(rate.RateBreakup?.Discounts, discount.Source),
                            Currency = GetDiscountCurrencyBySource(rate.RateBreakup?.Discounts, discount.Source),
                            DisplayCurrency = rate.DisplayFare?.Currency,
                            DisplayAmount = GetDiscountAmountBySource(rate.DisplayFare?.Discounts, discount.Source),
                            BaseEquivAmount = discount.Amount,
                            BaseEquivCurrency = discount.Currency,
                            Type = discount.Description
                        });
            }
            return null;
        }

        private static decimal GetDiscountAmountBySource(List<Discount> discounts, string source)
        {
            var discount = discounts.Find(x => x.Source.Equals(source));
            if (discount != null)
                return discount.Amount;

            return 0;
        }

        private static string GetDiscountCurrencyBySource(List<Discount> discounts, string source)
        {
            var discount = discounts.Find(x => x.Source.Equals(source));
            return discount?.Currency;
        }
    }
}
