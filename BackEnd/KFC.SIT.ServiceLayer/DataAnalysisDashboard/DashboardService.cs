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
    public class DashboardService
    {
        private readonly IMongoCollection<TelemetryLog> Collection;

        private const string _collectionName = "TelemetryLogs";

        MongoDBRepo _repo = new MongoDBRepo("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true", "test");

        public DashboardService()
        {
            this.Collection = _repo.Db.GetCollection<TelemetryLog>(_collectionName);
        }
    }
}