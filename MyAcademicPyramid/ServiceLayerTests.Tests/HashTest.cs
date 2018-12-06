using DataAccessLayer.PasswordChecking.HashFunctions;
using Xunit;

namespace ServiceLayerTests.Tests
{
    public class HashTest
    {
        SHA1HashFunction Sha1 = new SHA1HashFunction();
        [Fact]
        public void SHA1HashFunctions_GetHashValue_CheckIfValid()
        {
            
            // Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2";
            // Act
            string input = "PolarIceCaps";
            string actual = Sha1.GetHashValue(input);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SHA1HashFunctions_GetHashValue_CheckIfInvalid()
        {
            // Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2";
            string input = "Polar";
            // Act

            string actual = Sha1.GetHashValue(input);
            // Assert
            Assert.NotEqual(expected, actual);
        }


    }
}
