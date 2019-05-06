namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSchoolIdinSchoolTeacher : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools");
            DropIndex("dbo.SchoolTeachers", new[] { "SchoolId" });
            RenameColumn(table: "dbo.SchoolTeachers", name: "SchoolId", newName: "School_Id");
            AlterColumn("dbo.SchoolTeachers", "School_Id", c => c.Int());
            CreateIndex("dbo.SchoolTeachers", "School_Id");
            AddForeignKey("dbo.SchoolTeachers", "School_Id", "dbo.Schools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolTeachers", "School_Id", "dbo.Schools");
            DropIndex("dbo.SchoolTeachers", new[] { "School_Id" });
            AlterColumn("dbo.SchoolTeachers", "School_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.SchoolTeachers", name: "School_Id", newName: "SchoolId");
            CreateIndex("dbo.SchoolTeachers", "SchoolId");
            AddForeignKey("dbo.SchoolTeachers", "SchoolId", "dbo.Schools", "Id", cascadeDelete: true);
        }
    }
}
