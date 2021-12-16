using ADO.NET_Task.Models;
using System.Collections.Generic;

namespace ADO.NET_Task.Repository
{
    public interface ILocationRepository
    {
        Location GetLocationById(int locationId);
        IList<Location> GetLocations(int subscriberId, int page, int pageSize);
        IList<ProviderAssignment> GetLocationAssigments(int locationId);
        IList<ProviderAssignment> GetLocationAssigmentForProvider(int locationId, int providerId);
        Location CreateLocation(Location location);
        Location UpdateLocation(Location location);
        void DeleteLocation(int locationId);
    }
}
