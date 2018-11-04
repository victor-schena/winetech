namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Producao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataCriacao = c.DateTime(nullable: false),
                        TipoId = c.Int(nullable: false),
                        UvaId = c.Int(nullable: false),
                        ClasseId = c.Int(nullable: false),
                        Volume = c.String(),
                        KgUva = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KgAcucar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vasilhame = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classe", t => t.ClasseId, cascadeDelete: true)
                .ForeignKey("dbo.Tipo", t => t.TipoId, cascadeDelete: true)
                .ForeignKey("dbo.Uva", t => t.ClasseId, cascadeDelete: true)
                .Index(t => t.TipoId)
                .Index(t => t.ClasseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producao", "ClasseId", "dbo.Uva");
            DropForeignKey("dbo.Producao", "TipoId", "dbo.Tipo");
            DropForeignKey("dbo.Producao", "ClasseId", "dbo.Classe");
            DropIndex("dbo.Producao", new[] { "ClasseId" });
            DropIndex("dbo.Producao", new[] { "TipoId" });
            DropTable("dbo.Producao");
        }
    }
}
