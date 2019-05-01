namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schoolteacheradddepartmentid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchoolTeachers", new[] { "SchoolDepartment_DepartmentID", "SchoolDepartment_SchoolId" }, "dbo.SchoolDepartments");
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolDepartment_DepartmentID", "SchoolDepartment_SchoolId" });
            RenameColumn(table: "dbo.SchoolTeachers", name: "SchoolDepartment_DepartmentID", newName: "DepartmentId");
            RenameColumn(table: "dbo.SchoolTeachers", name: "SchoolDepartment_SchoolId", newName: "SchoolId2");
            AlterColumn("dbo.SchoolTeachers", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.SchoolTeachers", "SchoolId2", c => c.Int(nullable: false));
            CreateIndex("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" });
            AddForeignKey("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" }, "dbo.SchoolDepartments", new[] { "DepartmentID", "SchoolId" }, cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" }, "dbo.SchoolDepartments");
            DropIndex("dbo.SchoolTeachers", new[] { "DepartmentId", "SchoolId2" });
            AlterColumn("dbo.SchoolTeachers", "SchoolId2", c => c.Int());
            AlterColumn("dbo.SchoolTeachers", "DepartmentId", c => c.Int());
            RenameColumn(table: "dbo.SchoolTeachers", name: "SchoolId2", newName: "SchoolDepartment_SchoolId");
            RenameColumn(table: "dbo.SchoolTeachers", name: "DepartmentId", newName: "SchoolDepartment_DepartmentID");
            CreateIndex("dbo.SchoolTeachers", new[] { "SchoolDepartment_DepartmentID", "SchoolDepartment_SchoolId" });
            AddForeignKey("dbo.SchoolTeachers", new[] { "SchoolDepartment_DepartmentID", "SchoolDepartment_SchoolId" }, "dbo.SchoolDepartments", new[] { "DepartmentID", "SchoolId" });
        }
    }
}
