using ADO.NET_Task.Models;
using ADO.NET_Task.Utils;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ADO.NET_Task.Repository
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        public LocationRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Location GetLocationById(int locationId)
        {
            return Task.Run(() => GetLocationByIdAsync(locationId)).GetAwaiter().GetResult();
        }


        public IList<Location> GetLocations(int subscriberId, int page, int pageSize)
        {
            return Task.Run(() => GetLocationsAsync(subscriberId, page, pageSize)).GetAwaiter().GetResult();
        }

        public IList<ProviderAssignment> GetLocationAssigments(int locationId)
        {
            return Task.Run(() => GetLocationAssigmentsAsync(locationId)).GetAwaiter().GetResult();
        }

        public IList<ProviderAssignment> GetLocationAssigmentForProvider(int locationId, int providerId)
        {
            return Task.Run(() => GetLocationAssigmentForProviderAsync(locationId, providerId)).GetAwaiter().GetResult();
        }

        public Location CreateLocation(Location location)
        {
            return Task.Run(() => CreateLocationAsync(location)).GetAwaiter().GetResult();
        }

        public Location UpdateLocation(Location location)
        {
            return Task.Run(() => UpdateLocationAsync(location)).GetAwaiter().GetResult();
        }

        public void DeleteLocation(int locationId)
        {
            Task.Run(() => DeleteLocation(locationId)).GetAwaiter().GetResult();
        }

        public async Task<Location> GetLocationByIdAsync(int locationId, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@LocationId", locationId);

            var location = await GetFirstFromProcAsync<Location>("sp_GetLocationById", parameters, token);
            return location;
        }

        public async Task<IList<Location>> GetLocationsAsync(int subscriberId, int page, int pageSize, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SubscriberId", subscriberId);
            parameters.Add("@Page", page);
            parameters.Add("@PageSize", pageSize);

            var locations = await GetListFromProcAsync<Location>("sp_GetLocations", parameters, token);
            return locations;
        }

        public async Task<IList<ProviderAssignment>> GetLocationAssigmentsAsync(int locationId, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@LocationId", locationId);

            var assignments = await GetListFromProcAsync<ProviderAssignment>("sp_GetLocationAssigments", parameters, token);
            return assignments;
        }

        public async Task<IList<ProviderAssignment>> GetLocationAssigmentForProviderAsync(int locationId, int providerId, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@LocationId", locationId);
            parameters.Add("@ProviderId", providerId);

            var assigments = await GetListFromProcAsync<ProviderAssignment>("sp_GetLocationAssigmentForProvider", parameters, token);
            return assigments;
        }

        public async Task<Location> CreateLocationAsync(Location location, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", location.Name);
            parameters.Add("@Region", location.Region);
            parameters.Add("@Country", location.Country);
            parameters.Add("@City", location.City);
            parameters.Add("@Zip", location.Zip);
            parameters.Add("@Latitude", location.Latitude);
            parameters.Add("@Longitude", location.Longitude);
            parameters.Add("@SubscriberId", location.SubscriberId);

            var createdLocation = await GetFirstFromProcAsync<Location>("sp_CreateLocation", parameters, token);
            return createdLocation;
        }

        public async Task<Location> UpdateLocationAsync(Location location, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", location.Id);
            parameters.Add("@Name", location.Name);
            parameters.Add("@Region", location.Region);
            parameters.Add("@Country", location.Country);
            parameters.Add("@City", location.City);
            parameters.Add("@Zip", location.Zip);
            parameters.Add("@Latitude", location.Latitude);
            parameters.Add("@Longitude", location.Longitude);
            parameters.Add("@SubscriberId", location.SubscriberId);

            var updatedLocation = await GetFirstFromProcAsync<Location>("sp_UpdateLocation", parameters, token);
            return updatedLocation;
        }

        public async Task DeleteLocationAsync(int locationId, CancellationToken token = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", locationId);

            await ExecuteProcAsync("sp_DeleteLocation", parameters, token);
        }
    }
}