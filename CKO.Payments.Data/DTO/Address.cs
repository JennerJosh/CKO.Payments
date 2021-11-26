using System.ComponentModel.DataAnnotations;

namespace CKO.Payments.Data.DTO
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [MaxLength(512)]
        public string Line1 { get; set; }

        [MaxLength(512)]
        public string Line2 { get; set; }

        [MaxLength(512)]
        public string? Line3 { get; set; }

        [MaxLength(512)]
        public string Town { get; set; }

        [MaxLength(512)]
        public string County { get; set; }

        [MaxLength(512)]
        public string Country { get; set; }

        [MaxLength(512)]
        public string PostCode { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
