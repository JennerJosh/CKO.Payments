using CKO.Payments.BL.Models.Merchants;

namespace CKO.Payments.BL.Services.Interfaces
{
    public interface IMerchantsService
    {
        MerchantModel RegisterMerchant(MerchantModel merchant);
        MerchantModel GetMerchantFromEmail(string email);
    }
}
