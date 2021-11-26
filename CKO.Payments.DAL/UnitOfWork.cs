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
            TransactionRepository = new TransactionRepository(context);
        }

        // Repositories
        public IMerchantRepository MerchantRepository { get; private set; }
        public ITransactionRepository TransactionRepository { get; private set; }
    }
}
