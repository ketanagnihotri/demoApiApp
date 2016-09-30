using System;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class AddressParser
    {
        internal static proxy.Address Parse(Model.Address address, Model.GeoCode geoCode)
        {
            return address != null ? new proxy.Address
            {
                AddressLine1 = address.Line1,
                GeoCode= new proxy.GeoCode { Latitude=Convert.ToSingle(geoCode?.Latitude), Longitude= Convert.ToSingle(geoCode?.Longitude) },
                AddressLine2 = address.Line2,
                ZipCode = address.ZipCode,
                City = new proxy.City
                {
                    Country = address.CountryCode,
                    State = address.State != null ? address.State.Code : string.Empty,
                    Name = address.City.Name,
                    //Id = passenger.ContactInfo.Address.City.Code
                }
            } : null;
        }

    }
}
