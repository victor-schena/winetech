namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableForStockHistoryControl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricoEstoque",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ajuste = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        CriadoEm = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
            AddColumn("dbo.Produto", "HistoricoEstoqueId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricoEstoque", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.HistoricoEstoque", new[] { "ProdutoId" });
            DropColumn("dbo.Produto", "HistoricoEstoqueId");
            DropTable("dbo.HistoricoEstoque");
        }
    }
}
