using Admin.Models;
using Admin.Tables;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Admin.DataContexts
{
  public class IdentityDb : IdentityDbContext<ApplicationUser>
  {
    public IdentityDb()
      : base("DefaultConnection")
    {
    }

    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<Credential> Credentials { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Credential>()
            .HasMany<ApplicationRole>(n => n.Roles)
            .WithMany(t => t.Credentials)
            .Map(up =>
            {
              up.MapLeftKey("CredentialId");
              up.MapRightKey("RoleId");
              up.ToTable("CredentialRole");
            });
    }

    public static IdentityDb Create()
    {
      return new IdentityDb();
    }
  }
}