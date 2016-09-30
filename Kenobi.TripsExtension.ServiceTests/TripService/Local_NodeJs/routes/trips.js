
exports.GetTrip = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var trip = {
            "id": "1",
            "name": "MyTripToParis",
            "ownerId": "201",
            "creatorId": "501",
            "posId": "51",
            "orgId": "20058",
            "type": "corporate",
            "status": "confirmed",
            "createdOn": "1990-07-16T19:20:30.45+01:00",
            "modifiedOn": "1990-07-16T19:20:30.45+01:00"
        };
        res.send(trip);
    }
};




exports.GetTripPassengers = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var tripPassengers = [{
            "id": "p001",
            "type": "adult",
            "name": {
                "title": "mr",
                "first": "FirstName",
                "middle": "MiddleName",
                "last": "LastName",
                "suffix": "Jr"
            },
            "dob": "1989-12-12",
            "gender": "male",
            "contactInfo": {
                "phoneNos": [{
                    "type": "mobile",
                    "no": "9087654321",
                    "countryCode": "91",
                    "ext": "123",
                    "areaCode": "022"
                }],
                "address": {
                    "line1": "AddressLine1",
                    "line2": "AddressLine2",
                    "city": {
                        "code": "PNQ",
                        "name": "Pune"
                    },
                    "state": {
                        "code": "MH",
                        "name": "Maharashtra"
                    },
                    "countryCode": "IN",
                    "zipCode": "411014"
                },
                "emails": ["cltsloyalty@noreply.com"]
            },
            "passport": {
                "no": "JCF233J",
                "placeOfIssue": "Mumbai",
                "dateOfIssue": "2012-12-12",
                "expiryDate": "2022-12-12",
                "nationality": "IN"
            },
            "memberships": {
                "hotelLoyalties": [{
                    "chainCode": "HI",
                    "no": "123"
                }]
            }
        }, {
            "id": "p021",
            "type": "adult",
            "name": {
                "title": "mrs",
                "first": "FirstName",
                "middle": "MiddleName",
                "last": "LastName",
                "suffix": "Jr"
            },
            "dob": "1989-12-12",
            "gender": "female",
            "contactInfo": {
                "phoneNos": [{
                    "type": "mobile",
                    "no": "9087654341",
                    "countryCode": "91",
                    "ext": "123",
                    "areaCode": "022"
                }],
                "address": {
                    "line1": "AddressLine1",
                    "line2": "AddressLine2",
                    "city": {
                        "code": "PNQ",
                        "name": "Pune"
                    },
                    "state": {
                        "code": "MH",
                        "name": "Maharashtra"
                    },
                    "countryCode": "IN",
                    "zipCode": "411014"
                },
                "emails": ["abcdef@xyz.com", "pqrs@xyz.com"]
            },
            "passport": {
                "no": "JCF255J",
                "placeOfIssue": "Mumbai",
                "dateOfIssue": "2012-12-13",
                "expiryDate": "2022-12-13",
                "nationality": "IN"
            },
            "memberships": {
                "hotelLoyalties": [{
                    "chainCode": "Bye",
                    "no": "12345"
                }]
            }
        }, {
            "id": "p023",
            "type": "senior",
            "name": {
                "title": "mr",
                "first": "FirstName",
                "middle": "MiddleName",
                "last": "LastName",
                "suffix": "Jr"
            },
            "dob": "1959-12-12",
            "gender": "male",
            "contactInfo": {
                "phoneNos": [{
                    "type": "mobile",
                    "no": "9087654331",
                    "countryCode": "91",
                    "ext": "123",
                    "areaCode": "022"
                }],
                "address": {
                    "line1": "AddressLine1",
                    "line2": "AddressLine2",
                    "city": {
                        "code": "PNQ",
                        "name": "Pune"
                    },
                    "state": {
                        "code": "MH",
                        "name": "Maharashtra"
                    },
                    "countryCode": "IN",
                    "zipCode": "411014"
                },
                "emails": ["zaq1@xyz.com"]
            },
            "passport": {
                "no": "JCF288J",
                "placeOfIssue": "Mumbai",
                "dateOfIssue": "1996-12-13",
                "expiryDate": "2020-12-13",
                "nationality": "IN"
            },
            "memberships": {
                "hotelLoyalties": [{
                    "chainCode": "Bye",
                    "no": "12345"
                }]
            }
        }


        ];
        res.send(tripPassengers);
    }
};

