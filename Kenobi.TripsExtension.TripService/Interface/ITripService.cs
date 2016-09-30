using System.Threading.Tasks;
using Kenobi.TripsExtension.TripService.Entity;
using Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripService.Interface
{
    public interface ITripService
    {
        Task<TripFolder> GetTripFolder(TripServiceRequest request, string tenantId);
        void UpdateOskiWithClassicTripId(string oskiTripId, TripFolder tripFolder, string tenantId);
    }
}
