using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public static class Controller
    {
        //Method to access Student Area
        
        public static void AccessStudentArea(AuthorizedUser authorizedUser)
        {
            //Check if user has require claim
            if (authorizedUser.hasClaim("CanAccessStudentArea"))
            {

                Console.WriteLine($"Student area access is granted to {authorizedUser.UserName}");

            }

            else
            {
                Console.WriteLine("You are not authorized to access student area");
            }

        }

        //Check if user has require claim
        public static void AccessAdminArea(AuthorizedUser authorizedUser)
        {
            if (authorizedUser.hasClaim("CanAccessAdminArea"))
            {
                Console.WriteLine($"Admin area access is granted to {authorizedUser.UserName}" );
            }
            else
            {
                Console.WriteLine("You are not authorized to access Admin area");
            }
        }




    }
}
