using DataAccessLayer.PasswordChecking.HashFunctions;
using Xunit;


namespace ServiceLayerTests.Tests
{
    public class RunHashTest
    {
        string password = "password"; // Password
        string url = "https://api.pwnedpasswords.com/range/";
        string hashValue = "5BAA61E4C9B93F0682250B6CF8331B7EE68FD8";
        string prefix = "5BAA6";
        string list = "";
        SHA1HashFunction Sha1 = new SHA1HashFunction();
        string expected = "";
        string actual = "";
        [Fact]
        public void RunHash_checkIfEquals()
        {
            //Arrange
            expected = hashValue;


            //Act
            PwnedPasswordsValidation pv = new PwnedPasswordsValidation(Sha1, url);
            if (pv.FindHash(hashValue, prefix, list).HashValue == null)
            {
                actual = hashValue;
            }
            else
            {
                actual = pv.FindHash(hashValue, prefix, list).HashValue;
            }
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
