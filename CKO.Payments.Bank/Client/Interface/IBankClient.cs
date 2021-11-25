using CKO.Payments.Bank.Models;

namespace CKO.Payments.Bank.Client.Interface
{
    public interface IBankClient
    {
        Task<ProcessingResponseModel> ProcessPaymentAsync(PaymentProcessingModel model);
        Task<SettlementResponseModel> SettlePaymentAsync(PaymentSettlementModel model);
    }
}
