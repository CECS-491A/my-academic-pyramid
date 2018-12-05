using DataAccessLayer;
using Service_Layer.UserManagement.UserTree;
using System;

namespace DemoProject
{
    class ProgramUMTree
    {
        static void Main(string[] args)
        {
            // Create users
            Console.WriteLine("Create Users:");
            User krystal = new User("Krystal", 100);
            User luis = new User("Luis", 200);
            User trong = new User("Trong", 300);
            User arturo = new User("Arturo", 400);
            User kevin = new User("Kevin", 500);
            User victor = new User("Victor", 600);

            Console.WriteLine(krystal.Id + ":" + krystal.UserName);
            Console.WriteLine(luis.Id + ":" + luis.UserName);
            Console.WriteLine(trong.Id + ":" + trong.UserName);
            Console.WriteLine(arturo.Id + ":" + arturo.UserName);
            Console.WriteLine(kevin.Id + ":" + kevin.UserName);
            Console.WriteLine(victor.Id + ":" + victor.UserName);

            

            Console.WriteLine("End");
            Console.ReadKey(true);
        }
    }
}
