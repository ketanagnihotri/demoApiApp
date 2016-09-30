using System;
using System.Collections.Generic;
using System.Linq;
using Kenobi.TripsExtension.TripsRepository.Model;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;


namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class HotelSearchCriteriaParser
    {
        internal static proxy.HotelSearchCriterion Parser(BookingSearchQuery searchQuery)
        {
            DateTime stayPeriodStart; DateTime stayPeriodEnd;
            int posId;
            return searchQuery != null ?

           new proxy.HotelSearchCriterion
           {
               StayPeriod = new proxy.DateTimeSpan { Start = DateTime.TryParse(searchQuery.StayPeriod?.Start, out stayPeriodStart) ? stayPeriodStart : DateTime.Now, End = DateTime.TryParse(searchQuery.StayPeriod?.End, out stayPeriodEnd) ? stayPeriodEnd : DateTime.Now.AddDays(2) },
               //NoOfBedRooms = searchQuery.RoomOccupancies.Count,
               Guests = searchQuery.RoomOccupancies != null ? GetGuestCount(searchQuery.RoomOccupancies) : null,
               Pos = new proxy.PointOfSale { PosId = int.TryParse(searchQuery.PosId, out posId) ? posId : 0 },
               PassengerRoomDistribution = GetpassengerRoomDistribution(searchQuery.RoomOccupancies),
               PriceCurrencyCode = searchQuery.Currency,
               SearchType = proxy.HotelSearchType.GeoCode
           } : null;

        }

        private static List<proxy.PassengerTypeQuantity> GetGuestCount(List<RoomOccupancy> roomOccupancies)
        {
            List<proxy.PassengerTypeQuantity> lstPaxType = new List<proxy.PassengerTypeQuantity>();

            int adultCount =roomOccupancies.Sum(roomOccupant => roomOccupant.Occupants.FindAll(ocupant => ocupant.Type.Equals("Adult", StringComparison.InvariantCultureIgnoreCase)).Count);
            int childCount = roomOccupancies.Sum(roomOccupant => roomOccupant.Occupants.FindAll(ocupant => ocupant.Type.Equals("Child", StringComparison.InvariantCultureIgnoreCase)).Count);
            int seniorCount = roomOccupancies.Sum(roomOccupant => roomOccupant.Occupants.FindAll(ocupant => ocupant.Type.Equals("Senior", StringComparison.InvariantCultureIgnoreCase)).Count);

            if (adultCount > 0)
            {
                proxy.PassengerTypeQuantity adultQuantity = new proxy.PassengerTypeQuantity
                {
                    Quantity = adultCount,
                    PassengerType = proxy.PassengerType.Adult
                };
                lstPaxType.Add(adultQuantity);
            }

            if (childCount > 0)
            {
                proxy.PassengerTypeQuantity childQuantity = new proxy.PassengerTypeQuantity
                {
                    Quantity = childCount,
                    PassengerType = proxy.PassengerType.Child
                };
                lstPaxType.Add(childQuantity);
            }

            if (seniorCount > 0)
            {
                proxy.PassengerTypeQuantity seniorQuantity = new proxy.PassengerTypeQuantity
                {
                    Quantity = seniorCount,
                    PassengerType = proxy.PassengerType.Senior
                };
                lstPaxType.Add(seniorQuantity);
            }
            return lstPaxType;
        }

        private static proxy.PassengerRoomDistribution GetpassengerRoomDistribution(List<RoomOccupancy> roomOccupancies)
        {

            List<proxy.RoomOccupancyDetail> roomOccupancyDetail = new List<proxy.RoomOccupancyDetail>();
            foreach (var roomOccupancy in roomOccupancies)
            {
                proxy.RoomOccupancyDetail proxyRoomOccupancy = new proxy.RoomOccupancyDetail
                {
                    OccupantsInfo = roomOccupancy.Occupants.ConvertAll(occupant => new proxy.OccupantInfo
                    {
                        Age = occupant.Age,
                        Type = GetOccupantDetails(occupant.Type)
                    })
                };
                roomOccupancyDetail.Add(proxyRoomOccupancy);
            }
            return new proxy.PassengerRoomDistribution { RoomOccupany = roomOccupancyDetail };
        }

        private static proxy.PassengerType GetOccupantDetails(string type)
        {
            if (type.Equals("Adult", StringComparison.InvariantCultureIgnoreCase))
                return proxy.PassengerType.Adult;
            if (type.Equals("child", StringComparison.InvariantCultureIgnoreCase))
                return proxy.PassengerType.Child;
            return type.Equals("senior", StringComparison.InvariantCultureIgnoreCase) ? proxy.PassengerType.Senior : proxy.PassengerType.Adult;
        }
    }
}