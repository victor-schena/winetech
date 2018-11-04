namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFlagForIssuedOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "isEmitido", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "isEmitido");
        }
    }
}
