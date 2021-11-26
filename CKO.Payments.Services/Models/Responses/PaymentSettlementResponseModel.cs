namespace CKO.Payments.BL.Models.Responses
{
    public class PaymentSettlementResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public PaymentSettlementResponseModel()
        {

        }

        public PaymentSettlementResponseModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