exports.GetTripTransactions = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var tripTransactions = [{
            "card": {
                "number": "####4444",
                "issuedBy": "VISA"
            },
            "amount": 100,
            "currency": "USD",
            "status": "Settled",
            "transactionId": "6b36c65d-88e5-4651-b48f-0c6f543e1691",
            "providerTransactionId": "0a5c4abb-2835-4ecf-82b9-ef41d83af2c9",
            "desc": "Mock Transaction",
            "paymentBreakup": [{
                "amount": 10,
                "voucherId": "1",
                "bookingId": "111"
            }, {
                "amount": 90,
                "voucherId": "2",
                "bookingId": "111"
            }]
        }, {
            "card": {
                "number": "####1111",
                "issuedBy": "VISA"
            },
            "amount": 100,
            "currency": "USD",
            "status": "Settled",
            "transactionId": "6f7498ce-528a-4e17-9e30-1b3108f39356",
            "providerTransactionId": "c5c05d45-c015-42f8-9f82-ca89f36ec099",
            "desc": "Mock Transaction",
            "paymentBreakup": [{
                "amount": 10,
                "voucherId": "1",
                "bookingId": "111"
            }, {
                "amount": 90,
                "voucherId": "2",
                "bookingId": "111"
            }]
        }];
        res.send(tripTransactions);
    }
};

exports.GetTripvouchers = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var tripvouchers = [{
            "number": "v001",
            "supplierConfirmation": "85621",
            "vendorConfirmation": "65412",
            "supplierCancellationNumber": "",
            "vendorCancelationNumber": "",
            "issuedBy": "HotelBed",
            "issuedOn": "2016-01-07T01:03:54.8079484+05:30",
            "status": "Pending"
        }];
        res.send(tripvouchers);
    }
};

exports.GetTripNotes = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var tripNotes = [{
            "id": "N001",
            "title": "Trip Note",
            "description": "This is mock note",
            "type": "Test Type",
            "creatorId": "HotelBeds",
            "createdDate": "10-10-2016"
        }];
        res.send(tripNotes);
    }
};

exports.GetTripHotels = function (req, res) {
    var id = req.params.tripId;
    console.log('Received tripId as: ' + id);
    if (id == '1') {
        var tripHotels = [
  {
      "id": "111",
      "posId": "51",
      "status": "Confirmed",
      "supplierId": "100",
      "hotelId": "Hotel_001",
      "hotelName": "Novotel",
      "hotelAddress": {
          "line1": "AddressLine1",
          "line2": "AddressLine2",
          "city": {
              "code": "PNQ",
              "name": "Pune"
          },
          "state": {
              "code": "MH",
              "name": "Maharashtra"
          },
          "countryCode": "IN",
          "zipCode": "411014"
      },
      "hotelGeoCode": {
          "lat": 13.43,
          "long": 12.45
      },
      "stayPeriod": {
          "end": "2015-12-19",
          "start": "2015-12-15"
      },
      "progress": "string",
      "paymentStatus": "Pending",
      "leadPaxId": "p001",
      "sessionId": "5ccc1e3e-48a8-49f7-b615-3bf4712d99ef",
      "supplierSessionId": "8f9b27b3-9963-42f7-bd94-5900be7ee048"
  },

        ];
        res.send(tripHotels);
    }
};

exports.GetTripHotelByBookingId = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var tripHotelByBookingId = {
            "id": "111",
            "posId": "51",
            "status": "Confirmed",
            "supplierId": "100",
            "hotelId": "Hotel_001",
            "hotelName": "Novotel",
            "hotelAddress": {
                "line1": "AddressLine1",
                "line2": "AddressLine2",
                "city": {
                    "code": "PNQ",
                    "name": "Pune"
                },
                "state": {
                    "code": "MH",
                    "name": "Maharashtra"
                },
                "countryCode": "IN",
                "zipCode": "411014"
            },
            "hotelGeoCode": {
                "lat": 13.43,
                "long": 12.45
            },
            "stayPeriod": {
                "end": "2015-12-19",
                "start": "2015-12-15"
            },
            "progress": "string",
            "paymentStatus": "Pending",
            "leadPaxId": "p001",
            "sessionId": "5ccc1e3e-48a8-49f7-b615-3bf4712d99ef",
            "supplierSessionId": "8f9b27b3-9963-42f7-bd94-5900be7ee048"
        };
        res.send(tripHotelByBookingId);
    }
};

