using System.Collections.Generic;
using DataAccessLayer;
using Xunit;
using System;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;
using DemoProject;

namespace ManagerLayerTests.Tests
{
    public class AuthorizationManagerTests
    {
        [Fact]
        public void AuthorizationManager_CheckClaims_ClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add(new Claim("CanDeleteUserPost"));
            //Trong.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = true;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost" )});

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorizationManager_CheckClaims_ClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add(new Claim("CanDeleteUserOwnAccount"));
            Trong.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = false;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<Claim>() { new Claim( "CanDeleteUserPost" )});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedRightClaimShouldReturnTrue()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add(new Claim("CanDeleteUserPost"));
            Trong.Claims.Add(new Claim("CanDeleteUserPost"));
            Trong.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = true;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedWrongClaimShouldReturnFalse()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add(new Claim("CanDeleteUserOwnAccount"));
            Trong.Claims.Add(new Claim("CanDeleteUserOwnAccount"));
            Trong.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = false;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_MultipleClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanDeleteUserPost"));
            Krystal.Claims.Add(new Claim("HasPoints"));
            Krystal.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = true;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost"), new Claim("HasPoints") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_HasPointsClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanDeleteOtherAccount"));
            Krystal.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost"), new Claim("HasPoints") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_CanDeleteOtherAccountClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("HasPoints"));
            Krystal.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost"), new Claim("HasPoints") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_MultipleClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost"), new Claim("HasPoints") });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedMultipleClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanDeleteUserPost"));
            Krystal.Claims.Add(new Claim("CanDeleteUserPost"));
            Krystal.Claims.Add(new Claim("HasPoints"));
            Krystal.Claims.Add(new Claim("HasPoints"));
            Krystal.Claims.Add(new Claim("CanUpdatePost"));
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = true;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<Claim>() { new Claim("CanDeleteUserPost"), new Claim("HasPoints") });

            // Assert
            Assert.Equal(expected, actual);

        }

        //Data for AuthorizationManager_FindHeight_ShouldReturnCorrectLevel()
        public static IEnumerable<object[]> GetFindHeightData()
        {
            // Arrange 
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            UnitOfWork uOW = new UnitOfWork();
            UserManagementServices userManager = new UserManagementServices(uOW);

            User Krystal = new User("Krystal0"); // Level 0
            userManager.CreateUser(Krystal);

            User Arturo = new User("Arturo0")
            {
                ParentUser_Id = Krystal.Id // Level 1
            };
            userManager.CreateUser(Arturo);

            User Kevin = new User("Kevin0")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Kevin);

            User Victor = new User("Victor0")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Victor);

            User Luis = new User("Luis0")
            {
                ParentUser_Id = Kevin.Id // Level 3
            };
            userManager.CreateUser(Luis);

            User Trong = new User("Trong0")
            {
                ParentUser_Id = Victor.Id // Level 3
            };
            userManager.CreateUser(Trong);

            yield return new object[] { 0, Krystal }; 
            yield return new object[] { 1, Arturo };
            yield return new object[] { 2, Kevin };
            yield return new object[] { 2, Victor };
            yield return new object[] { 3, Luis };
            yield return new object[] { 3, Trong };
        }

        [Theory]
        // Arrange 
        [MemberData(nameof(GetFindHeightData))]
        public void AuthorizationManager_FindHeight_ShouldReturnCorrectLevel(int e, User a)
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            User Trong2 = new User("Trong2");

            AuthorizationManager Authorization = new AuthorizationManager(Trong2);

            int expected = e;
            int actual;

            // Act 
            actual = Authorization.FindHeight(a);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorizationManager_FindHeight_NullUserShouldThrowNullReferenceException()
        {
            // Arrange
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
           
            UnitOfWork uOW = new UnitOfWork();
            UserManagementServices userManager = new UserManagementServices(uOW);
            User Krystal = new User("Krystal");
            AuthorizationManager Authorization = new AuthorizationManager(Krystal);
            bool expected = true;
            bool actual;

            // Act 
            try
            {
                int test = Authorization.FindHeight(null);
                actual = false;
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        // Data for AuthorizationManager_HasHigherPrivilege_ShouldReturnTrue()
        public static IEnumerable<object[]> GetPrivelegeLevelDataForTrue()
        {
            // Arrange 
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            UnitOfWork uOW = new UnitOfWork();
            UserManagementServices userManager = new UserManagementServices(uOW);

            User Krystal = new User("Krystal2"); // Level 0
            userManager.CreateUser(Krystal);

            User Arturo = new User("Arturo2")
            {
                ParentUser_Id = Krystal.Id // Level 1
            };
            userManager.CreateUser(Arturo);

            User Kevin = new User("Kevin2")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Kevin);

            User Victor = new User("Victor2")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Victor);

            User Luis = new User("Luis2")
            {
                ParentUser_Id = Kevin.Id // Level 3
            };
            userManager.CreateUser(Luis);

            User Trong = new User("Trong2")
            {
                ParentUser_Id = Victor.Id // Level 3
            };
            userManager.CreateUser(Trong);

            yield return new object[] { Krystal, Arturo }; 
            yield return new object[] { Krystal, Kevin };
            yield return new object[] { Krystal, Luis };
            yield return new object[] { Arturo, Victor };
            yield return new object[] { Arturo, Trong };
            yield return new object[] { Kevin, Trong };
            yield return new object[] { Victor, Luis };
        }

        [Theory]
        // Arrange 
        [MemberData(nameof(GetPrivelegeLevelDataForTrue))]
        public void AuthorizationManager_HasHigherPrivilege_ShouldReturnTrue(User parent, User child)
        {
            // Arrange 
            User Krystal = new User("Krystal");
            AuthorizationManager Authorization = new AuthorizationManager(Krystal);
            bool expected = true;
            bool actual;

            // Act 
            actual = Authorization.HasHigherPrivilege(parent, child);

            // Assert
            Assert.Equal(expected, actual);
        }

        // Data for AuthorizationManager_HasHigherPrivilege_ShouldReturnFalse()
        public static IEnumerable<object[]> GetPrivelegeLevelDataForFalse()
        {
            // Arrange 
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            UnitOfWork uOW = new UnitOfWork();
            UserManagementServices userManager = new UserManagementServices(uOW);

            User Krystal = new User("Krystal"); // Level 0
            userManager.CreateUser(Krystal);

            User Arturo = new User("Arturo")
            {
                ParentUser_Id = Krystal.Id // Level 1
            };
            userManager.CreateUser(Arturo);

            User Kevin = new User("Kevin")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Kevin);

            User Victor = new User("Victor")
            {
                ParentUser_Id = Arturo.Id // Level 2
            };
            userManager.CreateUser(Victor);

            User Luis = new User("Luis")
            {
                ParentUser_Id = Kevin.Id // Level 3
            };
            userManager.CreateUser(Luis);

            User Trong = new User("Trong")
            {
                ParentUser_Id = Victor.Id // Level 3
            };
            userManager.CreateUser(Trong);

            yield return new object[] { Arturo, Krystal };
            yield return new object[] { Kevin, Krystal };
            yield return new object[] { Luis, Krystal };
            yield return new object[] { Victor, Arturo };
            yield return new object[] { Trong, Arturo };
            yield return new object[] { Trong, Kevin };
            yield return new object[] { Luis, Victor }; 
            yield return new object[] { Luis, Trong }; // same level
        }

        [Theory]
        // Arrange 
        [MemberData(nameof(GetPrivelegeLevelDataForFalse))]
        public void AuthorizationManager_HasHigherPrivilege_ShouldReturnFalse(User parent, User child)
        {
            // Arrange 
            User Krystal = new User("Krystal");
            AuthorizationManager Authorization = new AuthorizationManager(Krystal);
            bool expected = false;
            bool actual;

            // Act 
            actual = Authorization.HasHigherPrivilege(parent, child);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorizationManager_HasHigherPrivilege_NullUserShouldThrowNullReferenceException()
        {
            // Arrange
            User Krystal = new User("Krystal");
            AuthorizationManager Authorization = new AuthorizationManager(Krystal);
            bool expected = true;
            bool actual;

            // Act 
            try
            {
                bool test = Authorization.HasHigherPrivilege(null, Krystal);
                actual = false;
            }
            catch (ArgumentException  )
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorizationManager_HasHigherPrivilege_BothUsersNullShouldThrowNullReferenceException()
        {
            // Arrange
            User Snoop = new User("Snoop Dogg");
            AuthorizationManager Authorization = new AuthorizationManager(Snoop);
            bool expected = true;
            bool actual;

            // Act 
            try
            {
                bool test = Authorization.HasHigherPrivilege(null, null);
                actual = false;
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
