using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public static  class DisplayFareExtension
    {
        public static decimal GetFareQuoteAmount(this Model.DisplayFare displayFare)
        {
            decimal fareQuoteAmount = 0;
            // decimal totalMarkup = displayFare.Markups != null ? displayFare.Markups.Sum(markup => markup.Amount) : 0;
            decimal baseFare = displayFare.BaseAmount;
            decimal totalTax = displayFare.Taxes?.Sum(tax => tax.Amount) ?? 0;
            decimal totalFee = displayFare.Fees?.Sum(fee => fee.Amount) ?? 0;
            decimal totalDiscount = displayFare.Discounts?.Sum(discount => discount.Amount) ?? 0;

            fareQuoteAmount += baseFare;
            fareQuoteAmount += totalFee;
            fareQuoteAmount += totalTax;
            fareQuoteAmount -= totalDiscount;
            return fareQuoteAmount;
        }
    }
}
