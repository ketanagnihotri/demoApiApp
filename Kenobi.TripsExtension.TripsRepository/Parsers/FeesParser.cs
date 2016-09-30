using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public class FeesParser
    {
        public static List<proxy.Fee> GetFees(Rate rate, AgencyFare agencyfare)
        {
            if (agencyfare.Fees != null && agencyfare.Fees.Count > 0)
            {
                return agencyfare.Fees.ConvertAll(
                    fee => new proxy.Fee
                    {
                        Type = fee.Description,
                        Amount = GetFeeAmountBySource(rate.RateBreakup?.Fees, fee.Description),
                        Currency = rate?.RateBreakup?.BaseFare?.Currency,
                        DisplayCurrency = rate.DisplayFare?.Currency,
                        DisplayAmount = GetFeeAmountBySource(rate.DisplayFare?.Fees, fee.Description),
                        BaseEquivAmount = fee.Amount,
                        BaseEquivCurrency = agencyfare.Currency,
                    });
            }
            return null;
        }

        private static decimal GetFeeAmountBySource(List<Fee> fees, string description)
        {
            var discount = fees.Find(x => x.Description.Equals(description));
            if (discount != null)
                return discount.Amount;

            return 0;
        }
        
    }
}
