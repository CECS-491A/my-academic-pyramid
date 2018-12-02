using System;
using System.Net;
using System.Threading.Tasks;
using ManagerLayer.Logic.PasswordChecking.PasswordValidations;
using ServiceLayer;
using ServiceLayer.PasswordChecking.HashFunctions;
using Xunit;

namespace ManagerLayerTests.Tests
{
    public class PasswordCheckerTests
    {
        // Arrange
        static SHA1HashFunction sha = new SHA1HashFunction();
        static string url = "https://api.pwnedpasswords.com/range/";
        static PwnedPasswordsValidation pv = new PwnedPasswordsValidation(sha, url);

        [Fact]
        public void FindHash_FoundShouldReturnCount()
        {
            // Arrange
            string hashValue = "1E4C9B93F3F0682250B6CF8331B7EE68FD8"; // password: "password"
            string response = "1D72CD07550416C216D8AD296BF5C0AE8E0:10 \n" +
                "1E2AAA439972480CEC7F16C795BBB429372:1 \n" +
                "1E3687A61BFCE35F69B7408158101C8E414:1 \n" +
                "1E4C9B93F3F0682250B6CF8331B7EE68FD8:3533661 \n";
            int expected = 3533661;
            int actual;

            //Act
            actual = pv.FindHash(hashValue, response);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindHash_NotFoundShouldReturnZero()
        {
            // Arrange
            string hashValue = "753D006EBCE8F59C93364725A9D5C4EC6BC"; // password = "fw836g1"
            string response = "1D72CD07550416C216D8AD296BF5C0AE8E0:10 \n" +
                "1E2AAA439972480CEC7F16C795BBB429372:1 \n" +
                "1E3687A61BFCE35F69B7408158101C8E414:1 \n" +
                "1E4C9B93F3F0682250B6CF8331B7EE68FD8:3533661 \n";
            int expected = 0;
            int actual;

            //Act
            actual = pv.FindHash(hashValue, response);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindHash_InvalidHashValueShouldThrowException()
        {
            // Arrange
            string response = "1D72CD07550416C216D8AD296BF5C0AE8E0: 10 \n" +
                "1E2AAA439972480CEC7F16C795BBB429372:1 \n" +
                "1E3687A61BFCE35F69B7408158101C8E414:1 \n" +
                "1E4C9B93F3F0682250B6CF8331B7EE68FD8:3533661 \n";
            Boolean expected = true;
            Boolean actual;

            //Act
            try
            {
                int test = pv.FindHash(null, response);
                actual = false;
            }
            catch (ArgumentNullException e)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindHash_InvalidResponseShouldThrowExecption()
        {
            // Arrange
            string hashValue = "1E4C9B93F3F0682250B6CF8331B7EE68FD8"; // password = "password"
            bool expected = true;
            bool actual;

            //Act
            try
            {
                int test = pv.FindHash(hashValue, null);
                actual = false;
            }
            catch (NullReferenceException e)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindHash_AllNullValuesShouldThrowException()
        {
            // Arrange
            bool expected = true;
            bool actual;

            //Act
            try
            {
                int test = pv.FindHash(null, null);
                actual = false;
            }
            catch (NullReferenceException e)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void RequestData_ShouldPass()
        {
            // Arrange
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool expected = true;
            bool actual;

            //Act
            try
            {
                Uri uri = new Uri("https://api.pwnedpasswords.com/range/5BAA6");
                Task<string> response = HttpClientMethods.RequestData(uri);
                actual = true;
            }
            catch (WebException e)
            {
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestData_InvalidUrlShouldFail()
        {
            // Arrange
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool expected = true;
            bool actual;

            //Act
            try
            {
                Uri uri = new Uri("should throw exception");
                Task<string> response = HttpClientMethods.RequestData(uri);
            }
            catch (UriFormatException e)
            {
                actual = true;
                //Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}

//go to code and put a try catch around the code
//catch will set the value to true
//so if the error happens then that means the code went into the catch 
//set actual = to true in catch 
//this makes sure that the error happens
//can put assertion in catch to speed things up or some bs
//assert.equals(expected, actual)

//test valid values, invalid values, performance - how long did something take, 
//test for duration - create a stop watch. stopwatch.Start() at start and stopwatch.End()
//at the end. Then Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000); 
//makes sure it took less than 1 second

//can have multiple assertions in a test 
//advice to create a unit test that only tests 1 scenario
//valid inputs, invalid inputs, performance testing at the end 