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
            DashboardManager temp = new DashboardManager(url, database);
            DashboardService temp2 = new DashboardService(url, database);
            IList<long> get = temp2.CountFailedLogin(6);
            IList<long> get2 = temp.GetAverageSuccessfulLogin();
            foreach (var s in get2)
            {
                Console.WriteLine(s);
            }
            Dictionary<string, long> something3 = temp.GetMostUsedFeature();
            foreach (var tmp in something3)
            {
                Console.WriteLine(tmp);
            }
            long[] something2 = temp.GetFailedSuccessfulLogIn();
            //Console.WriteLine(something2[0]);

            //long[] something = temp.GetSuccessfulLogin();
            Console.WriteLine("End");
   
            
            Console.ReadKey();
        }
    }
}
