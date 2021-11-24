
namespace CKO.Payments.BL.Models
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
    }
}
