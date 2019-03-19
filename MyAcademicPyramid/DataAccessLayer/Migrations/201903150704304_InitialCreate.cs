namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        Firstname = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Location = c.String(),
                        PasswordHash = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        ParentUser_Id = c.Int(),
                        PasswordQuestion1 = c.String(),
                        PasswordQuestion2 = c.String(),
                        PasswordQuestion3 = c.String(),
                        PasswordAnswer1 = c.String(),
                        PasswordAnswer2 = c.String(),
                        PasswordAnswer3 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ParentUser_Id)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderUserName = c.String(),
                        ReceiverUserName = c.String(),
                        MessageContent = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropTable("dbo.Conversations");
            DropTable("dbo.Users");
            DropTable("dbo.Claims");
        }
    }
}
