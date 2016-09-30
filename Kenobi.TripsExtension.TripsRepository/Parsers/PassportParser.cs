using System;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
   internal static class PassportParser
    {

        internal static proxy.Passport Parse(Model.Passport passport, int passsengerRph)
        {
            DateTime dateofIssue;
            DateTime expiryDate;
            return passport != null ? new proxy.Passport
            {
                DateOfIssue = DateTime.TryParse(passport.DateOfIssue,out dateofIssue)? dateofIssue: DateTime.Now,
                ExpirationDate = DateTime.TryParse(passport.ExpiryDate, out expiryDate)? expiryDate: DateTime.Now,
                Nationality = passport.Nationality,
                PassportNumber = passport.Number,
                PlaceOfIssue = passport.PlaceOfIssue,
                PassengerRPH = passsengerRph
            } : null;
        }
    }
}
