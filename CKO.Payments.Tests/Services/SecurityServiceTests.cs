using CKO.Payments.BL.Services;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace CKO.Payments.Tests.Services
{
    public class SecurityServiceTests
    {
        private MockRepository mockRepository;


        public SecurityServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private SecurityService CreateService()
        {
            return new SecurityService();
        }

        [Fact]
        public void GenerateAuthTokenAndReadToken_ValidModel_Success()
        {
            // Arrange
            var service = this.CreateService();

            Guid merchantId = Guid.NewGuid();
            string merchantName = "Test";
            string merchantEmail = "Test@test.com";
            string merchantSecret = "ABC";

            // Act
            var writeResult = service.GenerateAuthToken(merchantId, merchantName, merchantEmail, merchantSecret);

            // Read token
            var readResult = service.IsTokenValid(writeResult, out SecurityToken token);

            // Read data from token
            var jwtToken = (JwtSecurityToken)token;
            var id = Guid.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var name = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var secret = jwtToken.Claims.First(x => x.Type == "secret").Value;


            // Write Asserts
            Assert.NotNull(writeResult);
            Assert.NotEqual(writeResult, string.Empty);

            // Write Asserts
            Assert.NotNull(token);
            Assert.Equal(id, merchantId);
            Assert.Equal(name, merchantName);
            Assert.Equal(email, merchantEmail);
            Assert.Equal(secret, merchantSecret);
        }
    }
}
