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
        public static DTOModels.Merchant GetDTOMerchant(BLModels.Merchant merchant)
        {
            return new DTOModels.Merchant()
            {
                Id = merchant.Id,
                Email = merchant.Email,
                Name = merchant.Name,
                MerchantSecret = merchant.Secret
            };
        }

        public static BLModels.Merchant GetBLMerchant(DTOModels.Merchant merchant)
        {
            return new BLModels.Merchant(merchant.Id, merchant.Name, merchant.Email, merchant.MerchantSecret);
        }
    }
}
