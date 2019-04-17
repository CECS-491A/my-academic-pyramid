namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendRelationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        friendId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendRelationships", "UserId", "dbo.Users");
            DropIndex("dbo.FriendRelationships", new[] { "UserId" });
            DropTable("dbo.FriendRelationships");
        }
    }
}
