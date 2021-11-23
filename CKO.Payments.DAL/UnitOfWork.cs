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
            merchantRepository = new MerchantRepository(context);
        }

        // Repositories
        public IMerchantRepository merchantRepository { get; private set; }
    }
}
