namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFriendRelationshipclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendRelationships", "friendUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FriendRelationships", "friendUsername");
        }
    }
}
