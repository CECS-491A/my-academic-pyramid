namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createschoolrelatedtables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Catergories", newName: "Categories");
            RenameColumn(table: "dbo.Users", name: "CatergoryId", newName: "CategoryId");
            RenameIndex(table: "dbo.Users", name: "IX_CatergoryId", newName: "IX_CategoryId");
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
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SchoolDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolDepartments", t => t.SchoolDepartmentId, cascadeDelete: true);

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
                        SchoolDepartmentId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDepartments", t => t.SchoolDepartmentId, cascadeDelete: false)
                //.Index(t => t.SchoolId)
                .Index(t => t.TeacherId)
                .Index(t => t.SchoolDepartmentId);

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
            
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolTeachers", "SchoolDepartmentId", "dbo.SchoolDepartments");
            DropForeignKey("dbo.SchoolTeachers", "TeacherId", "dbo.Teachers");
            //DropForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolTeacherCourses", "SchoolTeacherId", "dbo.SchoolTeachers");
            DropForeignKey("dbo.SchoolDepartments", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SchoolTeacherCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SchoolDepartmentId", "dbo.SchoolDepartments");
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "SchoolTeacherCourseId", "dbo.SchoolTeacherCourses");
            DropForeignKey("dbo.SchoolTeacherCourseStudents", "StudentId", "dbo.Users");
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolDepartmentId" });
            DropIndex("dbo.SchoolTeachers", new[] { "TeacherId" });
            //DropIndex("dbo.SchoolTeachers", new[] { "SchoolId" });
            DropIndex("dbo.SchoolDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.SchoolDepartments", new[] { "SchoolId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "CourseId" });
            DropIndex("dbo.SchoolTeacherCourses", new[] { "SchoolTeacherId" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "StudentId" });
            DropIndex("dbo.SchoolTeacherCourseStudents", new[] { "SchoolTeacherCourseId" });
            DropColumn("dbo.Users", "Discriminator");
            DropTable("dbo.Teachers");
            DropTable("dbo.SchoolTeachers");
            DropTable("dbo.Schools");
            DropTable("dbo.SchoolDepartments");
            DropTable("dbo.Departments");
            DropTable("dbo.SchoolTeacherCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.SchoolTeacherCourseStudents");
            RenameIndex(table: "dbo.Users", name: "IX_CategoryId", newName: "IX_CatergoryId");
            RenameColumn(table: "dbo.Users", name: "CategoryId", newName: "CatergoryId");
            RenameTable(name: "dbo.Categories", newName: "Catergories");
        }
    }
}
