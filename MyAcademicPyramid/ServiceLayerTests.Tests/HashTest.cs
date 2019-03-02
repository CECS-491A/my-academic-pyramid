using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using Xunit;

namespace ServiceLayerTests.Tests
{
    public class HashTest
    {
        SHA1HashFunction Sha1 = new SHA1HashFunction();
        [Fact]
        public void SHA1HashFunctions_GetHashValue_ValidStringShouldReturnHashValue()
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
        public void SHA1HashFunctions_GetHashValue_InvalidStringShouldReturnException()
        {
            // Arrange
            bool expected = true;
            bool actual;
            // Act
            try
            {
                Sha1.GetHashValue(null);
                actual = false;
            }
            catch(ArgumentNullException)
            {
                actual = true;
            }
            
            // Assert
            Assert.Equal(expected, actual);
        }


    }
}
