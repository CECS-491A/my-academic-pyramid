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

        public IList<string> GetRecentMonths()
        {
            int numOfMonth = BusinessRuleConstants.GetRecentMonths_Months;
            int monthToday = DateTime.Today.Month;
            IList<string> dateList = new List<string>();
            for (int i = 0; i < numOfMonth; i++)
            {
                dateList.Add(dateFormatConverter[monthToday]);
                monthToday--;
                if (monthToday == 0) { monthToday = 12; }
            }
            return dateList;
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

            for (int i = 1; i < numOfMonth + 1; i++)
            {
                double avgSuccessfulLogin = 0;

                if (successLogin.ContainsKey(monthToday)) // checks there is logged in user in the month
                {
                    double numSuccessfulLogin = 0;
                    double numFailedLogin = 0;
                    numSuccessfulLogin = successLogin[monthToday];

                    if (failedLogin.ContainsKey(monthToday)) // checks there is failed logged attempts in the month
                    {
                        numFailedLogin = failedLogin[monthToday];
                    }
                    double totalLoginAttempt = numSuccessfulLogin + numFailedLogin;
                    avgSuccessfulLogin = numSuccessfulLogin / totalLoginAttempt;
                }
                avgLogin.Add(dateFormatConverter[monthToday], avgSuccessfulLogin);
                monthToday--;
                if (monthToday == 0) { monthToday = 12; }
            }
            return avgLogin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long[] GetAverageSessionDuration()
        {
            int duration = 6;
            int monthToday = DateTime.Today.Month;
            int yearToday = DateTime.Today.Year;
            long[] output = new long[1000];
            IDictionary<int, long> numLoginInCertainDuration = _dashboardService.CountSuccessfulLogin(6);
            for (int i = 0; i < duration; i++)
            {
                ICollection<DateTime> dateLogInNOut = _dashboardService.CountAverageSessionDuration(monthToday, yearToday);
                int count = 0;
                for (int j = 0; j < dateLogInNOut.Count(); j++)
                {

                }
                monthToday--;
                if (monthToday == 0) { monthToday = 12; yearToday--; }
            }

            return null;
            //return avgSessionDur;
        }

        /// <summary>
        /// Get total number of successful login, failed login and attempted login from the service layer and save into the list
        /// index 0 = Total number of successful login
        /// index 1 = Total number of failed login
        /// index 2 = Total number of attempted login
        /// </summary>
        /// <returns></returns>
        public IEnumerable<long> GetFailedSuccessfulLogIn()
        {
            long[] successFailed = new long[3];
            successFailed[0] = _dashboardService.CountTotalSuccessfulLogin();
            successFailed[1] = _dashboardService.CountTotalFailedLogin();
            successFailed[2] = successFailed[0] + successFailed[1];

            return successFailed;
        }

        public IDictionary<string, long> GetAverageTimeSpentPage()
        {
            IDictionary<string, long> featureTime = _dashboardService.CountAverageTimeSpentPage();
            return featureTime;
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

        public long[] GetAvgSessionDurationSixMonth()
        {
            throw new Exception();
        }

        /// <summary>
        /// Get the list of logged in users and number of total users 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> GetSuccessfulLoggedInUsers()
        {
            int duration = BusinessRuleConstants.GetSuccessfulLoggedInUsers_Duartion; // 6
            Dictionary<string, long> successfulLoggedInUsers = new Dictionary<string, long>();
            long numTotalUser = _dashboardService.CountTotalUsers();
            int monthToday = DateTime.Today.Month;
            int yearToday = DateTime.Today.Year;

            for (int i = 1; i < duration + 1; i++)
            {
                successfulLoggedInUsers.Add(dateFormatConverter[monthToday], _dashboardService.CountUniqueLoggedInUsers(monthToday, yearToday));
                monthToday--;
                if (monthToday == 0) { monthToday = 12; }
            }

            return successfulLoggedInUsers;
        }

    }
}