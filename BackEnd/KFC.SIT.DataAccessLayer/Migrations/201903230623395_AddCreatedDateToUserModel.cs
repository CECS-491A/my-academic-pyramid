namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedAt");
        }
    }
}