exports.GetHotelBookingPassengers = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var tripHotelBookingPassengers = [
  {
      "id": "p001",
      "type": "adult",
      "name": {
          "title": "mr",
          "first": "FirstName",
          "middle": "MiddleName",
          "last": "LastName",
          "suffix": "Jr"
      },
      "dob": "1989-12-12",
      "gender": "male",
      "contactInfo": {
          "phoneNos": [
            {
                "type": "mobile",
                "no": "9087654321",
                "countryCode": "91",
                "ext": "123",
                "areaCode": "022"
            }
          ],
          "address": {
              "line1": "AddressLine1",
              "line2": "AddressLine2",
              "city": {
                  "code": "PNQ",
                  "name": "Pune"
              },
              "state": {
                  "code": "MH",
                  "name": "Maharashtra"
              },
              "countryCode": "IN",
              "zipCode": "411014"
          },
          "emails": ["cltsloyalty@noreply.com"]
      },
      "passport": {
          "no": "JCF233J",
          "placeOfIssue": "Mumbai",
          "dateOfIssue": "2012-12-12",
          "expiryDate": "2022-12-12",
          "nationality": "IN"
      },
      "memberships": {
          "hotelLoyalties": [
            {
                "chainCode": "HI",
                "no": "123"
            }
          ]
      }
  },

  {
      "id": "p021",
      "type": "adult",
      "name": {
          "title": "mrs",
          "first": "FirstName",
          "middle": "MiddleName",
          "last": "LastName",
          "suffix": "Jr"
      },
      "dob": "1989-12-12",
      "gender": "female",
      "contactInfo": {
          "phoneNos": [{
              "type": "mobile",
              "no": "9087654341",
              "countryCode": "91",
              "ext": "123",
              "areaCode": "022"
          }],
          "address": {
              "line1": "AddressLine1",
              "line2": "AddressLine2",
              "city": {
                  "code": "PNQ",
                  "name": "Pune"
              },
              "state": {
                  "code": "MH",
                  "name": "Maharashtra"
              },
              "countryCode": "IN",
              "zipCode": "411014"
          },
          "emails": ["abcdef@xyz.com", "pqrs@xyz.com"]
      },
      "passport": {
          "no": "JCF255J",
          "placeOfIssue": "Mumbai",
          "dateOfIssue": "2012-12-13",
          "expiryDate": "2022-12-13",
          "nationality": "IN"
      },
      "memberships": {
          "hotelLoyalties": [{
              "chainCode": "Bye",
              "no": "12345"
          }]
      }
  }
        ];

        res.send(tripHotelBookingPassengers);
    }
};

