using ADO.NET_Task.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ADO.NET_Task.Repository
{
    public abstract class RepositoryBase
    {
        protected string connectionString;

        public RepositoryBase(IConfiguration configuration)
        {
            connectionString = configuration.ConnectionString;
        }

        protected T GetFirstFromProc<T>(string storedProcName, DynamicParameters parameters = null)
            where T : class, new()
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var result = connection.QueryFirst<T>(storedProcName,
                                                           parameters,
                                                           commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<T> GetFirstFromProcAsync<T>(string storedProcName,
                                                         DynamicParameters parameters = null,
                                                         CancellationToken token = default)
            where T : class, new()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var result = await connection.QueryFirstAsync<T>(new CommandDefinition(
                        storedProcName,
                        parameters,
                        commandType: CommandType.StoredProcedure,
                        cancellationToken: token));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected IList<T> GetListFromProc<T>(string storedProcName, DynamicParameters parameters = null) 
            where T : class, new()
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var results = connection.Query<T>(storedProcName,
                                                       parameters,
                                                       commandType: CommandType.StoredProcedure).AsList();
                    return results;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task<IList<T>> GetListFromProcAsync<T>(string storedProcName,
                                                               DynamicParameters parameters = null,
                                                               CancellationToken token = default)
            where T : class, new()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var results = await connection.QueryAsync<T>(new CommandDefinition(
                        storedProcName,
                        parameters,
                        commandType: CommandType.StoredProcedure,
                        cancellationToken: token));
                    return results.AsList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ExecuteProc(string storedProcName, DynamicParameters parameters = null)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(storedProcName,
                                        parameters,
                                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task ExecuteProcAsync(string storedProcName,
                                              DynamicParameters parameters = null,
                                              CancellationToken token = default)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(new CommandDefinition(storedProcName,
                        parameters,
                        commandType: CommandType.StoredProcedure,
                        cancellationToken: token));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}