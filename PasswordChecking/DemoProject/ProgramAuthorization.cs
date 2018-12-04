using ManagerLayer.Logic.UserManagement;
using DataAccessLayer;
using System;

namespace DemoProject
{
    class ProgramAuthorization
    {
        
        static void Main(string[] args)
        {
            // Controller that handles any request involving user management
            UserManagementController userManagementController = new UserManagementController();

            // Create user Trong 
            User Trong = new User("Trong", 1);
            //Assign claim CanDeleteUserOwnAccount

            // Delete own account request is received
            Console.WriteLine("***Let Trong delete his own user account***");
            //Access DeleteUserOwnAccount method using the controller
            userManagementController.DeleteOwnAction(Trong);

            // Delete other account request is received
            Console.WriteLine("***Let Trong delete other user account***");
            //Access DeleteOtherAccount method using the controller
            userManagementController.DeleteOtherAction(Trong);

            //Create user Krystal 
            User Krystal = new User("Krystal", 2);

            //Assign CanDeleteUserOwnAccount and CanDeleteUserOwnAccount;

            // Delete own account request is received
            Console.WriteLine("\n***Let Krystal delete her own user account***");
            userManagementController.DeleteOwnAction(Krystal);

            // Delete other account request is received
            Console.WriteLine("***Let Krystal delete other user account***");
            userManagementController.DeleteOtherAction(Krystal);

            // null example
            //userManagementController.DeleteOwnAction(null);
            Console.ReadLine();




        }
    }
}
