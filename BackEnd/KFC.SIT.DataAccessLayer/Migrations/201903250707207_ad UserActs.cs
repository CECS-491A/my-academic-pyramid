namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adUserActs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserActionUsers",
                c => new
                    {
                        UserAction_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserAction_Id, t.User_Id })
                .ForeignKey("dbo.UserActions", t => t.UserAction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.UserAction_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActionUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserActionUsers", "UserAction_Id", "dbo.UserActions");
            DropIndex("dbo.UserActionUsers", new[] { "User_Id" });
            DropIndex("dbo.UserActionUsers", new[] { "UserAction_Id" });
            DropTable("dbo.UserActionUsers");
            DropTable("dbo.UserActions");
        }
    }
}
