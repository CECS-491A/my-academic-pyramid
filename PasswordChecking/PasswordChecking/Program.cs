using PasswordChecking.HashFunctions;
using System;
using System.Diagnostics;

namespace PasswordChecking
{
    class Program
    {
        private static SHA1HashFunction sha = new SHA1HashFunction();
        private static string url = "https://api.pwnedpasswords.com/range/";
        private static PwnedPasswordsValidation pv = new PwnedPasswordsValidation(sha, url);

        static void Main(string[] args)
        {
            // Password Input
            string password = "password";
            Console.WriteLine("Password: " + password);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000; i++)
            {
                // Get password count
                int hashCount = pv.GetCount(password);
                //Console.WriteLine("Count: " + hashCount);
                if(hashCount < 0)
                {
                    Console.WriteLine("Invalid Values");
                }
                else
                {
                    // Check Business Rules
                    //PasswordCheckingBR.CheckPasswordCount(hashCount);
                }

            }

            sw.Stop();
            Console.WriteLine("Duration: " + sw.ElapsedMilliseconds + " ms");
            Console.WriteLine("End");
            Console.ReadKey(true);//80005 ms, 80643, 79484
            // 75954, 87209, 76206
        }
    }
}