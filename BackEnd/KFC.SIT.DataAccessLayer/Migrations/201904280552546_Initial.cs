namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
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
                        CategoryId = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ParentUser_Id = c.Int(),
                        SchoolId = c.Int(),
                        DepartmentId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.ParentUser_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactUserId = c.Int(nullable: false),
                        ContactUsername = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutgoingMessage = c.Boolean(nullable: false),
                        MessageContent = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
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
                        FriendId = c.Int(nullable: false),
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
                "dbo.SchoolTeacherCourseStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolTeacherCourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.ChatConnectionMappings",
                c => new
                    {
                        ConnectionId = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectionId);
            
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
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "StudentId", "dbo.Users");
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ParentUser_Id", "dbo.Users");
            DropForeignKey("dbo.FriendRelationships", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.Conversations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Users", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ClaimUsers", new[] { "User_Id" });
            DropIndex("dbo.ClaimUsers", new[] { "Claim_Id" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "StudentId" });
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.FriendRelationships", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ParentUser_Id" });
            DropIndex("dbo.Users", new[] { "CategoryId" });
            DropTable("dbo.ClaimUsers");
            DropTable("dbo.ChatConnectionMappings");
            DropTable("dbo.SchoolTeacherCourseStudents");
            DropTable("dbo.UserSessions");
            DropTable("dbo.FriendRelationships");
            DropTable("dbo.Claims");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}
