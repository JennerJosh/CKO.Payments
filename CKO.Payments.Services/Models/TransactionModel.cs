using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CKO.Payments.BL.Enums.Transactions;

namespace CKO.Payments.BL.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public int Status { get; set; } = (int)TransactionStatus.Pending;

        public string StatusMessage { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public CustomerModel Customer { get; set; }

        public MerchantModel Merchant { get; set; }

        public List<LineItemModel> LineItems { get; set; }

        public TransactionModel()
        {

        }

        public TransactionModel(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

    }
}
