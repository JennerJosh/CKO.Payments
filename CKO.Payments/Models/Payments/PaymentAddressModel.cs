using CKO.Payments.BL.Models.Transactions;

namespace CKO.Payments.Models.Payments
{
    public class PaymentAddressModel
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        public AddressModel GetAddressModel()
        {
            return new()
            {
                Line1 = Line1,
                Line2 = Line2,
                Line3 = Line3,
                Town = Town,
                County = County,
                Country = Country,
                PostCode = PostCode
            };
        }
    }
}
