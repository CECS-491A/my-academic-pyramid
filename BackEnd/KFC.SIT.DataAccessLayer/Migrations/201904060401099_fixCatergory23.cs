namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCatergory23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catergories", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catergories", "UserId");
        }
    }
}
