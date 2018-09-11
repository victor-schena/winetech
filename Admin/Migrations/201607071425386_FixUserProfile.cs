namespace Admin.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class FixUserProfile : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.AspNetUsers", "Name", c => c.String());
      AddColumn("dbo.AspNetUsers", "Img", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.AspNetUsers", "Img");
      DropColumn("dbo.AspNetUsers", "Name");
    }
  }
}
