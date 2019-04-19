namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionMappingfix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatConnectionMappings", "username", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatConnectionMappings", "username");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "username", c => c.String());
            AlterColumn("dbo.ChatConnectionMappings", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ChatConnectionMappings", "Id");
        }
    }
}
