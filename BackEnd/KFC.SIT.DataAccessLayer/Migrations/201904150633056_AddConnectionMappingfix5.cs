namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionMappingfix5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatConnectionMappings", "Username");
            DropColumn("dbo.ChatConnectionMappings", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatConnectionMappings", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ChatConnectionMappings");
            AlterColumn("dbo.ChatConnectionMappings", "Username", c => c.String());
            AddPrimaryKey("dbo.ChatConnectionMappings", "Id");
        }
    }
}
