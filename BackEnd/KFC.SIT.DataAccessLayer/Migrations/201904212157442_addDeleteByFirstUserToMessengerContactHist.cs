namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeleteByFirstUserToMessengerContactHist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessengerContactHists", "DeleteByFirstUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessengerContactHists", "DeleteByFirstUser");
        }
    }
}
