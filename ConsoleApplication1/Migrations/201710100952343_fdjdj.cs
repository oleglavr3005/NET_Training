namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdjdj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "ProductQuantity", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "ProductPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "ProductQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
