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
        [Fact]
        public void GetHashValue_CheckByteValue()
        {
            SHA1HashFunction Sha1 = new SHA1HashFunction();
            //Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2";
            //Act
            string input = "PolarIceCaps";
            string actual = Sha1.GetHashValue(input);
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
