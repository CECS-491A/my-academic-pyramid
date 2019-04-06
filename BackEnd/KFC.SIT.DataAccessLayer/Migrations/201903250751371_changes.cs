namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserActions", "User_Id", "dbo.Users");
            DropIndex("dbo.Claims", new[] { "User_Id" });
            DropIndex("dbo.UserActions", new[] { "User_Id" });
            RenameColumn(table: "dbo.Claims", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserActions", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Claims", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserActions", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Claims", "UserId");
            CreateIndex("dbo.UserActions", "UserId");
            AddForeignKey("dbo.Claims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserActions", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropIndex("dbo.UserActions", new[] { "UserId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            AlterColumn("dbo.UserActions", "UserId", c => c.Int());
            AlterColumn("dbo.Claims", "UserId", c => c.Int());
            RenameColumn(table: "dbo.UserActions", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Claims", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.UserActions", "User_Id");
            CreateIndex("dbo.Claims", "User_Id");
            AddForeignKey("dbo.UserActions", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Claims", "User_Id", "dbo.Users", "Id");
        }
    }
}
