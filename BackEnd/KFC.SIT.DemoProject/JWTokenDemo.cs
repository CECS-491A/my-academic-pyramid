using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ManagerLayer.UserManagement;
using SecurityLayer;
using ServiceLayer.PasswordChecking.HashFunctions;
using DataAccessLayer.DTOs;

namespace DemoProject
{
    class JWTokenDemo
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> test = new Dictionary<string, string>()
            {
                { "fed", "food" },
                { "blue", "23" },
                { "cred", "43" }
            };

            test["c"] = "New 3";

            CreateUsers();


            DatabaseContext _DbContext = new DatabaseContext();
            var um = new UserManager();

            User user = um.FindByUserName("Abc@gmail.com");
            JWTokenManager tm = new JWTokenManager(_DbContext);

            String token = tm.GenerateToken(user.Id, test);
            Console.Out.WriteLine(token);
            Console.Out.WriteLine("Attempting to validate token");
            Dictionary<string, string> payload = null;
            if (tm.ValidateToken(token))
            {
                Console.Out.WriteLine("Getting payload");
                payload = tm.GetPayload(token);
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

            System.Threading.Thread.Sleep(50000);

            if (!tm.ValidateToken(token))
            {
                Console.Out.WriteLine("Token is now invalid. Good.");
            }
            else
            {
                Console.Out.WriteLine("Error: Token should be invalid.");
            }
            string newToken = tm.RefreshToken(token, payload);
            if (tm.ValidateToken(newToken))
            {
                Console.Out.WriteLine("Good! The refresh worked!");
            }
            else
            {
                Console.Out.WriteLine("Something is wrong with refresh.");
            }

            tm.InvalidateToken(newToken);
            if (tm.ValidateToken(newToken))
            {
                Console.Out.WriteLine("Something is wrong. Token should have been deleted.");
            }
            else
            {
                Console.Out.WriteLine("The token is invalid as it should be. It was deleted after all.");
            }

            Console.In.Read();
            Console.Out.WriteLine("Ending program.");

        }

        private static void CreateUsers()
        {
            UserDTO user1 = new UserDTO()
            {
                UserName = "Abc@gmail.com",
                FirstName = "Jackie",
                LastName = "Chan",
                Email = "Abc@gmail.com"
            };

            UserDTO user2 = new UserDTO()
            {
                UserName = "tri@yahoo.com",
                FirstName = "David",
                LastName = "Gonzales",
                Email = "tri@yahoo.com"
            };

            UserDTO user3 = new UserDTO()
            {
                UserName = "Smith@gmail.com",
                FirstName = "Michael",
                LastName = "Nguyen",
                Email = "Smith@gmail.com"
            };

            DatabaseContext db = new DatabaseContext();
            UserManager uM = new UserManager();
            uM.CreateUserAccount(user1);
            uM.CreateUserAccount(user2);
            uM.CreateUserAccount(user3);
            db.SaveChanges();

        }
    }
}
