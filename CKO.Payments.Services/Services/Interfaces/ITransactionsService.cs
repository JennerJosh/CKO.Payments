using CKO.Payments.BL.Models;

namespace CKO.Payments.BL.Services.Interfaces
{
    public interface ITransactionsService
    {
        TransactionModel AddTransactionStub(TransactionModel transaction);
        Task<PaymentProcessingResponseModel> ProcessTransactionAsync(TransactionModel transaction);
        Task<PaymentSettlementResponseModel> SettleTransactionAsync(TransactionModel transaction);
        List<TransactionModel> GetMerchantTransactions(Guid merchantId);
    }
}
