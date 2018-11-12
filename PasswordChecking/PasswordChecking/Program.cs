using PasswordChecking.HashFunctions;
using System;

namespace PasswordChecking
{
    class Program
    {

        static void Main(string[] args)
        {
            // Use SHA1 Hash Function
            SHA1HashFunction sha = new SHA1HashFunction();

            // Make Password Validation Object
            string password = "password";
            Console.WriteLine("Password: " + password);
            PasswordValidation pv = new PasswordValidation(sha, password);

            // Run validation and return hash object
            Hash hash = pv.Run();
            Console.WriteLine(hash);
            Console.WriteLine("End");
            Console.ReadKey(true);
        }
    }
}
