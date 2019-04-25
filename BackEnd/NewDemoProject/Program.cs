using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Logging;
using DataAccessLayer;
using ManagerLayer;
using ServiceLayer.DataAnalysisDashboard;
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
            DashboardService temp = new DashboardService();
            Dictionary<string, long> something3 = temp.CountMostUsedFeature();
            foreach (var tmp in something3)
            {
                Console.WriteLine(tmp);
            }
            long[] something2 = temp.CountFailedSuccessfulLogIn();
            Console.WriteLine(something2[0]);
            long[] something = temp.CountSuccessfulLogin();
            Console.WriteLine("End");
            
            foreach (long i in something)
            {
                Console.WriteLine("Finally: " + i);
            }
            
            Console.ReadKey();
        }
    }
}
