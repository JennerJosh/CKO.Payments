using CKO.Payments.Bank.Models;

namespace CKO.Payments.Bank.Client.Interface
{
    public interface IBankClient
    {
        Task<BankResponseModel> ProcessPayment(PaymentProcessingModel model);
        Task<BankResponseModel> SettlePayment(PaymentSettlementModel model);
    }
}
