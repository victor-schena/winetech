namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedManyToManyRelantionship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PedidoProduto", name: "ProdutoId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.PedidoProduto", name: "PedidoId", newName: "ProdutoId");
            RenameColumn(table: "dbo.PedidoProduto", name: "__mig_tmp__0", newName: "PedidoId");
            RenameIndex(table: "dbo.PedidoProduto", name: "IX_ProdutoId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.PedidoProduto", name: "IX_PedidoId", newName: "IX_ProdutoId");
            RenameIndex(table: "dbo.PedidoProduto", name: "__mig_tmp__0", newName: "IX_PedidoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PedidoProduto", name: "IX_PedidoId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.PedidoProduto", name: "IX_ProdutoId", newName: "IX_PedidoId");
            RenameIndex(table: "dbo.PedidoProduto", name: "__mig_tmp__0", newName: "IX_ProdutoId");
            RenameColumn(table: "dbo.PedidoProduto", name: "PedidoId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.PedidoProduto", name: "ProdutoId", newName: "PedidoId");
            RenameColumn(table: "dbo.PedidoProduto", name: "__mig_tmp__0", newName: "ProdutoId");
        }
    }
}
