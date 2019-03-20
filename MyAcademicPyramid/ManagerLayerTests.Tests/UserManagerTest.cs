using DataAccessLayer;
using DemoProject;
using ManagerLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManagerLayer.Tests
{
    public class UserManagerTest
    {
        [Fact]
        public void UserManager_CreateSystemAdminUsingOverloadConstructor_ShouldReturnTrue()
        {
            // Arrange
            UserManager temp = new UserManager();
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UserManager AdminUserManager = new UserManager("Admin", true);
                actual = true;
            }

            catch (ArgumentNullException)
            {
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_CreateUserAction_TargetUserNameNullShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            String targetUserName = null;
            UserManager userManager = new UserManager("Admin", true);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                userManager.CreateUserAction(targetUserName);
                actual = false;
            }

            catch(ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_CreateUserAction_RequiredClaimsNotMeet_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager userManager = new UserManager("Admin", true);
            String targetUserName = "SubAdmin";      
            userManager.CreateUserAction(targetUserName);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UserManager SubAdmin_UserManager = new UserManager("SubAdmin");
                SubAdmin_UserManager.CreateUserAction("Normal User");
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_CreateUserAction_ShouldCreateAccount()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            AdminUserManager.AddClaimAction("SubAdmin", new Claim("UserManager")); 
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UserManager SubAdmin_UserManager = new UserManager("SubAdmin");
                SubAdmin_UserManager.CreateUserAction("Normal User");
                actual = true;
            }

            catch (ArgumentException)
            {
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteAction_TargetUsernameNull_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = null;
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.DeleteUserAction(targetedUserName);
                actual = false;
            }

            catch(ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteAction_UsernameNotExists_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = "User";
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.DeleteUserAction(targetedUserName);
                actual = false;
            }

            catch (ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void UserManager_DeleteAction_RequiredClaimsNotMeet_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            UserManager SubAdminUserManager = new UserManager("SubAdmin");
            bool expected = true;
            bool actual;

            // Act
            try
            {
                SubAdminUserManager.DeleteUserAction("Admin");
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteAction_RequestingUserHasLessPrivilege_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            AdminUserManager.AddClaimAction("SubAdmin", new Claim ("UserManager"));
            UserManager SubAdminUserManager = new UserManager("SubAdmin");
            bool expected = true;
            bool actual;

            // Act
            try
            {
                SubAdminUserManager.DeleteUserAction("Admin");
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteAction_AllConditionsMeet_ShouldAbleToDeleteUser()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.DeleteUserAction("SubAdmin");
                actual = true;
            }

            catch (ArgumentException)
            {
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_PrintAllUser_ShouldReturnListOfUser()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin1");
            AdminUserManager.CreateUserAction("SubAdmin2");
            AdminUserManager.CreateUserAction("SubAdmin3");
            List<User> userList;

            // Act
                userList = AdminUserManager.GetAllUser();

            // Assert
            Assert.NotEmpty(userList);
        }

        [Fact]
        public void UserManager_AddClaimAction_TargetUsernameNull_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = null;
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.AddClaimAction(targetedUserName, new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_AddClaimAction_UsernameNotExists_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = "User";
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.AddClaimAction(targetedUserName, new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_AddClaimAction_RequiredClaimsNotMeet_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            UserManager SubAdminUserManager = new UserManager("SubAdmin");
            bool expected = true;
            bool actual;

            // Act
            try
            {
                SubAdminUserManager.AddClaimAction("Admin", new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_AddClaimAction_AllConditionsMeet_ShouldAbleToAddClaim ()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            Claim claim = new Claim("Over 18");
            bool expected = true;
            bool actual;

            AdminUserManager.AddClaimAction("SubAdmin", claim);

            // Act
            User SubAdmin = AdminUserManager.FindUserAction("SubAdmin");
            actual = SubAdmin.Claims.Contains(claim);

            // Assert
            Assert.Equal(expected, actual);
        }



        /////////////////////////////////////
        ///
        [Fact]
        public void UserManager_RemoveClaimAction_TargetUsernameNull_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = null;
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.RemoveClaimAction(targetedUserName, new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_RemoveClaimAction_UsernameNotExists_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            string targetedUserName = "User";
            bool expected = true;
            bool actual;

            // Act
            try
            {
                AdminUserManager.RemoveClaimAction(targetedUserName, new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_RemoveClaimAction_RequiredClaimsNotMeet_ShouldThrowException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            UserManager SubAdminUserManager = new UserManager("SubAdmin");
            bool expected = true;
            bool actual;

            // Act
            try
            {
                SubAdminUserManager.RemoveClaimAction("Admin", new Claim("Over 18"));
                actual = false;
            }

            catch (ArgumentException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_RemoveClaimAction_AllConditionsMeet_ShouldAbleToRemoveClaim ()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            EffortFactory.ResetDb();
            UserManager AdminUserManager = new UserManager("Admin", true);
            AdminUserManager.CreateUserAction("SubAdmin");
            Claim claim = new Claim("Over 18");
            bool expected = false;
            bool actual;
            AdminUserManager.AddClaimAction("SubAdmin", claim);

            // Act
            AdminUserManager.RemoveClaimAction("SubAdmin", claim);
            User SubAdmin = AdminUserManager.FindUserAction("SubAdmin");
            actual = SubAdmin.Claims.Contains(claim);

            // Assert
            Assert.Equal(expected, actual);
        }


    }
}
