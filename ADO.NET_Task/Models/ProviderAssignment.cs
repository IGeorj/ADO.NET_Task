using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.NET_Task.Models
{
    public class ProviderAssignment : IEntity
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Trade { get; set; }
        public int ProviderId { get; set; }
        public int LocationId { get; set; }
    }
}