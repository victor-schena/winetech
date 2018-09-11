namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusToAllTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pais", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Safra", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Safra", "Status");
            DropColumn("dbo.Pais", "Status");
        }
    }
}
