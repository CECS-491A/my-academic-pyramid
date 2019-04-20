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

        public long CountUsers()
        {
            DateTime currentTime = DateTime.Now;
            // practice query don't touch them 
            long queryResult = CollectionT.CountDocuments(new BsonDocument { { "Action", "Login" } });
            var query2 = from userInfo in CollectionT.AsQueryable()
                         where userInfo.Action == "Login"
                         orderby userInfo.Date
                         group userInfo by userInfo.Month into monthlyInfo
                         select monthlyInfo;
            var query = from userInfo in CollectionT.AsQueryable()
                        where userInfo.Date.Month == 1
                        group userInfo by userInfo.Date.Month into monthlyInfo
                        select monthlyInfo.Max();
            return queryResult;
        }

        public long[] CountAverageSuccessfulLogin()
        {
            long[] avgLoginMonth = new long[12];
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == "Login")
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
                avgLoginMonth[count] = monthly.Result;
                count++;
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

        //public long CountSuccessfulLogin(int year, int month)
        //{
        //    DateTime startDate = new DateTime(year, month, 0);
        //    int numOfDays = DateTime.DaysInMonth(year, month);
        //    DateTime endDate = new DateTime(year, month, numOfDays);
        //    long queryResult = Collection.AsQueryable<TelemetryLog>().ToList();
        //    return queryResult;
        //}

        public long CountFeatureUsage()
        {
            long queryResult = CollectionT.CountDocuments(new BsonDocument { { "Action", "Feature" } });
            return queryResult;
        }

        //public long CountDate()
        //{
        //    var builder = Builders<BsonDocument>.Filter;
        //    DateTime time = DateTime.Now;
        //    int value = time.Second;
        //    long queryResult = Collection.CountDocuments(i => i.Date. > "30");
        //    return queryResult;
        //}

    }
}