exports.GetHotelBookingFares = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelBookingFares = {
            "perRoomFares": [{
                "id": "1",
                "supplierId": "100",
                "supplierHotelId": "1233",
                "groupId": "12",
                "refundability": "nonRefundable",
                "onlineCancellable": false,
                "perRoomCancellationPossible": true,
                "onRequest": false,
                "voucherId": "1",
                "inclusions": ["Sample1, sample2"],
                "room": {
                    "name": "string",
                    "description": "string",
                    "type": "string",
                    "maxOccupancy": 1,
                    "smokingPreference": "string",
                    "roomViews": ["Sample1, sample2"],
                    "id": "string",
                    "rateRoomOccupancy": {
                        "adultCount": 1,
                        "childCount": 0
                    },
                    "bedDetails": [{
                        "type": "string",
                        "count": 1,
                        "description": "string"
                    }]
                },
                "displayFare": {
                    "currency": "USD",
                    "baseAmount": 190,
                    "fees": [{
                        "amount": 10,
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": 10,
                        "desc": "",
                        "source": "fareComponentSource"
                    }],
                    "taxes": [{
                        "amount": 10,
                        "desc": "",
                        "code": "121"
                    }]
                },
                "policies": [{
                    "type": "POLICY_PREPAY",
                    "text": "The total price of the reservation may be charged anytime after booking."
                }],
                "boardBasis": {
                    "code": "",
                    "desc": "",
                    "type": "",
                    "price": "10 USD",
                    "amountIncludedInRate": true
                },
                "cancellationPolicy": {
                    "text": "",
                    "penaltyRules": [{
                        "value": 12.34,
                        "valueType": "Amount/Percent/Nights",
                        "window": {
                            "start": "2016-01-07T01:03:54.8079484+05:30",
                            "end": "2016-01-27T01:03:54.8079484+05:30"
                        }
                    }]
                }
            }],
            "perBookingFares": [{
                "id": "1",
                "supplierId": "100",
                "supplierHotelId": "1233",
                "groupId": "12",
                "refundability": "nonRefundable",
                "onlineCancellable": false,
                "perRoomCancellationPossible": true,
                "onRequest": false,
                "voucherId": "1",
                "inclusions": ["Sample1, sample2"],
                "rooms": [{
                    "name": "string",
                    "description": "string",
                    "type": "string",
                    "maxOccupancy": 1,
                    "smokingPreference": "string",
                    "roomViews": ["Sample1, sample2"],
                    "id": "string",
                    "rateRoomOccupancy": {
                        "adultCount": 1,
                        "childCount": 0
                    },
                    "bedDetails": [{
                        "type": "string",
                        "count": 1,
                        "description": "string"
                    }]
                }],
                "displayFare": {
                    "currency": "USD",
                    "baseAmount": 190,
                    "fees": [{
                        "amount": 10,
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": 10,
                        "desc": "",
                        "source": "fareComponentSource"
                    }],
                    "taxes": [{
                        "amount": 10,
                        "desc": "",
                        "code": "121"
                    }]
                },
                "policies": [{
                    "type": "POLICY_PREPAY",
                    "text": "The total price of the reservation may be charged anytime after booking."
                }],
                "boardBasis": {
                    "code": "",
                    "desc": "",
                    "type": "",
                    "price": "10 USD",
                    "amountIncludedInRate": true
                },
                "cancellationPolicy": {
                    "text": "",
                    "penaltyRules": [{
                        "value": 12.34,
                        "valueType": "Amount/Percent/Nights",
                        "window": {
                            "start": "2016-01-07T01:03:54.8079484+05:30",
                            "end": "2016-01-27T01:03:54.8079484+05:30"
                        }
                    }]
                }
            }]
        };
        res.send(hotelBookingFares);
    }
};


