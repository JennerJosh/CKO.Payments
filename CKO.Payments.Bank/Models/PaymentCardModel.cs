
namespace CKO.Payments.Bank.Models
{
    public class PaymentCardModel
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public BillingAddressModel BillingAddress { get; set; }
    }
}
