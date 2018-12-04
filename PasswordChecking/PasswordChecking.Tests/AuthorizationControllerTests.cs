﻿using ManagerLayer.Logic.Authorization.AuthorizationManagers;
using System.Collections.Generic;
using DataAccessLayer;
using Xunit;

namespace ManagerLayerTests.Tests
{
    public class AuthorizationControllerTests
    {
        [Fact]
        public void CheckClaims_FoundShouldReturnTrue()
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
        public void CheckClaims_NotFoundShouldReturnFalse()
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
        public void CheckClaims_ClaimValueFalseShouldReturnFalse()
        {
            // Arrange 
            User Trong = new User("Trong");
            Trong.Claims.Add("CanDeleteUserPost");
            AuthorizationManager TrongAuthorization = new AuthorizationManager(Trong);
            bool expected = false;

            // Act
            bool actual = TrongAuthorization.CheckClaims(new List<string>() { "CanDeleteUserPost" });

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckClaims_NotFoundClaimValueFalseShouldReturnFalse()
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
        public void CheckClaims_DuplicatedClaimShouldReturnFalse()
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
        public void CheckClaims_DuplicatedClaim2ShouldReturnFalse()
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
        public void CheckClaims_MultipleClaimFoundShouldReturnTrue()
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
        public void CheckClaims_MultipleClaimNotFoundSingleClaimShouldReturnFalse()
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
        public void CheckClaims_MultipleClaimNotFoundTwoClaimShouldReturnFalse()
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
        public void CheckClaims_MultipleClaimSingleValueFalseShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            Krystal.Claims.Add("HasPoints");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CheckClaims_MultipleClaimTwoValueFalseShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal");
            Krystal.Claims.Add("CanDeleteOtherAccount");
            Krystal.Claims.Add("HasPoints");
            AuthorizationManager KrystalAuthorization = new AuthorizationManager(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}
