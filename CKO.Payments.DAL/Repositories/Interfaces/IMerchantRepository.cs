using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories.Interfaces
{
    public interface IMerchantRepository
    {
        Merchant GetMerchant(Guid id);
        void AddMerchant(Merchant merchant);
        Merchant GetMerchantByEmail(string email);
        bool IsMerchantRegistered(string email);
    }
}
