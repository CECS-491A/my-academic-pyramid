namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                        RefreshedTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ExpirationTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Users", "DateOfBirth");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "Location");
            DropColumn("dbo.Users", "PasswordQuestion1");
            DropColumn("dbo.Users", "PasswordQuestion2");
            DropColumn("dbo.Users", "PasswordQuestion3");
            DropColumn("dbo.Users", "PasswordAnswer1");
            DropColumn("dbo.Users", "PasswordAnswer2");
            DropColumn("dbo.Users", "PasswordAnswer3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PasswordAnswer3", c => c.String());
            AddColumn("dbo.Users", "PasswordAnswer2", c => c.String());
            AddColumn("dbo.Users", "PasswordAnswer1", c => c.String());
            AddColumn("dbo.Users", "PasswordQuestion3", c => c.String());
            AddColumn("dbo.Users", "PasswordQuestion2", c => c.String());
            AddColumn("dbo.Users", "PasswordQuestion1", c => c.String());
            AddColumn("dbo.Users", "Location", c => c.String());
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropTable("dbo.UserSessions");
        }
    }
}
