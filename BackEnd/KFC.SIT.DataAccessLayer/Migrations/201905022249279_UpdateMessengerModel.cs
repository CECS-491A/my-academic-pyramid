namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessengerModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ChatHistories", new[] { "UserId" });
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutgoingMessage = c.Boolean(nullable: false),
                        MessageContent = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
            AddColumn("dbo.Conversations", "ContactUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "ContactUsername", c => c.String());
            AddColumn("dbo.Conversations", "HasNewMessage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Conversations", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Conversations", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Conversations", "UserId");
            DropColumn("dbo.Conversations", "SenderId");
            DropColumn("dbo.Conversations", "ReceiverId");
            DropColumn("dbo.Conversations", "MessageContent");
            DropTable("dbo.ChatHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChatHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        ContactUsername = c.String(),
                        ContactTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Conversations", "MessageContent", c => c.String());
            AddColumn("dbo.Conversations", "ReceiverId", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "SenderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "UserId" });
            DropColumn("dbo.Conversations", "UserId");
            DropColumn("dbo.Conversations", "ModifiedDate");
            DropColumn("dbo.Conversations", "HasNewMessage");
            DropColumn("dbo.Conversations", "ContactUsername");
            DropColumn("dbo.Conversations", "ContactUserId");
            DropTable("dbo.Messages");
            CreateIndex("dbo.ChatHistories", "UserId");
        }
    }
}
