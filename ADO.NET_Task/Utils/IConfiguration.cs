using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Task.Utils
{
    public interface IConfiguration
    {
        string ConnectionString { get; set; }
    }
}
