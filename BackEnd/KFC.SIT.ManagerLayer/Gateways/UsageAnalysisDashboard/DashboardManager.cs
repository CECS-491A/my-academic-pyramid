using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceLayer.DataAnalysisDashboard;

namespace ManagerLayer.Gateways.UsageAnalysisDashboard
{
    public class DashboardManager
    {
        private DashboardService _dashboardService;
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";

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
        public Dictionary<int, long> GetAverageSuccessfulLogin()
        {
            int numOfMonth = 12;
            Dictionary<int, long> successLogin = _dashboardService.CountSuccessfulLogin(numOfMonth);
            Dictionary<int, long> failedLogin = _dashboardService.CountFailedLogin(numOfMonth);
            Dictionary<int, long> avgLogin = new Dictionary<int, long>();
            int monthToday = DateTime.Today.Month;

            for (int i = 1; i < numOfMonth + 1; i++)
            {
                long avgSuccessfulLogin = 0;
                long numSuccessfulLogin = 0;
                long numFailedLogin = 0;

                if (successLogin.ContainsKey(i))
                {
                    numSuccessfulLogin = successLogin[i];
                    if (failedLogin.ContainsKey(i))
                    {
                        numFailedLogin = failedLogin[i];
                    }
                    long totalLoginAttempt = successLogin[i] + failedLogin[i];

                }
                avgLogin.Add(monthToday, avgSuccessfulLogin);
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
            long[] avgSessionDur = _dashboardService.CountAverageSessionDuration();
            return avgSessionDur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<long> GetFailedSuccessfulLogIn()
        {
            long[] successFailed = new long[2];
            successFailed[0] = _dashboardService.CountTotalSuccessfulLogin();
            successFailed[1] = _dashboardService.CountTotalFailedLogin();

            return successFailed;
        }

        public Dictionary<string, long> GetAverageTimeSpentPage()
        {
            Dictionary<string, long> featureTime = _dashboardService.CountAverageTimeSpentPage();
            return featureTime;
        }

        public Dictionary<string, long> GetMostUsedFeature()
        {
            Dictionary<string, long> featureNumUsed = _dashboardService.CountMostUsedFeature();
            return featureNumUsed;
        }

        public long[] GetSuccessfulLogin()
        {
            throw new Exception();
           // long[] numLogin = _dashboardService.CountSuccessfulLogin();
           // return numLogin;
        }



    }
}