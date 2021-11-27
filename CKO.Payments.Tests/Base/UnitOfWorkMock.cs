using CKO.Payments.DAL.Interfaces;
using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data.DTO;
using CKO.Payments.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.Tests.Base
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        private Mock<IMerchantRepository> _mockedMerchantsRepository = new();
        private Mock<ITransactionRepository> _mockedTransactionsRepository = new();

        private static List<Merchant> merchantsData = new();
        private static List<Transaction> transactionsData = new();

        public IMerchantRepository MerchantRepository { get; private set; }

        public ITransactionRepository TransactionRepository { get; set; }

        public UnitOfWorkMock()
        {
            MerchantRepository = GetMerchantsMock();
            TransactionRepository = GetTransactionsMock();
        }

        public IMerchantRepository GetMerchantsMock()
        {
            merchantsData = new MerchantsMockedData().Merchants;

            _mockedMerchantsRepository.Setup(x => x.IsMerchantRegistered(It.IsAny<string>()))
                .Returns((string email) =>
                {
                    return merchantsData.Any(s => s.Email == email);
                });

            _mockedMerchantsRepository.Setup(x => x.GetMerchant(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return merchantsData.FirstOrDefault(s => s.Id == id);
                });

            _mockedMerchantsRepository.Setup(x => x.GetMerchantByEmail(It.IsAny<string>()))
                .Returns((string email) =>
                {
                    return merchantsData.FirstOrDefault(s => s.Email == email);
                });

            _mockedMerchantsRepository.Setup(x => x.AddMerchant(It.IsAny<Merchant>()))
                .Callback((Merchant merchant) =>
                {
                    merchantsData.Add(merchant);
                })
                .Verifiable();

            return _mockedMerchantsRepository.Object;
        }

        public ITransactionRepository GetTransactionsMock()
        {
            transactionsData = new TransactionsMockedData().Transactions;

            _mockedTransactionsRepository.Setup(x => x.GetPendingTransaction(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return transactionsData
                    .Where(x => x.Status == (int)TransactionStatus.Pending)
                    .FirstOrDefault(x => x.Id == id);
                });

            _mockedTransactionsRepository.Setup(x => x.GetApprovedTransaction(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return transactionsData
                    .Where(x => x.Status == (int)TransactionStatus.Approved)
                    .FirstOrDefault(x => x.Id == id);
                });

            _mockedTransactionsRepository.Setup(x => x.GetTransaction(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return transactionsData
                    .FirstOrDefault(x => x.Id == id);
                });

            _mockedTransactionsRepository.Setup(x => x.UpdateTransaction(It.IsAny<Transaction>()))
                .Callback((Transaction transaction) =>
                {
                    var index = transactionsData.FindIndex(x => x.Id == transaction.Id);
                    transactionsData[index] = transaction;
                })
                .Verifiable();


            _mockedTransactionsRepository.Setup(x => x.GetTransactionsByMerchant(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return transactionsData
                    .Where(x => x.MerchantId == id);
                });

            _mockedTransactionsRepository.Setup(x => x.AddTransaction(It.IsAny<Transaction>()))
                .Callback((Transaction Transaction) =>
                {
                    transactionsData.Add(Transaction);
                })
                .Verifiable();



            return _mockedTransactionsRepository.Object;
        }
    }
}
