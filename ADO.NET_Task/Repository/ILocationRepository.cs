using ADO.NET_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Task.Repository
{
    internal interface ILocationRepository
    {
        Location GetLocationById(int locationId);
        IList<Location> GetLocations(int subscriberId, int page, int pageSize);
        IList<ProviderAssignment> GetLocationAssigments(int locationId);
        IList<ProviderAssignment> GetLocationAssigmentForProvider(int locationId, int providerId);
        void CreateLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int locationId);
    }
}
