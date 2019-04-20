using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SecurityLayer.Sessions;

namespace SecurityLayer.Tests
{

    public class JWTokenManagerTests
    {
        [Fact]
        public void VerifyHeader_PassingFakeEncodedString_ReturnFalse()
        {
            // Arrange
            string fakeToken = "FakeToken";
            bool expected = false;
            JWTokenManager jwtManager = new JWTokenManager();
            // Act
            bool actual = jwtManager.VerifyHeader(fakeToken);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyHeader_HeaderWithInvalidAlgorithm_ReturnFalse()
        {
            // Arrange
            string fakeToken = "eyJhbGciOiJTSEFlZmppbyIsInR5cCI6IkpXVCJ9";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;
            // Act
            bool actual = jwtManager.VerifyHeader(fakeToken);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyHeader_PassingInvalidTokenType_ReturnFalse()
        {
            // Arrange
            string fakeToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJBTFQifQ";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;
            // Act
            bool actual = jwtManager.VerifyHeader(fakeToken);
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void VerifyHeader_PassingMissingAlgEntry_ReturnFalse()
        {
            // Arrange
            string fakeToken = "eyJ0eXAiOiJKV1QifQ";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;
            // Act
            bool actual = jwtManager.VerifyHeader(fakeToken);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyHeader_PassingCorrectHeader_ReturnTrue()
        {
            // Arrange
            string fakeToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = true;
            // Act
            bool actual = jwtManager.VerifyHeader(fakeToken);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyHeader_PassingNullToken_ReturnFalse()
        {
            // Arrange
            string nullToken = null;
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;
            // Act
            bool actual = jwtManager.VerifyHeader(nullToken);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateToken_ValidTokenCreated_ReturnTrue()
        {
            // Arrange
            string expectedToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ" +
                                   ".eyJ1c2VyIjoidGVzdCIsImNsYWltIjoiW2JlYWNoLCBibHVlLCB0ZXN0XSJ9" +
                                   ".pIJZEFcp5o9T9pzPqYZHXvQyt61RuZmNMdxbkmue3VY";
            Dictionary<string, string> testPayload = new Dictionary<string, string>()
            {
                { "user", "test" },
                { "claim", "[beach, blue, test]" }
            };
            JWTokenManager jwtManager = new JWTokenManager();

            // Act
            string actualToken = jwtManager.CreateToken(testPayload);

            // Assert
            Assert.Equal(expectedToken, actualToken);
        }

        [Fact]
        public void CreateToken_NullPayloadPassed_ThrowArgumentNullException()
        {
            // Arrange
            bool expected = true;
            Dictionary<string, string> testPayload = null;
            JWTokenManager jwtManager = new JWTokenManager();
            bool actual = false;

            // Act
            try
            {
                string token = jwtManager.CreateToken(testPayload);
            }
            catch(ArgumentNullException)
            {
                actual = true;
            }
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateSignature_PassTokenWithValidSignature_ReturnTrue()
        {
            // Arrange
            string validToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ" +
                                ".eyJ1c2VyIjoidGVzdEBlbWFpbC5jb20iLCJjbGFpbSI6IltQb3N0LCBEZWxldGUsIEVkaXRdIn0" +
                                ".a_ZtyrD4iNplCbpCXUxPySaNWySp3enUEI57ib5Vw4U";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = true;

            // Act
            bool actual = jwtManager.ValidateSignature(validToken);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateSignature_PassTokenWithInvalidSignature_ReturnFalse()
        {
            // Arrange
            string validToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ" +
                                ".eyJ1c2VyIjoidGVzdEBlbWFpbC5jb20iLCJjbGFpbSI6IltQb3N0LCBEZWxldGUsIEVkaXRdIn0" +
                                ".FalseSign";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;

            // Act
            bool actual = jwtManager.ValidateSignature(validToken);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateSignature_PassTokenWithNoSignature_ReturnFalse()
        {
            // Arrange
            string validToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ" +
                                ".eyJ1c2VyIjoidGVzdEBlbWFpbC5jb20iLCJjbGFpbSI6IltQb3N0LCBEZWxldGUsIEVkaXRdIn0";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;

            // Act
            bool actual = jwtManager.ValidateSignature(validToken);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateSignature_PassFakeToken_ReturnFalse()
        {
            // Arrange
            string validToken = "FakeToken";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;

            // Act
            bool actual = jwtManager.ValidateSignature(validToken);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateSignature_PassEmptyString_ReturnFalse()
        {
            // Arrange
            string validToken = "";
            JWTokenManager jwtManager = new JWTokenManager();
            bool expected = false;

            // Act
            bool actual = jwtManager.ValidateSignature(validToken);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DecodePayload_PassValidToken_ReturnCorrectPayload()
        {
            // Arrange
            JWTokenManager jwtManager = new JWTokenManager();
            string validToken = "eyJhbGciOiJTSEEyNTYiLCJ0eXAiOiJKV1QifQ" +
                                ".eyJ1c2VyIjoidGVzdEBlbWFpbC5jb20iLCJjbGFpbSI6IltQb3N0LCBEZWxldGUsIEVkaXRdIn0" +
                                ".a_ZtyrD4iNplCbpCXUxPySaNWySp3enUEI57ib5Vw4U";
            Dictionary<string, string> expectedPayload = new Dictionary<string, string>()
            {
                { "user", "test@email.com" },
                { "claim", "[Post, Delete, Edit]" }
            };

            // Act
            Dictionary<string, string> actualPayload = jwtManager.DecodePayload(validToken);

            // Assert
            // Order of dictionary entries doesn't matter.
            Assert.Equal<Dictionary<string, string>>(expectedPayload, actualPayload);
        }
    }
}
