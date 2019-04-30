namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserdeleteemailaddmiddleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MiddleName", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            DropColumn("dbo.Users", "MiddleName");
        }
    }
}
