using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ConsoleApplication1;
using log4net;
using System.Data.SqlClient;

namespace BusinessLogic
{
    public class AggregatedCalculations
    {
        private ProductsContext ctx;
        private log4net.ILog logger;

        public AggregatedCalculations()
        {
            ctx = new ProductsContext();
            logger = LogManager.GetLogger(typeof(Program));
        }

        /*(   public AggregatedCalculations(DbContext dbctx, ILog log)
           {
               ctx = dbctx;
               logger = log;
           }*/

        public override string ToString()
        {
            Console.WriteLine("haha");
            return base.ToString();
        }

        public decimal? GetTotalCost(int orderId)
        {
            var JoinedCollection =
            from od in ctx.OrderDetails
            join pr in ctx.Products on od.ProductID equals pr.ProductId
            join or in ctx.Orders on od.OrderID equals or.ID into i
            where od.OrderID == orderId
            select new { OrderDetail = od.ID, OrderDetailAmount = pr.ProductPrice * od.ProductQuantity };
            return JoinedCollection.Sum(s => s.OrderDetailAmount);
        }

        public void GetRecentOrders(int clientId)
        {
            IQueryable<int> orders = (from od in ctx.Orders where od.ClientID == clientId orderby od.ID select od)
                .Include("OrderDetails").Include("Products").Select(o => o.ID);
            orders = orders.Take(10);
            foreach (int o in orders)
            {
                Console.WriteLine("id=" + o + "  cost=" + GetTotalCost(o));
            }
        }
        public decimal? GetTotalCostByClient(int clientId)
        { 
        SqlParameter param1 = new SqlParameter("@ClientId", clientId);
        var total =  ctx.Database.SqlQuery<decimal?>("CountTotalCostByClient  @ClientID", param1).Single<decimal?>();
            Console.WriteLine(total);
            return total;
    }
        public void GetTotalCostByClients()
        {
            IQueryable<int> clients = from cl in ctx.Clients select cl.ID;
           foreach (int id in clients)
            {
                Console.WriteLine("Client ID="+id+"  total="+GetTotalCostByClient(id));
            }
        }
    }
}
