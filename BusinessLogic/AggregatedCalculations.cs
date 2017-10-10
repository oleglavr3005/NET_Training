using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ConsoleApplication1;
using log4net;

namespace BusinessLogic
{
    class AggregatedCalculations
    {
        private ProductsContext ctx;
        private log4net.ILog logger;

        public AggregatedCalculations()
        {
            ctx = new ProductsContext();
            logger= LogManager.GetLogger(typeof(Program));
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

        public void GetRecentOrders (int clientId)
        {
            IQueryable <int> orders = from od in ctx.Orders  where od.ClientID == clientId orderby od.ID select od.ID;
            orders = orders.Take(10);
            foreach (int o in orders)
            {
                Console.WriteLine("id="+o+"  cost="+GetTotalCost(o));
            }
        }
    }
}
