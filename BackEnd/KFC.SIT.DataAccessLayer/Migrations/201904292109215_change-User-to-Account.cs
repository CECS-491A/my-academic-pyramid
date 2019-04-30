namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUsertoAccount : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Accounts");
            RenameTable(name: "dbo.ClaimUsers", newName: "ClaimAccounts");
            RenameColumn(table: "dbo.ClaimAccounts", name: "User_Id", newName: "Account_Id");
            RenameColumn(table: "dbo.Accounts", name: "User_Id", newName: "Account_Id");
            RenameIndex(table: "dbo.Accounts", name: "IX_User_Id", newName: "IX_Account_Id");
            RenameIndex(table: "dbo.ClaimAccounts", name: "IX_User_Id", newName: "IX_Account_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ClaimAccounts", name: "IX_Account_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Accounts", name: "IX_Account_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Accounts", name: "Account_Id", newName: "User_Id");
            RenameColumn(table: "dbo.ClaimAccounts", name: "Account_Id", newName: "User_Id");
            RenameTable(name: "dbo.ClaimAccounts", newName: "ClaimUsers");
            RenameTable(name: "dbo.Accounts", newName: "Users");
        }
    }
}
