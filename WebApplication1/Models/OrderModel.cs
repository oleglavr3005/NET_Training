using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime DateCreated { get; set; }
        public String Status { get; set; }
    }
}