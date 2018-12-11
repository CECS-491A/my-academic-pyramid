using DataAccessLayer;
using System;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;
using ManagerLayer.UserManagement;

namespace DemoProject
{
    class ProgramAuthorization
    {
        
        static void Main(string[] args)
        {
            // Initialize mock database with Effort Framework
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            // Create System Admin User Manager using overloading constructor
            Console.WriteLine("\n---Create System Admin User Manager using overloading constructor---");
            UserManager SystemAdminManager = new UserManager("SystemAdmin", true);

            // Use System Admin's user manager to create 2 additional account
            Console.WriteLine("\n---Use System Admin's user manager to create 2 additional account---");
            SystemAdminManager.CreateUserAction("SubAdmin1");
            SystemAdminManager.CreateUserAction("SubAdmin2");

            // Print all users in database
            Console.WriteLine("\n***Print all users in database***");
            SystemAdminManager.PrintAllUser();

            // Let SubAdmin1 initialize his user manager 
            UserManager SubAdminManager1 = new UserManager("SubAdmin1");

            // Try using  SubAdmin1 to delete SubAdmin2 account. 
            // Should return error because the SubAdmin1 does not have claim "UserManager"
            Console.WriteLine("\n---Try using  SubAdmin1 to delete SubAdmin2 account. Should return error---");
            try
            {
                SubAdminManager1.DeleteAction("SubAdmin2");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine("Message: {0}", argEx.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured in deleting SubAdmin2");
            }

            // Let System Admin grant SubAdmin1 the claim "UserManager" 
            Console.WriteLine("\n---Let System Admin give SubAdmin1 the claim UserManager to delete other account--- ");
            SystemAdminManager.AddClaimAction("SubAdmin1", new Claim("UserManager"));

            // Try using  SubAdmin1 to delete SubAdmin2 account agiain. 
            // Should show error be
            Console.WriteLine("\n---Try using  SubAdmin1 to delete SubAdmin2 account. Still not possible because they are on the same level.---");
            // Reload SubAdminManager1
            SubAdminManager1 = new UserManager("SubAdmin1");
            try
            {
                SubAdminManager1.DeleteAction("SubAdmin2");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine("Message: {0}", argEx.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured.");
            }

            // Try using  SubAdmin1 to delete SystemAdmin account. Should return error
            Console.WriteLine("\n---Try using  SubAdmin1 to delete SystemAdmin account . Should return error--- ");
            try
            {

                SubAdminManager1.DeleteAction("SystemAdmin");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine("Message: {0}", argEx.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured.");
            }


            // Let SubAdmin1 create an account call Sub_SubAdmin
            Console.WriteLine("\n---Let SubAdmin1 create an account call User--- ");
            SubAdminManager1.CreateUserAction("User");
            Console.WriteLine("\n***Print all users in database again***");
            SystemAdminManager.PrintAllUser();

            // Let System Admin Delete User
            Console.WriteLine("\n---Let System Admin delete User. Should be ok---");
            SystemAdminManager.DeleteAction("User");
            Console.WriteLine("\n***Print all users in database again***");
            SystemAdminManager.PrintAllUser();

            // Let System Admin delete SubAdmin 1. Should be ok 
            Console.WriteLine("\n---Let System Admin delete SubAdmin 1 . Should be ok ---");
            SystemAdminManager.DeleteAction("SubAdmin1");
            Console.WriteLine("\n***Print all users in database again***");

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
