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
            var result = collection.GetAll();
            Console.In.Read();
            foreach (var re in result)
            {
                Console.WriteLine(re.Action + ", " + re.Date);
            }
            IDashboardService temp = new DashboardService();
            Console.ReadKey();
        }
    }
}
