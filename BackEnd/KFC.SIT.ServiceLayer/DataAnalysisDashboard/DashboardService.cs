using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Logging;
using DataAccessLayer.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace ServiceLayer.DataAnalysisDashboard
{
    public class DashboardService
    {
        private const string _collectionTName = "TelemetryLogs";
        private const string _collectionEName = "ErrorLogs";

        private readonly IMongoCollection<TelemetryLog> CollectionT;
        private readonly IMongoCollection<ErrorLog> CollectionE;

        protected MongoDBRepo _repo;

        public DashboardService(string url, string database)
        {
            _repo = new MongoDBRepo(url, database);
            CollectionT = _repo.Db.GetCollection<TelemetryLog>(_collectionTName);
            CollectionE = _repo.Db.GetCollection<ErrorLog>(_collectionEName);
        }

        /// <summary>
        /// Get a list of total number of successful login per month from the MongoDB using a query
        /// Divide that by the number of students
        /// The order would be chrnological order January to December
        /// </summary>
        /// <returns></returns>
        public long[] CountAverageSuccessfulLogin()
        {
            long[] avgLoginMonth = new long[12];
            long[] failedLogIn = new long[12];
            var queryS = CollectionT.Aggregate()
                        .Match(x => x.Action == "Login")
                        .SortByDescending(x => x.Date)
                        .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.ID).Count()
                }
                ).ToList();

            var queryF = CollectionE.Aggregate()
                        .Match(x => x.Action == "Login")
                        .SortByDescending(x => x.Date)
                        .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.ID).Count()
                }
                ).ToList();

            int count = 0;
            foreach (var monthly in queryF)
            {
                failedLogIn[count] = monthly.Result;
                count++;
                if (count == 12) { break; }
            }
            count = 0;
            foreach (var monthly in queryS)
            {
                avgLoginMonth[count] = monthly.Result / (failedLogIn[count] + monthly.Result);
                count++;
                if (count == 12) { break; }
            }
            return avgLoginMonth;
        }

        /// <summary>
        /// Get a list of session duration per month from the MongoDB using a query
        /// Divide that by the number of students
        /// The order would be chrnological order January to December
        /// </summary>
        /// <returns></returns>
        public long[] CountAverageSessionDuration()
        {
            long[] avgSessionDurMonth = new long[12];
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "Login" || x.Action == "Logout")
                        .SortByDescending(x => x.Date)
                        .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.ID).Count(),
                }
                ).ToList();

            int count = 0;
            foreach (var monthly in query)
            {
                avgSessionDurMonth[count] = monthly.Result;
                count++;
                if (count == 12) { break; }
            }
            return avgSessionDurMonth;
        }

        /// <summary>
        /// Get the number of successful logins from CollectionT and Get the number of failed logins from CollectionE
        /// Save them into the list and return it
        /// </summary>
        /// <returns></returns>
        public long[] CountFailedSuccessfulLogIn()
        {
            long[] attemptLogins = new long[2];
            Task<long> queryS = CollectionT.CountDocumentsAsync(x => x.Action == "Login");
            Task<long> queryF = CollectionE.CountDocumentsAsync(x => x.Action == "Login");

            attemptLogins[0] = queryS.Result;
            attemptLogins[1] = queryF.Result;
            return attemptLogins;
        }
    
        /// <summary>
        /// Get the average time that user spent per page and top five pages that user spent their time most
        /// Save them into the dictionary and return it
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, long> CountAverageTimeSpentPage()
        {
            // need to fix it @Todo
            Dictionary<string, long> avgTime = new Dictionary<string, long>();
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "???" || x.Action == "???")
                        .Group(
                x => x.Action,
                i => new
                {
                    Result = i.Select(x => x.ID).Count(),
                }
                ).ToList();

            return avgTime;
        }

        /// <summary>
        /// Count how many times user used the each feature and get the feature name
        /// Save them into the dictionary and return it
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, long> CountMostUsedFeature()
        {
            Dictionary<string, long> featureNum = new Dictionary<string, long>();
            string[] features = { "Feature", "Feature2" };
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == features[0] || x.Action == features[1])
                        .Group(
                x => x.Action,
                i => new
                {
                    NumUsed = i.Select(x => x.ID).Count(),
                    Feature = i.Select(x => x.Action).First()
                }
                ).ToList();

            foreach (var feature in query)
            {
                featureNum.Add(feature.Feature, feature.NumUsed);
            }

            return featureNum;
        }

        /// <summary>
        /// Count the number of logged in users per month over 6 months and save them into the list
        /// return it
        /// </summary>
        /// <returns></returns>
        public long[] CountSuccessfulLogin()
        {
            long[] avgLoginMonth = new long[6];
            for (int i = 0; i < 6; i++)
            {

            }
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "Login")
                        .SortByDescending(x => x.Date)
                        .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.UserName).ToList(),
                    sum = i.Select(x => x.UserName).Count()
                }
                ).ToList();
            string[] list = new string[12];

            int count = 0;
            int sum = 0;
            foreach (var monthly in query)
            {
                Console.WriteLine("Line___________________________" + monthly.Result + monthly.sum);
                List<string> temp = monthly.Result;
                foreach(var a in temp)
                {
                    Console.WriteLine(a);
                }
            }
            return avgLoginMonth;
        }

    }
}
