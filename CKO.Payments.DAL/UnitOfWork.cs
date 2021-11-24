using CKO.Payments.DAL.Interfaces;
using CKO.Payments.DAL.Repositories;
using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;

namespace CKO.Payments.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CkoContext context)
        {
            // Init Repositories
            MerchantRepository = new MerchantRepository(context);
            CustomerRepository = new CustomerRepository(context);
            TransactionRepository = new TransactionRepository(context);
            LineItemsRepository = new LineItemsRepository(context);
            CardRepository = new CardRepository(context);
        }

        // Repositories
        public IMerchantRepository MerchantRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ITransactionRepository TransactionRepository { get; private set; }
        public ILineItemsRepository LineItemsRepository { get; private set; }
        public ICardRepository CardRepository { get; private set; }
    }
}
