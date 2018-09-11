namespace Admin.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class FixCredentials : DbMigration
  {
    public override void Up()
    {
      DropForeignKey("dbo.CredentialRole", "CredentialId", "dbo.Credential");
      DropIndex("dbo.CredentialRole", new[] { "CredentialId" });
      DropPrimaryKey("dbo.Credential");
      DropPrimaryKey("dbo.CredentialRole");
      AlterColumn("dbo.Credential", "Id", c => c.Int(nullable: false, identity: true));
      AlterColumn("dbo.CredentialRole", "CredentialId", c => c.Int(nullable: false));
      AddPrimaryKey("dbo.Credential", "Id");
      AddPrimaryKey("dbo.CredentialRole", new[] { "CredentialId", "RoleId" });
      CreateIndex("dbo.CredentialRole", "CredentialId");
      AddForeignKey("dbo.CredentialRole", "CredentialId", "dbo.Credential", "Id", cascadeDelete: true);
    }

    public override void Down()
    {
      DropForeignKey("dbo.CredentialRole", "CredentialId", "dbo.Credential");
      DropIndex("dbo.CredentialRole", new[] { "CredentialId" });
      DropPrimaryKey("dbo.CredentialRole");
      DropPrimaryKey("dbo.Credential");
      AlterColumn("dbo.CredentialRole", "CredentialId", c => c.String(nullable: false, maxLength: 128));
      AlterColumn("dbo.Credential", "Id", c => c.String(nullable: false, maxLength: 128));
      AddPrimaryKey("dbo.CredentialRole", new[] { "CredentialId", "RoleId" });
      AddPrimaryKey("dbo.Credential", "Id");
      CreateIndex("dbo.CredentialRole", "CredentialId");
      AddForeignKey("dbo.CredentialRole", "CredentialId", "dbo.Credential", "Id", cascadeDelete: true);
    }
  }
}
