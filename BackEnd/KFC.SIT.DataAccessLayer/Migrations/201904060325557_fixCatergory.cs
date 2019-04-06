namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCatergory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserClaims", newName: "ClaimUsers");
            DropPrimaryKey("dbo.ClaimUsers");
            CreateTable(
                "dbo.Catergories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Catergory_Id", c => c.Int());
            AddPrimaryKey("dbo.ClaimUsers", new[] { "Claim_Id", "User_Id" });
            CreateIndex("dbo.Users", "Catergory_Id");
            AddForeignKey("dbo.Users", "Catergory_Id", "dbo.Catergories", "Id");
            DropColumn("dbo.Users", "Catergory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Catergory", c => c.String());
            DropForeignKey("dbo.Users", "Catergory_Id", "dbo.Catergories");
            DropIndex("dbo.Users", new[] { "Catergory_Id" });
            DropPrimaryKey("dbo.ClaimUsers");
            DropColumn("dbo.Users", "Catergory_Id");
            DropTable("dbo.Catergories");
            AddPrimaryKey("dbo.ClaimUsers", new[] { "User_Id", "Claim_Id" });
            RenameTable(name: "dbo.ClaimUsers", newName: "UserClaims");
        }
    }
}
