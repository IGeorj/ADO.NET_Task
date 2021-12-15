using ADO.NET_Task.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Utils
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationRepository>().To<LocationRepository>();

            DatabaseOptions options = new DatabaseOptions();
            options.ConnectionString = ConfigurationManager.ConnectionStrings["AdoNetTask"].ConnectionString;

            Bind<DatabaseOptions>().ToConstant(options);
        }
    }
}