using DataAccessLayer;
using DataAccessLayer.DTOs;
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
        public void UserManager_CreateUserAccount_ShouldCreateUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.CreateUserAccount(new UserDTO
                {
                    UserName = "SystemAdmin",
                    FirstName = "Arturo",
                    LastName = "NA",
                    Catergory = "User",
                    BirthDate = DateTime.UtcNow,
                    RawPassword = "PasswordArturo",
                    Location = "Long Beach",
                    Email = "Arturo@gmail.com",
                    PasswordQuestion1 = "What is our favourite food ?",
                    PasswordQuestion2 = "Where is your school?",
                    PasswordQuestion3 = "What is your major",
                    PasswordAnswer1 = "Burger",
                    PasswordAnswer2 = "CSULB",
                    PasswordAnswer3 = "CS",
                });
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
        public void UserManager_CreateUserAccount_InvalidEmail_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            string invalidEmail = "hello";
            bool expected = true;
            bool actual;

            // Act
            try
            {
                var user = UM.CreateUserAccount(new UserDTO
                {
                    UserName = "SystemAdmin",
                    FirstName = "Arturo",
                    LastName = "NA",
                    Catergory = "User",
                    BirthDate = DateTime.UtcNow,
                    RawPassword = "PasswordArturo",
                    Location = "Long Beach",
                    Email = invalidEmail,
                    PasswordQuestion1 = "What is our favourite food ?",
                    PasswordQuestion2 = "Where is your school?",
                    PasswordQuestion3 = "What is your major",
                    PasswordAnswer1 = "Burger",
                    PasswordAnswer2 = "CSULB",
                    PasswordAnswer3 = "CS",
                });
                if (user == null)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }
            }

            catch(ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteUserAccount_ShouldDeleteUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
                PasswordQuestion1 = "What is our favourite food ?",
                PasswordQuestion2 = "Where is your school?",
                PasswordQuestion3 = "What is your major",
                PasswordAnswer1 = "Burger",
                PasswordAnswer2 = "CSULB",
                PasswordAnswer3 = "CS",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.DeleteUserAccount(createdUser);
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
        public void UserManager_DeleteUserAccount_UserNotFound_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
                PasswordQuestion1 = "What is our favourite food ?",
                PasswordQuestion2 = "Where is your school?",
                PasswordQuestion3 = "What is your major",
                PasswordAnswer1 = "Burger",
                PasswordAnswer2 = "CSULB",
                PasswordAnswer3 = "CS",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.DeleteUserAccount(createdUser);
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
        public void UserManager_UpdateUserAccount_ShouldUpdateUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
                PasswordQuestion1 = "What is our favourite food ?",
                PasswordQuestion2 = "Where is your school?",
                PasswordQuestion3 = "What is your major",
                PasswordAnswer1 = "Burger",
                PasswordAnswer2 = "CSULB",
                PasswordAnswer3 = "CS",
            });
            string newEmail = "Naruto@gmail.com";
            createdUser.Email = newEmail;
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.UpdateUserAccount(createdUser);
                User user = UM.FindUserByEmail(newEmail);
                if (user == createdUser)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }
            }
            catch (ArgumentNullException)
            {
                actual = false;
            }
            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void UserManager_UpdateUserAccount_UserNotFound_ShouldReturnZero()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
                PasswordQuestion1 = "What is our favourite food ?",
                PasswordQuestion2 = "Where is your school?",
                PasswordQuestion3 = "What is your major",
                PasswordAnswer1 = "Burger",
                PasswordAnswer2 = "CSULB",
                PasswordAnswer3 = "CS",
            });
            string newEmail = "Naruto@gmail.com";
            createdUser.Email = newEmail;
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.UpdateUserAccount(createdUser);
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
        public void UserManager_AssignUserAccount_ShouldAssignUsertoUser()
        {

        }

        [Fact]
	    public void UserManager_AssignUserAccount_ChildUserNotFound_ShouldThrowException()
        {

        }

        [Fact]
        public void UserManager_AssignUserAccount_ParentUserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_FindUserByEmail_ShouldReturnUser()
        {

        }
        [Fact]
        public void UserManager_FindUserByEmail_InvalidEmail_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_FindUserByEmail_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_FindUserById_ShouldReturnUser()
        {

        }
        [Fact]
        public void UserManager_FindUserById_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_FindByUserName_ShouldReturnUser()
        {

        }
        [Fact]
        public void UserManager_FindByUserName_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_GetAllUser_ShouldReturnListOfUsers()
        {

        }
        [Fact]
        public void UserManager_AddClaimAction_ShouldAddClaim()
        {

        }
        [Fact]
        public void UserManager_AddClaimAction_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_RemoveClaimAction_ShouldRemoveClaim()
        {

        }
        [Fact]
        public void UserManager_RemoveClaimAction_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_ChangePassword_ShouldChangePassword()
        {

        }
        [Fact]
        public void UserManager_ChangePassword_UserNotFound_ShouldThrowException()
        {

        }
        [Fact]
        public void UserManager_VerifyPassword_ComparePasswordEqual_ShouldReturnTrue()
        {

        }
        [Fact]
        public void UserManager_VerifyPassword_ComparePasswordNotEqual_ShouldThrowException()
        {

        }





        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /**
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
    */


    }
}
