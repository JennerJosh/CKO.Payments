using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Models
{
    public class PaymentProcessingModel
    {
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public PaymentCardModel PaymentCard { get; set; }
        public PayeeModel Payee { get; set; }
    }
}
