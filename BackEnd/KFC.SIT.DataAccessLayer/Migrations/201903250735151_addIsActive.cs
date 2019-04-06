namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserActions", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserActions", "isActive");
        }
    }
}
