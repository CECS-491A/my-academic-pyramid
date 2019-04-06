namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUserActions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserActions", "UserId", "dbo.Users");
            DropIndex("dbo.UserActions", new[] { "UserId" });
            DropTable("dbo.UserActions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                        isActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserActions", "UserId");
            AddForeignKey("dbo.UserActions", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
