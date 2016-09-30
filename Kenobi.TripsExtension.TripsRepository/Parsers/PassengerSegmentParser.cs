using System;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class PassengerSegmentParser
    {
        internal static proxy.PassengerSegment Parser(Model.Hotel hotelBooking, Model.Voucher voucher, int passengerRph, int rph)
        {
            
            return new proxy.PassengerSegment
            {
                SupplierConfirmationNumber = voucher.SupplierConfirmation,
                CancellationNumber = voucher.SupplierCancellationNumber,
                VendorConfirmationNumber = voucher.VendorConfirmation,
                //   PostBookingStatus =  hotelBooking.Status,
                BookingStatus = string.IsNullOrEmpty(hotelBooking.Status)? proxy.TripProductStatus.Planned : GetBookingStatus(hotelBooking.Status),
                Rph = rph,
                PassengerRph = passengerRph
            };
        }

        private static proxy.TripProductStatus GetBookingStatus(string status)
        {
            proxy.TripProductStatus bookingstatus = proxy.TripProductStatus.Planned;

            if (status.Equals("Confirmed", StringComparison.InvariantCultureIgnoreCase))
                bookingstatus = proxy.TripProductStatus.Purchased;
            else if(status.Equals("Canceled", StringComparison.InvariantCultureIgnoreCase))
                bookingstatus = proxy.TripProductStatus.Canceled;

            return bookingstatus;
        }
    }
}
