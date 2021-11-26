namespace CKO.Payments.BL.Models.Responses
{
    public class PaymentProcessingResponseModel
    {
        public bool IsSuccess { get; set; } 
        public string PaymentId { get; set; }
        public string Message { get; set; }
        public Guid TransactionId { get; set; }

        public void SetTransactionId(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
