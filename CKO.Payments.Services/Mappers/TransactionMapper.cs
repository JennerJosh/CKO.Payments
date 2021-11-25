using CKO.Payments.BL.Models;
using CKO.Payments.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Mappers
{
    internal static class TransactionMapper
    {
        public static Transaction MapToTransaction(TransactionModel model)
        {
            return new()
            {
                Id = model.Id,
                Amount = model.Amount,
                CreatedDate = model.CreatedDate,
                Currency = model.Currency,
                Status = model.Status,
                StatusMessage = model.StatusMessage,
                MerchantId = model.MerchantId,
                Customer = model.Customer == null ? null : CustomerMapper.MapToCustomer(model.Customer),
                Card = model.Card == null ? null : CardMapper.MapToCard(model.Card),
                LineItems = model.LineItems == null ? null : LineItemMapper.MapToLineItems(model.LineItems),
            };
        }

        public static TransactionModel MapToTransactionModel(Transaction model)
        {
            return new()
            {
                Id = model.Id,
                Amount = model.Amount,
                CreatedDate = model.CreatedDate,
                Currency = model.Currency,
                Status = model.Status,
                StatusMessage = model.StatusMessage ?? String.Empty,
                MerchantId = model.MerchantId,
                Card = model.Card == null ? null : CardMapper.MapToCardModel(model.Card),
                Customer = model.Customer == null ? null : CustomerMapper.MapToCustomerModel(model.Customer),
                LineItems = model.LineItems == null ? null : LineItemMapper.MapToLineItemModels(model.LineItems.ToList()),
                BankPaymentId = model.BankPaymentId
            };
        }
    }
}
