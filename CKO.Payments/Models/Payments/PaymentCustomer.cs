using CKO.Payments.BL.Models;

namespace CKO.Payments.Models.Payments
{
    public class PaymentCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PaymentAddress Address { get; set; }

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
