using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Models;

namespace CKO.Payments.Bank.Client
{
    public class NakatomiClient : IBankClient
    {
        private readonly IHttpRequests _httpRequests;

        public NakatomiClient(IHttpRequests httpRequests)
        {
            _httpRequests = httpRequests;
        }

        public BankResponseModel ProcessPayment(PaymentProcessingModel model)
        {
            throw new NotImplementedException();
        }

        public BankResponseModel SettlePayment(PaymentSettlementModel model)
        {
            throw new NotImplementedException();
        }
    }
}
