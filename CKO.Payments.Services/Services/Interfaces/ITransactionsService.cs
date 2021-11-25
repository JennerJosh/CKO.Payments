using CKO.Payments.BL.Models;

namespace CKO.Payments.BL.Services.Interfaces
{
    public interface ITransactionsService
    {
        TransactionModel AddTransactionStub(TransactionModel transaction);
        PaymentProcessingResponseModel ProcessTransaction(TransactionModel transaction);
        PaymentSettlementResponseModel SettleTransaction(TransactionModel transaction);
        List<TransactionModel> GetMerchantTransactions(Guid merchantId);
    }
}
