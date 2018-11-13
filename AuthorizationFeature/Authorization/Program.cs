using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.DataAccess;

namespace Authorization
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create user with type Student
            Console.WriteLine("***Testing account with username Victor as a student***");
            AuthorizedUser student = new AuthorizedUser("Victor", "Student");

            //Create security center 
            SecurityCenter secure = new SecurityCenter();

            //Re-enable AccessStudentArea claim in claim table for testing purpose 
            secure.EnableClaim("CanAccessStudentArea");

            //Authorize User
            AuthorizedUser authorizedStudent = secure.AuthorizeUser(student);

            //Print list of claims belong to student account
            Console.WriteLine("\n----List of claims Victor has----- ");
            Console.WriteLine(String.Format("{0,-30} {1,-5}", "Claim Type", "Claim Value"));
            Console.WriteLine();
            foreach (Claim claim in authorizedStudent.UserClaims)
            {
                
                Console.WriteLine(String.Format("{0,-30} {1,-5}", claim.ClaimType, claim.ClaimValue));
            }

            //Trying to access student area with student account
           Console.WriteLine("\n----Have Victor try to access Student area ------");
            Controller.AccessStudentArea(authorizedStudent);

            //Trying to disable Access student area claim in Victor'saccount
            secure.DisableClaim("CanAccessStudentArea");
            Console.WriteLine("\n---Disable Student Area access in Victor---");
            Console.WriteLine("\n----Have Victor try to access Student area again------");

            //Reauthorize user
            authorizedStudent = secure.AuthorizeUser(student);

            //Recheck if the user can still access student area after disable the require claim
            Controller.AccessStudentArea(authorizedStudent);

            //Trying to access student area with Admin account
            Console.WriteLine("\n----Have Victor try to  access Admin area ------");
            Controller.AccessAdminArea(authorizedStudent);

            //Create user with type Admin
            Console.WriteLine("\n\n***Testing account with username Krystal as an admin***");
            AuthorizedUser admin = new AuthorizedUser("Krystal", "Admin");

            //Use security center to authorize user 
            AuthorizedUser authorizedAdmin = secure.AuthorizeUser(admin);

            Console.WriteLine("\n---List of claims Krystal has---- ");
            foreach (Claim claim in authorizedAdmin.UserClaims)
            {
                Console.WriteLine(claim.ClaimType);
            }

            //Trying to access student area with admin account
            Console.WriteLine("\n----Have Krystal try to access Student area ------");
            Controller.AccessStudentArea(authorizedAdmin);

            //Trying to access admin area with admin account
            Console.WriteLine("\n----Have Krystal try to  access Admin area ------");
            Controller.AccessAdminArea(authorizedAdmin);

           
            Console.ReadLine();


           
        }
    }
}
