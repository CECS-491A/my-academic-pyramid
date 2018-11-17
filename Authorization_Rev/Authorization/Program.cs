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

            //Create controller and use it to authorize user
            Controller c1 = new Controller(Trong);
            Console.WriteLine("***Let Trong delete his own user account***");

            //Access DeleteUserOwnAccount method using the controller
            c1.DeleteUserOwnAccount();

            Console.WriteLine("***Let Trong delete other user account***");

            //Access DeleteOtherAccount method using the controller
            c1.DeleteOtherAccount("Krystal");

            //Create user Krystal 
            User Krystal = new User("Krystal", "Admin");

            //Assign CanDeleteUserOwnAccount and CanDeleteUserOwnAccount
            Krystal.addClaim("CanDeleteUserOwnAccount", true);
            Krystal.addClaim("CanDeleteOtherAccount", true);

            Controller c2 = new Controller(Krystal);

            Console.WriteLine("\n***Let Krystal delete her own user account***");
            c2.DeleteUserOwnAccount();

            Console.WriteLine("***Let Krystal delete other user account***");
            c2.DeleteOtherAccount("Krystal");

            Console.ReadLine();


           
        }
    }
}
