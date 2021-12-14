using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Models
{
    public class Location : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int SubscriberId { get; set; }
    }
}