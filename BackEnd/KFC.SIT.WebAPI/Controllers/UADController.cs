using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using ManagerLayer.Gateways.UsageAnalysisDashboard;
using DataAccessLayer.Models.Dashboard;
using System.Collections.Generic;

namespace KFC.SIT.WebAPI.Controllers
{
   [EnableCors(origins: "*", headers: "*", methods: "*")]
   public class UADController : ApiController
   {
       private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
       private const string database = "test";
       private DashboardManager _dashboardManager = new DashboardManager(url, database);

        [HttpGet]
        [ActionName("AvgSuccessfulLogin")]
        public GraphData<double> GetAvgSuccessfulLogin()
        {
            IDictionary<string, double> avgSuccessfulLogin = _dashboardManager.GetAverageSuccessfulLogin();
            GraphData<double> successfulLoginData = new GraphData<double>(avgSuccessfulLogin.Keys, avgSuccessfulLogin.Values);
            return successfulLoginData;
        }

        [HttpGet]
        [ActionName("AvgSessionDuration")]
        public GraphData<double> GetAvgSessionDuration()
        {
            IDictionary<string, double> avgSessionDuration = _dashboardManager.GetAverageSessionDuration();
            GraphData<double> avgSessionDurationData = new GraphData<double>(avgSessionDuration.Keys, avgSessionDuration.Values);
            return avgSessionDurationData;
        }

        [HttpGet]
        [ActionName("TotalFailedSuccessfulLogin")]
        public GraphData<long> GetTotalFailedSuccessfulLogin()
        {
            IDictionary<string, long> totalSuccessfulFailed = _dashboardManager.GetFailedSuccessfulLogIn();
            GraphData<long> totalSuccessFailedNumData = new GraphData<long>(totalSuccessfulFailed.Keys, totalSuccessfulFailed.Values);
            return totalSuccessFailedNumData;
        }

        [HttpGet]
        [ActionName("MostVisitedPage")]
        public GraphData<double> GetMostVisitedPage()
        {
            IDictionary<string, double> mostVisitedPage = _dashboardManager.GetMostAverageTimeSpentPage();
            GraphData<double> mostVisitedPageData = new GraphData<double>(mostVisitedPage.Keys, mostVisitedPage.Values);
            return mostVisitedPageData;
        }

        [HttpGet]
        [ActionName("MostUsedFeature")]
        public GraphData<long> GetMostUsedFeature()
        {
            IDictionary<string, long> mostUsedFeature = _dashboardManager.GetMostUsedFeature();
            GraphData<long> featureNameNumUsedData = new GraphData<long>(mostUsedFeature.Keys, mostUsedFeature.Values);
            return featureNameNumUsedData;
        }

        [HttpGet]
        [ActionName("UniqueLoggedInUser")]
        public GraphData<long> GetUniqueLoggedInUser()
        {
            IDictionary<string, long> successfulLoggedInUser = _dashboardManager.GetSuccessfulLoggedInUsers();
            GraphData<long> successfulLoggedInUserData = new GraphData<long>(successfulLoggedInUser.Keys, successfulLoggedInUser.Values);
            return successfulLoggedInUserData;
        }

   }
}
