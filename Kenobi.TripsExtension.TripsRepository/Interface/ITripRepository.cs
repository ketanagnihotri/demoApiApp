using System.Collections.Generic;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Model;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Passenger = Kenobi.TripsExtension.TripsRepository.Model.Passenger;


namespace Kenobi.TripsExtension.TripsRepository.Interface
{
    public interface ITripRepository
    {
        Task<Trip> GetTrip(string tripId);

        Task<List<Passenger>> GetTripPassengers(string tripId);

        Task<List<Voucher>> GetTripVouchers(string tripId);

        Task<List<BookingTransaction>> GetTripTransactions(string tripId);

        Task<List<Note>> GetTripNotes(string tripId);

        Task<List<Hotel>> GetTripHotels(string tripId);

        Task<Hotel> GetTripHotelByBookingId(string tripId, string bookingId);

        Task<List<Passenger>> GetHotelBookingPassengers(string tripId, string bookingId);

        Task<BookingFare> GetHotelBookingFare(string tripId, string bookingId);

        Task<Rates> GetHotelBookingRate(string tripId, string bookingId);

        Task<List<Voucher>> GetHotelBookingVouchers(string tripId, string bookingId);

        Task<BookingSearchQuery> GetHotelBookingSearchQuery(string tripId, string bookingId);

        Task<List<BookingTransaction>> GetHotelBookingTransactions(string tripId, string bookingId);

        Task<HotelDetails> GetHotelBookingHotelDetails(string tripId, string bookingId);

        void UpdateOskiWithClassicTripId(string oskiTripId, TripFolder tripFolder);

        Task<AgencyFare> GetHotelAgencyFare(string tripId, string id);

        Task<SupplierCard> GetSupplierCards(string tripId, string id);
    }
}
