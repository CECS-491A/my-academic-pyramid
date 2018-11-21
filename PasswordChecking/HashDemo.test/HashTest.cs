using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordChecking.HashFunctions;
using Xunit;

namespace HashDemo.test
{
    public class HashTest
    {
        SHA1HashFunction Sha1 = new SHA1HashFunction();
        [Fact]
        public void GetHashValue_CheckIfValid()
        {
            
            //Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2";
            //Act
            string input = "PolarIceCaps";
            string actual = Sha1.GetHashValue(input);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetHashValue_CheckIfInvalid()
        {
            //Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2";
            string input = "Polar";
            //Act

            string actual = Sha1.GetHashValue(input);
            //Assert
            Assert.NotEqual(expected, actual);
        }


    }
}
