namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropIndex("dbo.Claims", new[] { "UserId" });
            CreateTable(
                "dbo.Catergories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessengerContactHists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderUserName = c.String(),
                        ReceiverUserName = c.String(),
                        ContactTime = c.DateTime(nullable: false),
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
            
            AddColumn("dbo.Users", "CatergoryId", c => c.Int());
            CreateIndex("dbo.Users", "CatergoryId");
            AddForeignKey("dbo.Users", "CatergoryId", "dbo.Catergories", "Id");
            DropColumn("dbo.Claims", "UserId");
            DropColumn("dbo.Users", "Catergory");
            DropColumn("dbo.Users", "PasswordHash");
            DropColumn("dbo.Users", "PasswordSalt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.String());
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
            AddColumn("dbo.Users", "Catergory", c => c.String());
            AddColumn("dbo.Claims", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClaimUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ClaimUsers", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.Users", "CatergoryId", "dbo.Catergories");
            DropIndex("dbo.ClaimUsers", new[] { "User_Id" });
            DropIndex("dbo.ClaimUsers", new[] { "Claim_Id" });
            DropIndex("dbo.Users", new[] { "CatergoryId" });
            DropColumn("dbo.Users", "CatergoryId");
            DropTable("dbo.ClaimUsers");
            DropTable("dbo.MessengerContactHists");
            DropTable("dbo.Catergories");
            CreateIndex("dbo.Claims", "UserId");
            AddForeignKey("dbo.Claims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
