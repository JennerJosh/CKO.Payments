using CKO.Payments.BL.Extensions;

namespace CKO.Payments.BL.Models.Transactions
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Line1)
                && !string.IsNullOrEmpty(Line2)
                && !string.IsNullOrEmpty(Town)
                && !string.IsNullOrEmpty(County)
                && !string.IsNullOrEmpty(Country)
                && !string.IsNullOrEmpty(PostCode);
        }

        public void MaskSensitiveData()
        {
            Line2 = Line2.Mask();
            Line3 = Line3.Mask();
            Town = Town.Mask();
            County = County.Mask();
            Country = Country.Mask();
            PostCode = PostCode.Mask();
        }
    }
}
