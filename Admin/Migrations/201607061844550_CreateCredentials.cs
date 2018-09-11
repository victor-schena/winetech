namespace Admin.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class CreateCredentials : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Credential",
          c => new
              {
                Id = c.String(nullable: false, maxLength: 128),
                Controller = c.String(),
                Action = c.String(),
                Param = c.String(),
                Descr = c.String(),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.CredentialRole",
          c => new
              {
                CredentialId = c.String(nullable: false, maxLength: 128),
                RoleId = c.String(nullable: false, maxLength: 128),
              })
          .PrimaryKey(t => new { t.CredentialId, t.RoleId })
          .ForeignKey("dbo.Credential", t => t.CredentialId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
          .Index(t => t.CredentialId)
          .Index(t => t.RoleId);

    }

    public override void Down()
    {
      DropForeignKey("dbo.CredentialRole", "RoleId", "dbo.AspNetRoles");
      DropForeignKey("dbo.CredentialRole", "CredentialId", "dbo.Credential");
      DropIndex("dbo.CredentialRole", new[] { "RoleId" });
      DropIndex("dbo.CredentialRole", new[] { "CredentialId" });
      DropTable("dbo.CredentialRole");
      DropTable("dbo.Credential");
    }
  }
}
