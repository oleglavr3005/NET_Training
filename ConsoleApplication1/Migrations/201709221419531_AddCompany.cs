namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Company");
        }
    }
}
