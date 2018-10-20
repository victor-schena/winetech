namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyRelationshipProductGrape : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produto", "UvaId", "dbo.Uva");
            DropIndex("dbo.Produto", new[] { "UvaId" });
            RenameColumn(table: "dbo.Produto", name: "UvaId", newName: "Uva_Id");
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
            
            AlterColumn("dbo.Produto", "Uva_Id", c => c.Int());
            CreateIndex("dbo.Produto", "Uva_Id");
            AddForeignKey("dbo.Produto", "Uva_Id", "dbo.Uva", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produto", "Uva_Id", "dbo.Uva");
            DropForeignKey("dbo.ProdutoUva", "UvaId", "dbo.Uva");
            DropForeignKey("dbo.ProdutoUva", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.ProdutoUva", new[] { "UvaId" });
            DropIndex("dbo.ProdutoUva", new[] { "ProdutoId" });
            DropIndex("dbo.Produto", new[] { "Uva_Id" });
            AlterColumn("dbo.Produto", "Uva_Id", c => c.Int(nullable: false));
            DropTable("dbo.ProdutoUva");
            RenameColumn(table: "dbo.Produto", name: "Uva_Id", newName: "UvaId");
            CreateIndex("dbo.Produto", "UvaId");
            AddForeignKey("dbo.Produto", "UvaId", "dbo.Uva", "Id", cascadeDelete: true);
        }
    }
}
