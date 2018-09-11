namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablePais : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pais");
        }
    }
}
