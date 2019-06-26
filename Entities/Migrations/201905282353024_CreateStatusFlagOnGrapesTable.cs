namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStatusFlagOnGrapesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uva", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uva", "Status");
        }
    }
}
