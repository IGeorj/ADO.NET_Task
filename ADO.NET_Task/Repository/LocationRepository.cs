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
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AdoNetTask"].ConnectionString;
        public Location GetLocationById(int locationId)
        {
            Location location = new Location();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_GetLocationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocationId", locationId);
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
                    location.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    location.Longitude = Convert.ToDecimal(reader["Longitude"]);
                    location.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                }
                return location;
            }
        }

        public IList<Location> GetLocations(int subscriberId, int page, int pageSize)
        {
            List<Location> locations = new List<Location>();

            using (SqlConnection connection = new SqlConnection(connectionString))
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
                        Latitude = Convert.ToDecimal(reader["Latitude"]),
                        Longitude = Convert.ToDecimal(reader["Longitude"]),
                        SubscriberId = Convert.ToInt32(reader["SubscriberId"])
                    };
                    locations.Add(location);

                }
                return locations;
            }
        }

        public IList<ProviderAssignment> GetLocationAssigments(int locationId)
        {
            List<ProviderAssignment> assignments = new List<ProviderAssignment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_GetLocationAssigments", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocationId", locationId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var assigment = new ProviderAssignment()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Rank = Convert.ToInt32(reader["Rank"]),
                        Trade = reader["Trade"].ToString(),
                        ProviderId = Convert.ToInt32(reader["ProviderId"]),
                        LocationId = Convert.ToInt32(reader["LocationId"])
                    };
                    assignments.Add(assigment);
                }
                return assignments;
            }
        }

        public IList<ProviderAssignment> GetLocationAssigmentForProvider(int locationId, int providerId)
        {
            List<ProviderAssignment> assignments = new List<ProviderAssignment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_GetLocationAssigmentForProvider", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocationId", locationId);
                command.Parameters.AddWithValue("@ProviderId", providerId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var assigment = new ProviderAssignment()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Rank = Convert.ToInt32(reader["Rank"]),
                        Trade = reader["Trade"].ToString(),
                        ProviderId = Convert.ToInt32(reader["ProviderId"]),
                        LocationId = Convert.ToInt32(reader["LocationId"])
                    };
                    assignments.Add(assigment);
                }
                return assignments;
            }
        }

        public Location CreateLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_CreateLocation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", location.Name);
                command.Parameters.AddWithValue("@Region", location.Region);
                command.Parameters.AddWithValue("@Country", location.Country);
                command.Parameters.AddWithValue("@City", location.City);
                command.Parameters.AddWithValue("@Zip", location.Zip);
                command.Parameters.AddWithValue("@Latitude", location.Latitude);
                command.Parameters.AddWithValue("@Longitude", location.Longitude);
                command.Parameters.AddWithValue("@SubscriberId", location.SubscriberId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Location newLocation = new Location();

                while (reader.Read())
                {
                    newLocation.Id = Convert.ToInt32(reader["Id"]);
                    newLocation.Name = reader["Name"].ToString();
                    newLocation.Region = reader["Region"].ToString();
                    newLocation.Country = reader["Country"].ToString();
                    newLocation.City = reader["City"].ToString();
                    newLocation.Zip = reader["Zip"].ToString();
                    newLocation.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    newLocation.Longitude = Convert.ToDecimal(reader["Longitude"]);
                    newLocation.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                }
                return newLocation;
            }
        }

        public Location UpdateLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateLocation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", location.Id);
                command.Parameters.AddWithValue("@Name", location.Name);
                command.Parameters.AddWithValue("@Region", location.Region);
                command.Parameters.AddWithValue("@Country", location.Country);
                command.Parameters.AddWithValue("@City", location.City);
                command.Parameters.AddWithValue("@Zip", location.Zip);
                command.Parameters.AddWithValue("@Latitude", location.Latitude);
                command.Parameters.AddWithValue("@Longitude", location.Longitude);
                command.Parameters.AddWithValue("@SubscriberId", location.SubscriberId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Location newLocation = new Location();

                while (reader.Read())
                {
                    newLocation.Id = Convert.ToInt32(reader["Id"]);
                    newLocation.Name = reader["Name"].ToString();
                    newLocation.Region = reader["Region"].ToString();
                    newLocation.Country = reader["Country"].ToString();
                    newLocation.City = reader["City"].ToString();
                    newLocation.Zip = reader["Zip"].ToString();
                    newLocation.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    newLocation.Longitude = Convert.ToDecimal(reader["Longitude"]);
                    newLocation.SubscriberId = Convert.ToInt32(reader["SubscriberId"]);
                }
                return newLocation;
            }
        }

        public void DeleteLocation(int locationId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteLocation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", locationId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}