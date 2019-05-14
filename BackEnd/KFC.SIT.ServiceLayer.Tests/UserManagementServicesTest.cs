using Xunit;
using DataAccessLayer;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;

namespace ServiceLayerTests.Tests
{
    public class UserManagementServicesTest
    {
        /*
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

        [Fact]
        public void UserManagementServices_CreateUser_ShouldAbleFindUserAfterCreation()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Victor");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            User actualUser = new User("Victor");

            //Act
            userManagementServ.CreateUser(user);
            User expectedUser = userManagementServ.FindUserbyUserName("Victor");

            //Assert
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
        }

        [Fact]
        public void UserManagementServices_CreateUser_NullUserShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            bool raiseException = false;
            bool expected = true;

            //Act
            try
            {
                userManagementServ.CreateUser(null);
            }
            catch (ArgumentNullException)
            {
                raiseException = true;
            }

            //Assert
            Assert.Equal(expected, raiseException);
        }

        [Fact]
        public void UserManagementServices_CreateUserWithDupplicateUsername_ShouldReturnOnlyOneUserObjectWhenFinding()
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
            catch (ArgumentException)
            {
                actual = true;
            }


            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManagementServices_FindUserbyUserName_ExistingUserShouldBeFound()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User victorUser = new User("Victor");
            User kevinUser = new User("Kevin");
            User johnUser = new User("John");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(victorUser);
            userManagementServ.CreateUser(kevinUser);
            userManagementServ.CreateUser(johnUser);
            User actualUser = johnUser;

            //Act
            User expectedUser = userManagementServ.FindUserbyUserName("John");

            //Assert
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
        }

        [Fact]
        public void UserManagementServices_FindUserbyUserName_NonExistingUserShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User victorUser = new User("Victor");
            User kevinUser = new User("Kevin");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(victorUser);
            userManagementServ.CreateUser(kevinUser);
            User nonExistingUser = new User("John");
            User actualUser = null;

            //Act
            User expectedUser = userManagementServ.FindUserbyUserName(nonExistingUser.UserName);

            //Assert
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void UserManagementServices_DeleteUser_UserShouldNotExistAfterDeletion()
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
        public void UserManagementServices_DeleteUser_NullUserShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            User actualUser = null;

            //Act
            userManagementServ.DeleteUser(user);
            User expectedUser = userManagementServ.FindUserbyUserName("Trong");

            //Assert
            Assert.Equal(expectedUser, actualUser);

        }

        [Fact]
        public void UserManagementServices_DeleteUser_UserNotFoundShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            bool actual = true;
            bool exceptionRaised = false;

            //Act
            try
            {
                userManagementServ.DeleteUser(new User("John"));
            }
            catch (ArgumentException)
            {
                exceptionRaised = true;
            }

            //Assert
            Assert.Equal(actual, exceptionRaised);

        }

        [Fact]
        public void UserManagementServices_UpdateUser_UserNameShouldGetUpdate()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            user.UserName = "Arturo";
            String expectedUserName = "Arturo";

            //Act
            userManagementServ.UpdateUser(user);
            String actualUserName = userManagementServ.FindUserbyUserName("Arturo").UserName;

            //Assert
            Assert.Equal(expectedUserName, actualUserName);

        }

        [Fact]
        public void UserManagementServices_UpdateUser_NullUserShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            user.UserName = "Arturo";
            bool raiseException = false;
            bool actual = true;

            //Act
            try
            {
                userManagementServ.UpdateUser(null);
            }
            catch (ArgumentNullException)
            {
                raiseException = true;
            }

            //Assert
            Assert.Equal(raiseException, actual);

        }

        [Fact]
        public void UserManagementServices_UpdateUser_UserNotFoundShouldRaiseException()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            user.UserName = "Arturo";
            bool exceptionRaised = false;
            bool actual = true;

            //Act
            try
            {
                userManagementServ.UpdateUser(new User("Kevin"));
            }
            catch (ArgumentException)
            {
                exceptionRaised = true;
            }

            //Assert
            Assert.Equal(exceptionRaised, actual);

        }

        [Fact]
        public void UserManagementServices_AddClaim_ClaimsShouldBeAddToUser()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Luis");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            Claim claim = new Claim("Over 18");
            bool actual = true;

            //Act
            userManagementServ.AddClaim(user, claim);
            bool expected = userManagementServ.FindUserbyUserName("Luis").Claims.Contains(claim);

            //Assert
            Assert.Equal(expected, actual);

        }




        [Fact]
        public void UserManagementServices_RemoveClaim_ClaimsShouldBeRemovedFromUser()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Luis");
            UserManagementServices userManagementServ = new UserManagementServices(new UnitOfWork());
            userManagementServ.CreateUser(user);
            Claim claim = new Claim("Over 18");

            userManagementServ.AddClaim(user, claim);
            userManagementServ.RemoveClaim(user, claim);
            bool expected = userManagementServ.FindUserbyUserName("Luis").Claims.Contains(claim);

            //Act
            bool actual = false;

            //Assert
            Assert.Equal(expected, actual);

        }

    */

    }

}
