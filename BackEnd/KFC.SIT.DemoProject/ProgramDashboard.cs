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
        static void Main(string[] args)
        {
            TelemetryLogCollection collection = new TelemetryLogCollection();
            var result = collection.GetAll();
            foreach(var re in result)
            {
                Console.WriteLine(re.Action + ", " + re.Date);
            }
            IDashboardService dashboard = new DashboardService();
            //long result = dashboard.CountUsers();
            Console.WriteLine(result);
            Console.WriteLine("Hello");
            Console.ReadKey();
            Console.ReadLine();
        }
        
    }
}
