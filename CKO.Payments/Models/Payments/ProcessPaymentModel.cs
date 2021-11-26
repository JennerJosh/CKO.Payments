using CKO.Payments.BL.Models.Merchants;
using CKO.Payments.BL.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CKO.Payments.Models.Payments
{
    public class ProcessPaymentModel
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public List<PaymentLineItemModel> Items { get; set; }
        public PaymentCustomerModel Customer { get; set; }
        public PaymentCardModel Card { get; set; }
        public string MerchantSecret { get; set; }
        public string BankPaymentId { get; set; }

        public TransactionModel GetTransactionModel(MerchantModel merchant)
        {
            return new()
            {
                Id = TransactionId,
                MerchantId = merchant.Id,
                Amount = Amount,
                Currency = Currency,
                BankPaymentId = BankPaymentId,
                Card = Card?.GetCardModel(),
                Customer = Customer?.GetCustomerModel(),
                LineItems = Items?
                .Select(x => x.GetLineItemModel())
                .ToList()
            };
        }

        public bool IsSecretValid(MerchantModel merchant)
        {
            return merchant.Secret == MerchantSecret;
        }
    }
}
