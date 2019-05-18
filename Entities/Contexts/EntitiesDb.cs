using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Contexts
{
  public class EntitiesDb : DbContext
  {
    public EntitiesDb()
      : base("DefaultConnection") { }
    public DbSet<Producao> Producao { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Safra> Safras { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Uva> Uvas { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Tipo> Tipos { get; set; }
    public DbSet<PedidoProduto> PedidosProdutos { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      //one-to-many (Endereco/Pessoa)
      modelBuilder.Entity<Endereco>()
                  .HasRequired<Pessoa>(e => e.Pessoa)
                  .WithMany(p => p.Enderecos)
                  .HasForeignKey(e => e.PessoaId);

      //one-to-many (Pedido/Pessoa)
      modelBuilder.Entity<Pedido>()
                  .HasOptional<Pessoa>(ped => ped.Pessoa)
                  .WithMany(pes => pes.Pedidos)
                  .HasForeignKey(ped => ped.PessoaId);

      //modelBuilder.Entity<Pedido>()
      //            .HasRequired<ApplicationUser>(pr => pr.User)
      //            .WithMany(pa => pa.Produtos)
      //            .HasForeignKey(pr => pr.PaisId);

      //one - to - many(Produto / Pais)
      modelBuilder.Entity<Produto>()
                  .HasRequired<Pais>(pr => pr.Pais)
                  .WithMany(pa => pa.Produtos)
                  .HasForeignKey(pr => pr.PaisId);

      //one - to - many(Produto / Safra)
      modelBuilder.Entity<Produto>()
                  .HasRequired<Safra>(pr => pr.Safra)
                  .WithMany(sa => sa.Produtos)
                  .HasForeignKey(sa => sa.SafraId);

      //one-to-many
      modelBuilder.Entity<Produto>()
                 .HasRequired<Classe>(pr => pr.Classe)
                 .WithMany(sa => sa.Produtos)
                 .HasForeignKey(sa => sa.ClasseId);

      //one-to-many
      modelBuilder.Entity<Produto>()
                 .HasRequired<Tipo>(pr => pr.Tipo)
                 .WithMany(sa => sa.Produtos)
                 .HasForeignKey(sa => sa.TipoId);

      //modelBuilder.Entity<Pedido>().HasKey(x => x.Id);
      //modelBuilder.Entity<Produto>().HasKey(x => x.Id);
      //modelBuilder.Entity<PedidoProduto>().HasKey(x =>
      //    new
      //    {
      //      x.PedidoId,
      //      x.ProdutoId
      //    });

      //modelBuilder.Entity<PedidoProduto>()
      //    .HasRequired(x => x.Pedido)
      //    .WithMany(x => x.PedidosProdutos)
      //    .HasForeignKey(x => x.PedidoId);

      //modelBuilder.Entity<PedidoProduto>()
      //    .HasRequired(x => x.Produto)
      //    .WithMany(x => x.PedidosProdutos)
      //    .HasForeignKey(x => x.ProdutoId);


      //  modelBuilder.Entity<Pedido>()
      //    .HasMany(x => x.Produtos)
      //    .WithMany(x => x.Pedidos)
      //.Map(x =>
      //{
      //  x.ToTable("PedidoProduto"); // third table is named Cookbooks
      //  x.MapLeftKey("PedidoId");
      //  x.MapRightKey("ProdutoId");
      //});

      //many-to-many(Produto/Uva)
      modelBuilder.Entity<Produto>()
                .HasMany<Uva>(p => p.Uvas)
                .WithMany(u => u.Produtos)
                .Map(cs =>
                {
                  cs.MapLeftKey("ProdutoId");
                  cs.MapRightKey("UvaId");
                  cs.ToTable("ProdutoUva");
                });

      //
      modelBuilder.Entity<Producao>()
                .HasRequired<Tipo>(pr => pr.Tipo)
                .WithMany(sa => sa.Producoes)
                .HasForeignKey(sa => sa.TipoId);
      //
      modelBuilder.Entity<Producao>()
                 .HasRequired<Classe>(pr => pr.Classe)
                 .WithMany(sa => sa.Producoes)
                 .HasForeignKey(sa => sa.ClasseId);

      modelBuilder.Entity<Producao>()
                .HasRequired<Uva>(pr => pr.Uva)
                .WithMany(sa => sa.Producoes)
                .HasForeignKey(sa => sa.UvaId);

      base.OnModelCreating(modelBuilder);
    }
  }
}
