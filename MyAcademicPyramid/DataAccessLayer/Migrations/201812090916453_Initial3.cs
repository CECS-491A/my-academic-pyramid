namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ParentUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ParentUser_Id)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Claim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Claim_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Claims", t => t.Claim_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Claim_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ParentUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.UserClaims", "User_Id", "dbo.Users");
            DropIndex("dbo.UserClaims", new[] { "Claim_Id" });
            DropIndex("dbo.UserClaims", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "ParentUser_Id" });
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Claims");
        }
    }
}
