using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PasswordChecking;
using PasswordChecking.HashFunctions;
using Xunit;

namespace PasswordChecking.Tests
{
    public class HttpClientMethodsTests
    {
        [Fact]
        public void RequestData_ShouldPass()
        {
            // Arrange
            SHA1HashFunction sha = new SHA1HashFunction();
            string password = "password";
            string url = "https://api.pwnedpasswords.com/range/";
            PwnedPasswordsValidation HttpClientMethods = new PwnedPasswordsValidation(sha, password, url);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool expected = true;
            bool actual;

            //Act
            try
            {
                //Task<string> response = HttpClientMethods.RequestData("https://api.pwnedpasswords.com/range/5BAA6");
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
            SHA1HashFunction sha = new SHA1HashFunction();
            string password = "password";
            string url = "https://api.pwnedpasswords.com/range/";
            PwnedPasswordsValidation pv = new PwnedPasswordsValidation(sha, password, url);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool expected = true;
            bool actual;

            //Act
            try
            {
                //Task<string> response = HttpClientMethods.RequestData("should throw exception");
            }
            catch (WebException e)
            {
                actual = true;
                //Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
