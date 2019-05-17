namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "UserId");
        }
    }
}
