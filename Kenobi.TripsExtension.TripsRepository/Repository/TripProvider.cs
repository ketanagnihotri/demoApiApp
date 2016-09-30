using System.Collections.Generic;
using Kenobi.TripsExtension.TripsRepository.Interface;
using Kenobi.TripsExtension.TripsRepository.Parsers;
using Kenobi.TripsExtension.TripsRepository.Util;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;
using System.Linq;
using Kenobi.Common.Context;
using Kenobi.TripsExtension.TripsRepository.Model;
using System;

namespace Kenobi.TripsExtension.TripsRepository.Repository
{
    public class TripProvider
    {
        private readonly ITripRepository _tripRepository;
        private readonly string _tenantId;
        private readonly ConfigurationHelper _configHelper;
        public TripProvider(string tenantId)
        {
            _tenantId = tenantId;
            _configHelper = new ConfigurationHelper(tenantId);
            _tripRepository = new TripsRepository(tenantId);
        }

        public proxy.TripFolder RetrieveTripFolderByPlatformTripId(string tripId)
        {
            var modelTrip = _tripRepository.GetTrip(tripId).Result;
            var lstModelPassengers = _tripRepository.GetTripPassengers(tripId).Result;
            var lstHotelBookings = _tripRepository.GetTripHotels(tripId)?.Result;
            var lsttripProducts = new List<proxy.TripProduct>();

            var tripFolder = BasicTripFolderParser.Parse(modelTrip, _tenantId);
            tripFolder.Passengers = PassengerParser.Parse(lstModelPassengers);
            // required to generate payments
            var dictProductRphBookingidMapping = new Dictionary<int, string>(); //bookingId and productRph mapping

            if (lstHotelBookings != null)
            {
                foreach (var hotelBooking in lstHotelBookings)
                {
                    var supplierCard = GetAgencyCardForSupplier(tripId, hotelBooking);
                    var leadPaxRph = GetLeadPaxRph(lstModelPassengers, hotelBooking);
                    var modelHotelDetails = _tripRepository.GetHotelBookingHotelDetails(tripId, hotelBooking.Id).Result;
                    var searchQuery = _tripRepository.GetHotelBookingSearchQuery(tripId, hotelBooking.Id)?.Result;
                    var modelBookingRate = _tripRepository.GetHotelBookingRate(tripId, hotelBooking.Id).Result;
                    //var bookingPasssengers = _tripRepository.GetHotelBookingPassengers(tripId, hotelBooking.Id).Result; //TODO Uncomment it
                    var vouchers = _tripRepository.GetHotelBookingVouchers(tripId, hotelBooking.Id).Result;
                    var agencyFare = _tripRepository.GetHotelAgencyFare(tripId, hotelBooking.Id).Result;

                    var productRph = 0;

                    if (modelBookingRate.RoomRates != null && modelBookingRate.RoomRates.Count > 0)
                    {
                        foreach (var roomRate in modelBookingRate.RoomRates)
                        {
                            if (roomRate.Room != null)
                            {
                                var hotelProduct = TripHotelProductParser.Parser(hotelBooking, modelHotelDetails,
                                    roomRate, searchQuery, leadPaxRph, productRph, supplierCard);
                                hotelProduct.HotelItinerary.Rooms = new List<proxy.Room>
                                {
                                    RoomParser_AgencyFare.Parse(roomRate, roomRate.Room, agencyFare)
                                };
                                dictProductRphBookingidMapping.Add(productRph, hotelBooking.Id);
                                var rateVoucher = vouchers.Find(voucher => voucher.Id == roomRate.VoucherIds?.First());
                                hotelProduct.PassengerSegments = new List<proxy.PassengerSegment>();
                                var passengerSegmentRph = 0;
                                foreach (var passenger in tripFolder.Passengers)
                                {
                                    hotelProduct.PassengerSegments.Add(PassengerSegmentParser.Parser(hotelBooking,
                                        rateVoucher, passenger.Rph, passengerSegmentRph));
                                    passengerSegmentRph++;
                                }

                                //TODO: Need to pass agency fare to OUC
                                AddProductAttributes(hotelProduct, modelTrip, supplierCard, roomRate.DisplayFare);
                                SetAdditionalInfoForOrders(hotelProduct, agencyFare, roomRate);
                                lsttripProducts.Add(hotelProduct);
                                productRph++;
                            }
                        }
                    }

                    if (modelBookingRate.BookingRates != null && modelBookingRate.BookingRates.Count > 0)
                    {
                        foreach (var bookingRate in modelBookingRate.BookingRates)
                        {
                            var hotelProduct = TripHotelProductParser.Parser(hotelBooking, modelHotelDetails,
                                bookingRate, searchQuery, leadPaxRph, productRph, supplierCard);
                            hotelProduct.HotelItinerary.Rooms =
                                bookingRate.Rooms.ConvertAll(
                                    room => RoomParser_AgencyFare.Parse(bookingRate, room, agencyFare));

                            dictProductRphBookingidMapping.Add(productRph, hotelBooking.Id);
                            hotelProduct.PassengerSegments = new List<proxy.PassengerSegment>();
                            var rateVoucher = vouchers.Find(voucher => voucher.Id == bookingRate.VoucherIds?.First());
                            var passengerSegmentRph = 0;
                            foreach (var passenger in tripFolder.Passengers)
                            {
                                hotelProduct.PassengerSegments.Add(PassengerSegmentParser.Parser(hotelBooking,
                                    rateVoucher, passenger.Rph, passengerSegmentRph));
                                passengerSegmentRph++;
                                //Todo: Contract is updated we will get voucherid in rate
                            }

                            //TODO: Need to pass agency fare to OUC
                            AddProductAttributes(hotelProduct, modelTrip, supplierCard, bookingRate.DisplayFare);

                            SetAdditionalInfoForOrders(hotelProduct, agencyFare, bookingRate);
                            lsttripProducts.Add(hotelProduct);
                            productRph++;
                        }
                    }
                }
            }
            tripFolder.Products = lsttripProducts;

            SetTripFolderPayments(tripId, tripFolder, dictProductRphBookingidMapping);

            SetTripFolderCreator(tripFolder);

            return tripFolder;
        }

