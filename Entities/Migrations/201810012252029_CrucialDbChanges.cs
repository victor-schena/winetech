namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrucialDbChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PedidoProduto", "PedidoId", "dbo.Produto");
            DropForeignKey("dbo.PedidoProduto", "ProdutoId", "dbo.Pedido");
            DropForeignKey("dbo.ProdutoUva", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.ProdutoUva", "UvaId", "dbo.Uva");
            DropIndex("dbo.PedidoProduto", new[] { "PedidoId" });
            DropIndex("dbo.PedidoProduto", new[] { "ProdutoId" });
            DropIndex("dbo.ProdutoUva", new[] { "ProdutoId" });
            DropIndex("dbo.ProdutoUva", new[] { "UvaId" });
            AddColumn("dbo.Produto", "PedidoId", c => c.Int());
            AddColumn("dbo.Uva", "Produto_Id", c => c.Int());
            AlterColumn("dbo.Produto", "Descricao", c => c.String(nullable: false));
            CreateIndex("dbo.Produto", "PedidoId");
            CreateIndex("dbo.Produto", "UvaId");
            CreateIndex("dbo.Uva", "Produto_Id");
            AddForeignKey("dbo.Produto", "PedidoId", "dbo.Pedido", "Id");
            AddForeignKey("dbo.Produto", "UvaId", "dbo.Uva", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Uva", "Produto_Id", "dbo.Produto", "Id");
            DropColumn("dbo.Produto", "Uva");
            DropTable("dbo.PedidoProduto");
            DropTable("dbo.ProdutoUva");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProdutoUva",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        UvaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.UvaId });
            
            CreateTable(
                "dbo.PedidoProduto",
                c => new
                    {
                        PedidoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PedidoId, t.ProdutoId });
            
            AddColumn("dbo.Produto", "Uva", c => c.String());
            DropForeignKey("dbo.Uva", "Produto_Id", "dbo.Produto");
            DropForeignKey("dbo.Produto", "UvaId", "dbo.Uva");
            DropForeignKey("dbo.Produto", "PedidoId", "dbo.Pedido");
            DropIndex("dbo.Uva", new[] { "Produto_Id" });
            DropIndex("dbo.Produto", new[] { "UvaId" });
            DropIndex("dbo.Produto", new[] { "PedidoId" });
            AlterColumn("dbo.Produto", "Descricao", c => c.String(nullable: false, maxLength: 2000));
            DropColumn("dbo.Uva", "Produto_Id");
            DropColumn("dbo.Produto", "PedidoId");
            CreateIndex("dbo.ProdutoUva", "UvaId");
            CreateIndex("dbo.ProdutoUva", "ProdutoId");
            CreateIndex("dbo.PedidoProduto", "ProdutoId");
            CreateIndex("dbo.PedidoProduto", "PedidoId");
            AddForeignKey("dbo.ProdutoUva", "UvaId", "dbo.Uva", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProdutoUva", "ProdutoId", "dbo.Produto", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PedidoProduto", "ProdutoId", "dbo.Pedido", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PedidoProduto", "PedidoId", "dbo.Produto", "Id", cascadeDelete: true);
        }
    }
}
