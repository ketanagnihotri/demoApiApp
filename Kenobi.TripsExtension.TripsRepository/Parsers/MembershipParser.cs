using System.Collections.Generic;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class MembershipParser
    {
        internal static List<proxy.Membership> Parse(List<Model.HotelLoyalty> hotelLoyalts)
        {
            var memberships = hotelLoyalts?.ConvertAll(memberShip => new proxy.Membership
            {
                Number = memberShip.Number,
                VendorCode = memberShip.ChainCode
            });
            return memberships;
        }
    }
}
