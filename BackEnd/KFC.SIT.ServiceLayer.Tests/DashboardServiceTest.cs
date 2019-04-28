using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DataAnalysisDashboard;
using Xunit;

namespace ServiceLayer.Tests
{
    public class DashboardServiceTest
    {
        private readonly DashboardService _dashboardService;
        private const string url = "mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true";
        private const string database = "test";

        DashboardServiceTest()
        {
            _dashboardService = new DashboardService(url, database);
        }

        [Fact]
        public void DashboardService_CountAverageSuccessfulLogin_ShouldCountAverageSuccessfulLogin()
        {
            // Arrange
            long[] expected = { 1, 1, 1, 1 };
            long[] actual;

            // Act
            actual = _dashboardService.CountAverageSuccessfulLogin();

            // Assert
            Assert.Equal(expected, actual);

        }


    }
}
