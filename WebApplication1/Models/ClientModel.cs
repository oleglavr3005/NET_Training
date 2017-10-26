using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication1.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        public ClientModel()
        {
            IsDeleted = false;
        }
        [JsonIgnore]
        public ICollection<ConsoleApplication1.Order> Orders { get; set; }
    }
}