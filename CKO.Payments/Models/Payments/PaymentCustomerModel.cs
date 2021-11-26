using CKO.Payments.BL.Models.Transactions;

namespace CKO.Payments.Models.Payments
{
    public class PaymentCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PaymentAddressModel Address { get; set; }

        public CustomerModel GetCustomerModel()
        {
            return new()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Address = Address.GetAddressModel()
            };
        }
    }
}
