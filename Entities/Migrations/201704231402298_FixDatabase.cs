namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 150),
                        Cidade = c.String(nullable: false, maxLength: 150),
                        Bairro = c.String(nullable: false, maxLength: 150),
                        CEP = c.String(nullable: false, maxLength: 150),
                        Rua = c.String(nullable: false, maxLength: 150),
                        Numero = c.String(nullable: false, maxLength: 150),
                        Complemento = c.String(maxLength: 150),
                        Status = c.Boolean(nullable: false),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(),
                        NomeFantasia = c.String(),
                        NomeCompleto = c.String(maxLength: 150),
                        RG = c.String(),
                        CPF = c.String(),
                        DataNascimento = c.DateTime(),
                        CNPJ = c.String(),
                        Email = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Status = c.Boolean(nullable: false),
                        LimiteCredito = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoPessoaId = c.Int(nullable: false),
                        PapelPessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataPedido = c.DateTime(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Descricao = c.String(nullable: false, maxLength: 150),
                        Uva = c.String(),
                        Classe = c.String(),
                        Teor_Alcolico = c.String(),
                        Tipo = c.String(),
                        CustoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataValidade = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        PaisId = c.Int(nullable: false),
                        SafraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pais", t => t.PaisId, cascadeDelete: true)
                .ForeignKey("dbo.Safra", t => t.SafraId, cascadeDelete: true)
                .Index(t => t.PaisId)
                .Index(t => t.SafraId);
            
            CreateTable(
                "dbo.Safra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ano = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PedidoProduto",
                c => new
                    {
                        PedidoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PedidoId, t.ProdutoId })
                .ForeignKey("dbo.Produtoes", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Pedido", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Endereco", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Produtoes", "SafraId", "dbo.Safra");
            DropForeignKey("dbo.PedidoProduto", "ProdutoId", "dbo.Pedido");
            DropForeignKey("dbo.PedidoProduto", "PedidoId", "dbo.Produtoes");
            DropForeignKey("dbo.Produtoes", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.PedidoProduto", new[] { "ProdutoId" });
            DropIndex("dbo.PedidoProduto", new[] { "PedidoId" });
            DropIndex("dbo.Produtoes", new[] { "SafraId" });
            DropIndex("dbo.Produtoes", new[] { "PaisId" });
            DropIndex("dbo.Pedido", new[] { "PessoaId" });
            DropIndex("dbo.Endereco", new[] { "PessoaId" });
            DropTable("dbo.PedidoProduto");
            DropTable("dbo.Safra");
            DropTable("dbo.Produtoes");
            DropTable("dbo.Pedido");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Endereco");
        }
    }
}
