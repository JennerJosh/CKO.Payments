using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Models
{
    public class PaymentSettlementModel
    {
        public string PaymentId { get; set; }

        public PaymentSettlementModel()
        {

        }

        public PaymentSettlementModel(string paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
