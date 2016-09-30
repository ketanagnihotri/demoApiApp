using System;
using System.Collections.Generic;
using System.Linq;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class HotelPropertyParser
    {

        public static proxy.HotelProperty Parse(Model.Hotel modelHotel, Model.HotelDetails modelHotelDetails)
        {
            return modelHotel != null && modelHotelDetails != null ?
              new proxy.HotelProperty
              {
                  Address = AddressParser.Parse(modelHotel.HotelAddress,modelHotelDetails.GeoCode),
                  Name = modelHotelDetails.Name,
                  Amenities = modelHotelDetails.Amenities != null ? GetAminities(modelHotelDetails.Amenities) : null,
                  HotelChain = new proxy.HotelChain
                  {
                      FullName = modelHotelDetails.HotelChain?.Name
                  },
                  HotelRating = new proxy.HotelRating
                  {
                      Rating = Convert.ToSingle(modelHotelDetails.Rating),
                      RatingType = proxy.RatingType.UnSpecified
                  },
                  NativeCurrencyCode = modelHotelDetails.HotelCurrencyCode,
                  Thumbnail = new proxy.Media
                  {
                      Url = modelHotelDetails.Thumbnails!=null && modelHotelDetails.Thumbnails.Count>0 ? modelHotelDetails.Thumbnails[0]?.Url: string.Empty,
                  },
                  WebsiteUrl = modelHotelDetails.WebsiteUrl,
                  Descriptions = GetHoteldescriptions(modelHotelDetails.Descriptions),
                  GeoCode = new proxy.GeoCode
                  {
                      Latitude = Convert.ToSingle(modelHotelDetails.GeoCode?.Latitude),
                      Longitude = Convert.ToSingle(modelHotelDetails.GeoCode?.Longitude)
                  },
                  PhoneNumber = GetPhoneNumber(modelHotelDetails.Contact),

                  MediaContent = GetMediaContent(modelHotelDetails.Images),
                  SupplierHotelId = modelHotel.SupplierHotelId,
                  FaxNumber = modelHotelDetails.Contact != null && modelHotelDetails.Contact.Phones != null ? GeFaxNumber(modelHotelDetails.Contact.Phones) : null
              }
             : null;
        }

        private static List<proxy.Media> GetMediaContent(List<Model.Image> images)
        {
            //return images.ConvertAll(image => new proxy.Media { Url = image.Url, Caption = StringToEnum<proxy.MediaCaptionType>(image.ImageCaption) });
            return null;
        }

        private static List<proxy.HotelDescription> GetHoteldescriptions(List<Model.Description> descriptions)
        {
            return descriptions != null && descriptions.Count > 0 ?
                descriptions.ConvertAll(description => new proxy.HotelDescription { Description = description.Value, Type = description.Type })
                : null;

        }
        private static string GetPhoneNumber(Model.Contact lstContact)
        {
            return lstContact?.Phones != null && lstContact.Phones.Count > 1 ? lstContact.Phones[0].AreaCode + lstContact.Phones[0].Extension + lstContact.Phones[0].Number : string.Empty;
        }

        private static string GeFaxNumber(List<Model.Phone> lstPhone)
        {
            return lstPhone != null
                     ? lstPhone.Where(phone => phone.Type != null && phone.Type.Equals("fax"))
                     .Select(phone => phone.AreaCode + phone.Extension + phone.Number).SingleOrDefault() : string.Empty;
        }

        private static List<proxy.Amenity> GetAminities(List<Model.Amenity> lstAminity)
        {
            return lstAminity.ConvertAll(aminity => new proxy.Amenity { Description = aminity.Description, Name = aminity.Name, MasterAmenityName = aminity.Category });
        }
    }
}
