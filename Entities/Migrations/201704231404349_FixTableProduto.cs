namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTableProduto : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Produtoes", newName: "Produto");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Produto", newName: "Produtoes");
        }
    }
}
