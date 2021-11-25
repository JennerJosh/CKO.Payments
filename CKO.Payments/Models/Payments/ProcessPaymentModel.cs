using CKO.Payments.BL.Models;
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
        public List<PaymentLineItem> Items { get; set; }
        public PaymentCustomer Customer { get; set; }
        public PaymentCard Card { get; set; }
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
