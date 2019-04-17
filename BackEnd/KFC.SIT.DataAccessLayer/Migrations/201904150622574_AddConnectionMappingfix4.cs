namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionMappingfix4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String());
            AlterColumn("dbo.ChatConnectionMappings", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ChatConnectionMappings", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatConnectionMappings", "Username");
        }
    }
}
