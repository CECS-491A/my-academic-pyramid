using PasswordChecking.HashFunctions;
using Newtonsoft.Json;
using System;

namespace PasswordChecking
{
    class PasswordCheckingBR
    {
        /// <summary>
        /// Checks that the number of times the password has
        /// been breached is within a safe range.
        /// </summary>
        /// <param name="count">The number of times the password has been breached</param>
        public static void CheckPasswordCount(int count)
        {
            if(count == 0)
            {
                // Return OK
                Console.WriteLine("Password OK");
            }
            else if(count == 1)
            {
                // Return OK, but warn user
                Console.WriteLine("Password OK, but has been breached once.");
            }
            else
            {
                // Return Not OK, change password
                Console.WriteLine("Password has been breached " + count + " times.  Change Password.");
            }

            
        }
    }
}
