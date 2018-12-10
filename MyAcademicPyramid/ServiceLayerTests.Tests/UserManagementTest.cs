using Xunit;
using DataAccessLayer;
using ServiceLayer.UserManagement.UserAccountServices;
using DemoProject;
using System;
using System.Collections.Generic;

namespace ServiceLayerTests.Tests
{
    public class UserManagementTest
    {


        [Fact]
        public void UserManagement_CreateUser_ShouldAbleFindUserAfterCreation()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Victor");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            User expectedUser = userManagementServ.FindUserbyUserName("Victor");

            //Act
            User actualUser = new User("Victor");

            //Assert
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
        }

        [Fact]
        public void UserManagement_CreateUserWithDupplicateUsername_ShouldReturnOnlyOneUserObjectWhenFinding()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user1 = new User("Victor");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user1);
            User user2 = new User("Victor");

            bool expected = true;
            bool actual;
            //Act
            try
            {
                userManagementServ.CreateUser(user2);
                actual = false;
            }
            catch(ArgumentException)
            {
                actual = true;
            }
     

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManagement_DeleteUser_UserShouldNotExistAfterDeletion()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            userManagementServ.DeleteUser(user);
            User expectedUser = userManagementServ.FindUserbyUserName("Trong");

            //Act
            User actualUser = null;

            //Assert
            Assert.Equal(expectedUser, actualUser);

        }

        [Fact]
        public void UserManagement_UpdateUser_UserNameShouldGetUpdate()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            user.UserName = "Arturo";
            userManagementServ.UpdateUser(user);

            String ExpectedUserName = userManagementServ.FindUserbyUserName("Arturo").UserName;

            //Act
            String actualUserName = "Arturo";

            //Assert
            Assert.Equal(ExpectedUserName, actualUserName);

        }

        [Fact]
        public void UserManagement_AddClaim_ClaimsShouldBeAddToUser()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Luis");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            Claim claim = new Claim("Over 18");
            userManagementServ.AddClaim(user, claim);
            bool expected = userManagementServ.FindUserbyUserName("Luis").Claims.Contains(claim);

            //Act
            bool actual = true;

            //Assert
            Assert.Equal(expected, actual);

        }




        [Fact]
        public void UserManagement_RemoveClaim_ClaimsShouldBeRemovedFromUser()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Luis");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            Claim claim = new Claim("Over 18");

            userManagementServ.AddClaim(user,claim);
            userManagementServ.RemoveClaim(user, claim);
            bool expected = userManagementServ.FindUserbyUserName("Luis").Claims.Contains(claim);

            //Act
            bool actual = false;

            //Assert
            Assert.Equal(expected, actual);

        }



    }
    
}
