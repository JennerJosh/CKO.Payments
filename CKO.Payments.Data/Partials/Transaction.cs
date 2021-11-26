using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Data.DTO
{
    public partial class Transaction
    {
        public void SetStatus(int status, string statusMessage)
        {
            Status = status;
            StatusMessage = statusMessage;
        }
    }
}
