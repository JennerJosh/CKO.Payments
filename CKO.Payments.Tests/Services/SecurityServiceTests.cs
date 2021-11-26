using CKO.Payments.BL.Services;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
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
        public void GenerateAuthToken_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid merchantId = default(global::System.Guid);
            string merchantName = null;
            string merchantEmail = null;
            string merchantSecret = null;

            // Act
            var result = service.GenerateAuthToken(
                merchantId,
                merchantName,
                merchantEmail,
                merchantSecret);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void IsTokenValid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string token = null;
            SecurityToken securityToken = null;

            // Act
            var result = service.IsTokenValid(
                token,
                out securityToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
