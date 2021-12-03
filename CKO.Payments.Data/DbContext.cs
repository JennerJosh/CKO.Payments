using CKO.Payments.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace CKO.Payments.Data
{
    public class CkoContext : DbContext
    {
        public CkoContext()
        {

        }

        public CkoContext(DbContextOptions<CkoContext> options) : base(options)
        {
            
        }

        #region DbSets

        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }

        #endregion
    }
}
