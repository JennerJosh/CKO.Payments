using CKO.Payments.BL.Exceptions.Merchants;
using CKO.Payments.BL.Models.Merchants;
using CKO.Payments.BL.Services;
using CKO.Payments.DAL.Interfaces;
using CKO.Payments.Tests.Base;
using CKO.Payments.Tests.Mock;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace CKO.Payments.Tests.Services
{
    public class MerchantsServiceTests
    {
        private MockRepository mockRepository;
        private UnitOfWorkMock unitOfWorkMock;

        public MerchantsServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private MerchantsService CreateService()
        {
            unitOfWorkMock = new UnitOfWorkMock();
            return new MerchantsService(unitOfWorkMock);
        }

        [Fact]
        public void RegisterMerchant_ExpectedModel_Success()
        {
            // Arrange
            var service = this.CreateService();
            MerchantModel merchant = new MerchantModel("Test", "Tester@test.com");

            // Act
            _ = service.RegisterMerchant(merchant);

            // Assert
            Assert.True(unitOfWorkMock.MerchantRepository.IsMerchantRegistered(merchant.Email));
        }

        [Fact]
        public void RegisterMerchant_InvalidEmail_Failure()
        {
            // Arrange
            var service = this.CreateService();
            MerchantModel merchant = new MerchantModel("Test", "Tester@test");

            // Act & Assert
            Assert.Throws<InvalidMerchantException>(() =>
            {
                _ = service.RegisterMerchant(merchant);
            });

            Assert.False(unitOfWorkMock.MerchantRepository.IsMerchantRegistered(merchant.Email));
        }

        [Fact]
        public void GetMerchantFromEmail_PreviouslyRegisteredEmail_Success()
        {
            // Arrange
            var service = this.CreateService();
            string email = "test2@test2.com";

            // Act
            var result = service.GetMerchantFromEmail(email);

            // Assert
            Assert.Equal(email, result.Email);
            Assert.Equal("Amazon", result.Name);
        }

        [Fact]
        public void GetMerchantFromEmail_PreviouslyRegisteredEmail_False()
        {
            // Arrange
            var service = this.CreateService();
            string email = "test2@test12.com";

            // Act & Assert
            Assert.Throws<NotRegisteredException>(() =>
            {
                _ = service.GetMerchantFromEmail(email);
            });

        }

        [Fact]
        public void HasMerchantPreviouslyRegistered_RegisteredEmail_True()
        {
            // Arrange
            var service = this.CreateService();
            string email = "test@test.com";

            // Act
            var result = service.HasMerchantPreviouslyRegistered(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasMerchantPreviouslyRegistered_RegisteredEmail_False()
        {
            // Arrange
            var service = this.CreateService();
            string email = "test@test12.com";

            // Act
            var result = service.HasMerchantPreviouslyRegistered(email);

            // Assert
            Assert.False(result);
        }
    }
}
