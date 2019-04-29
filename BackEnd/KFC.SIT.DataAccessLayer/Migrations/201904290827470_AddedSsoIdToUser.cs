namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSsoIdToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SsoId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SsoId");
        }
    }
}
