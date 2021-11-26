using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;
using Microsoft.EntityFrameworkCore;
using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.DAL.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(CkoContext context) : base(context)
        {

        }

        public Transaction GetTransaction(Guid id)
        {
            return base.GetById(id);
        }

        public Transaction GetPendingTransaction(Guid id)
        {
            return base.GetQuery()
                .Where(x => x.Status == (int)TransactionStatus.Pending)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Transaction> GetTransactionsByMerchant(Guid merchantId)
        {
            return base
                .GetQuery()
                .Include(x => x.Card)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Address)
                .Where(x => x.MerchantId == merchantId);
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
