namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTableForClasse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Decricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdutoClasse",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        ClasseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.ClasseId })
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Classe", t => t.ClasseId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.ClasseId);
            
            AddColumn("dbo.Produto", "ClasseId", c => c.Int(nullable: false));
            DropColumn("dbo.Produto", "Classe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produto", "Classe", c => c.String());
            DropForeignKey("dbo.ProdutoClasse", "ClasseId", "dbo.Classe");
            DropForeignKey("dbo.ProdutoClasse", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.ProdutoClasse", new[] { "ClasseId" });
            DropIndex("dbo.ProdutoClasse", new[] { "ProdutoId" });
            DropColumn("dbo.Produto", "ClasseId");
            DropTable("dbo.ProdutoClasse");
            DropTable("dbo.Classe");
        }
    }
}
