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
        private ProdContext ctx;
        private log4net.ILog logger;

        public AggregatedCalculations()
        {
            ctx = new ProdContext();
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
            Console.ReadLine();
            return base.ToString();
        }

        public decimal GetTotalCost(int orderId)
        {
            var JoinedCollection =
            from od in ctx.OrderDetails
            join pr in ctx.Products on od.ProductID equals pr.ProductId
            join or in ctx.Orders on od.OrderID equals or.ID into i
            where od.OrderID == orderId
            select new { OrderDetail = od.ID, OrderDetailAmount = pr.ProductPrice * od.ProductQuantity };
            return JoinedCollection.Sum(s => s.OrderDetailAmount);
        }

    }
}
