namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProductIdFromGrapeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Uva", "Produto_Id", "dbo.Produto");
            DropIndex("dbo.Uva", new[] { "Produto_Id" });
            DropColumn("dbo.Uva", "ProdutoId");
            DropColumn("dbo.Uva", "Produto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uva", "Produto_Id", c => c.Int());
            AddColumn("dbo.Uva", "ProdutoId", c => c.Int());
            CreateIndex("dbo.Uva", "Produto_Id");
            AddForeignKey("dbo.Uva", "Produto_Id", "dbo.Produto", "Id");
        }
    }
}
