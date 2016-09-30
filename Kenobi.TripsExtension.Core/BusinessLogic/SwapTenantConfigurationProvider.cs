using System.Globalization;
using System.Linq;
using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Interfaces;
using Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.Core.BusinessLogic
{
    internal class SwapTenantConfigurationProvider : ISwapTenantConfigurationProvider
    {
        private readonly ITenantConfigurationProviderV1 _tenantConfigProvider;

        public SwapTenantConfigurationProvider(ITenantConfigurationProviderV1 configurationProvider)
        {
            _tenantConfigProvider = configurationProvider;
        }

        public TripFolder SwapTenantConfiguration(TripFolder tripFolder, string tenantId)
        {
            if (tripFolder == null || string.IsNullOrEmpty(tenantId)) return null;

            var platformPosId = string.Empty;
            var posIdFromAdditionInfo = tripFolder.Pos?.AdditionalInfo.FirstOrDefault(x => x.Name == Constants.PosId);
            if (posIdFromAdditionInfo != null)
                platformPosId = posIdFromAdditionInfo.Value;

            if (posIdFromAdditionInfo == null) return tripFolder;

            tripFolder.Pos.PosId = _tenantConfigProvider.GetClassicPosIdByPlatformPosId(tenantId, platformPosId);

            foreach (var product in tripFolder.Products)
            {
                var hoteTripProduct = product as HotelTripProduct;
                if (hoteTripProduct.HotelItinerary?.Rooms != null)
                {
                    foreach (var room in hoteTripProduct.HotelItinerary.Rooms)
                    {
                        var platformSupplierId = room.AdditionalInformation.Find(k => k.Name == "platformsupplierid").Value;
                        var roomFaresourceId = platformSupplierId != null
                            ? _tenantConfigProvider.GetClassicSupplierIdByTenantIdPlatformSupplierId(tenantId, platformSupplierId, platformPosId) //room.HotelFareSource.Id.ToString(CultureInfo.InvariantCulture), posId)
                            : 0;// hoteFareSource.Id;

                        room.HotelFareSource = new HotelFareSource { Id = roomFaresourceId };
                    }
                }
            }

            return tripFolder;
        }
    }
}