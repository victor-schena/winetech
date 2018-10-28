namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvertFieldsOnProductOrderTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PedidoProduto", newName: "ProdutoPedido");
            RenameColumn(table: "dbo.ProdutoPedido", name: "PedidoId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ProdutoPedido", name: "ProdutoId", newName: "PedidoId");
            RenameColumn(table: "dbo.ProdutoPedido", name: "__mig_tmp__0", newName: "ProdutoId");
            RenameIndex(table: "dbo.ProdutoPedido", name: "IX_ProdutoId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ProdutoPedido", name: "IX_PedidoId", newName: "IX_ProdutoId");
            RenameIndex(table: "dbo.ProdutoPedido", name: "__mig_tmp__0", newName: "IX_PedidoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProdutoPedido", name: "IX_PedidoId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ProdutoPedido", name: "IX_ProdutoId", newName: "IX_PedidoId");
            RenameIndex(table: "dbo.ProdutoPedido", name: "__mig_tmp__0", newName: "IX_ProdutoId");
            RenameColumn(table: "dbo.ProdutoPedido", name: "ProdutoId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ProdutoPedido", name: "PedidoId", newName: "ProdutoId");
            RenameColumn(table: "dbo.ProdutoPedido", name: "__mig_tmp__0", newName: "PedidoId");
            RenameTable(name: "dbo.ProdutoPedido", newName: "PedidoProduto");
        }
    }
}
