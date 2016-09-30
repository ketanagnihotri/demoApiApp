using System;
using System.Threading.Tasks;
using Kenobi.TripsExtension.TripsRepository.Repository;
using Kenobi.TripsExtension.TripService.Entity;
using Kenobi.TripsExtension.TripService.Interface;
using Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripService.Service
{
    public class TripService : ITripService
    {

        public async Task<TripFolder> GetTripFolder(TripServiceRequest request, string tenantId)
        {
            try
            {
                var tripprovider = new TripProvider(tenantId);
                var classicTripFolder = tripprovider.RetrieveTripFolderByPlatformTripId(request.TripId);
                return await Task.FromResult(classicTripFolder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOskiWithClassicTripId(string oskiTripId, TripFolder tripFolder, string tenantId)
        {
            try
            {
                var tripprovider = new TripProvider(tenantId);
                tripprovider.UpdateOskiWithClassicTripId(oskiTripId, tripFolder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
