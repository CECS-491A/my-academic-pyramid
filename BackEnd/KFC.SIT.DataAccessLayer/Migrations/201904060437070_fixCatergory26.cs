namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCatergory26 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Claims", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Claims", "UserId", c => c.Int(nullable: false));
        }
    }
}
