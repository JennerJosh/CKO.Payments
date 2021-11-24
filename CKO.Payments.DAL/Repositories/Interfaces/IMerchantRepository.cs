using CKO.Payments.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
