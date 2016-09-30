using System;
using System.Collections.Generic;
using System.Linq;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class PassengerParser
    {
        internal static List<proxy.Passenger> Parse(List<Model.Passenger> modelPassengers)
        {
            List<proxy.Passenger> proxyPassengers = new List<proxy.Passenger>();
            int rph = 0;
            foreach (var passenger in modelPassengers)
            {
                DateTime dob;
                var pax = new proxy.Passenger
                {
                    //todo: age check
                    BirthDate = DateTime.TryParse(passenger.DateOfBirth, out dob) ? dob : DateTime.Now,
                    FirstName = passenger.Name.FirstName,
                    MiddleName = passenger.Name.MiddleName,
                    LastName = passenger.Name.LastName,
                    HotelMemberships = passenger.Memberships != null && passenger.Memberships.HotelLoyalties!=null ? MembershipParser.Parse(passenger.Memberships.HotelLoyalties) : null,
                    Gender = string.IsNullOrEmpty(passenger.Gender)? proxy.Gender.NotSpecified: GetGender(passenger.Gender),
                    //Converter.StringToEnum<proxy.Gender>(passenger.Gender),
                    DeliveryAddress = passenger.ContactInfo != null ? AddressParser.Parse(passenger.ContactInfo.Address, new Model.GeoCode()) : new proxy.Address(),
                    PhoneNumber = GetPhoneNumber(passenger.ContactInfo),
                    // Age = DateTime.Today.Year- passenger.DateOfBirth TODO: dob given by oski is string
                    Email = (passenger.ContactInfo?.Emails!=null && passenger.ContactInfo?.Emails?.Count>0) ? passenger.ContactInfo?.Emails?.First() : string.Empty,
                    PassengerType = string.IsNullOrEmpty(passenger.Type)? proxy.PassengerType.Adult : GetPassengerType(passenger.Type),
                    Passport = PassportParser.Parse(passenger.Passport, modelPassengers.IndexOf(passenger)),
                    Rph = rph,
                    Age=passenger.Age
                };
                proxyPassengers.Add(pax);
                rph++;
                //classicTripFolder.Passengers.Add(pax);
            }

            return proxyPassengers;
        }

        private static proxy.PassengerType GetPassengerType(string type)
        {
            proxy.PassengerType paxType = proxy.PassengerType.Adult;

            if (type.Equals("child", StringComparison.InvariantCultureIgnoreCase))
                paxType = proxy.PassengerType.Child;
            else if (type.Equals("senior", StringComparison.InvariantCultureIgnoreCase))
                paxType = proxy.PassengerType.Senior;
            else if (type.Equals("infantInLap", StringComparison.InvariantCultureIgnoreCase))
                paxType = proxy.PassengerType.Infant;

            return paxType;
        }

        private static proxy.Gender GetGender(string modelGender)
        {
            proxy.Gender gender = proxy.Gender.NotSpecified;

            if (modelGender.Equals("male", StringComparison.InvariantCultureIgnoreCase))
                gender = proxy.Gender.Male;
            else if (modelGender.Equals("female", StringComparison.InvariantCultureIgnoreCase))
                gender = proxy.Gender.Female;

            return gender;
        }

        private static string GetPhoneNumber(Model.Contact lstContact)
        {
            return (lstContact?.Phones != null && lstContact.Phones.Count > 0) ? lstContact.Phones[0].AreaCode + lstContact.Phones[0].Extension + lstContact.Phones[0].Number : string.Empty;
        }

    }
}
