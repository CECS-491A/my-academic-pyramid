//using DataAccessLayer;
//using Service_Layer.UserManagement.UserTree;
//using ServiceLayer.UserManagement.UserTree;
//using System;

//namespace DemoProject
//{
//    class ProgramUMTree
//    {
//        static void Main(string[] args)
//        {
//            // Create users
//            Console.WriteLine("Create Users:");
//            User krystal = new User("Krystal");
//            User luis = new User("Luis");
//            User trong = new User("Trong");
//            User arturo = new User("Arturo");
//            User kevin = new User("Kevin");
//            User victor = new User("Victor");

//            Console.WriteLine(krystal.Id + ":" + krystal.UserName);
//            Console.WriteLine(luis.Id + ":" + luis.UserName);
//            Console.WriteLine(trong.Id + ":" + trong.UserName);
//            Console.WriteLine(arturo.Id + ":" + arturo.UserName);
//            Console.WriteLine(kevin.Id + ":" + kevin.UserName);
//            Console.WriteLine(victor.Id + ":" + victor.UserName);

//            // Create tree
//            Console.WriteLine("\nCreate Tree");
//            Tree tree = new Tree(new Node("Root"));

//            // Add Children
//            Console.WriteLine("\nAdd Children");
//            tree.AddUser(krystal, tree.Root.User);
//            Console.WriteLine("Add Krystal under root");
//            tree.AddUser(luis, 1);
//            Console.WriteLine("Add Luis to level 1");
//            tree.Root.PrintTree("", true);
            
//            tree.AddUser(trong, 5);
//            Console.WriteLine("\nAdd Trong to invalid level 5");
//            tree.AddUser(krystal, 2);
//            Console.WriteLine("Add duplicate Krystal to level 2");
//            Console.WriteLine("Tree should not change.");
//            tree.Root.PrintTree("", true);

//            Console.WriteLine("\nAdd more children");
//            tree.AddUser(trong, krystal);
//            Console.WriteLine("Add Trong as child of Krystal");
//            tree.AddUser(arturo, trong);
//            Console.WriteLine("Add Arturo as child of Trong");
//            tree.AddUser(kevin, arturo);
//            Console.WriteLine("Add Kevin as child of Arturo");
//            tree.AddUser(victor, luis);
//            Console.WriteLine("Add Victor as child Luis");
//            tree.Root.PrintTree("", true);

//            Console.WriteLine("\nDelete Node with children");
//            tree.DeleteUser(trong);
//            Console.WriteLine("Delete Trong, move children to another node with same level.");
//            tree.Root.PrintTree("", true);

//            Console.WriteLine("\nDelete Node with no children");
//            tree.DeleteUser(kevin);
//            Console.WriteLine("Delete Kevin");
//            tree.Root.PrintTree("", true);
            
//            tree.DeleteUser(victor);
//            Console.WriteLine("\nDelete Victor, move children up a level, no nodes with same level");
//            tree.Root.PrintTree("", true);

//            // Move Child
//            tree.MoveUser(arturo, krystal);
//            Console.WriteLine("\nMove Arturo to child of Krystal");
//            tree.Root.PrintTree("", true);

//            Console.WriteLine("\nCompare Authorization");
//            Node l = tree.Root.FindUser(luis);
//            Node a = tree.Root.FindUser(arturo);
//            Console.WriteLine("Luis is a direct parent of Arturo: " + l.IsDirectParentOf(arturo));
//            Console.WriteLine("Luis is above Arturo: " + l.IsAbove(a));
            
//            Console.WriteLine("\nEnd");
//            Console.ReadKey(true);
//        }
//    }
//}
