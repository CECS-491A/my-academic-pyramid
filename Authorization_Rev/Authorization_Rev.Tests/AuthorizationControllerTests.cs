using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Authorization;

namespace Authorization_Rev.Tests
{
    public class AuthorizationControllerTests
    {
        [Fact]
        public void CheckClaims_FoundShouldReturnTrue()
        {
            // Arrange 
            User Trong = new User("Trong", "Student");
            Trong.addClaim("CanDeleteUserPost", true);
            UserManagement TrongAuthorization = new UserManagement(Trong);
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
            User Trong = new User("Trong", "Student");
            Trong.addClaim("CanDeleteUserOwnAccount", true);
            UserManagement TrongAuthorization = new UserManagement(Trong);
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
            User Trong = new User("Trong", "Student");
            Trong.addClaim("CanDeleteUserPost", false);
            UserManagement TrongAuthorization = new UserManagement(Trong);
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
            User Trong = new User("Trong", "Student");
            Trong.addClaim("CanDeleteUserOwnAccount", false);
            UserManagement TrongAuthorization = new UserManagement(Trong);
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
            User Krystal = new User("Krystal", "Admin");
            Krystal.addClaim("CanDeleteOtherAccount", true);
            Krystal.addClaim("HasPoints", true);
            UserManagement KrystalAuthorization = new UserManagement(Krystal);
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
            User Krystal = new User("Krystal", "Admin");
            Krystal.addClaim("CanDeleteOtherAccount", true);
            UserManagement KrystalAuthorization = new UserManagement(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CheckClaims_MultipleClaimNotFoundTwoClaimShouldReturnFalse()
        {
            // Arrange 
            User Krystal = new User("Krystal", "Admin");
            UserManagement KrystalAuthorization = new UserManagement(Krystal);
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
            User Krystal = new User("Krystal", "Admin");
            Krystal.addClaim("CanDeleteOtherAccount", true);
            Krystal.addClaim("HasPoints", false);
            UserManagement KrystalAuthorization = new UserManagement(Krystal);
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
            User Krystal = new User("Krystal", "Admin");
            Krystal.addClaim("CanDeleteOtherAccount", false);
            Krystal.addClaim("HasPoints", false);
            UserManagement KrystalAuthorization = new UserManagement(Krystal);
            bool expected = false;

            // Act
            bool actual = KrystalAuthorization.CheckClaims(new List<string>() { "CanDeleteOtherAccount",
                                                 "HasPoints"});

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}
