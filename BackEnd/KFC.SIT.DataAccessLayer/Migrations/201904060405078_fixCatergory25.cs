namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCatergory25 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catergories", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catergories", "UserId", c => c.Int(nullable: false));
        }
    }
}
