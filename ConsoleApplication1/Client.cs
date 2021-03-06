﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Client()
        {
            IsDeleted = false;
        }
        public ICollection<Order> Orders { get; set; }

        static public Client GetRandomClient(int n)
        {
            Random rnd = new Random(n);
            Client newClient = new Client();
            newClient.Name = "Client#" + rnd.Next(100, 1000);
            return newClient;

        }
    }
}
