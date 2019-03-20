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
                    Email = "Arturo1@gmail.com",
                });
                User createdUser = UM.FindUserByEmail("Arturo1@gmail.com");
                if (createdUser != null)
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
                UserName = "User",
                FirstName = "Kevin",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Artur1o@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.DeleteUserAccount(createdUser);
                User shouldBeNull = UM.FindUserByEmail("Artur1o@gmail.com");
                if (shouldBeNull == null)
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
        public void UserManager_DeleteUserAccount_UserNotFound_ShouldReturnZero()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin5",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            int expected = 0;
            int actual;

            // Act
            try
            {
                actual = UM.DeleteUserAccount(createdUser);
            }

            catch (ArgumentNullException)
            {
                actual = 1;
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
                UserName = "Prof",
                FirstName = "Luis",
                LastName = "IDK",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Luis@gmail.com",
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
                UserName = "User",
                FirstName = "Kry",
                LastName = "Leon",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "123Arturo@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            int expected = 0;
            int actual;

            // Act
            try
            {
                actual = UM.UpdateUserAccount(createdUser);
            }
            catch (ArgumentNullException)
            {
                actual = 1;
            }
            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void UserManager_AssignUserAccount_ShouldAssignUsertoUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            User createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User returnedUser = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
                if (returnedUser != null)
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
	    public void UserManager_AssignUserAccount_ChildUserNotFound_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            User createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            UM.DeleteUserAccount(createdChildUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User shouldBeNull = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
                if (shouldBeNull == null)
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
        public void UserManager_AssignUserAccount_ParentUserNotFound_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            User createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            UM.DeleteUserAccount(createdParentUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User shouldBeNull = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
                if (shouldBeNull == null)
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
        public void UserManager_FindUserByEmail_ShouldReturnUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "Admin",
                FirstName = "Kevin",
                LastName = "Kim",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "BTS@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserByEmail("BTS@gmail.com");
                if (createdUser == foundUser)
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
        public void UserManager_FindUserByEmail_InvalidEmail_ShouldReturnNull()
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
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserByEmail("ArturoLuis@gmail.com");
                if (createdUser == foundUser)
                {
                    actual = false;
                }
                else
                {
                    actual = true;
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
        public void UserManager_FindUserByEmail_UserNotFound_ShouldReturnNull()
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
                Email = "ArturoKevin2@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserByEmail("ArturoKevin2@gmail.com");
                if (foundUser == null)
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
        public void UserManager_FindUserById_ShouldReturnUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserById(100);
                if (foundUser == createdUser)
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
        public void UserManager_FindUserById_UserNotFound_ShouldReturnNull()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserById(100);
                if (foundUser == null)
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
        public void UserManager_FindByUserName_ShouldReturnUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin2",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindByUserName("SystemAdmin2");
                if (foundUser == createdUser)
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
        public void UserManager_FindByUserName_UserNotFound_ShouldReturnNull()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "Arturo1Kevin@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindByUserName(createdUser.UserName);
                if (foundUser == null)
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
        public void UserManager_GetAllUser_ShouldReturnListOfUsers()
        {
            // Arrange
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Catergory = "User",
                BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<User> userList = UM.GetAllUser();
                if (userList.Contains(createdUser) && userList.Count == 2)
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
    }// end of class
}
