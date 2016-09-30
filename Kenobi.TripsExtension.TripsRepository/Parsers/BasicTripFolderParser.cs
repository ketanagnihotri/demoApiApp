using System;
using System.Collections.Generic;
using Kenobi.TripsExtension.TripsRepository.Util;
using static System.Int32;
using static System.Int64;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class BasicTripFolderParser
    {
        internal static proxy.TripFolder Parse(Model.Trip trip, string tenantId)
        {
            int ruleGroupId;

            DateTime createdDate;
            DateTime modifiedDate;
            proxy.TripFolder classicTripFolder = new proxy.TripFolder
            {
                ConfirmationNumber = string.Empty,
                FolderName = trip.Name,

                Owner = new proxy.User
                {
                    Email = Config.OwnerEmail,
                    FirstName = Config.OwnerFirstName,
                    LastName = Config.OwnerLastName,
                    UserName = ConfigurationHelper.GetOwnerUserName()
                },
                Pos = new proxy.PointOfSale
                {
                    //PosId = TryParse(trip.PosId, out posId) ? posId : 0, //platform datatype is string putting in additional info
                    Requester = new proxy.Company
                    {
                        DK = Config.RequesterPosDk,
                        Code = Config.RequesterCode,
                        FullName = "Connexions",
                        ShortName = "Portal",
                        Agency = new proxy.Agency
                        {
                            AgencyName = "Connexions",
                            AgencyId = 1
                        }
                    },
                    AdditionalInfo = new List<proxy.StateBag>
                    {
                        new proxy.StateBag()
                        {
                            Name = Constants.Default.PosId,
                            Value = trip.PosId
                        }
                    }
                },
                CreatedDate = DateTime.TryParse(trip.CreatedOn, out createdDate) ? createdDate : DateTime.Now,
                LastModifiedDate = DateTime.TryParse(trip.ModifiedOn, out modifiedDate) ? modifiedDate : DateTime.Now,
                Type =
                    trip.type != null && trip.type.Equals("Corporate", StringComparison.InvariantCultureIgnoreCase)
                        ? proxy.TripFolderType.Corporate
                        : proxy.TripFolderType.Personal,
                Status = GetTripStatus(trip.Status)
            };
            classicTripFolder.Pos.AdditionalInfo.AddRange(ConfigurationHelper.GetPosAdditionalInfo());
            classicTripFolder.CustomData = new proxy.CustomData
            {
                TextFields = new proxy.stringCustomFields
                {
                    Field04 = trip.Id,
                    Field02 = GetTripDetalsUrl(trip.Id, tenantId)
                },
                IntFields = new proxy.intCustomFields
                {
                    Field01 = TryParse(ConfigurationHelper.GetTenantRuleId(), out ruleGroupId) ? ruleGroupId : 0
                },
                StringFields = new proxy.stringCustomFields()
                {
                    Field02 = GetTripDetalsUrl(trip.Id, tenantId)
                }
            };
            return classicTripFolder;
        }

        private static string GetTripDetalsUrl(string oskiTripId, string tenantId)
        {
            string tripsDataUrl = Config.OskiTripsBookingDetailsUrl;
            if (string.IsNullOrWhiteSpace(tripsDataUrl))
                tripsDataUrl = "https://stage.cnxloyalty.com/data/api/v1.0/trips/{0}?oski-tenantid={1}";
            tripsDataUrl = String.Format(tripsDataUrl, oskiTripId, tenantId);
            return tripsDataUrl;
        }

        private static proxy.TripStatus GetTripStatus(string status)
        {
            if (status.Equals("Confirmed", StringComparison.InvariantCultureIgnoreCase))
                return proxy.TripStatus.Purchased;
            if (status.Equals("Planned", StringComparison.InvariantCultureIgnoreCase))
                return proxy.TripStatus.Planned;
            return status.Equals("Canceled", StringComparison.InvariantCultureIgnoreCase) ? proxy.TripStatus.Canceled : proxy.TripStatus.Planned;
        }
    }
}