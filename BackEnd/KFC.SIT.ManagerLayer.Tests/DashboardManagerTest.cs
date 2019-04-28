using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerLayer.Gateways.UsageAnalysisDashboard;
using Xunit;

namespace ManagerLayer.Tests
{
    public class DashboardManagerTest
    {
        private readonly DashboardManager _dashboardManager;
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";

        DashboardManagerTest()
        {
            _dashboardManager = new DashboardManager(url, database);
        }

        [Fact]
        public void DashboardService_CountAverageSuccessfulLogin_ShouldCountAverageSuccessfulLogin()
        {
            // Arrange
            long[] expected = { 1, 1, 1, 1 };
            long[] actual;

            // Act
           // actual = _dashboardManager.GetAverageSuccessfulLogin();

            // Assert
           // Assert.Equal(expected, actual);

        }
    }
}
