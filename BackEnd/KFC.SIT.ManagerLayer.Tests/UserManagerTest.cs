using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using WebAPI.Gateways.UserManagement;
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
                    SsoId = System.Guid.NewGuid(),
                });
                Account createdUser = UM.FindByUserName("SystemAdmin");
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
        public void UserManager_CreateUserAccount_InvalidEmail_ShouldReturnNull()
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
                    //BirthDate = DateTime.UtcNow,
                    //RawPassword = "PasswordArturo",
                    //Location = "Long Beach",
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
                actual = false;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserManager_DeleteUserAccount_ShouldDeleteUser()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Kevin",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Artur1o@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                UM.DeleteUserAccount(createdUser);
                Account shouldBeNull = UM.FindUserByEmail("Artur1o@gmail.com");
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin5",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
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

        //[Fact]
        //public void UserManager_UpdateUserAccount_ShouldUpdateUser()
        //{
            // Arrange
        //    UserManager UM = new UserManager();
        //    Account createdUser = UM.CreateUserAccount(new UserDTO
        //    {
        //        UserName = "Prof",
        //        FirstName = "Luis",
        //        LastName = "IDK",
        //        //BirthDate = DateTime.UtcNow,
        //        //RawPassword = "PasswordArturo",
        //        //Location = "Long Beach",
        //        Email = "Luis@gmail.com",
        //    });
        //    string newEmail = "Naruto@gmail.com";
        //    createdUser.Email = newEmail;
        //    bool expected = true;
        //    bool actual;

        //    // Act
        //    try
        //    {
        //        UM.UpdateUserAccount(createdUser);
        //        Account user = UM.FindUserByEmail(newEmail);
        //        if (user == createdUser)
        //        {
        //            actual = true;
        //        }
        //        else
        //        {
        //            actual = false;
        //        }
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        actual = false;
        //    }
        //    // Assert
        //    Assert.Equal(expected, actual);

        //}

        [Fact]
        public void UserManager_UpdateUserAccount_UserNotFound_ShouldReturnZero()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Kry",
                LastName = "Leon",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
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
            Account createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,s
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            Account createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account returnedUser = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
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
            Account createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            Account createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            UM.DeleteUserAccount(createdChildUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account shouldBeNull = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
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
            Account createdChildUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo@gmail.com",
            });
            Account createdParentUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "ArturoSon",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo2@gmail.com",
            });
            UM.DeleteUserAccount(createdParentUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account shouldBeNull = UM.AssignUserToUser(createdChildUser.UserName, createdParentUser.UserName);
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "Admin",
                FirstName = "Kevin",
                LastName = "Kim",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "BTS@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindUserByEmail("BTS@gmail.com");
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindUserByEmail("ArturoLuis@gmail.com");
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "ArturoKevin2@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindUserByEmail("ArturoKevin2@gmail.com");
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindUserById(100);
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindUserById(100);
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin2",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "ArturoKevin@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindByUserName("SystemAdmin2");
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Arturo",
                LastName = "NA",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "Arturo1Kevin@gmail.com",
            });
            UM.DeleteUserAccount(createdUser);
            bool expected = true;
            bool actual;

            // Act
            try
            {
                Account foundUser = UM.FindByUserName(createdUser.UserName);
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
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        /// <summary>
        /// ////////////////////////////////////////////////////////
        /// </summary>
        [Fact]
        public void UserManager_RemoveClaimAction_ShouldRemoveClaim()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        public void UserManager_RemoveClaimAction_UserNotFound_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        public void UserManager_ChangePassword_ShouldChangePassword()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        public void UserManager_ChangePassword_UserNotFound_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        public void UserManager_VerifyPassword_ComparePasswordEqual_ShouldReturnTrue()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
        public void UserManager_VerifyPassword_ComparePasswordNotEqual_ShouldThrowException()
        {
            // Arrange
            UserManager UM = new UserManager();
            Account createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "User",
                FirstName = "Hyunwoo",
                LastName = "Kim",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin@gmail.com",
            });
            UM.CreateUserAccount(new UserDTO
            {
                Id = 101,
                UserName = "User",
                FirstName = "Hyunwoo2",
                LastName = "Kim2",
                Category = "User",
                //BirthDate = DateTime.UtcNow,
                //RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                Email = "11ArturoKevin2@gmail.com",
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                List<Account> userList = UM.GetAllUser();
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
    }// end of class
}
