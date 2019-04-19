namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Another_First_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catergories",
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
                        FirstName = c.String(),
                        LastName = c.String(),
                        CatergoryId = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ParentUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catergories", t => t.CatergoryId)
                .ForeignKey("dbo.Users", t => t.ParentUser_Id)
                .Index(t => t.CatergoryId)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendRelationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        friendId = c.Int(nullable: false),
                        friendUsername = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                        RefreshedTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ExpirationTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ChatConnectionMappings",
                c => new
                    {
                        ConnectionId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.ConnectionId);
            
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
                "dbo.MessengerContactHists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderUserName = c.String(),
                        ReceiverUserName = c.String(),
                        ContactTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClaimUsers",
                c => new
                    {
                        Claim_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Claim_Id, t.User_Id })
                .ForeignKey("dbo.Claims", t => t.Claim_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Claim_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ParentUser_Id", "dbo.Users");
            DropForeignKey("dbo.FriendRelationships", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.Users", "CatergoryId", "dbo.Catergories");
            DropIndex("dbo.ClaimUsers", new[] { "User_Id" });
            DropIndex("dbo.ClaimUsers", new[] { "Claim_Id" });
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.FriendRelationships", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ParentUser_Id" });
            DropIndex("dbo.Users", new[] { "CatergoryId" });
            DropTable("dbo.ClaimUsers");
            DropTable("dbo.MessengerContactHists");
            DropTable("dbo.Conversations");
            DropTable("dbo.ChatConnectionMappings");
            DropTable("dbo.UserSessions");
            DropTable("dbo.FriendRelationships");
            DropTable("dbo.Claims");
            DropTable("dbo.Users");
            DropTable("dbo.Catergories");
        }
    }
}
