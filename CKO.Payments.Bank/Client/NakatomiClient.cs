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

        public ProcessingResponseModel ProcessPayment(PaymentProcessingModel model)
        {
            throw new NotImplementedException();
        }

        public void SettlePayment()
        {
            throw new NotImplementedException();
        }
    }
}
