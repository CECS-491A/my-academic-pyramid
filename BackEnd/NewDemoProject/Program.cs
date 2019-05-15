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
            DashboardManager manager = new DashboardManager(url, database);
            DashboardService service = new DashboardService(url, database);
            IDictionary<int, long> get = service.CountSuccessfulLogin(24);
            IDictionary<int, long> get2 = service.CountFailedLogin(12);
            //IList<Double> get3 = manager.GetAverageSuccessfulLogin();
            IDictionary<int, long> get4 = service.CountSuccessfulLogin(1);

            IDictionary<int, long> test3 = service.CountFailedLogin(6);
            foreach(var s in test3)
            {
                Console.WriteLine("Is heere: " + s);
            }
            /**
            Console.WriteLine("succesufl");
            foreach (var s in get)
            {
                Console.WriteLine(s.Key + "," + s.Value);
            }
            Console.WriteLine("failed login");
            foreach (var s in get2)
            {
                Console.WriteLine(s.Key + "," + s.Value);
            }
            Console.WriteLine("Avg successful login");
            foreach (var s in get3)
            {
                Console.WriteLine(s.Key + "," + s.Value);
            }
            **/
            //ICollection<DateTime> list = service.CountAverageSessionDuration(1, 2019);
            //for (int i = 0; i < list.Count(); i++)
            //{
            //    Console.WriteLine("Print:" + list.ElementAt(i));
            //}
            long sum = service.CountUniqueLoggedInUsers(1, 2019);
            Console.WriteLine("sum:" + sum);
            //IDictionary<string, long> something3 = service.CountMostUsedFiveFeature(2);
            Console.WriteLine("hi");


            Console.ReadKey();
        }
    }
}
