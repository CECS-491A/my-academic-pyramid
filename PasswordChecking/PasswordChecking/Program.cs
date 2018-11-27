
using PasswordChecking.HashFunctions;
using System;
using System.Diagnostics;
using System.Net;

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
            WebClient client = new WebClient();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PwnedPasswordsValidation pv = new PwnedPasswordsValidation(sha, password, url);

            // Run validation and return hash object
            int hashCount = pv.Run();
            sw.Stop();
            Console.WriteLine("Duration: " + sw.ElapsedMilliseconds + " ms");
            Console.WriteLine("Count: " + hashCount);
            Console.WriteLine("End");
            Console.ReadKey(true);
        }
    }
}
