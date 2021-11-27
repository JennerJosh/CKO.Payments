using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Tests.Base
{
    public class BankClientMock
    {
        private Mock<IBankClient> _bankClientMock = new();

        public BankClientMock()
        {

        }

        public IBankClient GetMockedClient()
        {
            return SetupMockedBankClient();
        }

        /// <summary>
        /// In this situation we are not testing whether the bank itself is working
        /// because of this the responses we will return will always be true, this can be extended later if needed
        /// 
        /// Due to business logic tests needing to be repeatable we do not want to implement any kind of randomness into this as it would cause tests to become unpredictable
        /// </summary>
        /// <returns>Mocked IBankClient</returns>
        private IBankClient SetupMockedBankClient()
        {
            _bankClientMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentProcessingModel>()))
                .ReturnsAsync((PaymentProcessingModel model) =>
                {
                    return new ProcessingResponseModel()
                    {
                        IsSuccess = true,
                        Message = "Payment approved, ready to settle",
                        PaymentId = Guid.NewGuid().ToString()
                    };
                });

            _bankClientMock.Setup(x => x.SettlePaymentAsync(It.IsAny<PaymentSettlementModel>()))
                .ReturnsAsync((PaymentSettlementModel model) =>
                {
                    return new SettlementResponseModel()
                    {
                        IsSuccess = true,
                        Message = "Payment settled"
                    };
                });


            return _bankClientMock.Object;
        }
    }
}
