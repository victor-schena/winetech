using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Contexts
{
  public class EntitiesDb : DbContext
  {
    public EntitiesDb()
      : base("DefaultConnection")
    {
    }
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
    public DbSet<HistoricoEstoque> HistoricoEstoque { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
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

      //one - to - many(Produto / HistoricoEstoque)
      modelBuilder.Entity<HistoricoEstoque>()
                    .HasRequired<Produto>(he => he.Produto)
                    .WithMany(p => p.HistoricoEstoque)
                    .HasForeignKey(he => he.ProdutoId);
      //many - to - many(Produto / Pedido)
      modelBuilder.Entity<Pedido>()
                .HasMany<Produto>(p => p.Produtos)
                .WithMany(u => u.Pedidos)
                .Map(cs =>
                {
                  cs.MapRightKey("ProdutoId");
                  cs.MapLeftKey("PedidoId");
                  cs.ToTable("ProdutoPedido");
                });
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
