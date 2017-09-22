using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace ConsoleApplication1
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Start application.");

            Product updProduct=null;

            List<Product> products = new List<Product>()
            { new Product("Prod1", 145445, 1013, 81.67m),
             new Product("Prod2", 435435436, 1017, 49.99m) ,
              new Product("Prod2", 435435435, 1044, 59.99m) ,
            new Product("Prod3", 354554, 1015, 49.00m) ,
            new Product("Prod3", 354554, 1015, 89.00m) ,
            new Product("Prod3", 354554, 1015, 49.00m) }
            ;
            products= products.Distinct<Product>(new EqualityComparer()).ToList<Product>();
            products.Sort();
            using (var ctx = new ProductsContext())
            {
                /*    foreach (Product prod in products)
                    {
                        Console.WriteLine(prod);
                        ctx.Products.Add(prod);

                    }
                    */
                updProduct= ctx.Products.Where(s => s.ProductId == 354554).FirstOrDefault<Product>();
                if (updProduct != null)
                    updProduct.ProductPrice = 59.00m;
                ctx.SaveChanges();


            }
            using (var dbCtx = new ProductsContext())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updProduct).State = System.Data.Entity.EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }

            Console.ReadLine();

            log.Info("End application.");

        }
    }
}
