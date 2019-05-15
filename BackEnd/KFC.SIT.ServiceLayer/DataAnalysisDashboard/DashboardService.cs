using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using DataAccessLayer.UADConstants;
using System.Collections;
using System.Collections.ObjectModel;
using DataAccessLayer.Logging;

namespace ServiceLayer.DataAnalysisDashboard
{
    public class DashboardService
    {
        private const string _collectionTName = "TelemetryLogs";
        private const string _collectionEName = "ErrorLogs";

        private readonly IMongoCollection<TelemetryLog> _collectionT;
        private readonly IMongoCollection<ErrorLog> _collectionE;

        protected LogRepository<TelemetryLog> _repoTelemetry;
        protected LogRepository<ErrorLog> _repoError;

        public DashboardService(string url, string database)
        {
            _repoTelemetry = new LogRepository<TelemetryLog>(_collectionTName);
            _repoError = new LogRepository<ErrorLog>(_collectionEName);
            _collectionT = _repoTelemetry._logCollection;
            _collectionE = _repoError._logCollection;
        }

        /// <summary>
        /// Use the query to get the list of the number of successful logged in users.
        /// Each element represents a month and sorted by date. Starts from Jan to Dec.
        /// </summary>
        /// <returns>successLogin</returns>
        public IDictionary<int, long> CountSuccessfulLogin(int numOfMonth)
        {
            Dictionary<int, long> successLogin = new Dictionary<int, long>();
            var queryResult = _collectionT.Aggregate()
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
            var queryResult = _collectionE.Aggregate()
                            .Match(x => x.Request == MongoDBAction.Login)
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
        public double CountTotalSessionTime(int chosenMonth, int chosenYear)
        {
            DateTime validationFirstDayMonth = new DateTime(chosenYear, chosenMonth, 1);
            DateTime validationLastDayMonth = new DateTime(chosenYear, chosenMonth, DateTime.DaysInMonth(chosenYear, chosenMonth)).AddDays(1);
            var query = _collectionT.Aggregate()
                        .Match(x => x.Action == MongoDBAction.Login || x.Action == MongoDBAction.Logout)
                        .Match(x => x.Date >= validationFirstDayMonth)
                        .Match(x => x.Date < validationLastDayMonth)
                        .Group(
                x => x.UserName,
                i => new
                {
                    Name = i.Select(x => x.UserName).First(),
                    Time = i.Select(x => x.Date).ToList(),
                })
                .ToList();

            double totalSessionTime = 0;
            foreach (var monthly in query)
            {
                Console.WriteLine(monthly.Name);
                int validationLogout = 0;
                DateTime dateTime = new DateTime();
                foreach(var userTime in monthly.Time)
                {
                    if (validationLogout % 2 == 0) // time that user logged in
                    {
                        dateTime = userTime;
                    }
                    else // find the session time of the user.
                    {
                        totalSessionTime += (dateTime.Subtract(userTime).TotalMinutes);
                    }
                    validationLogout++;
                    Console.WriteLine(userTime);
                }
            }
            return totalSessionTime;
        }

        /// <summary>
        /// Get the total number of successful logins from CollectionT
        /// </summary>
        /// <returns></returns>
        public long CountTotalSuccessfulLogin()
        {
            long queryResult = _collectionT.CountDocuments(x => x.Action == MongoDBAction.Login);
            return queryResult;
        }

        /// <summary>
        /// Get the total number of failed logins from CollectionE
        /// </summary>
        /// <returns></returns>
        public long CountTotalFailedLogin()
        {
            long queryResult = _collectionE.CountDocuments(x => x.Request == MongoDBAction.Login);
            return queryResult;
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
            var query = _collectionT.Aggregate()
                        .Match(x =>
                        x.Action == MongoDBAction.Page[0] ||
                        x.Action == MongoDBAction.Page[1] ||
                        x.Action == MongoDBAction.Page[2] ||
                        x.Action == MongoDBAction.Page[3] ||
                        x.Action == MongoDBAction.Page[4] ||
                        x.Action == MongoDBAction.Page[5] ||
                        x.Action == MongoDBAction.Page[6]
                        )
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
        public IDictionary<string, long> CountMostUsedFeature(int numOfFeature)
        {
            Dictionary<string, long> featureNum = new Dictionary<string, long>();
            var query = _collectionT.Aggregate()
                        .Match(x =>
                        x.Action == MongoDBAction.Feature[0] ||
                        x.Action == MongoDBAction.Feature[1] ||
                        x.Action == MongoDBAction.Feature[2] ||
                        x.Action == MongoDBAction.Feature[3] ||
                        x.Action == MongoDBAction.Feature[4]
                        )
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
        public long CountUniqueLoggedInUsers(int chosenMonth, int chosenYear)
        {
            DateTime validationFirstDayMonth = new DateTime(chosenYear, chosenMonth, 1);
            DateTime validationLastDayMonth = new DateTime(chosenYear, chosenMonth, DateTime.DaysInMonth(chosenYear, chosenMonth)).AddDays(1);
            var query = _collectionT.Aggregate()
                        .Match(x => x.Action == MongoDBAction.Login)
                        .Match(x => x.Date >= validationFirstDayMonth)
                        .Match(x => x.Date < validationLastDayMonth)
                        .Group(
                x => x.UserName,
                i => new
                {
                    Name = i.Select(x => x.UserName).First()
                }
                )
                .ToList()
                .Count();
            
            return query;
       }

    }
}
