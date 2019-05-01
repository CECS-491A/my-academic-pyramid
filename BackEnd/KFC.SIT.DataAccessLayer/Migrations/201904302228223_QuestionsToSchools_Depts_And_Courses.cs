namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionsToSchools_Depts_And_Courses : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseStudents", newName: "StudentCourses");
            DropPrimaryKey("dbo.StudentCourses");
            AddColumn("dbo.Answers", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "StudentUserName", c => c.String());
            AddColumn("dbo.Questions", "SchoolId", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "StudentUserName", c => c.String());
            AddColumn("dbo.Questions", "UpdatedDate", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.StudentCourses", new[] { "Student_Id", "Course_Id" });
            CreateIndex("dbo.Questions", "SchoolId");
            CreateIndex("dbo.Questions", "DepartmentId");
            CreateIndex("dbo.Questions", "CourseId");
            CreateIndex("dbo.Questions", "UserId");
            AddForeignKey("dbo.Questions", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Questions", "SchoolId", "dbo.Schools", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Questions", "UserId", "dbo.Users", "Id", cascadeDelete: false);
            DropColumn("dbo.Answers", "PosterId");
            DropColumn("dbo.Answers", "PosterUserName");
            DropColumn("dbo.Questions", "PosterId");
            DropColumn("dbo.Questions", "PosterUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "PosterUserName", c => c.String());
            AddColumn("dbo.Questions", "PosterId", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "PosterUserName", c => c.String());
            AddColumn("dbo.Answers", "PosterId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Questions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Questions", "CourseId", "dbo.Courses");
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "CourseId" });
            DropIndex("dbo.Questions", new[] { "DepartmentId" });
            DropIndex("dbo.Questions", new[] { "SchoolId" });
            DropPrimaryKey("dbo.StudentCourses");
            DropColumn("dbo.Questions", "UpdatedDate");
            DropColumn("dbo.Questions", "StudentUserName");
            DropColumn("dbo.Questions", "UserId");
            DropColumn("dbo.Questions", "CourseId");
            DropColumn("dbo.Questions", "DepartmentId");
            DropColumn("dbo.Questions", "SchoolId");
            DropColumn("dbo.Answers", "StudentUserName");
            DropColumn("dbo.Answers", "StudentId");
            AddPrimaryKey("dbo.StudentCourses", new[] { "Course_Id", "Student_Id" });
            RenameTable(name: "dbo.StudentCourses", newName: "CourseStudents");
        }
    }
}
