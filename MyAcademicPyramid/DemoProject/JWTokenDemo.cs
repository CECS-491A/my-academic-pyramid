using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ManagerLayer.UserManagement;
using SecurityLayer;
using ServiceLayer.PasswordChecking.HashFunctions;

namespace DemoProject
{
    class JWTokenDemo
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> test = new Dictionary<string, string>()
            {
                { "a", "1" },
                { "b", "2" },
                { "c", "3" }
            };

            DatabaseContext _DbContext = new DatabaseContext();
            var um = new UserManager(_DbContext);

            SHA256HashFunction HashFunction = new SHA256HashFunction();
            String userPassword = "Trong@90";
            String hashedPassword = HashFunction.GetHashValue(userPassword);
            PasswordQA passwordQA = new PasswordQA("What's your name", "Me", "What is your dog name", "Fox", "what is your heihgt", "5.09");
            User newUser1 = new User("Trong", passwordQA);
            um.CreateUserAction(newUser1, hashedPassword);
            _DbContext.SaveChanges();


            JWTokenManager tm = new JWTokenManager(_DbContext);
            
            String token = tm.GenerateToken(newUser1, test);
            Console.Out.WriteLine(token);
            Console.Out.WriteLine("Attempting to validate token");
            if (tm.ValidateToken(token))
            {
                Console.Out.WriteLine("Getting payload");
                Dictionary<string, string> payload = tm.GetPayload(token);
                Console.Out.WriteLine(payload.ToString());
            }

            if (tm.ValidateToken("FakeToken"))
            {
                Console.Out.WriteLine("Error: FakeToken isn't a real token.");

            }
            else
            {
                Console.Out.WriteLine("Correct: FakeToken wasn't valid.");
            }

            Console.In.Read();
            Console.Out.WriteLine("Ending program.");

        }
        


    }
}
