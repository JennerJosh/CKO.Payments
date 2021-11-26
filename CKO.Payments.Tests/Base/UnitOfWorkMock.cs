using CKO.Payments.DAL;
using CKO.Payments.DAL.Interfaces;
using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;
using CKO.Payments.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CKO.Payments.Tests.Base
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        private Mock<UnitOfWork> _mockedUnitofWork = new Mock<UnitOfWork>();
        private Mock<IMerchantRepository> _mockedMerchantsRepository = new Mock<IMerchantRepository>();
        private Mock<ITransactionRepository> _mockedTransactionsRepository = new Mock<ITransactionRepository>();

        private static List<Merchant> merchantsData = new List<Merchant>();

        public IMerchantRepository MerchantRepository { get; private set; }

        public ITransactionRepository TransactionRepository { get; set; }

        public UnitOfWorkMock()
        {
            MerchantRepository = GetMerchantsMock();
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
    }
}
