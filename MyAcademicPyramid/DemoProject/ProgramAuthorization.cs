using DataAccessLayer;
using System;
using DemoProject.MockUserManagementNameSpace;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;

namespace DemoProject
{
    class ProgramAuthorization
    {
        
        static void Main(string[] args)
        {
            // Initialize mock database with Effort Framework
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            // Create System Admin User Manager using overloading constructor
            Console.WriteLine("\nCreate System Admin User Manager using overloading constructor");
            UserManagement SystemAdminManager = new UserManagement("SystemAdmin", true);

            // Use System Admin's user manager to create 2 additional account
            Console.WriteLine("\nUse System Admin's user manager to create 2 additional account");
            SystemAdminManager.CreateUserAction("SubAdmin1");
            SystemAdminManager.CreateUserAction("SubAdmin2");

            // Print all users in database
            Console.WriteLine("\nPrint all users in database");
            SystemAdminManager.PrintAllUser();

            // Let SubAdmin1 initialize his user manager 
            UserManagement SubAdminManager1 = new UserManagement("SubAdmin1");

            // Try using  SubAdmin1 to delete SubAdmin2 account. 
            // Should return error because the SubAdmin1 does not have claim "UserManager"
            Console.WriteLine("\nTry using  SubAdmin1 to delete SubAdmin2 account. Should return error");
            SubAdminManager1.DeleteAction("SubAdmin2");

            // Let System Admin grant SubAdmin1 the claim "UserManager" 
            Console.WriteLine("\n Let System Admin grant SubAdmin1 the claim UserManager ");
            SystemAdminManager.AddClaimAction("SubAdmin1", new Claim("UserManager"));

            // Try using  SubAdmin1 to delete SubAdmin2 account agiain. 
            // Should show error be
            Console.WriteLine("\nTry using  SubAdmin1 to delete SubAdmin2 account. Should be ok because  SubAdmin1 now has claim UserManager");
            // Reload SubAdminManager1
            SubAdminManager1 = new UserManagement("SubAdmin1");
            SubAdminManager1.DeleteAction("SubAdmin2");

            // Try using  SubAdmin1 to delete SystemAdmin account. Should return error
            Console.WriteLine("\nTry using  SubAdmin1 to delete SystemAdmin account . Should return error ");
            SubAdminManager1.DeleteAction("SystemAdmin");


            // Let SubAdmin1 create an account call Sub_SubAdmin
            Console.WriteLine("\nLet SubAdmin1 create an account call User ");
            SubAdminManager1.CreateUserAction("User");
            Console.WriteLine("\nPrint all users in database again");
            SystemAdminManager.PrintAllUser();

            // Let System Admin Delete User
            Console.WriteLine("\nLet System Admin delete User. Should be ok");
            SystemAdminManager.DeleteAction("User");
            Console.WriteLine("\nPrint all users in database again");
            SystemAdminManager.PrintAllUser();

            // Let System Admin delete SubAdmin 1. Should be ok 
            Console.WriteLine("\nLet System Admin delete SubAdmin 1 . Should be ok ");
            SystemAdminManager.DeleteAction("SubAdmin1");
            Console.WriteLine("\nPrint all users in database again");

// For AuthorizationManager FindHeight(User) testing purposes 
            UnitOfWork uOW = new UnitOfWork();
            UserManagementServices userManager = new UserManagementServices(uOW);

            // Arrange 
            // Highes lv
            User Krystal = new User("Krystal");
            userManager.CreateUser(Krystal);
            // Level 2
            User Arturo = new User("Arturo")
            {
                ParentUser_Id = Krystal.Id
            };
            userManager.CreateUser(Arturo);
            // Level 3
            User Kevin = new User("Kevin")
            {
                ParentUser_Id = Arturo.Id
            };
            userManager.CreateUser(Kevin);
            User Victor = new User("Victor")
            {
                ParentUser_Id = Arturo.Id
            };
            userManager.CreateUser(Victor);
            // Level 4
            User Luis = new User("Luis")
            {
                ParentUser_Id = Kevin.Id
            };
            userManager.CreateUser(Luis);
            User Trong = new User("Trong")
            {
                ParentUser_Id = Victor.Id
            };
            userManager.CreateUser(Trong);

            Console.WriteLine("\n\n\n\n\nPrint levels");

            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);

            int level0 = KrystalAuthorization.FindHeight(Krystal);
            int level1 = KrystalAuthorization.FindHeight(Arturo);
            int level2 = KrystalAuthorization.FindHeight(Kevin);
            int level3 = KrystalAuthorization.FindHeight(Victor);
            int level4 = KrystalAuthorization.FindHeight(Luis);
            int level5 = KrystalAuthorization.FindHeight(Trong);

            Console.WriteLine("Krystal's lv: " + level0 + " Arturo's lv: " + level1 + " Kevin's lv: " + level2 +
                " Victor's lv: " + level3 + " Luis' lv: " + level4 + " Trong's lv: " +  level5);
// End of Find Height test



            SystemAdminManager.PrintAllUser();

            Console.ReadLine();
        }
    }
}
