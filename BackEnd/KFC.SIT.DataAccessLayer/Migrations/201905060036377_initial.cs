namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        Text = c.String(),
                        HelpfulCount = c.Int(nullable: false),
                        UnHelpfulCount = c.Int(nullable: false),
                        SpamCount = c.Int(nullable: false),
                        IsCorrectAnswer = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        SsoId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Exp = c.Int(),
                        CategoryId = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ParentUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Accounts", t => t.ParentUser_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.ParentUser_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactUserId = c.Int(nullable: false),
                        ContactUsername = c.String(),
                        HasNewMessage = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.Accounts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false),
                        SchoolId2 = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        DepartmentId2 = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        PosterId = c.Int(nullable: false),
                        PosterUserName = c.String(),
                        Text = c.String(),
                        MinimumExpForAnswer = c.Int(nullable: false),
                        IsDraft = c.Boolean(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        SpamCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: false)
                .ForeignKey("dbo.Courses", t => new { t.CourseId, t.DepartmentId2 }, cascadeDelete: false)
                .ForeignKey("dbo.SchoolDepartments", t => new { t.DepartmentId, t.SchoolId2 }, cascadeDelete: false)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: false)
                .Index(t => new { t.DepartmentId, t.SchoolId2 })
                .Index(t => new { t.CourseId, t.DepartmentId2 })
                .Index(t => t.SchoolId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DepartmentId })
                .ForeignKey("dbo.SchoolDepartments", t => new { t.DepartmentId, t.SchoolId }, cascadeDelete: true)
                .Index(t => new { t.DepartmentId, t.SchoolId });
            
            CreateTable(
                "dbo.SchoolDepartments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DepartmentID, t.SchoolId })
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        SchoolId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SchoolId2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolId, t.TeacherId })
                .ForeignKey("dbo.SchoolDepartments", t => new { t.DepartmentId, t.SchoolId2 }, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.SchoolId)
                .Index(t => t.TeacherId)
                .Index(t => new { t.DepartmentId, t.SchoolId2 });
            
            CreateTable(
                "dbo.SchoolTeacherCourses",
                c => new
                    {
                        SchoolId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolId, t.TeacherId, t.CourseId, t.DepartmentId })
                .ForeignKey("dbo.Courses", t => new { t.CourseId, t.DepartmentId }, cascadeDelete: false)
                .ForeignKey("dbo.SchoolTeachers", t => new { t.SchoolId, t.TeacherId }, cascadeDelete: false)
                .Index(t => new { t.SchoolId, t.TeacherId })
                .Index(t => new { t.CourseId, t.DepartmentId });
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDepartments", t => new { t.DepartmentId, t.SchoolId }, cascadeDelete: true)
                .Index(t => new { t.DepartmentId, t.SchoolId })
                .Index(t => t.AccountId);
            
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
                .ForeignKey("dbo.Accounts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ChatConnectionMappings",
                c => new
                    {
                        ConnectionId = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectionId);
            
            CreateTable(
                "dbo.ClaimAccounts",
                c => new
                    {
                        Claim_Id = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Claim_Id, t.Account_Id })
                .ForeignKey("dbo.Claims", t => t.Claim_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Claim_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.StudentSchoolTeacherCourses",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        SchoolTeacherCourse_SchoolId = c.Int(nullable: false),
                        SchoolTeacherCourse_TeacherId = c.Int(nullable: false),
                        SchoolTeacherCourse_CourseId = c.Int(nullable: false),
                        SchoolTeacherCourse_DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.SchoolTeacherCourse_SchoolId, t.SchoolTeacherCourse_TeacherId, t.SchoolTeacherCourse_CourseId, t.SchoolTeacherCourse_DepartmentId })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.SchoolTeacherCourses", t => new { t.SchoolTeacherCourse_SchoolId, t.SchoolTeacherCourse_TeacherId, t.SchoolTeacherCourse_CourseId, t.SchoolTeacherCourse_DepartmentId }, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => new { t.SchoolTeacherCourse_SchoolId, t.SchoolTeacherCourse_TeacherId, t.SchoolTeacherCourse_CourseId, t.SchoolTeacherCourse_DepartmentId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.Questions", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Questions", new[] { "DepartmentId", "SchoolId2" }, "dbo.SchoolDepartments");
            DropForeignKey("dbo.Questions", new[] { "CourseId", "DepartmentId2" }, "dbo.Courses");
            DropForeignKey("dbo.Courses", new[] { "DepartmentId", "SchoolId" }, "dbo.SchoolDepartments");
            DropForeignKey("dbo.SchoolDepartments", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" }, "dbo.SchoolDepartments");
            DropForeignKey("dbo.Students", new[] { "DepartmentId", "SchoolId" }, "dbo.SchoolDepartments");
            DropForeignKey("dbo.StudentSchoolTeacherCourses", new[] { "SchoolTeacherCourse_SchoolId", "SchoolTeacherCourse_TeacherId", "SchoolTeacherCourse_CourseId", "SchoolTeacherCourse_DepartmentId" }, "dbo.SchoolTeacherCourses");
            DropForeignKey("dbo.StudentSchoolTeacherCourses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.SchoolTeacherCourses", new[] { "SchoolId", "TeacherId" }, "dbo.SchoolTeachers");
            DropForeignKey("dbo.SchoolTeacherCourses", new[] { "CourseId", "DepartmentId" }, "dbo.Courses");
            DropForeignKey("dbo.SchoolDepartments", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Questions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "ParentUser_Id", "dbo.Accounts");
            DropForeignKey("dbo.FriendRelationships", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.ClaimAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ClaimAccounts", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.Conversations", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Accounts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.StudentSchoolTeacherCourses", new[] { "SchoolTeacherCourse_SchoolId", "SchoolTeacherCourse_TeacherId", "SchoolTeacherCourse_CourseId", "SchoolTeacherCourse_DepartmentId" });
            DropIndex("dbo.StudentSchoolTeacherCourses", new[] { "Student_Id" });
            DropIndex("dbo.ClaimAccounts", new[] { "Account_Id" });
            DropIndex("dbo.ClaimAccounts", new[] { "Claim_Id" });
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.Students", new[] { "AccountId" });
            DropIndex("dbo.Students", new[] { "DepartmentId", "SchoolId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "CourseId", "DepartmentId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "SchoolId", "TeacherId" });
            DropIndex("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" });
            DropIndex("dbo.SchoolTeachers", new[] { "TeacherId" });
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolId" });
            DropIndex("dbo.SchoolDepartments", new[] { "SchoolId" });
            DropIndex("dbo.SchoolDepartments", new[] { "DepartmentID" });
            DropIndex("dbo.Courses", new[] { "DepartmentId", "SchoolId" });
            DropIndex("dbo.Questions", new[] { "AccountId" });
            DropIndex("dbo.Questions", new[] { "SchoolId" });
            DropIndex("dbo.Questions", new[] { "CourseId", "DepartmentId2" });
            DropIndex("dbo.Questions", new[] { "DepartmentId", "SchoolId2" });
            DropIndex("dbo.FriendRelationships", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "UserId" });
            DropIndex("dbo.Accounts", new[] { "ParentUser_Id" });
            DropIndex("dbo.Accounts", new[] { "CategoryId" });
            DropIndex("dbo.Answers", new[] { "AccountId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.StudentSchoolTeacherCourses");
            DropTable("dbo.ClaimAccounts");
            DropTable("dbo.ChatConnectionMappings");
            DropTable("dbo.UserSessions");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.SchoolTeacherCourses");
            DropTable("dbo.SchoolTeachers");
            DropTable("dbo.Schools");
            DropTable("dbo.Departments");
            DropTable("dbo.SchoolDepartments");
            DropTable("dbo.Courses");
            DropTable("dbo.Questions");
            DropTable("dbo.FriendRelationships");
            DropTable("dbo.Claims");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.Categories");
            DropTable("dbo.Accounts");
            DropTable("dbo.Answers");
        }
    }
}
