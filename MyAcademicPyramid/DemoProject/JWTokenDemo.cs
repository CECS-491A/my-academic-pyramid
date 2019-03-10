using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityLayer;

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

            String jsonDict = JWToken.GenerateToken(test, "");

            Console.In.Read();
            Console.Out.WriteLine("Ending program.");

        }
        


    }
}
