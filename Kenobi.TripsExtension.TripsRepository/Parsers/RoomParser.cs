using System;
using System.Collections.Generic;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Util;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;
using System.Linq;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class RoomParser
    {
        internal static proxy.Room Parse(Model.Rate rate, Model.Room room)
        {
            //int faresourceId = int.TryParse(rate.SupplierId, out faresourceId) ? faresourceId : 0;

            proxy.Room proxyRoom = new proxy.Room
            {
                GuaranteeRequired = rate.GuaranteeRequired,
                RoomName = room.Name,
                DepositRequired = rate.DepositRequired,
                BaseOccupancy = 0,
                NumberOfBeds = room.BedDetails != null && room.BedDetails.Count > 0 ? room.BedDetails[0].Count : 1,
                BedType = room.BedDetails != null && room.BedDetails.Count > 0 ? room.BedDetails[0].Description : string.Empty,
                Prepaid = rate.IsPrepaid,
                NumberOfBedRooms = 1,
                RoomDescription = room.Description,
                GuestCount = room.RateRoomOccupancy != null ? room.RateRoomOccupancy.AdultCount : 1,
                MaximumOccupancy = room.MaxOccupancy,
                Quantity = 1,
                RoomType = room.Type,
                DisplayRoomRate = rate.DisplayFare != null ? new proxy.RoomRate
                {
                    Discounts = GetDiscount(rate.DisplayFare.Discounts, rate.DisplayFare.Currency),
                    BaseFare = new proxy.BaseFare
                    {
                        Amount = rate.DisplayFare.BaseAmount,
                        Currency = rate.DisplayFare.Currency,
                        DisplayCurrency = Config.DisplayCurrency
                    },
                    Fees = GetFees(rate.DisplayFare.Fees, rate.DisplayFare.Currency),
                    Taxes = GetTaxes(rate.DisplayFare.Taxes, rate.DisplayFare.Currency),
                    Markup  =  GetMarkup(rate),
                    //Markups = new List<proxy.Markup>() { GetMarkup(rate) },
                    TaxIncludedInBase = false,
                    Commissions = GetCommissions(rate.SupplierCommissions, rate.DisplayFare.Currency),
                    FareType = rate.IsPrepaid ? proxy.FareType.Negotiated : proxy.FareType.Published,
                    RatePlanCode = rate.Code,
                    FareRestrictionTypes = !string.IsNullOrEmpty(rate.Refundability) ? rate.Refundability.Equals("Refundable", StringComparison.InvariantCultureIgnoreCase)
                                             ? new List<proxy.FareRestrictionType> { proxy.FareRestrictionType.RefundableFares }
                                             : new List<proxy.FareRestrictionType> { proxy.FareRestrictionType.NonRefundableFares } : null,


                } : null,
                Policies = rate.Policies != null ? rate.Policies.ConvertAll(policy => new proxy.HotelPolicy { Text = policy.Text, Type = policy.Type }) : null,
                View = room.RoomViews != null ? room.RoomViews.ToString() : string.Empty,
                AdditionalInformation = new List<proxy.StateBag>
                    {
                        new proxy.StateBag()
                        {
                            Name = Constants.Default.PlatformSupplierId,
                            Value = rate.SupplierId
                        }
                    }
            };
            if (proxyRoom.DisplayRoomRate.Discounts != null)
            {
                proxyRoom.DisplayRoomRate.BaseFare.Amount -= proxyRoom.DisplayRoomRate.Discounts.Sum(disc => disc.Amount);
                proxyRoom.DisplayRoomRate.Discounts = new List<proxy.Discount>();
            }
            return proxyRoom;
        }

        private static proxy.Money GetMarkup(Rate rate)
        {
            return new proxy.Money() { Amount = rate.DisplayFare.TotalMarkup, Currency= rate.DisplayFare.Currency };
        }

        //private static proxy.Money GetMarkup(List<Markup> markups)
        //{
        //    return new proxy.Money
        //    {
        //        Amount = markups.Sum(markup => markup.Amount),
        //        Currency = markups[0].Currency

        //    };

        //}

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
    }
}
