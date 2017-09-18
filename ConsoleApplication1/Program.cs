using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>()
            { new Product("1", "1", 1013, 81.67m),
             new Product("2", "2", 1017, 49.99m) ,
            new Product("3", "3", 1015, 49.00m) }
            ;
            products.Sort();
            foreach (Product prod in products) Console.WriteLine(prod);
            Console.ReadLine();
        }
    }
}
