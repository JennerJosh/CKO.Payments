using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Models
{
    public class PayeeModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
    }
}
