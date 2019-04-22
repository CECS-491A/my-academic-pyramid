namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleteTimeMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatHistories", "TimeWhenMarkedDeleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatHistories", "TimeWhenMarkedDeleted");
        }
    }
}
