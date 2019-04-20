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
            long[] something = temp.CountAverageSuccessfulLogin();
            foreach (long i in something)
            {
                Console.WriteLine("Finally: " + i);
            }
            Console.ReadKey();
        }
    }
}
