using kenobi.TripsExtension.TestDataProvider;
using System;
using System.Configuration;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.TripService.RepositoryTest
{
    public class TripsRepositiryUnitTest
    {
        private readonly string _tripId = ConfigurationManager.AppSettings["OskiTripId"];
        private readonly string _bookingId = ConfigurationManager.AppSettings["OskiBookingId"];
        private const bool Success = true;
        readonly TripsRepository.Repository.TripsRepository _tripsRepository = new TripsRepository.Repository.TripsRepository(ConfigurationManager.AppSettings["oski-tenantId"]);

        public TripsRepositiryUnitTest()
        {
            WebhookContainer.SetLocatorWithContainer();
        }


        [Fact]
        public async void GetTrips_Valid_Successful()
        {
            var trip = await _tripsRepository.GetTrip(_tripId);
            if (trip != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTrips_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetTrip(string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetTripHotels_Valid_Successful()
        {
            var hotels = await _tripsRepository.GetTripHotels(_tripId);
            if (hotels != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripHotels_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetTripHotels(string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetTripNotes_Valid_Successful()
        {
            var notes = await _tripsRepository.GetTripNotes(_tripId);
            if (notes != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripNotes_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetTripNotes(string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetTripVouchers_Valid_Successful()
        {

            var vouchers = await _tripsRepository.GetTripVouchers(_tripId);
            if (vouchers != null)
            {
                Assert.True(Success);
            }
        }


        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripVouchers_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetTripVouchers(string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetTripPassengers_Valid_Successful()
        {

            var passengers = await _tripsRepository.GetTripPassengers(_tripId);
            if (passengers != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripPassengers_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetTripPassengers(string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetHotelBookingFare_Valid_Successful()
        {

            var bookingFare = await _tripsRepository.GetHotelBookingFare(_tripId, _bookingId);
            if (bookingFare != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingFare_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingFare(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingFare_BookingIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingFare(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetHotelBookingHotelDetails_Valid_Successful()
        {

            var hotelDetails = await _tripsRepository.GetHotelBookingHotelDetails(_tripId, _bookingId);
            if (hotelDetails != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingHotelDetails_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingHotelDetails(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }


        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingHotelDetails_BookingIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingHotelDetails(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetHotelBookingPassengers_Valid_Successful()
        {

            var passengers = await _tripsRepository.GetHotelBookingPassengers(_tripId, _bookingId);
            if (passengers != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingPassengers_TripIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingHotelDetails(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingPassengers_BookingIdMissing_Successful()
        {
            try
            {

                var trip = await _tripsRepository.GetHotelBookingHotelDetails(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }


        [Fact]
        public async void GetHotelBookingRate_Valid_Successful()
        {
            var bookingRate = await _tripsRepository.GetHotelBookingRate(_tripId, _bookingId);
            if (bookingRate != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingRate_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingRate(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingRate_BookingIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingRate(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }


        [Fact]
        public async void GetHotelBookingSearchQuery_Valid_Successful()
        {
            var bookingSearchQuery = await _tripsRepository.GetHotelBookingSearchQuery(_tripId, _bookingId);
            if (bookingSearchQuery != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingSearchQuery_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingSearchQuery(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingSearchQuery_BookingIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingSearchQuery(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetHotelBookingTransactions_Valid_Successful()
        {
            var bookingTransactions = await _tripsRepository.GetHotelBookingTransactions(_tripId, _bookingId);
            if (bookingTransactions != null)
            {
                Assert.True(Success);
            }
        }


        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingTransactions_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingTransactions(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingTransactions_BookingIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingTransactions(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetHotelBookingVouchers_Valid_Successful()
        {
            var vouchers = await _tripsRepository.GetHotelBookingVouchers(_tripId, _bookingId);
            if (vouchers != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingVouchers_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingVouchers(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetHotelBookingVouchers_BookingIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetHotelBookingVouchers(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        public async void GetTripHotelByBookingId_Valid_Successful()
        {
            var hotel = await _tripsRepository.GetTripHotelByBookingId(_tripId, _bookingId);
            if (hotel != null)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripHotelByBookingId_TripIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetTripHotelByBookingId(string.Empty, _bookingId);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

        [Fact]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(ArgumentNullException))]
        public async void GetTripHotelByBookingId_BookingIdMissing_Successful()
        {
            try
            {
                var trip = await _tripsRepository.GetTripHotelByBookingId(_tripId, string.Empty);
            }
            catch (ArgumentNullException)
            {
                Assert.True(Success);
            }
        }

    }
}
