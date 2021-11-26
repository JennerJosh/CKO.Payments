
namespace CKO.Payments.Bank.Models
{
    public class ProcessingResponseModel
    {
        public bool IsSuccess { get; set; }
        public string PaymentId { get; set; }
        public string Message { get; set; }
    }
}
