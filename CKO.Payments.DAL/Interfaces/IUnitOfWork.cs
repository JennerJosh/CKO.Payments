using CKO.Payments.DAL.Repositories.Interfaces;

namespace CKO.Payments.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IMerchantRepository MerchantRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public ILineItemsRepository LineItemsRepository { get; }
        public ICardRepository CardRepository { get; }
    }
}
