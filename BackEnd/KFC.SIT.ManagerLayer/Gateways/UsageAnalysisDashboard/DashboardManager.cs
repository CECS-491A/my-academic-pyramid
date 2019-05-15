using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using ServiceLayer.DataAnalysisDashboard;

namespace ManagerLayer.Gateways.UsageAnalysisDashboard
{
    public class DashboardManager
    {
        private DashboardService _dashboardService;
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";
        public static string[] dateFormatConverter = { "Empty", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        public DashboardManager(string url, string database)
        {
            _dashboardService = new DashboardService(url, database);
        }

        /// <summary>
        /// Get the list of number of successful login and list of number of failed login per month 
        /// Divide number of successful login by the number of total login attempts
        /// The avgLogin will be sorted in reverse chronological order
        /// It will get the data of recent 12 month according to the business rules. 
        /// </summary>
        /// <returns>avgLogin</returns>
        public IDictionary<string, double> GetAverageSuccessfulLogin()
        {
            int numOfMonth = BusinessRuleConstants.GetAverageSuccessfulLogin_NumOfMonth;
            IDictionary<int, long> successLogin = _dashboardService.CountSuccessfulLogin(numOfMonth);
            IDictionary<int, long> failedLogin = _dashboardService.CountFailedLogin(numOfMonth);
            IDictionary<string, double> avgLogin = new Dictionary<string, double>();
            int monthToday = DateTime.Today.Month;

            // Calculate average successful login per month
            for (int i = 1; i < numOfMonth + 1; i++)
            {
                double avgSuccessfulLogin = 0;

                if (successLogin.ContainsKey(monthToday)) // checks there is logged in user in the month
                {
                    double numSuccessfulLogin = 0;
                    double numFailedLogin = 0;
                    numSuccessfulLogin = successLogin[monthToday];

                    if (failedLogin.ContainsKey(monthToday)) // checks there is failed logged attempt in the month
                    {
                        numFailedLogin = failedLogin[monthToday];
                    }
                    double totalLoginAttempt = numSuccessfulLogin + numFailedLogin;
                    avgSuccessfulLogin = numSuccessfulLogin / totalLoginAttempt;
                }
                avgLogin.Add(dateFormatConverter[monthToday], avgSuccessfulLogin * 100); // percentage
                monthToday--;
                if (monthToday == 0) { monthToday = 12; }
            }
            return avgLogin;
        }

        /// <summary>
        /// Get the list of successful login and the number of total session time in specific duration
        /// Get average session duration by dividing the total session duration by number of successful login
        /// return the dictionary that contains months and average session duration per month
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, double> GetAverageSessionDuration()
        {
            int numOfMonth = BusinessRuleConstants.GetAverageSessionDuration_NumOfMonth; // 6
            int monthToday = DateTime.Today.Month;
            int yearToday = DateTime.Today.Year;
            IDictionary<string, double> monthAvgSessionDuration = new Dictionary<string, double>();

            // Request the total session time for the number of months
            // Flaw: request the data to the database for numOfMonth(6) times, it slows down the performance
            for (int i = 0; i < numOfMonth; i++)
            {
                double avgSessionTime = _dashboardService.CountAverageSessionTime(monthToday, yearToday);
                monthAvgSessionDuration.Add(dateFormatConverter[monthToday], avgSessionTime);
                monthToday--;
                if (monthToday == 0) { monthToday = 12; yearToday--; }
            }

            return monthAvgSessionDuration;
        }

        /// <summary>
        /// Get total number of successful login, failed login and attempted login from the service layer and save into the list
        /// index 0 = Total number of successful login
        /// index 1 = Total number of failed login
        /// index 2 = Total number of attempted login
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> GetFailedSuccessfulLogIn()
        {
            IDictionary<string, long> totalSuccessFailed = new Dictionary<string, long>();

            long numSuccessLogin = _dashboardService.CountTotalSuccessfulLogin();
            long numFailedLogin = _dashboardService.CountTotalFailedLogin();
            long totalLogin = numSuccessLogin + numFailedLogin;

            totalSuccessFailed.Add(BusinessRuleConstants.GetFailedSuccessfulLogIn_Total, totalLogin); // Total
            totalSuccessFailed.Add(BusinessRuleConstants.GetFailedSuccessfulLogIn_Successful, numSuccessLogin); // Successful
            totalSuccessFailed.Add(BusinessRuleConstants.GetFailedSuccessfulLogIn_Failed, numFailedLogin); // Failed

            return totalSuccessFailed;
        }

        public IDictionary<string, double> GetMostAverageTimeSpentPage()
        {
            int numOfPages = BusinessRuleConstants.GetMostAverageTimeSpentPage_NumOfPage; // 5
            IDictionary<string, double> pageTime = _dashboardService.CountAverageTimeSpentPage();
            IDictionary<string, double> average = new Dictionary<string, double>();

            var pageTimeSorted = from page in pageTime orderby page.Value descending select (page.Key, page.Value);
            pageTimeSorted = pageTimeSorted.Take(numOfPages);

            // add an item into the dictionary
            foreach (var pageNum in pageTimeSorted)
            {
                average.Add(pageNum.Key, pageNum.Value);
            }

            return average;
        }

        /// <summary>
        /// Get the list of five features that are used most
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> GetMostUsedFeature()
        {
            IDictionary<string, long> featureNumUsed = _dashboardService.CountMostUsedFeature(BusinessRuleConstants.GetMostUsedFeature_FeatureNumber);
            return featureNumUsed;
        }

        // Line Chart

        /// <summary>
        /// Get the list of logged in users and number of total users 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> GetSuccessfulLoggedInUsers()
        {
            int duration = BusinessRuleConstants.GetSuccessfulLoggedInUsers_Duartion; // 6
            Dictionary<string, long> successfulLoggedInUsers = new Dictionary<string, long>();
            int monthToday = DateTime.Today.Month;
            int yearToday = DateTime.Today.Year;

            // Get the number of unique logged in users during the specific month
            // Flaw: request the data to the database for duration(6) times, it slows down the performance
            for (int i = 1; i < duration + 1; i++)
            {
                successfulLoggedInUsers.Add(dateFormatConverter[monthToday], _dashboardService.CountUniqueLoggedInUsers(monthToday, yearToday));
                monthToday--;
                if (monthToday == 0) { monthToday = 12; yearToday--; }
            }

            return successfulLoggedInUsers;
        }

    }
}