        private void SetTripFolderPayments(string tripId, proxy.TripFolder tripFolder, Dictionary<int, string> dictProductRphBookingidMapping)
        {
            var transactions = _tripRepository.GetTripTransactions(tripId)?.Result;
            var tripVouchers = _tripRepository.GetTripVouchers(tripId)?.Result;
            if (transactions != null && tripVouchers != null)
            {
                tripFolder.Payments = PaymentParser.Parse(transactions, tripVouchers, dictProductRphBookingidMapping);
            }
        }

        private SupplierCard GetAgencyCardForSupplier(string tripId, Hotel hotelBooking)
        {
            SupplierCard supplierCard = _tripRepository.GetSupplierCards(tripId, hotelBooking.Id).Result;
            return UpdateSupplierCard(supplierCard);
        }

        private static int GetLeadPaxRph(List<Passenger> lstModelPassengers, Hotel hotelBooking)
        {
            return lstModelPassengers.FindIndex(
                pax => pax.Id.TrimStart('0') == hotelBooking.LeadPaxId.TrimStart('0'));
        }


        private static void AddProductAttributes(proxy.HotelTripProduct hotelProduct, Trip modelTrip, SupplierCard supplierCard,
            DisplayFare displayFare)
        {
            if (hotelProduct.Attributes == null)
                hotelProduct.Attributes = new List<proxy.StateBag>();

            hotelProduct.Attributes.Add(new proxy.StateBag { Name = Constants.Default.PlatformTripId, Value = modelTrip.Id });
            hotelProduct.Attributes.Add(new proxy.StateBag { Name = Constants.Default.PlatformCorrelationId, Value = CallContext.Current.CorrelationId });

            if (!string.IsNullOrEmpty(supplierCard?.Number))
            {
                hotelProduct.Attributes.Add(new proxy.StateBag
                {
                    Name = Constants.Default.OneUseCardNumber,
                    Value = supplierCard?.Number
                });
                hotelProduct.Attributes.Add(new proxy.StateBag
                {
                    Name = Constants.Default.OneUseCardAmount,
                    Value = displayFare.GetFareQuoteAmount().ToString()
                });
            }
        }

