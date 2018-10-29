namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUvaIdColumnToProductsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "UvaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "UvaId");
        }
    }
}
