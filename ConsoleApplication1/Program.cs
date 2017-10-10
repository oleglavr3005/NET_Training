using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

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

        public static void InsertOrdersDetailsInDB(int numberOfRecords, ProductsContext ctx)
        {

            OrderDetails newDetails = new OrderDetails();
            List<OrderDetails> listOfDetails = new List<OrderDetails>();
            List<Product> listOfProducts = ctx.Products.ToList<Product>();
            foreach (Order order in ctx.Orders)
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    newDetails = new OrderDetails();
                    newDetails.OrderID = order.ID;
                    newDetails.ProductID = listOfProducts[i].ProductId;
                    newDetails.ProductQuantity = 10 + i+order.ID;
                    ctx.OrderDetails.Add(newDetails);
                }
            }

            //      ctx.Orders.AddRange(listOfOrders);

            ctx.SaveChanges();
        }
        private static DataTable MakeTable(ICollection<Order> orders)
        // Create a new DataTable named NewProducts. 
        {
            DataTable newOrdersDetails = new DataTable("dbo.OrdersDetails");

            // Add three column objects to the table. 
            DataColumn ID = new DataColumn();
            ID.DataType = System.Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            ID.AutoIncrement = true;
            newOrdersDetails.Columns.Add(ID);

            DataColumn orderID = new DataColumn();
            orderID.DataType = System.Type.GetType("System.Int32");
            orderID.ColumnName = "OrderID";
            newOrdersDetails.Columns.Add(orderID);

            DataColumn ProductID = new DataColumn();
            ProductID.DataType = System.Type.GetType("System.Int32");
            ProductID.ColumnName = "ProductID";
            newOrdersDetails.Columns.Add(ProductID);
            DataColumn Quantity = new DataColumn();
            Quantity.DataType = System.Type.GetType("System.Decimal");
            Quantity.ColumnName = "ProductQuantity";
            newOrdersDetails.Columns.Add(Quantity);
            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ID;
            newOrdersDetails.PrimaryKey = keys;

            // Add some new rows to the collection. 
            foreach (Order currentOrder in orders)
            {
                for (int i = 0; i < 8; i++)
                { 
                DataRow row = newOrdersDetails.NewRow();
                row["OrderID"] = currentOrder.ID;

                    row["ProductID"] = i + 1; ;
                row["ProductQuantity"] = (decimal)(25+i)/100;

                newOrdersDetails.Rows.Add(row);
            }
            }
            newOrdersDetails.AcceptChanges();

            // Return the new DataTable. 
            return newOrdersDetails;
        }
        public static void InsertOrdersDetailsSQLBulk(string connectionString,ICollection<Order> orders)
        {
            // Open a connection to the AdventureWorks database.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
         
         
            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a table with some rows. 
                DataTable newOrdersDetails = MakeTable(orders);

                // Create the SqlBulkCopy object. 
                // Note that the column positions in the source DataTable 
                // match the column positions in the destination table so 
                // there is no need to map columns. 
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName =
                        "dbo.OrderDetails";

                    try

                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(newOrdersDetails);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
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
                 foreach (Product prod in products)
                  {
                      Console.WriteLine(prod);
                      ctx.Products.Add(prod);
                  }

                  for (int i=0; i<10;i++)

                  {
                      ctx.Clients.Add(Client.GetRandomClient(i));
                  }
                InsertOrdersInDB(100, ctx);


                ctx.Configuration.AutoDetectChangesEnabled = true;
              ctx.Database.ExecuteSqlCommand("delete  from dbo.OrderDetails");
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                InsertOrdersDetailsInDB(3, ctx);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                ctx.Configuration.AutoDetectChangesEnabled = false;
                Stopwatch stopWatch1 = new Stopwatch();
                stopWatch1.Start();
                InsertOrdersDetailsInDB(3, ctx);
                stopWatch1.Stop();

                ts = stopWatch1.Elapsed;

                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                ctx.Configuration.AutoDetectChangesEnabled = true;
               
           //     ctx.Database.ExecuteSqlCommand("delete  from dbo.OrderDetails");
                ctx.SaveChanges();
                List<Order> allOrders = ctx.Orders.ToList<Order>();
                InsertOrdersDetailsSQLBulk(ctx.Database.Connection.ConnectionString,allOrders);
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
