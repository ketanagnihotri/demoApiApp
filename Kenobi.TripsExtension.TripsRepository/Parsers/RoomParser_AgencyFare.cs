using System;
using System.Collections.Generic;
using System.Linq;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Util;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class RoomParser_AgencyFare
    {
        internal static proxy.Room Parse(Rate rate, Room room, AgencyFare agencyfare)
        {
            proxy.Room proxyRoom = new proxy.Room
            {
                GuaranteeRequired = rate.GuaranteeRequired,
                RoomName = room.Name,
                DepositRequired = rate.DepositRequired,
                BaseOccupancy = 0,
                NumberOfBeds = room.BedDetails != null && room.BedDetails.Count > 0 ? room.BedDetails[0].Count : 1,
                BedType =
                    room.BedDetails != null && room.BedDetails.Count > 0 ? room.BedDetails[0].Description : string.Empty,
                Prepaid = rate.IsPrepaid,
                NumberOfBedRooms = 1,
                RoomDescription = room.Description,
                GuestCount = room.RateRoomOccupancy?.AdultCount ?? 1,
                MaximumOccupancy = room.MaxOccupancy,
                Quantity = 1,
                RoomType = room.Type,
                DisplayRoomRate = GetDisplayRoomRate(rate, agencyfare),
                Policies = GetPolicies(rate),
                View = room.RoomViews?.ToString() ?? string.Empty,
                AdditionalInformation = GetAdditionalInformation(rate)
            };
            if (proxyRoom.DisplayRoomRate.Discounts != null)
            {
                proxyRoom.DisplayRoomRate.BaseFare.Amount -= proxyRoom.DisplayRoomRate.Discounts.Sum(disc => disc.Amount);
                proxyRoom.DisplayRoomRate.BaseFare.BaseEquivAmount -=
                    proxyRoom.DisplayRoomRate.Discounts.Sum(disc => disc.BaseEquivAmount);
                proxyRoom.DisplayRoomRate.BaseFare.DisplayAmount -=
                    proxyRoom.DisplayRoomRate.Discounts.Sum(disc => disc.DisplayAmount);
                proxyRoom.DisplayRoomRate.Discounts = new List<proxy.Discount>();
            }
            return proxyRoom;
        }

        private static List<proxy.StateBag> GetAdditionalInformation(Rate rate)
        {
            return new List<proxy.StateBag>
            {
                new proxy.StateBag
                {
                    Name = Constants.Default.PlatformSupplierId,
                    Value = rate.SupplierId
                }
            };
        }

        private static List<proxy.HotelPolicy> GetPolicies(Rate rate)
        {
            return rate.Policies?.ConvertAll(
                policy => new proxy.HotelPolicy {Text = policy.Text, Type = policy.Type});
        }

        private static proxy.RoomRate GetDisplayRoomRate(Rate rate, AgencyFare agencyfare)
        {
            return agencyfare != null
                ? new proxy.RoomRate
                {
                    Discounts = DiscountParser.GetDiscount(rate, agencyfare),
                    BaseFare = BaseFareParser.GetBaseFare(rate, agencyfare),
                    Fees = FeesParser.GetFees(rate, agencyfare),
                    Taxes = TaxParser.GetTaxes(rate, agencyfare),
                    Markup = MarkupsParser.GetMarkup(rate, agencyfare),
                    Markups = MarkupsParser.GetMarkups(rate, agencyfare),
                    TaxIncludedInBase = false,
                    //TODO: Add Commissions parser 
                    Commissions =
                        GetCommissions(rate.SupplierCommissions, agencyfare.Currency, rate.DisplayFare.Currency),
                    FareType = rate.IsPrepaid ? proxy.FareType.Negotiated : proxy.FareType.Published,
                    RatePlanCode = rate.Code,
                    FareRestrictionTypes = GetFareRestrictionTypes(rate),
                }
                : null;
        }



        private static List<proxy.FareRestrictionType> GetFareRestrictionTypes(Rate rate)
        {
            return !string.IsNullOrEmpty(rate.Refundability)
                ? rate.Refundability.Equals("Refundable", StringComparison.InvariantCultureIgnoreCase)
                    ? new List<proxy.FareRestrictionType> {proxy.FareRestrictionType.RefundableFares}
                    : new List<proxy.FareRestrictionType> {proxy.FareRestrictionType.NonRefundableFares}
                : null;
        }

        //private static proxy.Money GetMarkup(List<Markup> markups)
        //{
        //    return markups != null && markups.Count > 0
        //        ? new proxy.Money
        //        {
        //            Amount = markups.Sum(markup => markup.Amount),
        //            Currency = string.IsNullOrEmpty(markups[0].Currency) ? Config.DisplayCurrency : markups[0].Currency
        //        }
        //        : null;
        //}



        private static List<proxy.Commission> GetCommissions(List<SupplierCommission> commissions, string currency,
            string displayCurrency)
        {
            decimal commissionAmount;
            return commissions?.ConvertAll(
                commission =>
                    new proxy.Commission
                    {
                        Amount = decimal.TryParse(commission.Amount, out commissionAmount) ? commissionAmount : 0,
                        Currency = currency,
                        Type = "Supplier",
                        DisplayCurrency = displayCurrency
                    });
        }

        //private static List<proxy.Tax> GetTaxes(List<Tax> taxes, string currency)
        //{
        //    return
        //        taxes.ConvertAll(
        //            tax =>
        //                new proxy.Tax
        //                {
        //                    Amount = tax.Amount,
        //                    Currency = currency,
        //                    Type = tax.Description,
        //                    DisplayCurrency = Config.DisplayCurrency
        //                });
        //}

        //private static List<proxy.Fee> GetFees(List<Fee> fees, string currency)
        //{
        //    return fees?.ConvertAll(
        //        fee =>
        //            new proxy.Fee
        //            {
        //                Amount = fee.Amount,
        //                Currency = currency,
        //                Type = fee.Description,
        //                DisplayCurrency = Config.DisplayCurrency
        //            });
        //}
    }
}