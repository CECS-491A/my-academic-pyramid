namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeleteByReceiverInChatHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessengerContactHists", "DeleteBySender", c => c.Boolean(nullable: false));
            AddColumn("dbo.MessengerContactHists", "DeleteByReceiver", c => c.Boolean(nullable: false));
            DropColumn("dbo.MessengerContactHists", "DeleteByFirstUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessengerContactHists", "DeleteByFirstUser", c => c.Boolean(nullable: false));
            DropColumn("dbo.MessengerContactHists", "DeleteByReceiver");
            DropColumn("dbo.MessengerContactHists", "DeleteBySender");
        }
    }
}
