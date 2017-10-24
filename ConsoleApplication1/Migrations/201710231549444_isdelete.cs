namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isdelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "IsDeleted");
        }
    }
}
