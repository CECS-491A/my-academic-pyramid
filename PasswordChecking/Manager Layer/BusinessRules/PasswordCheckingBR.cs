
using ManagerLayer.Logic;
using System;

namespace ManagerLayer.BusinessRules
{
    class PasswordCheckingBR
    {
        public static Validation CheckPasswordCount(int count)
        {
            Validation validation;

            if(count == 0)
            {
                // Return OK, don't warn user
                validation = new Validation(true, false);
                Console.WriteLine("Password OK.");
            }
            else if(count == 1)
            {
                // Return OK, but warn user
                validation = new Validation(true, true);
                Console.WriteLine("Password OK, but has been breached once.");
            }
            else
            {
                // Return Not OK, change password
                validation = new Validation(false, true);
                Console.WriteLine("Change Password, has been breached " + count + " times.");
            }

            return validation;
        }
    }
}
