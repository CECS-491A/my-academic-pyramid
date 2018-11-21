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
            // Create user Trong 
            User Trong = new User("Trong", "Student");
            //Assign claim CanDeleteUserOwnAccount
            Trong.addClaim("CanDeleteUserOwnAccount", true);

            //Create a user management for trong account
            UserManagement userMag1= new UserManagement(Trong);
            Console.WriteLine("***Let Trong delete his own user account***");

            //Access DeleteUserOwnAccount method using the controller
            userMag1.DeleteUserOwnAccount();

            Console.WriteLine("***Let Trong delete other user account***");

            //Access DeleteOtherAccount method using the controller
            userMag1.DeleteOtherAccount();

            //Create user Krystal 
            User Krystal = new User("Krystal", "Admin");

            //Assign CanDeleteUserOwnAccount and CanDeleteUserOwnAccount
            Krystal.addClaim("CanDeleteUserOwnAccount", true);
            Krystal.addClaim("CanDeleteOtherAccount", true);
            Krystal.addClaim("HasPoints", true);

            //Create a user management for Krysital account
            UserManagement userMag2 = new UserManagement(Krystal);

            Console.WriteLine("\n***Let Krystal delete her own user account***");
            userMag2.DeleteUserOwnAccount();

            Console.WriteLine("***Let Krystal delete other user account***");
            userMag2.DeleteOtherAccount();

            Console.ReadLine();


           
        }
    }
}
