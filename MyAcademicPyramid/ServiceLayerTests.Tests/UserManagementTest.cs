using Xunit;
using DataAccessLayer;
using DataAccessLayer.UserManagement.UserAccountServices;
using DemoProject;
using System;

namespace ManagerLayer.Tests
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
            UserManagement userManagement = new UserManagement();
            userManagement.CreateUser(user);
            User expectedUser = userManagement.FindUserbyUserName("Victor");

            //Act
            User actualUser = new User("Victor");

            //Assert
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
        }

        [Fact]
        public void UserManagement_DeleteUser_UserShouldNotExistAfterDeletion()
        {
            //Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            User user = new User("Trong");
            UserManagement userManagement = new UserManagement();
            userManagement.CreateUser(user);
            userManagement.DeleteUser(user);
            User expectedUser = userManagement.FindUserbyUserName("Trong");

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
            UserManagement userManagement = new UserManagement();
            userManagement.CreateUser(user);
            user.UserName = "Arturo";
            userManagement.UpdateUser(user);

            String ExpectedUserName = userManagement.FindUserbyUserName("Arturo").UserName;

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
            UserManagement userManagement = new UserManagement();
            userManagement.CreateUser(user);
            userManagement.AddClaim(user, new Claim("Over 18"));
            bool expected = userManagement.FindUserbyUserName("Luis").Claims.Exists(u => u.Value.Equals("Over 18"));

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
            UserManagement userManagement = new UserManagement();
            userManagement.CreateUser(user);
            userManagement.AddClaim(user, new Claim("Over 18"));
            userManagement.RemoveClaim(user, new Claim("Over 18"));
            bool expected = userManagement.FindUserbyUserName("Luis").Claims.Exists(u => u.Value.Equals("Over 18"));

            //Act
            bool actual = true;

            //Assert
            Assert.Equal(expected, actual);

        }



    }
    
}
