namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableUvaToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uva",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdutoUva",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        UvaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.UvaId })
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Uva", t => t.UvaId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.UvaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutoUva", "UvaId", "dbo.Uva");
            DropForeignKey("dbo.ProdutoUva", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.ProdutoUva", new[] { "UvaId" });
            DropIndex("dbo.ProdutoUva", new[] { "ProdutoId" });
            DropTable("dbo.ProdutoUva");
            DropTable("dbo.Uva");
        }
    }
}
