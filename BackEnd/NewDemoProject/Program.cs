using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Logging;
using DataAccessLayer;
using ManagerLayer;
using ServiceLayer.DataAnalysisDashboard;
using ManagerLayer.Gateways.UsageAnalysisDashboard;
namespace NewDemoProject
{
    class Program
    {
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";

        static void Main(string[] args)
        {
            //var result = collection.GetAll();
            //foreach (var re in result)
            //{
            //    Console.WriteLine(re.Action + ", " + re.Date.ToString());
            //}
            //long[] something2 = (long[]) manager.GetFailedSuccessfulLogIn();
            //Console.WriteLine(something2[0]);

            //long[] something = temp.GetSuccessfulLogin();
            //Console.WriteLine("End");
            Console.WriteLine("wow");
   
            
            Console.ReadKey();
        }
    }
}
