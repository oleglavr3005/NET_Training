using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ProductsController
    {
        public void AddProduct(Product prod)
        {
            using (var ctx = new ProductsContext())
            {
                ctx.Products.Add(prod);
                ctx.SaveChanges();

            }
        }

        public Product GetProduct(String name, int code)
        {
            Product product = null;
            using (var ctx = new ProductsContext())
            {
                product = ctx.Products.Where(s => s.ProductCode == code && s.ProductName.Equals(name)).FirstOrDefault<Product>();
                return product;
            }
        }

        public void UpdateProduct(String name, int code, Product newProduct)
        {
            Product product = null;
            using (var ctx = new ProductsContext())
            {
                product = ctx.Products.Where(s => s.ProductCode == code && s.ProductName.Equals(name)).FirstOrDefault<Product>();
                product = newProduct;
                ctx.SaveChanges();
            }
        }

        public void DeleteOrders()
        {
           
            using (var ctx = new ProductsContext())
            {
               
            }
        }
    }
}
