namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyProductOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produto", "PedidoId", "dbo.Pedido");
            DropIndex("dbo.Produto", new[] { "PedidoId" });
            CreateTable(
                "dbo.PedidoProduto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        PedidoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.PedidoId })
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Pedido", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.PedidoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoProduto", "PedidoId", "dbo.Pedido");
            DropForeignKey("dbo.PedidoProduto", "ProdutoId", "dbo.Produto");
            DropIndex("dbo.PedidoProduto", new[] { "PedidoId" });
            DropIndex("dbo.PedidoProduto", new[] { "ProdutoId" });
            DropTable("dbo.PedidoProduto");
            CreateIndex("dbo.Produto", "PedidoId");
            AddForeignKey("dbo.Produto", "PedidoId", "dbo.Pedido", "Id");
        }
    }
}
