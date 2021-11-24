using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
    }
}
