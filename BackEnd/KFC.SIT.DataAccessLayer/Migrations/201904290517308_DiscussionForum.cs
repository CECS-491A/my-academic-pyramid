namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscussionForum : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosterId = c.Int(nullable: false),
                        PosterUserName = c.String(),
                        Text = c.String(),
                        HelpfulCount = c.Int(nullable: false),
                        UnHelpfulCount = c.Int(nullable: false),
                        SpamCount = c.Int(nullable: false),
                        IsCorrectAnswer = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosterId = c.Int(nullable: false),
                        PosterUserName = c.String(),
                        Text = c.String(),
                        MinimumExpForAnswer = c.Int(nullable: false),
                        IsDraft = c.Boolean(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        SpamCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Exp", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropColumn("dbo.Users", "Exp");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
