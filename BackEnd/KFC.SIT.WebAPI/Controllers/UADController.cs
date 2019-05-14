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
        [ActionName("sLogin")]
        public GraphData<double> GetScuessfulLogin()
        {
            IDictionary<string, double> avgSuccessfulLogin = _dashboardManager.GetAverageSuccessfulLogin();
            GraphData<double> successfulLogin = new GraphData<double>(avgSuccessfulLogin.Keys, avgSuccessfulLogin.Values);
            return successfulLogin;
        }
        /*
        [HttpGet]
        [ActionName("avgSession")]
        public GraphData<long> GetavgSessionTime()
        {
            GraphData<long> temp = new GraphData<long>();
            return _dashboardManager.GetAverageSessionDuration();
        }
        */

        [HttpGet]
        [Route("api/dashboard/fslogin")]
        public long[] GetFSLogin()
        {
            return (long[]) _dashboardManager.GetFailedSuccessfulLogIn();
        }

        [HttpGet]
        [ActionName("api/dashboard/feature")]
        public IDictionary<string, long> GetFeatureData()
        {
            return _dashboardManager.GetMostUsedFeature();
        }

    }
}
