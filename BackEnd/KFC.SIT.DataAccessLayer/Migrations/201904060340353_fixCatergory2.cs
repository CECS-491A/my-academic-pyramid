namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCatergory2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "Catergory_Id", newName: "CatergoryId");
            RenameIndex(table: "dbo.Users", name: "IX_Catergory_Id", newName: "IX_CatergoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_CatergoryId", newName: "IX_Catergory_Id");
            RenameColumn(table: "dbo.Users", name: "CatergoryId", newName: "Catergory_Id");
        }
    }
}
