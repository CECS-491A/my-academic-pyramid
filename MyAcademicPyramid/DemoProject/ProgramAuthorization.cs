using DataAccessLayer;
using System;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;
using ManagerLayer.UserManagement;
using System.Collections.Generic;

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
            List<User> list = SystemAdminManager.GetAllUser();
            PrintAllUser(list);

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
            List<User> list4 = SystemAdminManager.GetAllUser();
            PrintAllUser(list4);

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
            List<User> list2 = SystemAdminManager.GetAllUser();
            PrintAllUser(list2);

            // Let System Admin Delete User
            Console.WriteLine("\n---Let System Admin delete User. Should be ok---");
            SystemAdminManager.DeleteAction("User");
            Console.WriteLine("\n***Print all users in database again***");
            List<User> list3 = SystemAdminManager.GetAllUser();
            PrintAllUser(list3);

            // Let System Admin delete SubAdmin 1. Should be ok 
            Console.WriteLine("\n---Let System Admin delete SubAdmin 1 . Should be ok ---");
            SystemAdminManager.DeleteAction("SubAdmin1");
            Console.WriteLine("\n***Print all users in database again***");
            List<User> list6 = SystemAdminManager.GetAllUser();
            PrintAllUser(list6);

            Console.ReadLine();
        }

        /// <summary>
        /// Method to print all user in database
        /// </summary>
        /// <param name="list"></param>
        public  static void PrintAllUser(List<User> list)
        {
            Console.WriteLine(String.Format("{0, -15} {1, -10} {2, -15}", "Name", "Level", "Claim"));
            foreach(User user in list)
            {
                AuthorizationManager authorization = new AuthorizationManager();
                int level = authorization.FindHeight(user);

                String claimList = "";
                foreach (Claim claim in user.Claims)
                {
                    claimList+= claim.Value + ", ";
                }
             
                Console.WriteLine(String.Format("{0, -15} {1, -10} {2, -15}", user.UserName, level, claimList));

            }
        }
    }
}
