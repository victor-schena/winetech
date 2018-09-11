namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTableProduto1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Volume", c => c.String(maxLength: 150));
            AlterColumn("dbo.Endereco", "CEP", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Safra", "Ano", c => c.String(maxLength: 4));
            DropColumn("dbo.Produto", "Quantidade");
            DropColumn("dbo.Produto", "Unidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produto", "Unidade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Produto", "Quantidade", c => c.Int(nullable: false));
            AlterColumn("dbo.Safra", "Ano", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Endereco", "CEP", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Produto", "Volume");
        }
    }
}
