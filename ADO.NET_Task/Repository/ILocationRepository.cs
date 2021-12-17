using ADO.NET_Task.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ADO.NET_Task.Repository
{
    public interface ILocationRepository
    {
        Location GetLocationById(int locationId);
        IList<Location> GetLocations(int subscriberId, int page, int pageSize);
        IList<ProviderAssignment> GetLocationAssigments(int locationId);
        Location CreateLocation(Location location);
        IList<ProviderAssignment> GetLocationAssigmentForProvider(int locationId, int providerId);
        Location UpdateLocation(Location location);
        void DeleteLocation(int locationId);
        Task<Location> GetLocationByIdAsync(int locationId, CancellationToken token);
        Task<IList<Location>> GetLocationsAsync(int subscriberId, int page, int pageSize, CancellationToken token);
        Task<IList<ProviderAssignment>> GetLocationAssigmentsAsync(int locationId, CancellationToken token);
        Task<IList<ProviderAssignment>> GetLocationAssigmentForProviderAsync(int locationId, int providerId, CancellationToken token);
        Task<Location> CreateLocationAsync(Location location, CancellationToken token);
        Task<Location> UpdateLocationAsync(Location location, CancellationToken token);
        Task DeleteLocationAsync(int locationId, CancellationToken token);
    }
}
