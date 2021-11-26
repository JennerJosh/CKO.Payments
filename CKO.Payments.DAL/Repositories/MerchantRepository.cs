using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories
{
    public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
    {
        public MerchantRepository(CkoContext context) : base(context)
        {

        }

        public Merchant GetMerchant(Guid id) =>
            base.GetById(id);

        public Merchant GetMerchantByEmail(string email) =>
            base.GetQuery().FirstOrDefault(x => x.Email == email);

        public bool IsMerchantRegistered(string email) =>
            base.GetQuery().Any(x => x.Email == email);

        public void AddMerchant(Merchant merchant)
        {
            base.Add(merchant);
            base.SaveChanges();
        }
    }
}
