namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovemanymanyInUseractions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserClaims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.UserUserActions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUserActions", "UserAction_Id", "dbo.UserActions");
            DropIndex("dbo.UserClaims", new[] { "User_Id" });
            DropIndex("dbo.UserClaims", new[] { "Claim_Id" });
            DropIndex("dbo.UserUserActions", new[] { "User_Id" });
            DropIndex("dbo.UserUserActions", new[] { "UserAction_Id" });
            AddColumn("dbo.Claims", "User_Id", c => c.Int());
            AddColumn("dbo.UserActions", "User_Id", c => c.Int());
            CreateIndex("dbo.Claims", "User_Id");
            CreateIndex("dbo.UserActions", "User_Id");
            AddForeignKey("dbo.Claims", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserActions", "User_Id", "dbo.Users", "Id");
            DropTable("dbo.UserClaims");
            DropTable("dbo.UserUserActions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserUserActions",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserAction_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserAction_Id });
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Claim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Claim_Id });
            
            DropForeignKey("dbo.UserActions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Claims", "User_Id", "dbo.Users");
            DropIndex("dbo.UserActions", new[] { "User_Id" });
            DropIndex("dbo.Claims", new[] { "User_Id" });
            DropColumn("dbo.UserActions", "User_Id");
            DropColumn("dbo.Claims", "User_Id");
            CreateIndex("dbo.UserUserActions", "UserAction_Id");
            CreateIndex("dbo.UserUserActions", "User_Id");
            CreateIndex("dbo.UserClaims", "Claim_Id");
            CreateIndex("dbo.UserClaims", "User_Id");
            AddForeignKey("dbo.UserUserActions", "UserAction_Id", "dbo.UserActions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserUserActions", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "Claim_Id", "dbo.Claims", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
