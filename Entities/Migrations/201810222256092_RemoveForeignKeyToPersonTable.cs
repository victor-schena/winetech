namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeignKeyToPersonTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.Pedido", new[] { "PessoaId" });
            AlterColumn("dbo.Pedido", "PessoaId", c => c.Int());
            CreateIndex("dbo.Pedido", "PessoaId");
            AddForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.Pedido", new[] { "PessoaId" });
            AlterColumn("dbo.Pedido", "PessoaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pedido", "PessoaId");
            AddForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa", "Id", cascadeDelete: true);
        }
    }
}
