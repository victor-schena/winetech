namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyBetweenProductAndOrderTwice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.Uva", t => t.UvaId, cascadeDelete: true)
                .Index(t => t.TipoId)
                .Index(t => t.UvaId)
                .Index(t => t.ClasseId);
            
            CreateTable(
                "dbo.Tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imagem = c.String(),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Descricao = c.String(nullable: false),
                        Teor_Alcolico = c.String(),
                        CustoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        Volume = c.String(maxLength: 100),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataValidade = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        PaisId = c.Int(nullable: false),
                        SafraId = c.Int(nullable: false),
                        PedidoId = c.Int(),
                        ClasseId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Uva_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classe", t => t.ClasseId, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.PaisId, cascadeDelete: true)
                .ForeignKey("dbo.Safra", t => t.SafraId, cascadeDelete: true)
                .ForeignKey("dbo.Tipo", t => t.TipoId, cascadeDelete: true)
                .ForeignKey("dbo.Uva", t => t.Uva_Id)
                .Index(t => t.PaisId)
                .Index(t => t.SafraId)
                .Index(t => t.ClasseId)
                .Index(t => t.TipoId)
                .Index(t => t.Uva_Id);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isVenda = c.Boolean(),
                        isPessoaFisica = c.Boolean(),
                        isEmitido = c.Boolean(),
                        DataPedido = c.DateTime(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Total = c.Decimal(precision: 18, scale: 2),
                        PessoaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.PedidoProduto",
                c => new
                    {
                        PedidoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PedidoId, t.ProdutoId })
                .ForeignKey("dbo.Pedido", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.ProdutoId);
            
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
                        TipoPessoaId = c.Int(nullable: false),
                        PapelPessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 150),
                        Cidade = c.String(nullable: false, maxLength: 150),
                        Bairro = c.String(nullable: false, maxLength: 150),
                        CEP = c.String(nullable: false, maxLength: 8),
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
                "dbo.Safra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ano = c.String(maxLength: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uva",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PedidoProduto1",
                c => new
                    {
                        Pedido_Id = c.Int(nullable: false),
                        Produto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pedido_Id, t.Produto_Id })
                .ForeignKey("dbo.Pedido", t => t.Pedido_Id, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.Produto_Id, cascadeDelete: true)
                .Index(t => t.Pedido_Id)
                .Index(t => t.Produto_Id);
            
            CreateTable(
                "dbo.ProdutoUva",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        UvaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.UvaId })
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Uva", t => t.UvaId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.UvaId);
            
        }
        
        public override void Down()
        {
          
        }
    }
}
