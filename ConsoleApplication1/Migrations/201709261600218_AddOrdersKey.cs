namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdersKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "ClientID");
            AddForeignKey("dbo.Orders", "ClientID", "dbo.Clients", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ClientID", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientID" });
        }
    }
}
