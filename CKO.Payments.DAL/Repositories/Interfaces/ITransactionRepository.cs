using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        Transaction GetTransaction(Guid Id);
        Transaction GetPendingTransaction(Guid id);
        IEnumerable<Transaction> GetTransactionsByMerchant(Guid merchantId);
        Transaction GetApprovedTransaction(Guid id);
    }
}
