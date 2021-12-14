using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        public decimal Latitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        public decimal Longitude { get; set; }
        public int SubscriberId { get; set; }
    }
}