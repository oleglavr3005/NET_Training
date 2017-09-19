﻿using log4net;
using System;
namespace ConsoleApplication1
{
    [UseForEqualityCheck("productName", "productId")]
    public class Product : ICloneable, IComparable
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Product));
        private string productName;
        private int productId;
        private int productCode;
        private decimal productPrice;
        public string ProductName { get { return productName; } set { productName = value; }  }
        public int ProductId { get { return productId; } set { productId = value; } }
        public int ProductCode { get { return productCode; } set { productCode = value; } }
        public decimal ProductPrice { get { return productPrice; } set { productPrice = value; } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public int CompareTo(Object obj)
        {
            return ProductCode - ((Product)obj).ProductCode;
        }

        public Product(string name, int id, int code, decimal price)
        {
            ProductName = name;
            ProductId = id;
            ProductCode = code;
            ProductPrice = price;
            log.Info("Product " + name + " is created");
        }

        public override string ToString()
        {
            return ProductName + "  " + ProductId + " " + ProductCode + " " + ProductPrice;
        }
    }
}

