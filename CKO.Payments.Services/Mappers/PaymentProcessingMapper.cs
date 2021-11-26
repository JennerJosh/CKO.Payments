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
    }
}
