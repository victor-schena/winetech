namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderIdToStockQuantityTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricoEstoque", "PedidoId", c => c.Int());
            AddColumn("dbo.Pedido", "HistoricoEstoqueId", c => c.Int());
            CreateIndex("dbo.HistoricoEstoque", "PedidoId");
            AddForeignKey("dbo.HistoricoEstoque", "PedidoId", "dbo.Pedido", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricoEstoque", "PedidoId", "dbo.Pedido");
            DropIndex("dbo.HistoricoEstoque", new[] { "PedidoId" });
            DropColumn("dbo.Pedido", "HistoricoEstoqueId");
            DropColumn("dbo.HistoricoEstoque", "PedidoId");
        }
    }
}
