using System;
using System.Collections.Generic;
using System.Linq;
using Kenobi.TripsExtension.TripsRepository.Util;
using static System.Int32;
using CancellationPolicy = Kenobi.TripsExtension.TripsRepository.Model.CancellationPolicy;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class HotelItineraryParser
    {
        static readonly string DisplayCurrency = Config.DisplayCurrency;
        internal static proxy.HotelItinerary Parse(Model.Rate modelRate, Model.Hotel modelHotel, Model.HotelDetails modelHotelDetails)
        {           
            int faresourceId;
            DateTime stayPeriodStart; DateTime stayPeriodEnd;
            TryParse(modelRate.SupplierId, out faresourceId);
            proxy.HotelItinerary hotelItinerary = new proxy.HotelItinerary
            {
                DepositRequired = modelRate.DepositRequired,
                AllPaxDetailsRequired = modelRate.AllGuestInfoRequired,
                HotelFareSource = new proxy.HotelFareSource { Id = faresourceId },
                Deals = modelRate.Offer != null ? HotelDealsParser.Parse(modelRate.Offer) : null,
                HotelCancellationPolicy = GetHotelCancellationPolicy(modelRate.CancellationPolicy),
                //AdditionalInformation = GetItineraryAdditionalInfo(),
                GuaranteeRequired = modelRate.GuaranteeRequired,
                //ItineraryStatus = Converter.StringToEnum<proxy.ItineraryStatusType>(modelHotel.Status), modelHotel.Status type not in enum
                StayPeriod = new proxy.DateTimeSpan { Start = DateTime.TryParse(modelHotel.StayPeriod?.Start, out stayPeriodStart) ? stayPeriodStart : DateTime.Now, End = DateTime.TryParse(modelHotel.StayPeriod?.End, out stayPeriodEnd) ? stayPeriodEnd : DateTime.Now.AddDays(2) },
                //  Fare = modelRate.SupplierDailyRates != null && modelRate.SupplierDailyRates.DailyRates!=null && modelRate.SupplierDailyRates.DailyRates.Count>0 ? SetItineraryFare(modelRate) : null,
                Fare= new proxy.HotelFare
                {

                    Discounts = GetDiscount(modelRate.DisplayFare.Discounts, modelRate.DisplayFare.Currency),
                    BaseFare = new proxy.BaseFare
                    {
                        Amount = modelRate.DisplayFare.BaseAmount,
                        Currency = modelRate.DisplayFare.Currency,
                        DisplayCurrency = Config.DisplayCurrency
                    },
                    Fees = GetFees(modelRate.DisplayFare.Fees, modelRate.DisplayFare.Currency),
                    Taxes = GetTaxes(modelRate.DisplayFare.Taxes, modelRate.DisplayFare.Currency),
                    Markup = GetMarkup(modelRate)

                },
                HotelProperty = HotelPropertyParser.Parse(modelHotel, modelHotelDetails),
                ItineraryStatus = string.IsNullOrEmpty(modelHotel.Status) ? proxy.ItineraryStatusType.Unbooked : GetItineraryStatus(modelHotel.Status)

            };

            if (hotelItinerary.Fare.Discounts != null)
            {
                hotelItinerary.Fare.BaseFare.Amount -=  hotelItinerary.Fare.Discounts.Sum(disc => disc.Amount);
                hotelItinerary.Fare.Discounts = new List<proxy.Discount>();
            }


            return hotelItinerary;
        }
        
        private static List<proxy.Discount> GetDiscount(List<Model.Discount> discounts, string currency)
        {
            return discounts != null && discounts.Count > 0 ? discounts.ConvertAll(discount => new proxy.Discount { Amount = discount.Amount, Currency = currency, Type = discount.Description, DisplayCurrency = Config.DisplayCurrency }) : null;
        }

        private static List<proxy.Commission> GetCommissions(List<Model.SupplierCommission> commissions, string currency)
        {
            decimal commissionAmount;
            return commissions != null ? commissions.ConvertAll(commission => new proxy.Commission { Amount = Decimal.TryParse(commission.Amount, out commissionAmount) ? commissionAmount : 0, Currency = currency, Type = "Supplier", DisplayCurrency = Config.DisplayCurrency }) : null;
        }

        private static List<proxy.Tax> GetTaxes(List<Model.Tax> taxes, string currency)
        {
            return taxes.ConvertAll(tax => new proxy.Tax { Amount = tax.Amount, Currency = currency, Type = tax.Description, DisplayCurrency = Config.DisplayCurrency });
        }

        private static List<proxy.Fee> GetFees(List<Model.Fee> fees, string currency)
        {
            return fees != null ? fees.ConvertAll(fee => new proxy.Fee { Amount = fee.Amount, Currency = currency, Type = fee.Description, DisplayCurrency = Config.DisplayCurrency }) : null;
        }

        private static proxy.Money GetMarkup(Model.Rate rate)
        {
            return new proxy.Money() { Amount = rate.DisplayFare.TotalMarkup, Currency = rate.DisplayFare.Currency };
        }
        private static proxy.ItineraryStatusType GetItineraryStatus(string status)
        {
            proxy.ItineraryStatusType bookingstatus = proxy.ItineraryStatusType.Unbooked;

            if (status.Equals("Confirmed", StringComparison.InvariantCultureIgnoreCase))
                bookingstatus = proxy.ItineraryStatusType.Booked;
            else if (status.Equals("Canceled", StringComparison.InvariantCultureIgnoreCase))
                bookingstatus = proxy.ItineraryStatusType.Canceled;

            return bookingstatus;
        }


        private static proxy.HotelFare SetItineraryFare(Model.Rate rates)
        {
            Model.SupplierDailyRate supplierDailyRates = rates.SupplierDailyRates;
            var lstDailyRates = new List<proxy.DailyRate>();
            string currency = supplierDailyRates.DailyRates[0].Currency;
            foreach (var modelDailyRate in supplierDailyRates.DailyRates)
            {
                DateTime dailyRateDate;
                DateTime.TryParse(modelDailyRate.Date, out dailyRateDate);
                var dailyRate = new proxy.DailyRate
                {
                    Amount = Convert.ToDecimal(modelDailyRate.Amount),
                    Currency = modelDailyRate.Currency,
                    StartDate = dailyRateDate,
                    EndDate = dailyRateDate.AddDays(1)
                };

                lstDailyRates.Add(dailyRate);
            }
            decimal minRate = 0;
            decimal maxRate = 0;
            GetMinMaxDailyRate(lstDailyRates, ref minRate, ref maxRate);

            proxy.HotelFare hotelFare = new proxy.HotelFare
            {
                MaxDailyRate = new proxy.Money { Amount = maxRate, Currency = currency, DisplayCurrency = DisplayCurrency },
                MinDailyRate = new proxy.Money { Amount = minRate, Currency = currency, DisplayCurrency = DisplayCurrency },
                BaseFare = new proxy.BaseFare { Amount = lstDailyRates.FirstOrDefault().Amount, Currency = lstDailyRates.FirstOrDefault().Currency, DisplayCurrency = DisplayCurrency },
                //Discount new proxy.Discount { Amount=}
                //FareType=proxy.FareType.Negotiated

            };
            return hotelFare;
        }

        private static void GetMinMaxDailyRate(List<proxy.DailyRate> dailyRates, ref decimal minRate, ref decimal maxRate)
        {
            if (dailyRates == null) return;
            foreach (proxy.DailyRate dailyRate in dailyRates)
            {
                if (dailyRate.Amount > maxRate)
                    maxRate = dailyRate.Amount;
                if (dailyRate.Amount < minRate)
                    minRate = dailyRate.Amount;
            }
        }


        private static proxy.HotelCancellationPolicy GetHotelCancellationPolicy(CancellationPolicy cancellationPolicy)
        {
            return cancellationPolicy != null && cancellationPolicy.PenaltyRules != null ?
            new proxy.HotelCancellationPolicy
            {
                CancellationRules = GetHotelCancellationRules(cancellationPolicy)

            } : null;
        }

        private static List<proxy.CancellationRule> GetHotelCancellationRules(CancellationPolicy cancellationPolicy)
        {


            if (cancellationPolicy.PenaltyRules != null)
            {
                var lstCancellationRule = new List<proxy.CancellationRule>();

                foreach (var penaltyRule in cancellationPolicy.PenaltyRules)
                {
                    proxy.HotelCancellationRule rule = new proxy.HotelCancellationRule
                    {

                        Penalty = new proxy.HotelCancellationPenalty { BasePenalty = new proxy.Money { Amount = Convert.ToDecimal(penaltyRule.Value), Currency = DisplayCurrency, DisplayCurrency = DisplayCurrency } },
                        Type = proxy.ProviderCancellationRuleType.Text,
                        Window = penaltyRule.Window != null ?
                                        new proxy.DateTimeSpan { Start = DateTime.Parse(penaltyRule.Window.Start), End = DateTime.Parse(penaltyRule.Window.End) }
                                        : null,
                        Description = cancellationPolicy.Text

                    };
                    lstCancellationRule.Add(rule);
                }
                return lstCancellationRule;
            }
            return null;
        }
    }
}
