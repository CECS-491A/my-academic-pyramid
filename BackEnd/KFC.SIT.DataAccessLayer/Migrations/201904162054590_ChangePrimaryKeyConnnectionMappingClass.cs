namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePrimaryKeyConnnectionMappingClass : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String());
            AlterColumn("dbo.ChatConnectionMappings", "ConnectionId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatConnectionMappings", "ConnectionId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "ConnectionId", c => c.String());
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatConnectionMappings", "Username");
        }
    }
}
