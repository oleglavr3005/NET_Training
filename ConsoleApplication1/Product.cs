using log4net;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    [UseForEqualityCheck("productName", "productId")]
    class Product : ICloneable, IComparable
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Product));
        private string productName;
        private int productId;
        private int productCode;
        private decimal productPrice;
        public string Company { get; set; }
        public string ProductName { get { return productName; } set { productName = value; }  }
        public int ProductId { get { return productId; } set { productId = value; } }
        public int ProductCode { get { return productCode; } set { productCode = value; } }
        public decimal ProductPrice { get { return productPrice; } set { if (value > 0) productPrice = value;
                else log.Warn("Incorrect price"); } }

        public ICollection<OrderDetails> ListOrderDetails { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public int CompareTo(Object obj)
        {
            return ProductCode - ((Product)obj).ProductCode;
        }
        public Product()
        {
            ProductName = "";
            ProductId = 0;
            ProductCode = 0;
            ProductPrice = 0m;
         //   log.Info("Product " + name + " is created");
        }

        public Product(string name, int id, int code, decimal price, string company="")
        {
            ProductName = name;
            ProductId = id;
            ProductCode = code;
            ProductPrice = price;
            Company = company;
            log.Info("Product " + name + " is created");
        }

        public override string ToString()
        {
            return ProductName + "  " + ProductId + " " + ProductCode + " " + ProductPrice;
        }
    static    public Product GetRandomProduct(int n)
        {
            Random rnd = new Random(n);
            Product newProduct = new Product();
            newProduct.ProductName = "Product#" + rnd.Next(100, 1000);
            newProduct.ProductCode = rnd.Next(100000, 999999);
            newProduct.ProductPrice = (decimal)(rnd.Next(2000, 100000))/100;
            newProduct.Company = "Company#" + rnd.Next(100, 1000);
            return newProduct;

        }

    }
}

