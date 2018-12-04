using DataAccessLayer;
using Service_Layer.UserManagement.TreeNodes;
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

            // Create Tree
            Console.WriteLine("\nCreate Tree");
            RootNode tree = new RootNode();
            Console.WriteLine("\nTree: " + tree);

            // Add users as direct children
            Console.WriteLine("\nAdd Users to Tree as Direct Children:");
            tree.AddDirectChild(krystal);
            Console.WriteLine("Add krystal as direct child.");
            tree.AddDirectChild(luis);
            Console.WriteLine("Add luis as direct child.");
            tree.AddDirectChild(trong);
            Console.WriteLine("Add trong as direct child.");

            Console.WriteLine("\nTree: " + tree);

            // Add users by level
            Console.WriteLine("\nAdd Users to Tree by Level:");
            tree.AddChild(arturo, 2);
            Console.WriteLine("Add Arturo to Level 2");
            Console.WriteLine("\nTree: " + tree);

            // Add duplicate user
            tree.AddChild(arturo,1);
            Console.WriteLine("\nAttempt to add duplicate arturo to Level 1");
            Console.WriteLine("\nTree: " + tree);

            // Add user to invalid level
            Console.WriteLine("\nAttempt to add user kevin to invalid level 5");
            tree.AddChild(kevin, 5);
            Console.WriteLine("\nTree: " + tree);

            tree.AddChild(kevin, 3);
            Console.WriteLine("Add kevin to level 3");
            tree.AddChild(victor, 4);
            Console.WriteLine("Add victor to level 4");
            Console.WriteLine("\nTree: " + tree);

            // Delete direct child
            Console.WriteLine("\nDelete Direct Child User from Tree");
            tree.DeleteDirectChild(trong);
            Console.WriteLine("Delete trong as direct child.");
            Console.WriteLine("\nTree: " + tree);

            // Delete user by level
            Console.WriteLine("\nDelete User by Level");
            tree.DeleteChild(arturo, 2);
            Console.Write("Delete arturo from level 2");
            Console.WriteLine("\nTree: " + tree);

            // Delete Level
            Console.WriteLine("\nDelete Level 2");
            tree.DeleteLevel(2);
            Console.WriteLine("\nTree: " + tree);

            //FIX: Adding levels gives exception
            //// Add Level 3
            //Console.WriteLine("\nAdd Level 3");
            //tree.AddLevel(3);
            //Console.WriteLine("\nTree: " + tree);

            //tree.AddChild(trong, 3);
            //Console.WriteLine("\nTree: " + tree);

            Console.WriteLine("End");
            Console.ReadKey(true);
        }
    }
}
