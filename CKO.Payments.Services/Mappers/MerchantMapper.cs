using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLModels = CKO.Payments.BL.Models;
using DTOModels = CKO.Payments.Data.DTO;

namespace CKO.Payments.BL.Mappers
{
    public static class MerchantMapper
    {
        public static DTOModels.Merchant GetDTOMerchant(BLModels.MerchantModel merchant)
        {
            return new DTOModels.Merchant()
            {
                Id = merchant.Id,
                Email = merchant.Email,
                Name = merchant.Name,
                MerchantSecret = merchant.Secret
            };
        }

        public static BLModels.MerchantModel GetBLMerchant(DTOModels.Merchant merchant)
        {
            return new BLModels.MerchantModel(merchant.Id, merchant.Name, merchant.Email, merchant.MerchantSecret);
        }
    }
}
