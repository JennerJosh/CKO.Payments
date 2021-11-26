using CKO.Payments.BL.Models.Merchants;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.BL.Mappers
{
    public static class MerchantMapper
    {
        public static Merchant MapToMerchant(MerchantModel merchant)
        {
            return new()
            {
                Id = merchant.Id,
                Email = merchant.Email,
                Name = merchant.Name,
                MerchantSecret = merchant.Secret
            };
        }

        public static MerchantModel MapToMerchantModel(Merchant merchant)
        {
            return new MerchantModel(merchant.Id, merchant.Name, merchant.Email, merchant.MerchantSecret);
        }
    }
}
