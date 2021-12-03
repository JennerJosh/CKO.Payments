using CKO.Payments.Bank.Models;
using CKO.Payments.BL.Models.Responses;
using CKO.Payments.BL.Models.Transactions;

namespace CKO.Payments.BL.Mappers
{
    public static class PaymentProcessingMapper
    {
        public static PaymentProcessingResponseModel MapToPaymentProcessingResponseModel(ProcessingResponseModel bankResponse, TransactionModel transaction)
        {
            return new()
            {
                IsSuccess = bankResponse.IsSuccess,
                Message = bankResponse.Message,
                PaymentId = bankResponse.PaymentId,
                TransactionId = transaction.Id
            };
        }

        public static PaymentProcessingModel MapToPaymentProcessingModel(TransactionModel transactionModel)
        {
            return new()
            {
                Amount = transactionModel.Amount,
                CurrencyCode = transactionModel.Currency,
                Reference = $"CKO-{transactionModel.MerchantId}",
                Payee = new()
                {
                    AccountName = "Bank of CKO",
                    AccountNumber = "123456789",
                    SortCode = "808080"
                },
                PaymentCard = new()
                {
                    BillingAddress = new()
                    {
                        Country = transactionModel.Customer.Address.Country,
                        County = transactionModel.Customer.Address.County,
                        Line1 = transactionModel.Customer.Address.Line1,
                        Line2 = transactionModel.Customer.Address.Line2,
                        Line3 = transactionModel.Customer.Address.Line3,
                        PostCode = transactionModel.Customer.Address.PostCode,
                        Town = transactionModel.Customer.Address.Town,
                    },
                    CardNumber = transactionModel.Card.Number,
                    Cvv = transactionModel.Card.Cvv?.ToString(),
                    ExpiryMonth = transactionModel.Card.ExpiryMonth.ToString(),
                    ExpiryYear = transactionModel.Card.ExpiryYear.ToString(),
                    Name = transactionModel.Card.Name,
                }
            };
        }
    }
}
