namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnProductId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uva", "ProdutoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uva", "ProdutoId");
        }
    }
}
