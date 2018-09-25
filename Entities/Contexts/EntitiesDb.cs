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

      modelBuilder.Entity<Produto>()
                 .HasRequired<Classe>(pr => pr.Classe)
                 .WithMany(sa => sa.Produtos)
                 .HasForeignKey(sa => sa.ClasseId);

      modelBuilder.Entity<Produto>()
                 .HasRequired<Tipo>(pr => pr.Tipo)
                 .WithMany(sa => sa.Produtos)
                 .HasForeignKey(sa => sa.TipoId);


      //many-to-many (Produto/Pedido)
      modelBuilder.Entity<Produto>()
                   .HasMany<Pedido>(pr => pr.Pedidos)
                   .WithMany(pe => pe.Produtos)
                   .Map(up =>
                   {
                     up.MapLeftKey("PedidoId");
                     up.MapRightKey("ProdutoId");
                     up.ToTable("PedidoProduto");
                   });

      modelBuilder.Entity<Produto>()
                   .HasMany<Uva>(pr => pr.Uvas)
                   .WithMany(uv => uv.Produtos)
                   .Map(up =>
                   {
                     up.MapLeftKey("ProdutoId");
                     up.MapRightKey("UvaId");
                     up.ToTable("ProdutoUva");
                   });

      //modelBuilder.Entity<Produto>()
      //             .HasMany<Classe>(pr => pr.Classes)
      //             .WithMany(cl => cl.Produtos)
      //             .Map(up =>
      //             {
      //               up.MapLeftKey("ProdutoId");
      //               up.MapRightKey("ClasseId");
      //               up.ToTable("ProdutoClasse");
      //             });

      //modelBuilder.Entity<Produto>()
      //            .HasMany<Tipo>(pr => pr.Tipos)
      //            .WithMany(cl => cl.Produtos)
      //            .Map(up =>
      //            {
      //              up.MapLeftKey("ProdutoId");
      //              up.MapRightKey("TipoId");
      //              up.ToTable("ProdutoTipo");
      //            });

      base.OnModelCreating(modelBuilder);
    }
  }
}
