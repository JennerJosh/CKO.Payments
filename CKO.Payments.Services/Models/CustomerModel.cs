
using CKO.Payments.BL.Extensions;

namespace CKO.Payments.BL.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }

        public bool IsValid()
        {
            var isAddressValid = Address.IsValid();

            return !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(Email)
                && isAddressValid;
        }

        public void MaskSensitiveData()
        {
            Address?.MaskSensitiveData();
            Email = Email.Mask();
            LastName = LastName.Mask();
        }
    }
}
