using System;
using System.Collections.Generic;
using Kenobi.TripsExtension.TripsRepository.Util;
using static System.Decimal;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class HotelDealsParser
    {
        internal static List<proxy.Deal> Parse(Model.Offer offer)
        {

            List<proxy.Deal> lstDeal = new List<proxy.Deal>();

            if (!string.IsNullOrEmpty(offer.FixedDiscountOffer))
            {
                decimal discountOffer;
                lstDeal.Add(new proxy.DiscountDeal
                {
                    LongDescription = offer.Description,
                    Amount = new proxy.Money { Amount = TryParse(offer.FixedDiscountOffer, out discountOffer) ? discountOffer : 0,DisplayCurrency=Config.DisplayCurrency },
                    DealStatus = proxy.DealStatus.Active,
                    DealType = "DiscountDeal",
                    RoomIds = new List<Guid>(),
                    ApplyOn = string.Empty
                });
            }

            if (offer.StayOffer != null)
            {
                lstDeal.Add(
                     new proxy.PaystayDeal
                     {
                         LongDescription = offer.Description,
                         ShortDescription = "StayNights=" + offer?.StayOffer?.StayNights + " " + "FreeNights=" + offer?.StayOffer?.FreeNights,
                         DealStatus = proxy.DealStatus.Active,
                         DealType = "PaystayDeal",
                         FreeNights = offer?.StayOffer?.FreeNights ?? 0,
                         RoomIds = new List<Guid>()//TODO: Set room ids
                     });
            }
            if (offer.PercentageDiscountOffer != null)
            {
                lstDeal.Add(
                    new proxy.PercentageDiscountDeal
                    {
                        LongDescription = offer.Description,
                        Percent = Convert.ToDecimal(offer.PercentageDiscountOffer.Amount),
                        DealStatus = proxy.DealStatus.Active,
                        ApplyOn= offer.PercentageDiscountOffer.AppliedOn,
                        DealType = "PercentageDiscountDeal",
                        RoomIds = new List<Guid>()
                    });
            }
            return lstDeal;
        }
    }
}
