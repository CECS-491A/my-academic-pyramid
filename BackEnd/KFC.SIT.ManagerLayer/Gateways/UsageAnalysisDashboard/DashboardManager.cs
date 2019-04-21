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




    }
}