        private void SetAdditionalInfoForOrders(proxy.HotelTripProduct hotelProduct, Model.AgencyFare agencyFare,
            Rate roomRate)
        {
            decimal fareQuoteAmount = roomRate != null
                ? GetFareQuoteAmount(roomRate)
                : GetFareQuoteAmount(hotelProduct);

            var additionalInfos = ConfigurationHelper.GetSupplierSideAdditionalData(
                hotelProduct.PassengerSegments.Count, fareQuoteAmount, roomRate?.DisplayFare?.Currency,
                BaseFareParser.GetExchangeRate(roomRate, agencyFare));

            if (hotelProduct.HotelItinerary.Fare == null)
                hotelProduct.HotelItinerary.Fare = new proxy.HotelFare() { SupplierSideData = additionalInfos };
            else
                hotelProduct.HotelItinerary.Fare.SupplierSideData = additionalInfos;

            hotelProduct.HotelItinerary.AdditionalInformation = additionalInfos;
            hotelProduct.HotelItinerary.Rooms.ForEach(room => room.DisplayRoomRate.SupplierSideData = additionalInfos);
        }

        private decimal GetFareQuoteAmount(Rate roomRate)
        {
            decimal fareWithoutDiscount = BaseFareParser.GetTotalDisplayFare(roomRate.DisplayFare);
            decimal totalDiscount = roomRate.DisplayFare?.Discounts.Sum(discount => discount.Amount) ?? 0;
            return fareWithoutDiscount - totalDiscount;
        }

        //private decimal GetFareQuoteAmount(Model.AgencyFare agenyFare)
        //{
        //    decimal fareQuoteAmount = 0;
        //    decimal totalMarkup = agenyFare.Markups?.Sum(markup => markup.Amount) ?? 0;
        //    decimal baseFare = agenyFare.BaseAmount;
        //    decimal totalTax = agenyFare.Taxes?.Sum(tax => tax.Amount) ?? 0;
        //    decimal totalFee = agenyFare.Fees?.Sum(fee => fee.Amount) ?? 0;
        //    decimal totalDiscount = agenyFare.Discounts?.Sum(discount => discount.Amount) ?? 0;

        //    fareQuoteAmount += baseFare;
        //    fareQuoteAmount += totalFee;
        //    fareQuoteAmount += totalTax;
        //    fareQuoteAmount += totalMarkup;
        //    fareQuoteAmount -= totalDiscount;
        //    return fareQuoteAmount;
        //}

        private decimal GetFareQuoteAmount(proxy.HotelTripProduct hotelProduct)
        {
            decimal fareQuoteAmount = 0;
            if (hotelProduct != null && hotelProduct.HotelItinerary != null && hotelProduct.HotelItinerary.Rooms != null && hotelProduct.HotelItinerary.Rooms.Count > 0
                       && hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate != null) &&
                       hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate.BaseFare != null))
            {
                fareQuoteAmount = hotelProduct.HotelItinerary.Rooms.Sum(room => room.DisplayRoomRate.BaseFare.Amount);

                decimal totaltax = 0;
                if (hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate.Taxes != null))
                    totaltax = hotelProduct.HotelItinerary.Rooms.Sum(room => room.DisplayRoomRate.Taxes.Sum(tax => tax.Amount));

