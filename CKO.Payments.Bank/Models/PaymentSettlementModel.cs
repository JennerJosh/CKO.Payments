
namespace CKO.Payments.Bank.Models
{
    public class PaymentSettlementModel
    {
        public string PaymentId { get; set; }

        public PaymentSettlementModel(string paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
