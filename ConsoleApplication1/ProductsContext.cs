using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public  class ProductsContext : DbContext
    {
        public ProductsContext() : base()
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

      //  public System.Data.Entity.DbSet<Client> ClientModels { get; set; }
    }
}
