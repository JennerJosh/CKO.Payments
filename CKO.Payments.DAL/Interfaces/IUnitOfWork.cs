using CKO.Payments.DAL.Repositories.Interfaces;

namespace CKO.Payments.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IMerchantRepository MerchantRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
    }
}
