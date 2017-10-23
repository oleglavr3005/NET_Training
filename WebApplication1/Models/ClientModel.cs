using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ConsoleApplication1.Order> Orders { get; set; }
    }
}