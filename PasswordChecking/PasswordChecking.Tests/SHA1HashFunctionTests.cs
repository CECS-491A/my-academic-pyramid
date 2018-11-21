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
        //Use the Sha1 Hash Function
        SHA1HashFunction Sha1 = new SHA1HashFunction();

        //Method checks if input matches the expected Hash Value
        [Fact]
        public void GetHashValue_CheckIfValid()
        {

            //Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2"; //Expected Hash Value
            string input = "PolarIceCaps";                                //Example input
            //Act
            string actual = Sha1.GetHashValue(input);                     //Gets Hash Value from input using Sha1.GetHashValue
            //Assert
            Assert.Equal(expected, actual);                               //Checks if actual and expected are the same
        }
        //Method checks if input does not match the expected Hash Value
        [Fact]
        public void GetHashValue_CheckIfInvalid()
        {
            //Arrange
            string expected = "25E6A14076898A344AB680E2A589A09885EB04C2"; //Expected Hash Value
            string input = "Polar1ceCap";                                 //Example input
            //Act

            string actual = Sha1.GetHashValue(input);                     //Gets Hash Value from input using Sha1.GetHashValue
            //Assert
            Assert.NotEqual(expected, actual);                            //Checks if actual and expected are not the same
        }


    }
}
