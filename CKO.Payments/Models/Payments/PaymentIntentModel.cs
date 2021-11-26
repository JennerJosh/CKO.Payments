using CKO.Payments.BL.Models.Merchants;
using CKO.Payments.BL.Models.Transactions;
using System;

namespace CKO.Payments.Models.Payments
{
    public class PaymentIntentModel
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantSecret { get; set; }

        public TransactionModel GetTransactionModel(MerchantModel merchant)
        {
            return new TransactionModel(merchant.Id, Amount, Currency);
        }

        public void SetTransactionId(Guid id)
        {
            TransactionId = id;
        }

        public bool IsSecretValid(MerchantModel merchant)
        {
            return merchant.Secret == MerchantSecret;
        }
    }
}
