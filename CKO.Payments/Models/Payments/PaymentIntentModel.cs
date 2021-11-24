using CKO.Payments.BL.Models;

namespace CKO.Payments.Models.Payments
{
    public class PaymentIntentModel
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantSecret { get; set; }

        public TransactionModel GetTransactionModel()
        {
            return new TransactionModel(Amount, Currency);
        }

        public bool IsSecretValid(MerchantModel merchant)
        {
            return merchant.Secret == MerchantSecret;
        }
    }
}
