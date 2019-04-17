using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DataAnalysisDashboard;
using DataAccessLayer.Logging;
using DataAccessLayer;
namespace DemoProject
{
    class ProgramDashboard
    {
        public static void Main(string[] args)
        {
            TelemetryLogCollection collection = new TelemetryLogCollection();
            List<DataAccessLayer.Logging.> list = collection.GetAll();
            IDashboardService dashboard = new DashboardService();
            long result = dashboard.CountUsers();
            Console.WriteLine(result);
            Console.WriteLine("Hello");
            Console.ReadKey();
            Console.ReadLine();
        }
        
    }
}
