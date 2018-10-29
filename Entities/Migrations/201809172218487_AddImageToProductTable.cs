namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Imagem", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "Imagem");
        }
    }
}
