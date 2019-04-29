﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Logging;
using DataAccessLayer.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using DataAccessLayer.MongoDBQueryConstants;
using System.Collections;

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
        /// Use the query to get the list of the number of successful logged in users.
        /// Each element represents a month and sorted by date. Starts from Jan to Dec.
        /// </summary>
        /// <returns>successLogin</returns>
        public IDictionary<int, long> CountSuccessfulLogin(int numOfMonth)
        {
            Dictionary<int, long> successLogin = new Dictionary<int, long>();
            var queryResult = CollectionT.Aggregate()
                            .Match(x => x.Action == MongoDBAction.Login)
                            .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.ID).Count(),
                    Date = i.Select(x => x.Date).First()
                })
                .SortByDescending(x => x.Date)
                .Limit(numOfMonth)
                .ToList();

            foreach (var monthly in queryResult)
            {
                successLogin.Add(monthly.Date.Month, monthly.Result);
            }
            return successLogin;
        }

        /// <summary>
        /// Use the query to get the list of the number of failed logged in users.
        /// Each element represents a month and sorted by date. Starts from Jan to Dec.
        /// </summary>
        /// <returns>failedLogin</returns>
        public IDictionary<int, long> CountFailedLogin(int numOfMonth)
        {
            Dictionary<int, long> failedLogin = new Dictionary<int, long>();
            var queryResult = CollectionE.Aggregate()
                            .Match(x => x.Action == MongoDBAction.Login)
                            .Group(
                x => x.Date.Month,
                i => new
                {
                    Result = i.Select(x => x.ID).Count(),
                    Date = i.Select(x => x.Date).First()
                })
                .SortByDescending(x => x.Date)
                .Limit(numOfMonth)
                .ToList();

            foreach (var monthly in queryResult)
            {
                failedLogin.Add(monthly.Date.Month, monthly.Result);
            }
            return failedLogin;
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
        /// Get the total number of successful logins from CollectionT
        /// </summary>
        /// <returns></returns>
        public long CountTotalSuccessfulLogin()
        {
            Task<long> queryResult = CollectionT.CountDocumentsAsync(x => x.Action == "Login");
            return queryResult.Result;
        }

        /// <summary>
        /// Get the total number of failed logins from CollectionE
        /// </summary>
        /// <returns></returns>
        public long CountTotalFailedLogin()
        {
            Task<long> queryResult = CollectionE.CountDocumentsAsync(x => x.Action == "Login");
            return queryResult.Result;
        }
    
        /// <summary>
        /// Get the average time that user spent per page and top five pages that user spent their time most
        /// Save them into the dictionary and return it
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> CountAverageTimeSpentPage()
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
        public IDictionary<string, long> CountMostUsedFiveFeature(int numOfFeature)
        {
            Dictionary<string, long> featureNum = new Dictionary<string, long>();
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action != MongoDBAction.Login) // extensibility problem
                        .Group(
                x => x.Action,
                i => new
                {
                    NumUsed = i.Select(x => x.ID).Count(),
                    Feature = i.Select(x => x.Action).First()
                }
                )
                .SortByDescending(x => x.NumUsed)
                .Limit(numOfFeature)
                .ToList();

            foreach (var feature in query)
            {
                featureNum.Add(feature.Feature, feature.NumUsed);
            }

            return featureNum;
        }

        /// <summary>
        /// Count the number of logged in users in specific month
        /// return it
        /// </summary>
        /// <returns></returns>
        public long[] CountUniqueLoggedInUsers(int chosenMonth)
        {
            long[] avgLoginMonth = new long[6];
            var query = CollectionT.Aggregate()
                        .Match(x => x.Action == MongoDBAction.Login)
                        .Group(
                x => x.UserName,
                i => new
                {
                    User = i.Select(x => x.UserName).Count(),
                    Name = i.Select(x => x.UserName).First(),
                    Month = i.Select(x => x.Date.Month).First()
                }
                )
                .Match(x => x.Month == chosenMonth)
                .ToList().Count();
            int sum = query;
            Console.WriteLine(sum);
            //foreach (var s in query)
            //{
             //   Console.WriteLine(s.User + ",," + s.Name);
            //}
            
            return avgLoginMonth;
        }
    }
}
