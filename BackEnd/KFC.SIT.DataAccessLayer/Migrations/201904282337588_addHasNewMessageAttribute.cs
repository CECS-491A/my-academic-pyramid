namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHasNewMessageAttribute : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Conversations", "HasNewMessage", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Users", "SchoolId");
            CreateIndex("dbo.SchoolTeacherCourseStudents", "SchoolTeacherCourseId");
            AddForeignKey("dbo.SchoolTeacherCourseStudents", "SchoolTeacherCourseId", "dbo.SchoolTeacherCourses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "SchoolId", "dbo.Schools", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolTeacherCourses", "SchoolTeacherId", "dbo.SchoolTeachers");
            DropForeignKey("dbo.Users", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolDepartments", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SchoolTeacherCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "SchoolTeacherCourseId", "dbo.SchoolTeacherCourses");
            DropIndex("dbo.SchoolTeachers", new[] { "TeacherId" });
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolId" });
            DropIndex("dbo.SchoolDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.SchoolDepartments", new[] { "SchoolId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "CourseId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "SchoolTeacherId" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "SchoolTeacherCourseId" });
            DropIndex("dbo.Users", new[] { "SchoolId" });
            DropColumn("dbo.Conversations", "HasNewMessage");
            DropTable("dbo.Teachers");
            DropTable("dbo.SchoolTeachers");
            DropTable("dbo.Schools");
            DropTable("dbo.SchoolDepartments");
            DropTable("dbo.Departments");
            DropTable("dbo.SchoolTeacherCourses");
            DropTable("dbo.Courses");
        }
    }
}
