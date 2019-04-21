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
    public class DashboardService : IDashboardService
    {
        private const string _collectionTName = "TelemetryLogs";
        private const string _collectionEName = "ErrorLogs";

        private readonly IMongoCollection<TelemetryLog> CollectionT;
        private readonly IMongoCollection<ErrorLog> CollectionE;

        protected MongoDBRepo _repo;

        public DashboardService()
        {
            _repo = new MongoDBRepo("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true", "test");
            CollectionT = _repo.Db.GetCollection<TelemetryLog>(_collectionTName);
            CollectionE = _repo.Db.GetCollection<ErrorLog>(_collectionEName);
        }

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

        public long[] CountFailedSuccessfulLogIn()
        {
            long[] attemptLogins = new long[2];
            Task<long> queryS = CollectionT.CountDocumentsAsync(x => x.Action == "Login");
            Task<long> queryF = CollectionE.CountDocumentsAsync(x => x.Action == "Login");

            attemptLogins[0] = queryS.Result;
            attemptLogins[1] = queryF.Result;
            return attemptLogins;
        }

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

        public Dictionary<string, long> CountMostUsedFeature()
        {
            Dictionary<string, long> featureNum = new Dictionary<string, long>();
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "Feature" || x.Action == "Feature2")
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

        public long[] CountSuccessfulLogin()
        {
            long[] avgLoginMonth = new long[6];
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "Login")
                        .SortByDescending(x => x.Date)
                        .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.UserName)
                }
                ).ToList();

            int count = 0;
            foreach (var monthly in query)
            {
                //monthly.Result.AsQueryable().GroupBy(monthly.Result)
                count++;
                if (count == 6) { break; }
            }
            return avgLoginMonth;
        }

    }
}