                decimal totalFee = 0;
                if (hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate.Fees != null))
                    totalFee = hotelProduct.HotelItinerary.Rooms.Sum(room => room.DisplayRoomRate.Fees.Sum(tax => tax.Amount));

                decimal markup = 0;
                if (hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate.Markup != null))
                    markup = hotelProduct.HotelItinerary.Rooms.Sum(room => room.DisplayRoomRate.Markup.Amount);

                decimal discount = 0;
                if (hotelProduct.HotelItinerary.Rooms.TrueForAll(room => room.DisplayRoomRate.Discounts != null))
                    discount = hotelProduct.HotelItinerary.Rooms.Sum(room => room.DisplayRoomRate.Discounts.Sum(dis => dis.Amount));

                fareQuoteAmount += totalFee;
                fareQuoteAmount += totaltax;
                fareQuoteAmount += markup;
                fareQuoteAmount -= discount;
            }

            return fareQuoteAmount;
        }
        private void SetTripFolderCreator(proxy.TripFolder tripFolder)
        {
            if (tripFolder?.Products != null && tripFolder.Products.Count > 0)
            {
                proxy.Passenger leadPax = tripFolder.Passengers.Find(pax => pax.Rph == tripFolder.Products[0].LeadPassengerRph);

                tripFolder.Creator = new proxy.User
                {
                    FirstName = leadPax?.FirstName,
                    LastName = leadPax?.LastName,
                    Email = leadPax?.Email,
                    UserName = ConfigurationHelper.GetCreatorUserName(),
                    UserId = leadPax?.UserId ?? 0
                };

            }
        }

        public void UpdateOskiWithClassicTripId(string oskiTripId, proxy.TripFolder tripFolder)
        {
            _tripRepository.UpdateOskiWithClassicTripId(oskiTripId, tripFolder);
        }

        public SupplierCard UpdateSupplierCard(SupplierCard supplierCard)
        {
            if (supplierCard?.Number != null)
            {
                int numberOfFirstCharToMask = 6;
                int numberOfLastCharToMask = 4;

                string maskSymbol = new string('X', 6);

                if (!CheckFirstDigits(supplierCard.Number.Substring(0, numberOfFirstCharToMask)))
                {
                    int numberOfMissingDigits = numberOfFirstCharToMask - supplierCard.Number.Substring(0, numberOfFirstCharToMask).Count(char.IsDigit);
                    string stringtoReplaceMissingDigits = new string('0', numberOfMissingDigits);

                    supplierCard.Number = supplierCard.Number.Substring(0, numberOfFirstCharToMask - numberOfMissingDigits) + stringtoReplaceMissingDigits + maskSymbol + supplierCard.Number.Substring(supplierCard.Number.Length - numberOfLastCharToMask);
                }

                if (!CheckLastDigits(supplierCard.Number.Substring(supplierCard.Number.Length - numberOfLastCharToMask)))
                {
                    int numberOfMissingDigits = numberOfLastCharToMask - supplierCard.Number.Substring(supplierCard.Number.Length - numberOfLastCharToMask).Count(char.IsDigit);
                    string stringtoReplaceMissingDigits = new string('0', numberOfMissingDigits);

                    supplierCard.Number = supplierCard.Number.Substring(0, supplierCard.Number.Length - numberOfLastCharToMask) + stringtoReplaceMissingDigits + supplierCard.Number.Substring(supplierCard.Number.Length - (numberOfLastCharToMask - numberOfMissingDigits));
                }

                supplierCard.Number = supplierCard.Number.Substring(0, numberOfFirstCharToMask) + maskSymbol + supplierCard.Number.Substring(supplierCard.Number.Length - numberOfLastCharToMask);
            }
            return supplierCard;
        }

        private bool CheckFirstDigits(string firstDigitsofCard)
        {
            return IsRequiredDigitsPresent(firstDigitsofCard);
        }
        private bool CheckLastDigits(string lasstDigitsofCard)
        {
            return IsRequiredDigitsPresent(lasstDigitsofCard);
        }

        private bool IsRequiredDigitsPresent(string characterssOfCreditCard)
        {
            if (characterssOfCreditCard.All(char.IsDigit))
                return true;
            return false;
        }
    }
}
