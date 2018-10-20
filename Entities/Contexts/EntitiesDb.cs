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

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Safra> Safras { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Uva> Uvas { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Tipo> Tipos { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      //one-to-many (Endereco/Pessoa)
      modelBuilder.Entity<Endereco>()
                  .HasRequired<Pessoa>(e => e.Pessoa)
                  .WithMany(p => p.Enderecos)
                  .HasForeignKey(e => e.PessoaId);

      //one-to-many (Pedido/Pessoa)
      modelBuilder.Entity<Pedido>()
                  .HasRequired<Pessoa>(ped => ped.Pessoa)
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

      ////one-to-many (Produto/Pedido)
      //modelBuilder.Entity<Produto>()
      //             .HasOptional<Pedido>(pr => pr.Pedido) 
      //             .WithMany(pe => pe.Produtos)
      //             .HasForeignKey(pe => pe.PedidoId);

      modelBuilder.Entity<Produto>()
                .HasMany<Pedido>(p => p.Pedidos)
                .WithMany(u => u.Produtos)
                .Map(cs =>
                {
                  cs.MapLeftKey("ProdutoId");
                  cs.MapRightKey("PedidoId");
                  cs.ToTable("PedidoProduto");
                });

      modelBuilder.Entity<Produto>()
                .HasMany<Uva>(p => p.Uvas)
                .WithMany(u => u.Produtos)
                .Map(cs =>
                {
                  cs.MapLeftKey("ProdutoId");
                  cs.MapRightKey("UvaId");
                  cs.ToTable("ProdutoUva");
                });


      base.OnModelCreating(modelBuilder);
    }
  }
}
