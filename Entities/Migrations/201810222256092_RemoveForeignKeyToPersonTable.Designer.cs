// <auto-generated />
namespace Entities.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class RemoveForeignKeyToPersonTable : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(RemoveForeignKeyToPersonTable));
        
        string IMigrationMetadata.Id
        {
            get { return "201810222256092_RemoveForeignKeyToPersonTable"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
