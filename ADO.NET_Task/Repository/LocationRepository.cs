using ADO.NET_Task.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly string connection = ConfigurationManager.ConnectionStrings["AdoNetTask"].ConnectionString;
        public Location GetLocationById(int locationId)
        {
            Location location = new Location();

            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand command = new SqlCommand("sp_GetLocationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    location.Id = Convert.ToInt32(reader["Id"]);
                    location.Name = reader["Name"].ToString();
                    location.Region = reader["Region"].ToString();
                    location.Country = reader["Country"].ToString();
                    location.City = reader["City"].ToString();
                    location.Zip = reader["Zip"].ToString();
                    location.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                }
                return location;
            }
        }

        public IList<Location> GetLocations(int subscriberId, int page, int pageSize)
        {
            List<Location> locations = new List<Location>();

            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand command = new SqlCommand("sp_GetLocations", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SubscriberId", subscriberId);
                command.Parameters.AddWithValue("@Page", page);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var location = new Location()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Region = reader["Region"].ToString(),
                        Country = reader["Country"].ToString(),
                        City = reader["City"].ToString(),
                        Zip = reader["Zip"].ToString(),
                        SubscriberId = Convert.ToInt32(reader["SubscriberId"])
                    };
                    locations.Add(location);

                }
                return locations;
            }
        }

        public IList<ProviderAssignment> GetLocationAssigments(int locationId)
        {
            //List<ProviderAssignment> assignments = new List<ProviderAssignment>();

            //using (SqlConnection connection = new SqlConnection())
            //{
            //    SqlCommand command = new SqlCommand("sp_GetLocationAssigments", connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@SubscriberId", subscriberId);
            //    command.Parameters.AddWithValue("@Page", page);
            //    command.Parameters.AddWithValue("@PageSize", pageSize);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        var location = new Location()
            //        {
            //            Id = Convert.ToInt32(reader["Id"]),
            //            Name = reader["Name"].ToString(),
            //            Region = reader["Region"].ToString(),
            //            Country = reader["Country"].ToString(),
            //            City = reader["City"].ToString(),
            //            Zip = reader["Zip"].ToString(),
            //            SubscriberId = Convert.ToInt32(reader["SubscriberId"])
            //        };
            //        locations.Add(location);

            //    }
            //    return locations;
            //}
            throw new NotImplementedException();
        }

        public ProviderAssignment GetLocationAssigmentForProvider(int locationId, int providerId)
        {
            throw new NotImplementedException();
        }

        public void CreateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void DeleteLocation(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}