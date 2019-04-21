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

        public DashboardManager()
        {
            _dashboardService = new DashboardService();
        }

        public void GetAverageSuccessfulLogin()
        {
            long[] avgSuccessLog = _dashboardService.CountAverageSuccessfulLogin();

        }

        public void GetAverageSessionDuration()
        {
            long[] avgSessionDur = _dashboardService.CountAverageSessionDuration();

        }

        public void GetFailedSuccessfulLogIn()
        {
            long[] logFS = _dashboardService.CountFailedSuccessfulLogIn();

        }

        public void GetAverageTimeSpentPage()
        {
            Dictionary<string, long> featureTime = _dashboardService.CountAverageTimeSpentPage();
        }

        public void GetMostUsedFeature()
        {
            Dictionary<string, long> featureNumUsed = _dashboardService.CountMostUsedFeature();
        }

        public void GetSuccessfulLogin()
        {
            long[] numLogin = _dashboardService.CountSuccessfulLogin();
        }





    }
}