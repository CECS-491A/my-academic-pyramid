using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ServiceLayer.HttpClients;

namespace ServiceLayerTests.Tests
{
    public class HttpClientStringTests
    {
        private IHttpClient HttpClientMethods = new HttpClientString(); // Http Client

        [Fact]
        public void HttpClientString_RequestData_ShouldPass()
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
            catch (WebException)
            {
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HttpClientString_RequestData_InvalidUrlShouldFail()
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
            catch (UriFormatException)
            {
                actual = true;
                //Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
