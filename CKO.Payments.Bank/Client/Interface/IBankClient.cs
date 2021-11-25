using CKO.Payments.Bank.Models;

namespace CKO.Payments.Bank.Client.Interface
{
    public interface IBankClient
    {
        public ProcessingResponseModel ProcessPayment(PaymentProcessingModel model);
        public void SettlePayment();

    }
}
