﻿using System;
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

    public class Test
    {
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";
        static void main(string[] args)
        {
            DashboardManager manager = new DashboardManager(url, database);
            DashboardService service = new DashboardService(url, database);
            IDictionary<int, long> get = service.CountSuccessfulLogin(12);
            IDictionary<int, long> get2 = service.CountFailedLogin(12);
            IDictionary<int, double> get3 = manager.GetAverageSuccessfulLogin();

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

            IDictionary<string, long> something3 = service.CountMostUsedFiveFeature(3);
            foreach (var tmp in something3)
            {
                Console.WriteLine(tmp);
            }
        }
    }
}