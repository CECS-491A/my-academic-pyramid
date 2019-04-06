namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDbConxt : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserActionUsers", newName: "UserUserActions");
            DropPrimaryKey("dbo.UserUserActions");
            AddPrimaryKey("dbo.UserUserActions", new[] { "User_Id", "UserAction_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserUserActions");
            AddPrimaryKey("dbo.UserUserActions", new[] { "UserAction_Id", "User_Id" });
            RenameTable(name: "dbo.UserUserActions", newName: "UserActionUsers");
        }
    }
}
