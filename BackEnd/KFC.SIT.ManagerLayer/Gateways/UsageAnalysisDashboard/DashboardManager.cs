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

        public DashboardManager()
        {
            _dashboardService = new DashboardService(url, database);
        }

        public long[] GetAverageSuccessfulLogin()
        {
            long[] avgSuccessLog = _dashboardService.CountAverageSuccessfulLogin();

            return avgSuccessLog;

        }

        public long[] GetAverageSessionDuration()
        {
            long[] avgSessionDur = _dashboardService.CountAverageSessionDuration();
            return avgSessionDur;
        }

        public long[] GetFailedSuccessfulLogIn()
        {
            long[] logFS = _dashboardService.CountFailedSuccessfulLogIn();
            return logFS;
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
            long[] numLogin = _dashboardService.CountSuccessfulLogin();
            return numLogin;
        }

        public Dictionary<string, long> GetTotalData()
        {
            Dictionary<string, long> totalData = new Dictionary<string, long>();

            return totalData;

        }





    }
}