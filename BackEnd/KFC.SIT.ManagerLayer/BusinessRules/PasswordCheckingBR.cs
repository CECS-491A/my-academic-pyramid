using ManagerLayer.Logic;
using System;

namespace ManagerLayer.BusinessRules
{
    public class PasswordCheckingBR
    {
        /// <summary>
        /// Checks the security of a password based on the number
        /// of times it has been breached.
        /// </summary>
        /// <param name="count">The breach count of the password</param>
        /// <returns>The password security status</returns>
        public static PasswordStatus CheckPasswordCount(int count)
        {
            PasswordStatus status; // The status of the security of the password.

            if(count == 0) // The password secure.  It has not been breached.
            {
                status = new PasswordStatus(0);
                Console.WriteLine("Password OK.");
            }
            else if(count == 1) // The password is secure, but has been breached once.
            {
                status = new PasswordStatus(1);
                Console.WriteLine("Password OK, but has been breached once.");
            }
            else // The password is not secure.  It has been breached multiple times.
            {
                status = new PasswordStatus(2);
                Console.WriteLine("Change Password, has been breached " + count + " times.");
            }

            return status;
        }
    }
}
