namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatConnectionMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        connectionId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatConnectionMappings");
        }
    }
}
