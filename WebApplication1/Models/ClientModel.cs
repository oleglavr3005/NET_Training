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

        public bool IsDeleted { get; set; }
        public ClientModel()
        {
            IsDeleted = false;
        }
        public ICollection<ConsoleApplication1.Order> Orders { get; set; }
    }
}