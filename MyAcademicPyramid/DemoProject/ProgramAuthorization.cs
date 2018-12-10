using DataAccessLayer;
using System;
using DemoProject.MockUserManagementNameSpace;

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
            UserManagementManager SystemAdminManager = new UserManagementManager("SystemAdmin", true);

            // Use System Admin's user manager to create 2 additional account
            Console.WriteLine("\nUse System Admin's user manager to create 2 additional account");
            SystemAdminManager.CreateUserAction("SubAdmin1");
            SystemAdminManager.CreateUserAction("SubAdmin2");

            // Print all users in database
            Console.WriteLine("\nPrint all users in database");
            SystemAdminManager.PrintAllUser();

            // Let SubAdmin1 initialize his user manager 
            UserManagementManager SubAdminManager1 = new UserManagementManager("SubAdmin1");

            // Try using  SubAdmin1 to delete SubAdmin2 account. 
            // Should return error because the SubAdmin1 does not have claim "UserManager"
            Console.WriteLine("\nTry using  SubAdmin1 to delete SubAdmin2 account. Should return error");
            SubAdminManager1.DeleteAction("SubAdmin2");

            // Let System Admin grant SubAdmin1 the claim "UserManager" 
            Console.WriteLine("\n Let System Admin grant SubAdmin1 the claim UserManager ");
            SystemAdminManager.AddClaimAction("SubAdmin1", new Claim("UserManager"));

            // Try using  SubAdmin1 to delete SubAdmin2 account agiain. 
            // Should be ok because  SubAdmin1 now has claim "UserManager"
            Console.WriteLine("\nTry using  SubAdmin1 to delete SubAdmin2 account. Should be ok because  SubAdmin1 now has claim UserManager");
            // Reload SubAdminManager1
            SubAdminManager1 = new UserManagementManager("SubAdmin1");
            SubAdminManager1.DeleteAction("SubAdmin2");

            // Try using  SubAdmin1 to delete SystemAdmin account. Should return error
            Console.WriteLine("\nTry using  SubAdmin1 to delete SystemAdmin account . Should return error ");
            SubAdminManager1.DeleteAction("SystemAdmin");

            // Try using  SubAdmin1 to delete SubAdmin2 account. Should be ok
            Console.WriteLine("\nTry using  SubAdmin1 to delete SubAdmin2 account. Should be ok");
            SubAdminManager1.DeleteAction("SubAdmin2");
            Console.WriteLine("\nPrint all users in database again");
            SystemAdminManager.PrintAllUser();

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
            SystemAdminManager.PrintAllUser();

            Console.ReadLine();




        }
    }
}
