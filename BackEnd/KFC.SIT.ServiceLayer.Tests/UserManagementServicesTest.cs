using Xunit;
using DataAccessLayer;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;

namespace ServiceLayerTests.Tests
{
    public class UserManagementServicesTest
    {
        [Fact]
        public void UserManagementServices_Constructor_ShouldReturnArgumentNullException()
        {
            DatabaseContext temp = new DatabaseContext();
            UserManagementServices temp2 = new UserManagementServices(temp);
            //Arrange
            bool exceptionRaised = false;
            bool expected = true;

            try
            {
                UserManagementServices userManagementServ = new UserManagementServices(null);
            }
            catch (ArgumentNullException)
            {
                exceptionRaised = true;
            }

            Assert.Equal(expected, exceptionRaised);
        }

        

    }

}
