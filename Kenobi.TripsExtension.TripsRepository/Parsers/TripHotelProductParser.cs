using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Util;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class TripHotelProductParser
    {
        internal static proxy.HotelTripProduct Parser(Model.Hotel hotelBooking, Model.HotelDetails modelHotelDetails,
            Model.Rate modelRate,
            Model.BookingSearchQuery searchQuery, int leadPassengerRph, int rph, Model.SupplierCard supplierCard)
        {
            proxy.HotelTripProduct tripProduct = new proxy.HotelTripProduct
            {
                Rph = rph,
                HotelItinerary = HotelItineraryParser.Parse(modelRate, hotelBooking, modelHotelDetails),
                IsOnlyLeadPaxInfoRequired = !modelRate.AllGuestInfoRequired,
                LeadPassengerRph = leadPassengerRph,
                HotelSearchCriterion = HotelSearchCriteriaParser.Parser(searchQuery),
                PaymentBreakups = modelRate.DisplayFare != null ? GetPaymentBreakup(modelRate.DisplayFare) : null,
                Attributes = new List<proxy.StateBag> {new proxy.StateBag {Name = "BookingId", Value = hotelBooking.Id}},
                Owner = GetOwner(),
                AgencyGeneratedPayments =
                    supplierCard != null ? GetAgencyPayments(supplierCard, modelRate.DisplayFare) : null
                //todo add suppliercard from oski here  
            };
            return tripProduct;
        }

        private static proxy.User GetOwner()
        {
            return new proxy.User
            {
                Email = Config.OwnerEmail,
                FirstName = Config.OwnerFirstName,
                LastName = Config.OwnerLastName,
                UserName = ConfigurationHelper.GetOwnerUserName()
            };
        }

        private static List<proxy.Payment> GetAgencyPayments(SupplierCard supplierCard, Model.DisplayFare fare)
        {
            try
            {
                string oucAddress = Config.OUCAddress;
                string oucAddressLine1 = oucAddress.Split('|')[0];
                string oucAddressCity = oucAddress.Split('|')[1];
                string oucAddressZipcode = oucAddress.Split('|')[2];
                decimal amount = fare.GetFareQuoteAmount();
                return new List<proxy.Payment>()
                {
                    new proxy.CreditCardPayment()
                    {
                        Number = supplierCard.Number,
                        //SecurityCode = "CVV_MASK",
                        NameOnCard = supplierCard.NameOnCard,
                        CardMake = new proxy.CreditCardMake() {Code = supplierCard.IssuedBy},
                        ExpiryMonthYear = DateTime.Now.AddDays(3),
                        Amount =
                            new proxy.Money()
                            {
                                Amount = amount,
                                Currency = fare.Currency,
                                DisplayAmount = amount,
                                DisplayCurrency = Config.DisplayCurrency
                            },
                        BillingAddress = new proxy.Address()
                        {
                            CodeContext = proxy.LocationCodeContext.Address,
                            AddressLine1 = oucAddressLine1,
                            ZipCode = oucAddressZipcode,
                            City =
                                new proxy.City()
                                {
                                    Name = oucAddressCity,
                                    CodeContext = proxy.LocationCodeContext.City,
                                    Country = "US",
                                    State = "TN"
                                },
                            CompleteAddress = Config.OUCCompleteAddress

                        }
                    }
                };
            }
            catch (Exception)
            {
                {
                }
                throw;
            }
        }

        //private static decimal GetFareQuoteAmount(Model.DisplayFare displayFare)
        //{
        //    decimal fareQuoteAmount = 0;
        //    // decimal totalMarkup = displayFare.Markups != null ? displayFare.Markups.Sum(markup => markup.Amount) : 0;
        //    decimal baseFare = displayFare.BaseAmount;
        //    decimal totalTax = displayFare.Taxes != null ? displayFare.Taxes.Sum(tax => tax.Amount) : 0;
        //    decimal totalFee = displayFare.Fees != null ? displayFare.Fees.Sum(fee => fee.Amount) : 0;
        //    decimal totalDiscount = displayFare.Discounts != null ? displayFare.Discounts.Sum(discount => discount.Amount) : 0;

        //    fareQuoteAmount += baseFare;
        //    fareQuoteAmount += totalFee;
        //    fareQuoteAmount += totalTax;
        //    fareQuoteAmount -= totalDiscount;
        //    return fareQuoteAmount;
        //}

        private static List<proxy.PaymentBreakup> GetPaymentBreakup(Model.DisplayFare fare)
        {
            return new List<proxy.PaymentBreakup>
            {
                new proxy.PaymentBreakup
                {
                    Amount = new proxy.Money
                    {
                        Amount = fare.BaseAmount,
                        Currency = fare.Currency,
                        DisplayCurrency = Config.DisplayCurrency
                    }
                }
            };
        }
    }


}
