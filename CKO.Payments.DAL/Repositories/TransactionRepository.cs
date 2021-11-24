using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories
{
    internal class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(CkoContext context) : base(context)
        {

        }

        public void AddTransaction(Transaction transaction)
        {
            base.Add(transaction);
            base.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            base.Update(transaction);
            base.SaveChanges();
        }
    }
}
