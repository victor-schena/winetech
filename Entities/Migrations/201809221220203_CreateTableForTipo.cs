namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableForTipo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdutoTipo",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.TipoId })
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Tipo", t => t.TipoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.TipoId);
            
            AddColumn("dbo.Classe", "Descricao", c => c.String());
            AddColumn("dbo.Produto", "TipoId", c => c.Int(nullable: false));
            DropColumn("dbo.Classe", "Decricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classe", "Decricao", c => c.String());
            DropForeignKey("dbo.ProdutoTipo", "TipoId", "dbo.Tipo");
            DropForeignKey("dbo.ProdutoTipo", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.ProdutoTipo", new[] { "TipoId" });
            DropIndex("dbo.ProdutoTipo", new[] { "ProdutoId" });
            DropColumn("dbo.Produto", "TipoId");
            DropColumn("dbo.Classe", "Descricao");
            DropTable("dbo.ProdutoTipo");
            DropTable("dbo.Tipo");
        }
    }
}
