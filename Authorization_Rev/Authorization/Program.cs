using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Controller that handles any request involving user management
            UserManagementController userManagementController = new UserManagementController();

            // Create user Trong 
            User Trong = new User("Trong", "Student");
            //Assign claim CanDeleteUserOwnAccount
            Trong.addClaim("CanDeleteUserOwnAccount", true);

            // Delete own account request is received
            Console.WriteLine("***Let Trong delete his own user account***");
            //Access DeleteUserOwnAccount method using the controller
            userManagementController.DeleteOwnAction(Trong);

            // Delete other account request is received
            Console.WriteLine("***Let Trong delete other user account***");
            //Access DeleteOtherAccount method using the controller
            userManagementController.DeleteOtherAction(Trong);

            //Create user Krystal 
            User Krystal = new User("Krystal", "Admin");

            //Assign CanDeleteUserOwnAccount and CanDeleteUserOwnAccount
            Krystal.addClaim("CanDeleteUserOwnAccount", true);
            Krystal.addClaim("CanDeleteOtherAccount", true);
            Krystal.addClaim("HasPoints", true);

            // Delete own account request is received
            Console.WriteLine("\n***Let Krystal delete her own user account***");
            userManagementController.DeleteOwnAction(Krystal);

            // Delete other account request is received
            Console.WriteLine("***Let Krystal delete other user account***");
            userManagementController.DeleteOtherAction(Krystal);

            Console.ReadLine();


           
        }
    }
}
