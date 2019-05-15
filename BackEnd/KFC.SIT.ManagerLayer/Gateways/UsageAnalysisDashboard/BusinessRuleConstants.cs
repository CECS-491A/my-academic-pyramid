using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerLayer.Gateways.UsageAnalysisDashboard
{
    public class BusinessRuleConstants
    {
        // Bar Chart
        public static int GetMostUsedFeature_FeatureNumber = 5;

        public static int GetSuccessfulLoggedInUsers_Duartion = 6;

        public static int GetAverageSessionDuration_NumOfMonth = 6;

        public static int GetAverageSuccessfulLogin_NumOfMonth = 6;

        public static string GetFailedSuccessfulLogIn_Total = "Total";
        public static string GetFailedSuccessfulLogIn_Successful = "Successful";
        public static string GetFailedSuccessfulLogIn_Failed = "Failed";

        // Line Chart

    }
}