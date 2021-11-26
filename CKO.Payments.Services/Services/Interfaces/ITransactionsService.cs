using CKO.Payments.BL.Models.Responses;
using CKO.Payments.BL.Models.Transactions;

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