exports.GetHotelBookingRate = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelBookingRate = {
            "perRoomRates": [{
                "id": "1",
                "supplierId": "100",
                "supplierHotelId": "1233",
                "groupId": "12",
                "voucherId": "v001",
                "code": "MER",
                "refundability": "nonRefundable",
                "depositRequired": false,
                "guaranteeRequired": false,
                "allGuestInfoRequired": true,
                "roomType": "string",
                "additionalCharges": ["Sample, Sample2"],
                "onlineCancellable": false,
                "perRoomCancellationPossible": true,
                "onRequest": false,
                "inclusions": ["Sample, Sample2"],
                "allowedCreditCards": ["Sample, Sample2"],
                "room": {
                    "name": "string",
                    "description": "string",
                    "code": "string",
                    "roomTypeCode": "string",
                    "type": "string",
                    "availableRoomCount": "10",
                    "maxOccupancy": "1",
                    "smokingPreference": "string",
                    "roomViews": ["Sample, Sample2"],
                    "id": "string",
                    "rateRoomOccupancy": {
                        "refId": "string",
                        "adultCount": "1",
                        "childCount": "0"
                    },
                    "bedDetails": [{
                        "type": "string",
                        "count": "1",
                        "description": "string"
                    }]
                },
                "displayFare": {
                    "currency": "USD",
                    "baseAmount": "190",
                    "totalMarkup": "10",
                    "fees": [{
                        "amount": "10",
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": "10",
                        "desc": "",
                        "source": "rateComponentSource"
                    }],
                    "taxes": [{
                        "amount": "10",
                        "desc": "",
                        "code": "121"
                    }]
                },
                "rateBreakup": {
                    "baseFare": "190 USD",
                    "fees": [{
                        "amount": "10 USD",
                        "desc": ""
                    }],
                    "markups": [{
                        "amount": "10 USD",
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": "10 USD",
                        "desc": "",
                        "source": ""
                    }],
                    "taxes": [{
                        "amount": "10 USD",
                        "desc": "",
                        "code": "121"
                    }]
                },
                "supplierDailyRates": {
                    "taxIncluded": true,
                    "dailyRates": [{
                        "date": "2015-12-10",
                        "amount": "24.55",
                        "discount": "10"
                    }]
                },
                "supplierDailyFares": {
                    "taxIncluded": true,
                    "dailyFares": [{
                        "date": "2015-12-10",
                        "amount": "24.55",
                        "discount": "10"
                    }]
                },
                "rackRate": "100 USD",
                "policies": [{
                    "type": "POLICY_PREPAY",
                    "text": "The total price of the reservation may be charged anytime after booking."
                }],
                "boardBasis": {
                    "code": "",
                    "desc": "",
                    "type": "",
                    "price": "10 USD",
                    "amountIncludedInRate": true
                },

                "offer": {
                    "title": "Amazing Offer",
                    "desc": "",
                    "fixedDiscountOffer": "25 USD",
                    "percentageDiscountOffer": {
                        "amount": "10.0",
                        "appliedOn": "baseRate/totalRate"
                    },
                    "stayOffer": {
                        "stayNights": "5",
                        "freeNights": "2"
                    }
                },
                "cancellationPolicy": {
                    "text": "",
                    "penaltyRules": [{
                        "value": "12.34",
                        "valueType": "Amount/Percent/Nights",
                        "window": {
                            "start": "2016-01-07T01:03:54.8079484+05:30",
                            "end": "2016-01-27T01:03:54.8079484+05:30"
                        }
                    }]
                },
                "supplierCommissions": [{
                    "amount": "0",
                    "desc": "sample Commission"
                }],
                "packageSaving": "10.00"
            }],
            "perBookingRates": [{
                "id": "1",
                "desc": "Rate description",
                "isPrepaid": false,
                "voucherId": "v001",
                "type": "published/negotiated",
                "supplierId": "100",
                "supplierHotelId": "1233",
                "groupId": "12",
                "code": "MER",
                "refundability": "nonRefundable",
                "depositRequired": false,
                "guaranteeRequired": false,
                "allGuestInfoRequired": true,
                "roomType": "string",
                "additionalCharges": ["Sample, Sample2"],
                "onlineCancellable": false,
                "perRoomCancellationPossible": true,
                "onRequest": false,
                "inclusions": ["Sample, Sample2"],
                "allowedCreditCards": ["Sample, Sample2"],
                "rooms": [{
                    "name": "string",
                    "description": "string",
                    "code": "string",
                    "roomTypeCode": "string",
                    "type": "string",
                    "availableRoomCount": "10",
                    "maxOccupancy": "1",
                    "smokingPreference": "string",
                    "roomViews": ["Sample, Sample2"],
                    "id": "string",
                    "rateRoomOccupancy": {
                        "refId": "string",
                        "adultCount": "1",
                        "childCount": "0"
                    },
                    "bedDetails": [{
                        "type": "string",
                        "count": "1",
                        "description": "string"
                    }]
                }],
                "displayFare": {
                    "currency": "USD",
                    "baseAmount": "190",
                    "totalMarkup": "10",
                    "fees": [{
                        "amount": "10",
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": "10",
                        "desc": "",
                        "source": "rateComponentSource"
                    }],
                    "taxes": [{
                        "amount": "10",
                        "desc": "",
                        "code": "121"
                    }]
                },
                "rateBreakup": {
                    "baseFare": "190 USD",
                    "fees": [{
                        "amount": "10 USD",
                        "desc": ""
                    }],
                    "markups": [{
                        "amount": "10 USD",
                        "desc": ""
                    }],
                    "discounts": [{
                        "amount": "10 USD",
                        "desc": "",
                        "source": ""
                    }],
                    "taxes": [{
                        "amount": "10 USD",
                        "desc": "",
                        "code": "121"
                    }]
                },
                "supplierDailyRates": {
                    "taxIncluded": true,
                    "dailyRates": [{
                        "date": "2015-12-10",
                        "amount": "24.55",
                        "discount": "10"
                    }]
                },
                "supplierDailyFares": {
                    "taxIncluded": true,
                    "dailyFares": [{
                        "date": "2015-12-10",
                        "amount": "24.55",
                        "discount": "10"
                    }]
                },
                "rackRate": "100 USD",
                "policies": [{
                    "type": "POLICY_PREPAY",
                    "text": "The total price of the reservation may be charged anytime after booking."
                }],
                "boardBasis": {
                    "code": "",
                    "desc": "",
                    "type": "",
                    "price": "10 USD",
                    "amountIncludedInRate": true
                },

                "offer": {
                    "title": "Amazing Offer",
                    "desc": "",
                    "fixedDiscountOffer": "25 USD",
                    "percentageDiscountOffer": {
                        "amount": "10.0",
                        "appliedOn": "baseRate/totalRate"
                    },
                    "stayOffer": {
                        "stayNights": "5",
                        "freeNights": "2"
                    }
                },
                "cancellationPolicy": {
                    "text": "",
                    "penaltyRules": [{
                        "value": "12.34",
                        "valueType": "Amount/Percent/Nights",
                        "window": {
                            "start": "2016-01-07T01:03:54.8079484+05:30",
                            "end": "2016-01-27T01:03:54.8079484+05:30"
                        }
                    }]
                },
                "supplierCommissions": [{
                    "amount": "0",
                    "desc": "sample Commission"
                }],
                "packageSaving": "10.00 USD"
            }]
        };
        res.send(hotelBookingRate);
    }
};

