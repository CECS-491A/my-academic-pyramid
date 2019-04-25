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
        static void Main(string[] args)
        {
            TelemetryLogCollection collection = new TelemetryLogCollection();
            //var result = collection.GetAll();
            //foreach (var re in result)
            //{
            //    Console.WriteLine(re.Action + ", " + re.Date.ToString());
            //}
            DashboardManager temp = new DashboardManager();
            Dictionary<string, long> something3 = temp.GetMostUsedFeature();
            foreach (var tmp in something3)
            {
                Console.WriteLine(tmp);
            }
            long[] something2 = temp.GetFailedSuccessfulLogIn();
            //Console.WriteLine(something2[0]);

            long[] something = temp.GetSuccessfulLogin();
            Console.WriteLine("End");
            
            foreach (long i in something)
            {
                Console.WriteLine("Finally: " + i);
            }
            
            Console.ReadKey();
        }
    }
}
