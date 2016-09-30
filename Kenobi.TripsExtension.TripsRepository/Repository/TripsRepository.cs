using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kenobi.Common;
using Kenobi.Common.Context;
using Kenobi.TripsExtension.TripsRepository.Interface;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Util;
using Newtonsoft.Json;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Passenger = Kenobi.TripsExtension.TripsRepository.Model.Passenger;

namespace Kenobi.TripsExtension.TripsRepository.Repository
{
    public class TripsRepository : ITripRepository
    {
        string _tenantId;
        public TripsRepository(string tenantId)
        {
            _tenantId = tenantId;
        }

        public async Task<Trip> GetTrip(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsUrl, tripId), GetHeaders(_tenantId));
            var response = webClient.Get<Trip>();
            if (response == null)
                ResponseValidationError("No trip found for given tripId :" + tripId);
            return await Task.FromResult(response);
        }

        public async Task<List<Hotel>> GetTripHotels(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsHotelUrl, tripId), GetHeaders(_tenantId));
            var response = webClient.Get<List<Hotel>>();
            if (response == null)
                ResponseValidationError("No hotel found for given tripId :" + tripId);
            return await Task.FromResult(response);
        }

        public async Task<List<Note>> GetTripNotes(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsNotesUrl, tripId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<List<Note>>());
        }

        public async Task<List<Passenger>> GetTripPassengers(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsPassengerUrl, tripId), GetHeaders(_tenantId));
            var response = webClient.Get<List<Passenger>>();
            if (response == null)
                ResponseValidationError("No passengers found for given tripId :" + tripId);
            return await Task.FromResult(response);

        }

        public async Task<List<Voucher>> GetTripVouchers(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsVouchersUrl, tripId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<List<Voucher>>());
        }

        public async Task<BookingFare> GetHotelBookingFare(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.HotelBookingFaresUrl, tripId, bookingId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<BookingFare>());
        }

        public async Task<HotelDetails> GetHotelBookingHotelDetails(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.BookedHotelDetailsUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<HotelDetails>();
            if (response == null)
                ResponseValidationError("No hotel details found for given tripId :" + tripId + "and boookingId:" + bookingId);
            return await Task.FromResult(response);
        }

        public void UpdateOskiWithClassicTripId(string oskiTripId, TripFolder tripFolder)
        {
            string tripsPutUrl = string.Format(Config.TripsPutUrl, oskiTripId);
            var webClient = new HttpClient(tripsPutUrl, GetHeaders(_tenantId));
            OskiTripUpdateReq tripUpdateRequest = GetTripUpdateRequest(tripFolder, oskiTripId);
            var response = webClient.Put<OskiTripUpdateReq, OskiTripUpdateRes>(tripUpdateRequest, TypeNameHandling.Auto);
            if (response == null)
                ResponseValidationError("No trip found for given tripId :" + oskiTripId);
        }

        private OskiTripUpdateReq GetTripUpdateRequest(TripFolder tripFolder, string oskiTripId)
        {
            return new OskiTripUpdateReq()
            {
                Id = oskiTripId,
                CNX_OrderId = tripFolder?.Pos?.AdditionalInfo?.Find(
                    stateBag => stateBag.Name == "TripOrderID")?.Value,
                CNX_TripId = tripFolder?.ConfirmationNumber,
                Type = "trip",
                CorrelationId = CallContext.Current?.CorrelationId
            };
        }

        public async Task<List<Passenger>> GetHotelBookingPassengers(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);

            var webClient = new HttpClient(string.Format(Config.HotelBookingPassengersUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<List<Passenger>>();
            if (response == null)
                ResponseValidationError("No passengers found for given tripId :" + tripId);
            return await Task.FromResult(response);
        }

        public async Task<Rates> GetHotelBookingRate(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.HotelBookingRateUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<Rates>();
            if (response == null)
                ResponseValidationError("No booking rates found for given tripId :" + tripId);
            return await Task.FromResult(response);
            //return null;
        }

        public async Task<BookingSearchQuery> GetHotelBookingSearchQuery(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.HotelSearchQueryUrl, tripId, bookingId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<BookingSearchQuery>());

        }

        public async Task<List<BookingTransaction>> GetHotelBookingTransactions(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.HotelBookingTransactionUrl, tripId, bookingId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<List<BookingTransaction>>());
        }

        public async Task<List<Voucher>> GetHotelBookingVouchers(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.HotelBookingVoucherUrl, tripId, bookingId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<List<Voucher>>());
        }

        public async Task<Hotel> GetTripHotelByBookingId(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.TripsHotelByBookingIdUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<Hotel>();
            if (response == null)
                ResponseValidationError("No hotel found for given tripId :" + tripId + "and boookingId:" + bookingId);
            return await Task.FromResult(response);
        }

        private static Dictionary<string, string> GetHeaders(string tenantId)
        {
            return new Dictionary<string, string>() { { "oski-tenantId", tenantId } };
        }

        private static void ValidateParameter(string value, string errorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(errorMessage);
            }
        }

        private static void ValidateRequestParameter(string tripId, string bookingId)
        {
            ValidateParameter(tripId, "TripId is null");
            ValidateParameter(bookingId, "bookingId is null");
        }

        private static void ValidateRequestParameter(string tripId)
        {
            ValidateParameter(tripId, "TripId is null");
        }

        public async Task<List<BookingTransaction>> GetTripTransactions(string tripId)
        {
            ValidateRequestParameter(tripId);
            var webClient = new HttpClient(string.Format(Config.TripsTransactionUrl, tripId), GetHeaders(_tenantId));
            return await Task.FromResult(webClient.Get<List<BookingTransaction>>());
        }
        private static void ResponseValidationError(string errorMessage)
        {
            throw new ArgumentNullException(errorMessage);
        }

        public async Task<AgencyFare> GetHotelAgencyFare(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.AgencyFareUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<AgencyFare>();
            if (response == null)
                ResponseValidationError("No hotel found for given tripId :" + tripId + "and boookingId:" + bookingId);
            return await Task.FromResult(response);
        }

        public async Task<SupplierCard> GetSupplierCards(string tripId, string bookingId)
        {
            ValidateRequestParameter(tripId, bookingId);
            var webClient = new HttpClient(string.Format(Config.TripSupplierCardUrl, tripId, bookingId), GetHeaders(_tenantId));
            var response = webClient.Get<SupplierCard>();
            return await Task.FromResult(response);
        }
    }
}