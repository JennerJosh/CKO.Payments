using CKO.Payments.BL.Services;
using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.DAL.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using CKO.Payments.BL.Models.Transactions;
using System.Transactions;

namespace CKO.Payments.Tests.Services
{
    public class TransactionsServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IBankClient> mockBankClient;

        public TransactionsServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
            this.mockBankClient = this.mockRepository.Create<IBankClient>();
        }

        private TransactionsService CreateService()
        {
            return new TransactionsService(
                this.mockUnitOfWork.Object,
                this.mockBankClient.Object);
        }

        [Fact]
        public void AddTransactionStub_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = null;

            // Act
            var result = service.AddTransactionStub(
                transaction);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ProcessTransactionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = null;

            // Act
            var result = await service.ProcessTransactionAsync(
                transaction);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void UpdateTransaction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = null;

            // Act
            var result = service.UpdateTransaction(
                transaction);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void UpdateTransaction_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var service = this.CreateService();
            Transaction transaction = null;

            // Act
            //var result = service.UpdateTransaction(
            //    transaction);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SettleTransactionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = null;

            // Act
            var result = await service.SettleTransactionAsync(
                transaction);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetMerchantTransactions_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid merchantId = default(global::System.Guid);

            // Act
            var result = service.GetMerchantTransactions(
                merchantId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
