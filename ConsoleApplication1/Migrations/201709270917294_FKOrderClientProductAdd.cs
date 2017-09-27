namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKOrderClientProductAdd : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OrderDetails", "OrderID");
            CreateIndex("dbo.OrderDetails", "ProductID");
            AddForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
        }
    }
}
