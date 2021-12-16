using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace ADO.NET_Task.Repository
{
    public abstract class RepositoryBase : IDisposable  
    {
        protected IDbConnection _connection;

        public RepositoryBase(IDbConnection connection)
        {
            _connection = connection;
        }

        protected T GetFirstFromProc<T>(string storedProcName, DynamicParameters parameters = null)
            where T : class, new()
        {
            try
            {
                _connection.Open();
                var result = _connection.QueryFirst<T>(sql: storedProcName,
                                                          param: parameters,
                                                          commandType: CommandType.StoredProcedure);
                _connection.Close();

                return result;
            }
            catch (Exception)
            {
                _connection.Close();
                throw;
            }
        }

        protected IList<T> GetListFromProc<T>(string storedProcName, DynamicParameters parameters = null) 
            where T : class, new()
        {
            try
            {
                _connection.Open();
                var results = _connection.Query<T>(sql: storedProcName,
                                                   param: parameters,
                                                   commandType: CommandType.StoredProcedure).AsList();
                _connection.Close();
                
                return results;
            }
            catch (Exception)
            {
                _connection.Close();
                throw;
            }
        }

        protected void ExecuteProc(string storedProcName, DynamicParameters parameters = null)
        {
            try
            {
                _connection.Open();
                _connection.Execute(sql: storedProcName, param: parameters, commandType: CommandType.StoredProcedure);
                _connection.Close();
            }
            catch (Exception)
            {
                _connection.Close();
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