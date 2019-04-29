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
        public IDictionary<int, double> GetAverageSuccessfulLogin()
        {
            int numOfMonth = 12;
            IDictionary<int, long> successLogin = _dashboardService.CountSuccessfulLogin(numOfMonth);
            IDictionary<int, long> failedLogin = _dashboardService.CountFailedLogin(numOfMonth);
            Dictionary<int, double> avgLogin = new Dictionary<int, double>();
            int monthToday = DateTime.Today.Month;

            for (int i = 1; i < numOfMonth + 1; i++)
            {
                double avgSuccessfulLogin = 0;

                if (successLogin.ContainsKey(monthToday))
                {
                    double numSuccessfulLogin = 0;
                    double numFailedLogin = 0;
                    numSuccessfulLogin = successLogin[monthToday];

                    if (failedLogin.ContainsKey(monthToday))
                    {
                        numFailedLogin = failedLogin[monthToday];
                    }
                    double totalLoginAttempt = numSuccessfulLogin + numFailedLogin;
                    avgSuccessfulLogin = numSuccessfulLogin / totalLoginAttempt;
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

        public IDictionary<string, long> GetAverageTimeSpentPage()
        {
            IDictionary<string, long> featureTime = _dashboardService.CountAverageTimeSpentPage();
            return featureTime;
        }

        /// <summary>
        /// Get the list of five features that are used most
        /// parameter = number of features
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, long> GetMostUsedFeature()
        {
            IDictionary<string, long> featureNumUsed = _dashboardService.CountMostUsedFiveFeature(5);
            return featureNumUsed;
        }

        public long[] GetAvgSessionDurationSixMonth()
        {
            throw new Exception();
        }

        public long[] GetSuccessfulLoggedInUsers()
        {
            throw new Exception();
           // long[] numLogin = _dashboardService.CountSuccessfulLogin();
           // return numLogin;
        }



    }
}