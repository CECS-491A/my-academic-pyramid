using System;
using System.Diagnostics;
using ServiceLayer.PasswordChecking.HashFunctions;
using ManagerLayer.Logic.PasswordChecking.PasswordValidations;
using ManagerLayer.Logic;

namespace DemoProject
{
    class ProgramPassword
    {
        private static SHA1HashFunction sha = new SHA1HashFunction();
        private static string url = "https://api.pwnedpasswords.com/range/";
        private static PwnedPasswordsValidation pv = new PwnedPasswordsValidation(sha, url);

        static void Main(string[] args)
        {
            while (true)
            {
                // Password Input
                Console.Write("\nPassword: ");
                string password = Console.ReadLine();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                // Get password count
                PasswordStatus pwStatus = pv.Validate(password);

                sw.Stop();
                Console.WriteLine("Duration: " + sw.ElapsedMilliseconds + " ms");
                Console.WriteLine("End");
                Console.ReadKey(true);
            }
        }
    }
}
