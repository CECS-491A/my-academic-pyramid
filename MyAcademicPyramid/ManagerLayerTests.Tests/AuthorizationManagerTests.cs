using ManagerLayer.Logic.Authorization.AuthorizationManagers;
using ManagerLayer;
using System.Collections.Generic;
using DataAccessLayer;
using Xunit;

namespace ManagerLayerTests.Tests
{
    public class AuthorizationManagerTests
    {
        [Fact]
        public void AuthorizationManager_CheckClaims_ClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add("CanDeleteUserPost");
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = true;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<string>() { "CanDeleteUserPost" });

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorizationManager_CheckClaims_ClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add("CanDeleteUserOwnAccount");
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = false;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<string>() { "CanDeleteUserPost" });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedRightClaimShouldReturnTrue()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add("CanDeleteUserPost");
            Trong.Claims.Add("CanDeleteUserPost");
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = true;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<string>() { "CanDeleteUserPost" });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedWrongClaim2ShouldReturnFalse()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add("CanDeleteUserOwnAccount");
            Trong.Claims.Add("CanDeleteUserOwnAccount");
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = false;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<string>() { "CanDeleteUserPost" });

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_MultiplClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            Krystal.Claims.Add("HasPoints");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = true;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_HasPointsClaimNotFoundShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_CanDeleteOtherAccountClaimNotFoundHashShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("HasPoints");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_MultipleClaimNotFoundTwoClaimShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AuthorizationManager_CheckClaims_DuplicatedMultiplClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            Krystal.Claims.Add("HasPoints");
            Krystal.Claims.Add("HasPoints");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = true;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

    }
}
