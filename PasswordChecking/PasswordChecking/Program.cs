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
            string url = "https://api.pwnedpasswords.com/range/";
            Console.WriteLine("URL: " + url);
            PasswordValidation pv = new PasswordValidation(sha, password, url);

            // Run validation and return hash object
            int hashCount = pv.Run();
            Console.WriteLine("Count: " + hashCount);
            Console.WriteLine("End");
            Console.ReadKey(true);
        }
    }
}