exports.GetHotelBookingVoucher = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelBookingVoucher = [{
            "number": "v001",
            "supplierConfirmation": "85621",
            "vendorConfirmation": "65412",
            "supplierCancellationNumber": "",
            "vendorCancelationNumber": "",
            "issuedBy": "HotelBed",
            "issuedOn": "2016-01-07T01:03:54.8079484+05:30",
            "status": "Pending"
        }];
        res.send(hotelBookingVoucher);
    }
};

exports.GetHotelSearchQuery = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelSearchQuery = {
            "bounds": {
                "circle": {
                    "center": {
                        "lat": "1.23",
                        "long": "5.67"
                    },
                    "radiusKm": "10"
                },
                "rectangle": {
                    "topLeft": {
                        "lat": "1.23",
                        "long": "5.67"
                    },
                    "bottomRight": {
                        "lat": "1.23",
                        "long": "5.67"
                    }
                }
            },
            "culture": "string",
            "currency": "USD",
            "filters": {
                "minHotelPrice": "100.23",
                "maxHotelPrice": "123.23",
                "minHotelRating": "1",
                "maxHotelRating": "1",
                "hotelChains": ["Sample hotel chain"]
            },
            "includeHotelsWithoutRates": false,
            "posId": "51",
            "roomOccupancies": [
              {
                  "occupants": [
                    {
                        "type": "adult",
                        "age": "25"
                    },
                    {
                        "type": "adult",
                        "age": "30"
                    },
                    {
                        "type": "child",
                        "age": "10"
                    }
                  ]
              }
            ],
            "stayPeriod": {
                "end": "2015-12-19",
                "start": "2015-12-15"
            },
            "travellerCountryCodeOfResidence": "IN",
            "travellerNationalityCode": "US"
        };
        res.send(hotelSearchQuery);
    }
};

