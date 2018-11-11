using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordChecking;
using Xunit;

namespace UnitTesting.Tests
{
    // In order to test:
    // Go to Test -> Windows -> Test Explorer 
    // and you will see Run All | Run | Playlist: All Tests
    public class DemoTest
    {
        // sample method.
        // method name must be descriptive
        [Fact] // test that runs in the test runner
        public void ShouldAddTwoNumberTogether()
        {
            // Arrange
            int expected = 10;

            // Act
            int actual = DemoProgram.Add(4, 6);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory] // allows us to pass in data and run test multiple times with different data sets
        [InlineData(4, 3, 7)] // data 1.  a = 4, b = 3, expected = 7
        [InlineData(4, 8, 12)] // data 2 
        [InlineData(4, 5, 9)] // data 3
        [InlineData(int.MaxValue, int.MinValue, -1)]
        public void Add_SimpleValueShouldCalculate(int a, int b, int expected)
        {
            // Arrange

            // Act
            int actual = DemoProgram.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}
