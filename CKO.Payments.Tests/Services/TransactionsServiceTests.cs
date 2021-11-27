using CKO.Payments.BL.Services;
using CKO.Payments.Bank.Client.Interface;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using CKO.Payments.BL.Models.Transactions;
using CKO.Payments.Tests.Base;
using CKO.Payments.BL.Exceptions.Transactions;
using System.Collections.Generic;
using static CKO.Payments.DAL.Enums.Transactions;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.Tests.Services
{
    public class TransactionsServiceTests
    {
        private MockRepository mockRepository;

        private UnitOfWorkMock mockUnitOfWork;
        private IBankClient mockBankClient;

        public TransactionsServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockUnitOfWork = new UnitOfWorkMock();
            this.mockBankClient = new BankClientMock().GetMockedClient();
        }

        private TransactionsService CreateService()
        {
            return new TransactionsService(
                this.mockUnitOfWork,
                this.mockBankClient);
        }

        [Fact]
        public void AddTransactionStub_ValidModel_Success()
        {
            // Arrange
            var service = this.CreateService();

            TransactionModel transaction = new(100M, "GBP");

            // Assign a merchant
            transaction.MerchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7");

            // Act
            var result = service.AddTransactionStub(transaction);

            // Assert
            Assert.NotNull(mockUnitOfWork.TransactionRepository.GetPendingTransaction(result.Id));
            Assert.Equal(result.Amount, transaction.Amount);
            Assert.Equal(result.Currency, transaction.Currency);
            Assert.Equal(result.MerchantId, transaction.MerchantId);
        }

        [Fact]
        public void AddTransactionStub_InvalidModel_Failure()
        {
            // Arrange
            var service = this.CreateService();

            TransactionModel transaction = new(0M, "GBP");

            // Assign a merchant
            transaction.MerchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7");

            // Act & Assert
            Assert.Throws<InvalidTransactionException>(() => { _ = service.AddTransactionStub(transaction); });
        }

        [Fact]
        public async Task ProcessTransactionAsync_ValidModel_Success()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new TransactionModel()
            {
                Id = Guid.Parse("ef0ab4d3-ba2e-4be0-8a22-8bc10f824150"),
                Amount = 123M,
                Currency = "GBP",
                LineItems = new()
                {
                    new()
                    {
                        Name = "Test Item 1",
                        Price = 50M,
                        Quantity = 2
                    },
                    new()
                    {
                        Name = "Test Item 2",
                        Price = 23M,
                        Quantity = 1
                    }
                },
                Customer = new()
                {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Email = "JoeBlogs@test.com",
                    Address = new()
                    {
                        Line1 = "test",
                        Line2 = "test",
                        Line3 = "test",
                        Town = "test",
                        County = "test",
                        Country = "test",
                        PostCode = "test",
                    }
                },
                Card = new()
                {
                    Name = "Joe Bloggs",
                    Number = "371449635398431",
                    ExpiryMonth = 1,
                    ExpiryYear = 2021,
                    Cvv = 123,
                }

            };

            // Act
            var result = await service.ProcessTransactionAsync(transaction);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ProcessTransactionAsync_UnknownTransactionId_Failure()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new TransactionModel()
            {
                Id = Guid.NewGuid(),
                Amount = 123M,
                Currency = "GBP",
                LineItems = new()
                {
                    new()
                    {
                        Name = "Test Item 1",
                        Price = 50M,
                        Quantity = 2
                    },
                    new()
                    {
                        Name = "Test Item 2",
                        Price = 23M,
                        Quantity = 1
                    }
                },
                Customer = new()
                {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Email = "JoeBlogs@test.com",
                    Address = new()
                    {
                        Line1 = "test",
                        Line2 = "test",
                        Line3 = "test",
                        Town = "test",
                        County = "test",
                        Country = "test",
                        PostCode = "test",
                    }
                },
                Card = new()
                {
                    Name = "Joe Bloggs",
                    Number = "371449635398431",
                    ExpiryMonth = 1,
                    ExpiryYear = 2021,
                    Cvv = 123,
                }

            };

            // Act & Assert
            _ = Assert.ThrowsAsync<InvalidTransactionException>(async () => { _ = await service.ProcessTransactionAsync(transaction); });
        }

        [Fact]
        public async Task ProcessTransactionAsync_AlreadyProcessed_Failure()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new TransactionModel()
            {
                Id = Guid.Parse("bcf1b67a-5a73-4157-bdb1-d522e5342880"),
                Amount = 123M,
                Currency = "GBP",
                LineItems = new()
                {
                    new()
                    {
                        Name = "Test Item 1",
                        Price = 50M,
                        Quantity = 2
                    },
                    new()
                    {
                        Name = "Test Item 2",
                        Price = 23M,
                        Quantity = 1
                    }
                },
                Customer = new()
                {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Email = "JoeBlogs@test.com",
                    Address = new()
                    {
                        Line1 = "test",
                        Line2 = "test",
                        Line3 = "test",
                        Town = "test",
                        County = "test",
                        Country = "test",
                        PostCode = "test",
                    }
                },
                Card = new()
                {
                    Name = "Joe Bloggs",
                    Number = "371449635398431",
                    ExpiryMonth = 1,
                    ExpiryYear = 2021,
                    Cvv = 123,
                }

            };

            // Act & Assert
            _ = Assert.ThrowsAsync<InvalidTransactionException>(async () => { _ = await service.ProcessTransactionAsync(transaction); });
        }

        [Fact]
        public void UpdateTransaction_ChangeStatus_Success()
        {
            // Arrange
            var service = this.CreateService();

            // Get unmodified transaction
            var originalTransaction = mockUnitOfWork.TransactionRepository.GetTransaction(Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"));

            TransactionModel transaction = new TransactionModel()
            {
                Amount = 1233.00M,
                BankPaymentId = "bcf1b67a-5a73-4157-bdb1-d522e5342880",
                Card = new()
                {
                    ExpiryMonth = 2,
                    ExpiryYear = 2023,
                    Number = "1234 5678 1234 5678",
                    Name = "John Jones",
                    Id = Guid.Parse("111feea0-eba7-4e30-b4bd-f9fab5fb4ce7")
                },
                CreatedDate = DateTime.Now.AddHours(-3),
                Currency = "GBP",
                Customer = new()
                {
                    Email = "john@jones.com",
                    FirstName = "John",
                    LastName = "Jones",
                    Id = Guid.Parse("1d2455d7-05e9-4502-9fb6-54830114ee66"),
                    Address = new()
                    {
                        Line1 = "1",
                        Line2 = "2",
                        Line3 = "3",
                        Town = "4",
                        County = "5",
                        Country = "6",
                        PostCode = "7",
                        Id = Guid.Parse("605b51a6-d52f-486f-a8e2-126230f471fe")
                    }
                },
                Id = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"),
                LineItems = new()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Some Product",
                        Price = 616.5M,
                        Quantity = 2,
                    }
                },
                Status = (int)TransactionStatus.Settled,
                StatusMessage = "Payment ready for settlement",
                MerchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7")
            };

            // Act
            var result = service.UpdateTransaction(transaction);

            // get transaction
            var modifiedTransaction = mockUnitOfWork.TransactionRepository.GetTransaction(transaction.Id);

            // Assert
            Assert.NotEqual(originalTransaction.Status, modifiedTransaction.Status);
            Assert.Equal(transaction.Status, modifiedTransaction.Status);
        }

        [Fact]
        public async Task SettleTransactionAsync_ValidModel_Success()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new()
            {
                Id = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"),
                BankPaymentId = "bcf1b67a-5a73-4157-bdb1-d522e5342880"
            };

            // Act
            var result = await service.SettleTransactionAsync(transaction);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task SettleTransactionAsync_MissingBankPaymentId_Failure()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new()
            {
                Id = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"),
                BankPaymentId = String.Empty
            };

            // Act
            var result = await service.SettleTransactionAsync(transaction);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task SettleTransactionAsync_PendingTransactionId_Failure()
        {
            // Arrange
            var service = this.CreateService();
            TransactionModel transaction = new()
            {
                Id = Guid.Parse("c60bd10b-0e20-4249-9b26-ce4a75a0ab15"),
                BankPaymentId = String.Empty
            };

            // Act
            var result = await service.SettleTransactionAsync(transaction);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void GetMerchantTransactions_ValidMerchantId_Success()
        {
            // Arrange
            var service = this.CreateService();
            Guid merchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7");
            int expectedCount = 2;

            // Act
            var result = service.GetMerchantTransactions(merchantId);

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void GetMerchantTransactions_UnknownMerchantId_Success()
        {
            // Arrange
            var service = this.CreateService();
            Guid merchantId = Guid.NewGuid();
            int expectedCount = 0;

            // Act
            var result = service.GetMerchantTransactions(merchantId);

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }
    }
}
