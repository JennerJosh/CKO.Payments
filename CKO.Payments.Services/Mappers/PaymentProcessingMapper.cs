using CKO.Payments.Bank.Models;
using CKO.Payments.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Mappers
{
    public static class PaymentProcessingMapper
    {
        public static PaymentProcessingResponseModel MapToPaymentProcessingResponseModel(BankResponseModel bankResponse, TransactionModel transaction)
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
