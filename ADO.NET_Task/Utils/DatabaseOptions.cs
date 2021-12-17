using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Utils
{
    public class DatabaseOptions : IConfiguration
    {
        public string ConnectionString { get; set; }

        public DatabaseOptions()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AdoNetTask"].ConnectionString;
        }
    }
}