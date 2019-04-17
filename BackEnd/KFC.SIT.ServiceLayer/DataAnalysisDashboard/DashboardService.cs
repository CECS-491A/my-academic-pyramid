using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Logging;
using DataAccessLayer.Models;
using System.Collections.Generic;
using MongoDB.Driver;
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
            long queryResult = Collection.CountDocuments(new BsonDocument { { "Action", "Login" } });
            return queryResult;
        }
    }
}