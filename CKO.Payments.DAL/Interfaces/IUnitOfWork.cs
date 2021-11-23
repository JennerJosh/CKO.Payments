using CKO.Payments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IMerchantRepository merchantRepository { get; }
    }
}
