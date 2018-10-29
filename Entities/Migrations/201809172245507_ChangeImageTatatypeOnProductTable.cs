namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeImageTatatypeOnProductTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "Imagem", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "Imagem", c => c.Binary());
        }
    }
}
