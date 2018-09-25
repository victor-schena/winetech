namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropManyTomanytables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProdutoClasse", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.ProdutoClasse", "ClasseId", "dbo.Classe");
            DropForeignKey("dbo.ProdutoTipo", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.ProdutoTipo", "TipoId", "dbo.Tipo");
            DropIndex("dbo.ProdutoClasse", new[] { "ProdutoId" });
            DropIndex("dbo.ProdutoClasse", new[] { "ClasseId" });
            DropIndex("dbo.ProdutoTipo", new[] { "ProdutoId" });
            DropIndex("dbo.ProdutoTipo", new[] { "TipoId" });
            CreateIndex("dbo.Produto", "ClasseId");
            AddForeignKey("dbo.Produto", "ClasseId", "dbo.Classe", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Produto", "ClasseId", "dbo.Tipo", "Id", cascadeDelete: true);
            DropColumn("dbo.Produto", "Tipo");
            DropTable("dbo.ProdutoClasse");
            DropTable("dbo.ProdutoTipo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProdutoTipo",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.TipoId });
            
            CreateTable(
                "dbo.ProdutoClasse",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        ClasseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.ClasseId });
            
            AddColumn("dbo.Produto", "Tipo", c => c.String());
            DropForeignKey("dbo.Produto", "ClasseId", "dbo.Tipo");
            DropForeignKey("dbo.Produto", "ClasseId", "dbo.Classe");
            DropIndex("dbo.Produto", new[] { "ClasseId" });
            CreateIndex("dbo.ProdutoTipo", "TipoId");
            CreateIndex("dbo.ProdutoTipo", "ProdutoId");
            CreateIndex("dbo.ProdutoClasse", "ClasseId");
            CreateIndex("dbo.ProdutoClasse", "ProdutoId");
            AddForeignKey("dbo.ProdutoTipo", "TipoId", "dbo.Tipo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProdutoTipo", "ProdutoId", "dbo.Produto", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProdutoClasse", "ClasseId", "dbo.Classe", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProdutoClasse", "ProdutoId", "dbo.Produto", "Id", cascadeDelete: true);
        }
    }
}
