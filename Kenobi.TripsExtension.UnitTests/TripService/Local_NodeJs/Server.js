var express = require('express');
    trips = require('./routes/trips');
	
var app = express();

app.get('/trips/:tripId', trips.GetTrip);
app.get('/trips/:tripId/passengers', trips.GetTripPassengers);
app.get('/trips/:tripId/transactions', trips.GetTripTransactions);
app.get('/trips/:tripId/vouchers', trips.GetTripvouchers);
app.get('/trips/:tripId/notes', trips.GetTripNotes);
app.get('/trips/:tripId/hotels', trips.GetTripHotels);
app.get('/trips/:tripId/hotels/:bookingId', trips.GetTripHotelByBookingId);
app.get('/trips/:tripId/hotels/:bookingId/passengers', trips.GetHotelBookingPassengers);
app.get('/trips/:tripId/hotels/:bookingId/fares', trips.GetHotelBookingFares);
app.get('/trips/:tripId/hotels/:bookingId/rates', trips.GetHotelBookingRate);
app.get('/trips/:tripId/hotels/:bookingId/vouchers', trips.GetHotelBookingVoucher);
app.get('/trips/:tripId/hotels/:bookingId/searchquery', trips.GetHotelSearchQuery);
app.get('/trips/:tripId/hotels/:bookingId/transactions', trips.GetHotelBookingTransaction);
app.get('/trips/:tripId/hotels/:bookingId/hotel', trips.GetBookingHotelDetails);




app.listen(5000);
console.log('Listening on port 5000...');