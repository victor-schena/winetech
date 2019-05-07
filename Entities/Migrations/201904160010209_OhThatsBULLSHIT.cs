namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OhThatsBULLSHIT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "HistoricoEstoqueId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "HistoricoEstoqueId", c => c.Int(nullable: false));
        }
    }
}
