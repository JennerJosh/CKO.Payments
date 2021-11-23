using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("Customers")]
    public class Customer
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength (100)]
        public string LastName { get; set; }

        [MaxLength(320)]
        public string Email { get; set; }
    }
}
