﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ProductsContext : DbContext
    {
        public ProductsContext() : base()
        {

        }
        public DbSet<Product> Products { get; set; }
 
    }
}
