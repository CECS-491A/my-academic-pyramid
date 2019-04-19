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

namespace ServiceLayer.DataAnalysisDashboard
{
    public class DashboardService : IDashboardService
    {
        private const string _collectionName = "TelemetryLogs";

        private readonly IMongoCollection<TelemetryLog> Collection;

        protected MongoDBRepo _repo;

        public DashboardService()
        {
            _repo = new MongoDBRepo("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true", "test");
            Collection = _repo.Db.GetCollection<TelemetryLog>(_collectionName);
        }

        public long CountUsers()
        {
            DateTime currentTime = DateTime.Now;
            long queryResult = Collection.CountDocuments(new BsonDocument { { "Action", "Login" } });
            return queryResult;
        }

        public long CountSuccessfulLogin(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 0);
            int numOfDays = DateTime.DaysInMonth(year, month);
            DateTime endDate = new DateTime(year, month, numOfDays);
            long queryResult = Collection.AsQueryable<TelemetryLog>().ToList();
            return queryResult;
        }

        public long CountFeatureUsage()
        {
            long queryResult = Collection.CountDocuments(new BsonDocument { { "Action", "Feature" } });
            return queryResult;
        }

        public long CountDate()
        {
            var builder = Builders<BsonDocument>.Filter;
            DateTime time = DateTime.Now;
            int value = time.Second;
            long queryResult = Collection.CountDocuments(i => i.Date. > "30");
            return queryResult;
        }

    }
}