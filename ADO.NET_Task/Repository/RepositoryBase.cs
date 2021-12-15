using ADO.NET_Task.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Repository
{
    public abstract class RepositoryBase : IDisposable  
    {
        protected IDbConnection _connection;

        public RepositoryBase(DatabaseOptions options)
        {
            _connection.ConnectionString = options.ConnectionString;
        }

        protected List<T> Query<T>(string storedProcName, Dictionary<string, object> parameters = null) 
            where T : class, new()
        {
            try
            {
                if (storedProcName != null)
                {
                    var dynamicParameters = new DynamicParameters(parameters);
                    var resultsWithParams = _connection.Query<T>(storedProcName,
                        dynamicParameters, 
                        commandType: CommandType.StoredProcedure)
                        .AsList();
                    return resultsWithParams;
                }

                _connection.Open();
                var results = _connection.Query<T>(storedProcName, commandType: CommandType.StoredProcedure).AsList();
                _connection.Close();
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if( _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}