namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatergoryOneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropIndex("dbo.Claims", new[] { "UserId" });
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Claim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Claim_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Claims", t => t.Claim_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Claim_Id);
            
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Users", "PasswordHash");
            DropColumn("dbo.Users", "PasswordSalt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.String());
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
            DropForeignKey("dbo.UserClaims", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.UserClaims", "User_Id", "dbo.Users");
            DropIndex("dbo.UserClaims", new[] { "Claim_Id" });
            DropIndex("dbo.UserClaims", new[] { "User_Id" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropTable("dbo.UserClaims");
            CreateIndex("dbo.Claims", "UserId");
            AddForeignKey("dbo.Claims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
