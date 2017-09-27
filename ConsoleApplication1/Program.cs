using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void InsertOrdersInDB(int numberOfRecords, ProductsContext ctx)
        {
   
            Order neworder = new Order();
            List<Order> listOfOrders = new List<Order>();
            for (int i = 0; i < numberOfRecords; i++)
            {
                neworder = new Order();
                neworder.DateCreated = DateTime.Now;
                neworder.Status = "Created";
                neworder.ClientID = (i % 10) + 1;
                ctx.Orders.Add(neworder);
                listOfOrders.Add(neworder);
            }

            //      ctx.Orders.AddRange(listOfOrders);

            ctx.SaveChanges();
        }
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Start application.");

            Product updProduct = null;
            List<Product> updProducts = new List<Product>();
            List<Product> products = new List<Product>()
            { new Product("Prod1", 145445, 1013, 81.67m),
              new Product("Prod2", 435435436, 1017, 49.99m) ,
              new Product("Prod2", 435435435, 1044, 59.99m) ,
              new Product("Prod3", 354554, 1015, 49.00m) ,
              new Product("Prod3", 354554, 1015, 89.00m) ,
              new Product("Prod3", 354554, 1015, 49.00m) }
            ;
            for (int i = 0; i < 100; i++)
                products.Add(Product.GetRandomProduct(i));
            products = products.Distinct<Product>(new EqualityComparer()).ToList<Product>();
            products.Sort();
            using (var ctx = new ProductsContext())
            {
                /*  foreach (Product prod in products)
                  {
                      Console.WriteLine(prod);
                      ctx.Products.Add(prod);
                  }

                  for (int i=0; i<10;i++)
                  {
                      ctx.Clients.Add(Client.GetRandomClient(i));
                  }
                   */
                ctx.Configuration.AutoDetectChangesEnabled = true;
                ctx.Database.ExecuteSqlCommand("delete  from dbo.Orders");
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                InsertOrdersInDB(500, ctx);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                ctx.Configuration.AutoDetectChangesEnabled = false;
                Stopwatch stopWatch1 = new Stopwatch();
                stopWatch1.Start();
                InsertOrdersInDB(500, ctx);
                stopWatch1.Stop();

                ts = stopWatch1.Elapsed;

                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                ctx.Configuration.AutoDetectChangesEnabled = true;
                updProducts = ctx.Products.Where(s => s.ProductCode == 1017).ToList<Product>();
                foreach (Product product in updProducts)
                    product.ProductPrice = 99.00m;
                ctx.SaveChanges();


            }
            /*       using (var dbCtx = new ProductsContext())
                   {
                       //3. Mark entity as modified
                  dbCtx.Entry(updProduct).State = System.Data.Entity.EntityState.Modified;
                       updProduct = dbCtx.Products.Where(s => s.ProductName == "Prod3").First<Product>();
                       if (updProduct != null)
                           updProduct.ProductPrice = 59.00m;
                       //4. call SaveChanges
                       dbCtx.SaveChanges();
                   }
       */
            Console.ReadLine();
            log.Info("End application.");

        }
    }
}
