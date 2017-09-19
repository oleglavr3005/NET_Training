using System;

public class Product : ICloneable, IComparable
{
    public string ProductName { get; set; }
    public string ProductId { get; set; }
    public int ProductCode { get; set; }
    public decimal ProductPrice { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }


    public int CompareTo(Object obj)
    {
        return ProductCode - ((Product)obj).ProductCode;
    }

    public Product(string name, string id, int code, decimal price)
    {
        ProductName = name;
        ProductId = id;
        ProductCode = code;
        ProductPrice = price;
    }

    public override string ToString()
    {
        return ProductName + "  " + ProductId+" "+ProductCode+" "+ProductPrice;
    }
}

