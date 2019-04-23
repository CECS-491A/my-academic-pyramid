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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.ParentUser_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.ChatHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        ContactUsername = c.String(),
                        ContactTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                .ForeignKey("dbo.SchoolTeacherCourses", t => t.SchoolTeacherCourseId, cascadeDelete: true)
                .Index(t => t.SchoolTeacherCourseId)
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
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        MessageContent = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SchoolDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolTeacherCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolTeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolTeachers", t => t.SchoolTeacherId, cascadeDelete: true)
                .Index(t => t.SchoolTeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .Index(t => t.SchoolId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        EmailDomain = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        SchoolDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.SchoolId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
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
            DropForeignKey("dbo.SchoolTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolTeacherCourses", "SchoolTeacherId", "dbo.SchoolTeachers");
            DropForeignKey("dbo.SchoolDepartments", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SchoolTeacherCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "SchoolTeacherCourseId", "dbo.SchoolTeacherCourses");
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "StudentId", "dbo.Users");
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ParentUser_Id", "dbo.Users");
            DropForeignKey("dbo.FriendRelationships", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.ChatHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ClaimUsers", new[] { "User_Id" });
            DropIndex("dbo.ClaimUsers", new[] { "Claim_Id" });
            DropIndex("dbo.SchoolTeachers", new[] { "TeacherId" });
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolId" });
            DropIndex("dbo.SchoolDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.SchoolDepartments", new[] { "SchoolId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "CourseId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "SchoolTeacherId" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "StudentId" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "SchoolTeacherCourseId" });
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.FriendRelationships", new[] { "UserId" });
            DropIndex("dbo.ChatHistories", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ParentUser_Id" });
            DropIndex("dbo.Users", new[] { "CategoryId" });
            DropTable("dbo.ClaimUsers");
            DropTable("dbo.Teachers");
            DropTable("dbo.SchoolTeachers");
            DropTable("dbo.Schools");
            DropTable("dbo.SchoolDepartments");
            DropTable("dbo.Departments");
            DropTable("dbo.SchoolTeacherCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.Conversations");
            DropTable("dbo.ChatConnectionMappings");
            DropTable("dbo.SchoolTeacherCourseStudents");
            DropTable("dbo.UserSessions");
            DropTable("dbo.FriendRelationships");
            DropTable("dbo.Claims");
            DropTable("dbo.ChatHistories");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}
