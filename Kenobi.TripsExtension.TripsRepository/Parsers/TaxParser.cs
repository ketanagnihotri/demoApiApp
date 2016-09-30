using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    public class TaxParser
    {
        public static List<proxy.Tax> GetTaxes(Rate rate, AgencyFare agencyfare)
        {
            //List<proxy.Tax> agencyFareTaxes = GetTaxesFromAgencyFare(rate, agencyfare);
            //List<proxy.Tax> mergedTaxes = GetMergedTaxes(rate, agencyfare);

            List<proxy.Tax> supplierFareTaxes = GetTaxesFromSupplierFare(rate, agencyfare);
            return supplierFareTaxes;
        }

        private static List<proxy.Tax> GetMergedTaxes(Rate rate, AgencyFare agencyfare)
        {
            List<proxy.Tax> agencyTaxes = new List<proxy.Tax>();
            proxy.Tax tax = new proxy.Tax
            {
                DisplayCurrency = rate.DisplayFare.Currency,
                Currency = rate.RateBreakup.BaseFare.Currency,
                BaseEquivCurrency = agencyfare.Currency,
                Amount = GetTotalTax(rate.RateBreakup.Taxes),
                BaseEquivAmount = GetTotalTax(agencyfare.Taxes),
                DisplayAmount = GetTotalTax(rate.DisplayFare.Taxes),
                Description = "SupplierTaxes"
            };
            agencyTaxes.Add(tax);
            return agencyTaxes;
        }

        private static List<proxy.Tax> GetTaxesFromSupplierFare(Rate rate, AgencyFare agencyfare)
        {
            List<proxy.Tax> agencyTaxes = new List<proxy.Tax>();

            try
            {
                var supplierTaxes = rate.RateBreakup.Taxes;
                for (int i = 0; i < supplierTaxes.Count; i++)
                {
                    proxy.Tax supplierTax = new proxy.Tax
                    {
                        Amount = supplierTaxes[i].Amount,
                        Currency = supplierTaxes[i].Currency,
                        DisplayCurrency = rate.DisplayFare.Currency,
                        DisplayAmount = rate.DisplayFare.Taxes[i].Amount,
                        BaseEquivCurrency = agencyfare.Currency,
                        Type = supplierTaxes[i].Description,
                        Description = supplierTaxes[i].Description,
                    };
                    agencyTaxes.Add(supplierTax);
                }
            }
            catch (Exception)
            {
                //TODO: Log the exception.
                agencyTaxes = GetMergedTaxes(rate, agencyfare);
            }

            return agencyTaxes;
        }

        private static decimal GetTotalTax(List<Tax> taxes)
        {
            return taxes.Sum(x => x.Amount);
        }

        private static List<proxy.Tax> GetTaxesFromAgencyFare(Rate rate, AgencyFare agencyfare)
        {
            List<proxy.Tax> agencyTaxes = new List<proxy.Tax>();
            if (agencyfare.Taxes != null && agencyfare.Taxes.Count > 0)
            {
                var taxes = agencyfare.Taxes.ConvertAll(
                    tax => new proxy.Tax
                    {
                        Description = tax.Description,
                        Amount = GetTaxAmountBySource(rate.RateBreakup?.Taxes, tax.Description),
                        Currency = rate?.RateBreakup?.BaseFare?.Currency,
                        DisplayCurrency = rate.DisplayFare?.Currency,
                        DisplayAmount = GetTaxAmountBySource(rate.DisplayFare?.Taxes, tax.Description),
                        BaseEquivAmount = tax.Amount,
                        BaseEquivCurrency = string.IsNullOrWhiteSpace(tax.Currency) ? agencyfare.Currency : tax.Currency,
                        Code = tax.Code,
                    });

                {
                    agencyTaxes = taxes;
                }
            }
            return agencyTaxes;
        }

        private static decimal GetTaxAmountBySource(List<Tax> taxes, string description)
        {
            var tax = taxes.Find(x => x.Description.Equals(description));
            if (tax != null)
                return tax.Amount;

            return 0;
        }
    }
}
