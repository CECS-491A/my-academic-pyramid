using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using ManagerLayer.Gateways.UsageAnalysisDashboard;
using System.Collections.Generic;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UADController: ApiController
    {
        private DashboardManager _dashboardManager = new DashboardManager();

        [HttpGet]
        [Route("api/dashboard/slogin")]
        public long[] GetScuessfulLogin()
        {

            return _dashboardManager.GetAverageSuccessfulLogin();
        }

        [HttpGet]
        [Route("api/dashboard/slogin")]
        public long[] GetavgSessionTime()
        {

            return _dashboardManager.GetAverageSessionDuration();
        }

        [HttpGet]
        [Route("api/dashboard/slogin")]
        public long[] GetFSLogin()
        {

            return _dashboardManager.GetFailedSuccessfulLogIn();
        }

        [HttpGet]
        [Route("api/dashboard/slogin")]
        public long[] GetFeatureData()
        {

            return _dashboardManager.GetAverageSuccessfulLogin();
        }

    }
}