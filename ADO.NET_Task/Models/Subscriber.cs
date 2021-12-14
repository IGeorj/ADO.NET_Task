using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Models
{
    public class Subscriber : IEntity
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}