namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Cpf = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 150),
                        Senha = c.String(nullable: false, maxLength: 150),
                        Sexo = c.String(nullable: false, maxLength: 20),
                        Endereco = c.String(nullable: false, maxLength: 250),
                        Cep = c.String(nullable: false, maxLength: 10),
                        Cidade = c.String(nullable: false, maxLength: 50),
                        Estado = c.String(nullable: false, maxLength: 50),
                        DataNasc = c.DateTime(),
                        Telefone = c.String(nullable: false, maxLength: 20),
                        Celular = c.String(maxLength: 20),
                        DataCadastro = c.DateTime(),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Participante");
        }
    }
}