exports.GetHotelBookingTransaction = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelBookingTransaction = [{
            "card": {
                "number": "####4444",
                "issuedBy": "VISA"
            },
            "amount": "100",
            "currency": "USD",
            "status": "Settled",
            "transactionId": "6b36c65d-88e5-4651-b48f-0c6f543e1691",
            "providerTransactionId": "0a5c4abb-2835-4ecf-82b9-ef41d83af2c9",
            "desc": "Mock Transaction",
            "paymentBreakup": [{
                "amount": "10",
                "voucherId": "1",
                "bookingId": "111"
            }, {
                "amount": "90",
                "voucherId": "2",
                "bookingId": "111"
            }]
        }, {
            "card": {
                "number": "####1111",
                "issuedBy": "VISA"
            },
            "amount": "100",
            "currency": "USD",
            "status": "Settled",
            "transactionId": "6f7498ce-528a-4e17-9e30-1b3108f39356",
            "providerTransactionId": "c5c05d45-c015-42f8-9f82-ca89f36ec099",
            "desc": "Mock Transaction",
            "paymentBreakup": [{
                "amount": "10",
                "voucherId": "1",
                "bookingId": "111"
            }, {
                "amount": "90",
                "voucherId": "2",
                "bookingId": "111"
            }]
        }];
        res.send(hotelBookingTransaction);
    }
};

exports.GetBookingHotelDetails = function (req, res) {
    var id = req.params.tripId;
    var bookingId = req.params.bookingId;
    console.log('Received tripId as: ' + id);
    console.log('Received bookingId as: ' + bookingId);
    if (id == '1' && bookingId == '111') {
        var hotelBookingHotelDetails = {
            "activities": [
              {
                  "name": "Outdoor tennis courts",
                  "desc": "sampleDescription",
                  "category": "sampleGroupName"
              }
            ],
            "amenities": [
              {
                  "name": "Meeting Rooms",
                  "desc": "sampleDescription",
                  "category": "sampleGroupName"
              }
            ],
            "areaAttractions": [
              {
                  "name": "Casino",
                  "desc": "sampleDescription"
              }
            ],
            "checkinCheckoutPolicy": [
              {
                  "inTime": "15:00",
                  "outTime": "11:00",
                  "days": ["sun", "mon", "tue", "wed", "thu"]

              },
              {
                  "inTime": "14:00",
                  "outTime": "10:00",
                  "days": ["fri", "sat"]
              }
            ],
            "contact": {
                "address": {
                    "line1": "AddressLine1",
                    "line2": "AddressLine2",
                    "city": {
                        "code": "SFO",
                        "name": "San Francisco"
                    },
                    "state": {
                        "code": "CA",
                        "name": "California"
                    },
                    "countryCode": "US",
                    "postalCode": "94133"
                },
                "phones": [
                  {
                      "type": "unknown/work/home/mobile/fax",
                      "num": "9087654321",
                      "countryCode": "91",
                      "ext": "123466534343",
                      "areaCode": "022"
                  }
                ],
                "email": "cltsloyalty@noreply.com"
            },
            "contentSupplierFamily": "supplier family name",
            "descriptions": [
              {
                  "type": "sample type",
                  "value": "sample value"
              }
            ],
            "geoCode": {
                "lat": "38.5365798628134",
                "long": "-0.132263749837875"
            },
            "hotelChain": {
                "name": "Fletcher Hotels",
                "uri": ""
            },
            "hotelCurrencyCode": "USD",
            "id": "4074",
            "images": [
              {
                  "height": "250",
                  "imageCaption": "Guestroom",
                  "url": "http://d3mj096p5q0e20.cloudfront.net/fi/HCM/457757/7251474_2_b.jpg",
                  "width": "150",
                  "horizontalResolution": "1.0",
                  "verticalResolution": "1.0"
              }
            ],
            "name": "Milord´s Suites",
            "policies": [
              {
                  "type": "",
                  "text": ""
              }
            ],
            "rating": "3.0",
            "source": {
                "selectedSupplier": "101",
                "suppliers": [
                  "101"
                ]
            },
            "supportsPostpaidRates": false,
            "supportsPrepaidRates": true,
            "thumbnails": [
              {
                  "height": "250",
                  "imageCaption": "Guestroom",
                  "url": "http://d3mj096p5q0e20.cloudfront.net/fi/HCM/457757/7251474_2_b.jpg",
                  "width": "150",
                  "horizontalResolution": "1.0",
                  "verticalResolution": "1.0"
              }
            ],
            "websiteUrl": ""
        };
        res.send(hotelBookingHotelDetails);
    }
};