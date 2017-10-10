using BusinessLogic.DTO;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;

namespace ConsoleApplication1
{
    class ProdContext : DbContext
    {
        public ProdContext() : base()
        {

        }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<ClientDTO> Clients { get; set; }
        public DbSet<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
