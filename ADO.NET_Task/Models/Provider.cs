using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Models
{
    public class Provider : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Years { get; set; }
    }
}