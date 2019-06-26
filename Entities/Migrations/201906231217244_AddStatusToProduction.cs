namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToProduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producao", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producao", "Status");
        }
    }
}
