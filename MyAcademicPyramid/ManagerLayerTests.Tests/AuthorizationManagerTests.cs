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
            Trong.Claims.Add(new Claim("CanDeleteUserPost"));
            Trong.Claims.Add(new Claim("CanUpdatePost"));
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
        public void AuthorizationManager_CheckClaims_MultiplClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanDeleteOtherAccount"));
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
        public void AuthorizationManager_CheckClaims_DuplicatedMultiplClaimFoundShouldReturnTrue()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add(new Claim("CanDeleteOtherAccount"));
            Krystal.Claims.Add(new Claim("CanDeleteOtherAccount"));
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

    }
}
